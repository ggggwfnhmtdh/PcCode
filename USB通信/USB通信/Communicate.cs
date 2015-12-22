using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using ProtocolSpace;

using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.LibUsb;
using HawkFileOperation;
using DataProSpace;

namespace CommunicateSpace
{
    public class Communicate
    {
        //public delegate ErrorCode WriteFun(byte[] buffer, int timeout, out int transferLength);
        public delegate ErrorCode WriteFun(byte[] buffer, int Index,int Count,int timeout, out int transferLength);
        public delegate ErrorCode ReadFun(byte[] buffer, int timeout, out int transferLength);
        public  WriteFun Write;
        public ReadFun Read;
        public  byte[] ReadBuffer = new byte[64];
        public  byte[] WriteBuffer = new byte[64];
        private int RealWriteLen=0;
        private int RealReadLen = 0;
        public byte[] m_cmdFrameBuf = new byte[500];
	    public byte[] m_usbRx = new byte[433 * 64]; // ?????2048?????+2ring rawdata +2 check sum + 1 frame,??+1chipsta
	    public byte[] m_usbStream = new byte[433 * 64 * 2]; // ??????,???
        public static ushort rxCnt = 0;          //?????????
        public static ushort frameCnt = 0;        //???????
        public static ushort rawRxCnt = 0;          //?????????
        public static ushort RawframeCnt = 0;        //???????
        public static int PID = 0x5740;
        public static int VID = 0x0483;
        public static int PixelRow = 72;
        public static int PixelCol = 128;
        public static ushort ChipVer = 0x5566;
        public double AvddVol { get; set; }
        public double VddIoVol { get; set; }

        private int StartTime = 0;
        public int ComSpeed = 0;
        public int ReportFre = 0;

        private long CountFrame = 0;

        public static ManualResetEvent m_EVKVersion = new ManualResetEvent(false);
        public static ManualResetEvent m_hSysTestUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hRegUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hArmUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hFlashUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hOrderUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hBufNotEmpty = new ManualResetEvent(false);
        public static ManualResetEvent m_hViewUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hImageUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hEnrollDlgUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hSNRUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_hGipoUpdate = new ManualResetEvent(false);
        public static ManualResetEvent m_OtpUpdate = new ManualResetEvent(false);

        HawkClassOutPutFile FileOut = new HawkClassOutPutFile();
        
        public  Communicate()
        {
            AvddVol = 3.3;
            VddIoVol = 1.8;
         }
        public  void InitWriteFun(WriteFun Fun)
        {
             Write = new WriteFun(Fun);
        }

        public void InitReadFun(ReadFun Fun)
        {
            Read = new ReadFun(Fun);
        }

        public void SendCmd(byte Cmd0, byte Cmd1, ushort cmdAddr, int Len, byte[] cmdData)
        {
            int OverTime = 2000;
            switch (Cmd0)
            {
                case ProCMD.USB_CMD_GET_RAW_DATA:
                    Array.Clear(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_GET_RAW_DATA;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = Cmd1;
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE,OverTime, out RealWriteLen);
                    break;
                case 2:
                    break;
                case ProCMD.USB_CMD_REG_RW:
                    Array.Clear(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE);
                    if (Cmd1 == ProCMD.USB_SPEC_REG_WRITE)
                    {
                        int maxCnt = Len / ProCMD.FRAME_DATA_SIZE + Convert.ToInt32(Len % ProCMD.FRAME_DATA_SIZE != 0);
                        int curCnt = 0;
                        int resByte = Len;
                        byte len;
                        ushort addr = cmdAddr;
                        while (curCnt < maxCnt)
                        {
                            len = (byte)min(ProCMD.FRAME_DATA_SIZE, resByte);
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_REG_RW;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_WRITE;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(addr >> 8);
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)addr;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_FRAME] = (byte)maxCnt;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_FRAME_CNT] = (byte)curCnt;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = len;
                            Array.Copy(cmdData, curCnt * ProCMD.FRAME_DATA_SIZE, m_cmdFrameBuf, ProCMD.OUT_INDEX_DATA, len);
                            Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                            resByte -= len;
                            curCnt++;
                            addr += len;
                        }
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_REG_READ)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_REG_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_READ;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)((byte)(cmdAddr >> 8));
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)cmdAddr;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = (byte)(Len >> 8);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = (byte)Len;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = 0;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_REG_ACCESS)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_REG_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_ACCESS;
                        m_cmdFrameBuf[2] = (byte)cmdAddr;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_STATUS_READ)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_REG_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_STATUS_READ;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(cmdAddr >> 8);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)cmdAddr;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = (byte)(Len >> 8);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = (byte)Len;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = 0;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_JIG_GPIO_WRITE)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_REG_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_JIG_GPIO_WRITE;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = 0;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = 0;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = 1;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = 0;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = (byte)(Len - 1);
                        Array.Copy(cmdData, 0 , m_cmdFrameBuf, ProCMD.OUT_INDEX_DATA, Len);
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    break;
                case ProCMD.USB_CMD_EVK_VERSION:
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_EVK_VERSION;
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    break;
                case ProCMD.USB_CMD_FLASH_RW:
                    Array.Clear(m_cmdFrameBuf, 0, 500);
                    if (Cmd1 == ProCMD.USB_SPEC_REG_WRITE)
                    {
                        int maxCnt = Len / ProCMD.FRAME_DATA_SIZE + Convert.ToInt32(Len % ProCMD.FRAME_DATA_SIZE != 0);
                        int curCnt = 0;
                        int resByte = Len;
                        byte len;
                        ushort addr = cmdAddr;
                        while (curCnt < maxCnt)
                        {
                            len = (byte)min(ProCMD.FRAME_DATA_SIZE, resByte);
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_FLASH_RW;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_WRITE;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(addr >> 8);
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)addr;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_FRAME] = (byte)maxCnt;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_FRAME_CNT] = (byte)curCnt;
                            m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = len;
                            Array.Copy(cmdData, curCnt * ProCMD.FRAME_DATA_SIZE, m_cmdFrameBuf, ProCMD.OUT_INDEX_DATA, len);
                            Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                            resByte -= len;
                            curCnt++;
                            addr += len;
                        }
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_REG_READ)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_FLASH_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_READ;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(cmdAddr >> 8);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)cmdAddr;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = (byte)(Len >> 8);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = (byte)Len;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = 0;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    else if (Cmd1 == ProCMD.USB_SPEC_REG_ACCESS)
                    {
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_FLASH_RW;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_REG_ACCESS;
                        m_cmdFrameBuf[2] = (byte)cmdAddr;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    break;
                case ProCMD.USB_CMD_ORDER:
                    Array.Clear(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_ORDER;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = Cmd1;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_DATA] = cmdData[0]; // for data to arm
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_DATA + 1] = cmdData[1]; // for data to arm
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    break;
                case ProCMD.USB_CMD_SYSTEM_TEST:
                    Array.Clear(m_cmdFrameBuf, 0, 500);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_SYSTEM_TEST;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = Cmd1;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(cmdAddr >> 8);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)cmdAddr;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = (byte)(Len >> 8);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = (byte)Len;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = 0;
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    break;
                case ProCMD.USB_CMD_ARM_FW_UPDATE:
                    Array.Clear(m_cmdFrameBuf, 0, 500);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_ARM_FW_UPDATE;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = Cmd1;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_H] = (byte)(cmdAddr >> 8);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_REG_L] = (byte)cmdAddr;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_H] = (byte)(Len >> 8);
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_RW_LEN_L] = (byte)Len;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_LEN] = 0;
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    break;
                case ProCMD.USB_CMD_SYSTEM_VERFY:
                    if (Cmd1 == ProCMD.USB_SPEC_OTP_READ)
                    {
                        Array.Clear(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE);
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = Cmd0;
                        m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = Cmd1;
                        Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    }
                    break;
                case ProCMD.USB_CMD_JIG_STATUS:
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_JIG_STATUS;
                    m_cmdFrameBuf[ProCMD.OUT_INDEX_SPEC] = 0x00; //
                    m_cmdFrameBuf[ProCMD.IN_INDEX_CURR_L] = (byte)Len;
                    Array.Copy(cmdData, 0, m_cmdFrameBuf, ProCMD.OUT_INDEX_DATA, Len);
                    Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
                    break;
            }
            Thread.Sleep(80);
            m_cmdAck.ackIsProc = ProCMD.CMD_ACK_WATITING;
            Write(m_cmdFrameBuf, 0, ProCMD.USB_PACKET_SIZE, OverTime, out RealWriteLen);
        }
        private T min<T>(T a,T b)
        {
            if ((a as IComparable).CompareTo(b) < 0) 
                return a;
            else
                return b;
        }
       // ??????
        public unsafe void ThreadDataProc(byte[] buf, byte nLen)
        {
            byte[] TempBuf = buf;
            switch (buf[ProCMD.IN_INDEX_USB_CMD])
            {
                //??rowdata??
                case ProCMD.USB_CMD_GET_RAW_DATA:
                    GetRawdata(TempBuf);
                    break;
                //????????
                case ProCMD.USB_CMD_FW_UPDATE:
                    GetUpdataStatus(TempBuf);
                    break;
                //????????
                case ProCMD.USB_CMD_REG_RW:
                    GetRegData(TempBuf);
                    break;
                case ProCMD.USB_CMD_FLASH_RW:
                    //GetFlashData(TempBuf);
                    break;
                //??????
                case ProCMD.USB_CMD_HW_CFG:
                    //GetConf(TempBuf);
                    break;
                case ProCMD.USB_CMD_EVK_VERSION:
                    GetEVKVersion(TempBuf);
                    break;
                case ProCMD.USB_CMD_ORDER:
                    GetCMDReplyData(TempBuf);
                    break;
                case ProCMD.USB_CMD_SYSTEM_TEST:
                    //GetSysTestData(TempBuf);
                    break;
                case ProCMD.USB_CMD_ARM_FW_UPDATE:
                    //GetArmFwUpdateData(TempBuf);
                    break;
                case ProCMD.USB_CMD_SYSTEM_VERFY:
                    GetSystemTestData(buf);
                    break;
                case ProCMD.USB_CMD_JIG_STATUS:
                    //GetJigData(buf);
                    break;
                case ProCMD.USB_CMD_ARM_NOTICE_PC:
                    break;
            }
            if (nLen > 0)
                CalcTime(64);
                
        }

        void GetCMDReplyData(byte[] buf)
        {
	        m_cmdAck.cmd0 = buf[ProCMD.IN_INDEX_USB_CMD];
	        m_cmdAck.cmd1 = buf[ProCMD.IN_INDEX_SPEC];
	        m_cmdAck.cmdStatus = buf[ProCMD.IN_INDEX_STATUS];


	        if (m_cmdAck.ackIsProc == ProCMD.CMD_ACK_WATITING)
	        {
		        if (buf[ProCMD.IN_INDEX_SPEC] == ProCMD.USB_SPEC_ORDER_FINGER_DETECT)
			        m_cmdAck.cmdData[0] = buf[ProCMD.IN_INDEX_DATA];

		        m_cmdAck.ackIsProc = ProCMD.CMD_ACK_RECEIVE;

		        m_hOrderUpdate.Set();
	        }
        }
        void GetUpdataStatus(byte[] buf)
        {
	        m_cmdAck.cmd0 = buf[ProCMD.IN_INDEX_USB_CMD];
	        m_cmdAck.cmd1 = buf[ProCMD.IN_INDEX_SPEC];
	        m_cmdAck.cmdStatus = buf[ProCMD.IN_INDEX_STATUS];
	        m_cmdAck.ackIsProc = 0;
        }

        // ????????
        void GetSystemTestData(byte[] buf)
        {
            if (ProCMD.USB_SPEC_OTP_READ == buf[ProCMD.IN_INDEX_SPEC])
	        {
                m_cmdAck.cmd0 = buf[ProCMD.IN_INDEX_USB_CMD];
                m_cmdAck.cmd1 = buf[ProCMD.IN_INDEX_SPEC];
                Array.Copy(buf, ProCMD.IN_INDEX_STATUS, m_cmdAck.cmdData, 0, 32);
		        if ((m_cmdFrameBuf[0] == m_cmdAck.cmd0) && (m_cmdFrameBuf[1] == m_cmdAck.cmd1))
		        {
			        // ?????????????
                    m_OtpUpdate.Set();
		        }
	        }
        }

        // ???????
        void GetRawdata(byte[] buf)
        {
	      
	        ushort totalByte;                  //??????,??64k
	        ushort curFrameNum;               //??????????????????
	        ushort frameDataLen;              //??????????
            ushort[] frameDataBuf = new ushort[ProCMD.RAWDATA_MAX_BUFFER];//??????

	        totalByte = (ushort)((buf[ProCMD.IN_INDEX_TOTAL_H] << 8) + buf[ProCMD.IN_INDEX_TOTAL_L]);

	        if (totalByte != (130 * 72 * 2) + 10)
	        {
		        return;
	        }
	        curFrameNum = (ushort)((buf[ProCMD.IN_INDEX_CURR_H] << 8) + buf[ProCMD.IN_INDEX_CURR_L]);
	        //TRACE(L"c:%d\n", curFrameNum);
	        frameDataLen = (ushort)min(buf[ProCMD.IN_INDEX_LEN], ProCMD.IN_DATA_LEN_MAX);

	        if ((RawframeCnt != 0) && (curFrameNum == 0))
	        {
		        //??????,????????????,??????
		        RawframeCnt = 0;
	        }

	        if (RawframeCnt == curFrameNum)//?????????,????
	        {
		        //??????????
		        if ((rawRxCnt + frameDataLen) <= m_usbRx.Length)
		        {
                    Array.Copy(buf,ProCMD.IN_INDEX_DATA,m_usbRx,rawRxCnt,frameDataLen);
		        }

		        rawRxCnt += frameDataLen;//?????????

		        RawframeCnt++;
		        if (rawRxCnt >= totalByte)
		        {
			        rawRxCnt = 0;
			        RawframeCnt = 0;

			        if (RawDataAnalyze(ref frameDataBuf))
			        {
                        ChangeToStardRawdata(ref frameDataBuf, PixelRow * PixelCol);
                         DataProClass.AppendDataBuff(frameDataBuf);
				        m_hViewUpdate.Set();
				        m_hImageUpdate.Set();
				        m_hEnrollDlgUpdate.Set();
				        m_hSNRUpdate.Set();
			        }
		        }
	        }
        }

        void CalcTime(int DataNum)
        {
            int DiffTime=0;
            int Period = 1000;
            if (CountFrame % Period == 0)
            {
                DiffTime = System.Environment.TickCount - StartTime;
                if (DiffTime == 0)
                {
                    ComSpeed = 0;
                    ReportFre = 0;
                }
                else
                {
                    ComSpeed = Period * DataNum * 1000 / DiffTime;   //Byte/s
                    ReportFre = (int)( 1000*Period/((double)72*130*2/56)/DiffTime );
                }
                StartTime = System.Environment.TickCount;
            }
                CountFrame++;
        }
        bool RawDataAnalyze(ref ushort[] DataBuf)
        {
	        int pos = 0;
            int row = PixelRow;
            int col = PixelCol;
	        int headLen = 10;
	        int rowDataLen = col * 2;
	        int rowPackageLen = 2 + rowDataLen + 2;
	        for (int i = 0; i < row; i++)
	        {
		        pos = i * rowPackageLen + headLen;
		        ushort usRowId = (ushort)((m_usbRx[pos] << 8) + (m_usbRx[pos + 1]));
		        if (usRowId >= row)
		        {
			        return false;
		        }
                Buffer.BlockCopy(m_usbRx, pos + 2, DataBuf, usRowId * rowDataLen, rowDataLen);
	        }

	        return true;
        }
         public void ChangeToStardRawdata(ref ushort[] Data, int Len)
        {
            int i;
            for(i=0;i<Len;i++)
                Data[i] = (ushort)((((Data[i] & 0x00FF) << 8) + (Data[i] >> 8))>>4);
        }




        // ???????
        void GetRegData(byte[] buf)
        {
	        m_cmdAck.cmd0 = buf[ProCMD.IN_INDEX_USB_CMD];
	        m_cmdAck.cmd1 = buf[ProCMD.IN_INDEX_SPEC];
	        m_cmdAck.cmdStatus = buf[ProCMD.IN_INDEX_STATUS];

	        // ???????????
            if (m_cmdAck.cmd1 == ProCMD.USB_SPEC_REG_READ || m_cmdAck.cmd1 == ProCMD.USB_SPEC_STATUS_READ)
	        {
                if (m_cmdAck.ackIsProc == ProCMD.CMD_ACK_WATITING)
		        {
			        GetRetDataExt(buf);
		        }
	        }
            else if (m_cmdAck.cmd1 == ProCMD.USB_SPEC_REG_WRITE || m_cmdAck.cmd1 == ProCMD.USB_SPEC_REG_ACCESS) //?????????
	        {
                if (m_cmdAck.ackIsProc == ProCMD.CMD_ACK_WATITING)
		        {
			        // ?????????????
			        m_hRegUpdate.Set();
                    m_cmdAck.ackIsProc = ProCMD.CMD_ACK_RECEIVE;
		        }
	        }
            else if (m_cmdAck.cmd1 == ProCMD.USB_SPEC_JIG_GPIO_WRITE)
	        {
                m_hGipoUpdate.Set();
                m_cmdAck.ackIsProc = ProCMD.CMD_ACK_RECEIVE;
	        }
        }

        private unsafe void GetEVKVersion(byte[] buf)
        {

            m_cmdAck.cmd0 = buf[ProCMD.IN_INDEX_USB_CMD];
            m_cmdAck.cmd1 = buf[ProCMD.IN_INDEX_SPEC];
            m_cmdAck.cmdStatus = buf[ProCMD.IN_INDEX_STATUS];
            if (m_cmdAck.ackIsProc == ProCMD.CMD_ACK_WATITING)
	        {
		        GetRetDataExt(buf);
	        }
        }

        public void ChangeToRawDataMode()
        {
            byte[] outReport = new byte[64];
            Array.Clear(outReport, 0, 64);
            outReport[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_GET_RAW_DATA;
            Write(outReport, 0, ProCMD.USB_PACKET_SIZE, 500, out RealWriteLen);

        }


        public void ChangeToIdleMode()
        {
            byte[] outReport = new byte[64];
            Array.Clear(outReport, 0, 64);
            outReport[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_NOP;
            Write(outReport, 0, ProCMD.USB_PACKET_SIZE, 500, out RealWriteLen);

        }


        private unsafe void GetRetDataExt(byte[] buf)
        {
	        ushort totalByte;                  //??????,??64k
	        ushort curFrameNum;               //??????????????????
	        ushort frameDataLen;              //??????????

	        totalByte = (ushort)((buf[ProCMD.IN_INDEX_TOTAL_H] <<8) + buf[ProCMD.IN_INDEX_TOTAL_L]);

	        curFrameNum = (ushort)((buf[ProCMD.IN_INDEX_CURR_H] << 8) + buf[ProCMD.IN_INDEX_CURR_L]);
            if(buf[ProCMD.IN_INDEX_LEN]<ProCMD.IN_DATA_LEN_MAX)
                frameDataLen = buf[ProCMD.IN_INDEX_LEN];
            else
                frameDataLen = ProCMD.IN_DATA_LEN_MAX;
	        if ((frameCnt != 0) && (curFrameNum == 0))
	        {
		        //??????,????????????,??????
		        frameCnt = 0;
	        }

	        if (frameCnt == curFrameNum)//?????????,????
	        {
		        //??????????
		        if ((rxCnt + frameDataLen) <= m_usbRx.Length)
		        {
                    Array.Copy(buf, ProCMD.IN_INDEX_DATA,m_usbRx, rxCnt, frameDataLen);
		        }

		        rxCnt += frameDataLen;//?????????

		        frameCnt++;

		        if (rxCnt >= totalByte)//????????
		        {
                    Array.Copy(m_usbRx, m_cmdAck.cmdData, rxCnt);

			        rxCnt = 0;
			        frameCnt = 0;

			        //?????????????
                    if (m_cmdAck.cmd0 == ProCMD.USB_CMD_REG_RW)
			        {
                        m_hRegUpdate.Set();
				        m_hFlashUpdate.Set();
			        }
                    else if (m_cmdAck.cmd0 == ProCMD.USB_CMD_SYSTEM_TEST)
			        {
				        m_hSysTestUpdate.Set();
			        }
                    else if (m_cmdAck.cmd0 == ProCMD.USB_CMD_EVK_VERSION)
			        {
				        m_hRegUpdate.Set();
			        }

			        //???????????
                    m_cmdAck.ackIsProc = ProCMD.CMD_ACK_RECEIVE;
		        }
	        }
        }

       public String ReadEVKVersion()
       {
            String m_EVKVersion = "";
            m_hRegUpdate.Reset();
		    SendCmd(ProCMD.USB_CMD_EVK_VERSION, 0, 0, 0, null);
            if (WaitForSingleObject(m_hRegUpdate,500)==true)
            {
                m_EVKVersion = Encoding.UTF8.GetString(m_cmdAck.cmdData, 0, 20);
                return m_EVKVersion;
            }
            else
                return null;
	        
       }
       private bool WaitForSingleObject(ManualResetEvent m_Event, int OverTime)
        {
            int Time = OverTime / 10;
            while (!m_Event.WaitOne(10))
            {
                if (Time == 0)
                    break;
                else
                    Time--;
            }
            if (Time != 0)
                return true;
            else
                return false;
        }

        // ????
       public  bool ChipRegRead(ushort addr, ref byte[] buf, ushort len, ushort ms)
        {
	        m_hRegUpdate.Reset();
	        SendCmd(ProCMD.USB_CMD_REG_RW, ProCMD.USB_SPEC_REG_READ, addr, len, buf);
	        if (WaitForSingleObject(m_hRegUpdate, ms) == false)
	        {
		        return false;
	        }
	        Array.Copy( m_cmdAck.cmdData,buf, len);
	        return true;
        }

        // ????
       public bool ChipRegWrite(ushort addr,  ref byte[] buf, ushort len, ushort ms)
        {
	        m_hRegUpdate.Reset();
	        SendCmd(ProCMD.USB_CMD_REG_RW, ProCMD.USB_SPEC_REG_WRITE, addr, len, buf);
            if (WaitForSingleObject(m_hRegUpdate, ms) == false)
	        {
		        return false;
	        }
	        return true;
        }

                // ?Otp??
        public bool ReadOtp(ref byte[] buf)
        {
	        //?OTP??
            m_OtpUpdate.Reset();
	        SendCmd(ProCMD.USB_CMD_SYSTEM_VERFY, ProCMD.USB_SPEC_OTP_READ, 0, 0, null);
	        //??????
            if (WaitForSingleObject(m_OtpUpdate, 500) == false)
            {
                return false;
            }
            Array.Copy(m_cmdAck.cmdData, buf, 32);
	        return true;
        }

        public String ReadModulID()
        {
            byte[] buf = new byte[32];
            String szModuleID = "";
            int i;
            if (ReadOtp(ref buf))
            {
                for (i = 0; i < 16; i++)
                {
                    szModuleID += buf[i].ToString("X2");
                }
                for (i = 24; i < 30; i++)
                {
                    szModuleID += buf[i].ToString("X2");
                }
                return szModuleID;
            }
            else
                return null;
        }

        public bool ReadDac(ref byte[] DAC) 
        {
            byte[] buf = new byte[32];
            int i;
            if (ReadOtp(ref buf))
            {
                for (i = 0; i < 4; i++)
                    DAC[i] = buf[i+18];
                return true;
            }
            else
                return false;
        }
        public bool SetVoltage( )
        {
             byte[] Buf = new byte[2];
             Buf[0] = (byte)(AvddVol * 10);
             Buf[1] = (byte)(VddIoVol * 10);
             SendCmd(ProCMD.USB_CMD_ORDER,ProCMD.USB_SPEC_ORDER_ARM_VOLTAGE,0,0,Buf);
             if (WaitForSingleObject(m_hOrderUpdate, 500) == true)
	         {
		         return false;
	         }
	         return true;
         }

        private bool SetVoltage(double Avdd,double Vddio)
        {
            byte[] Buf = new byte[2];
            Buf[0] = (byte)(Avdd * 10);
            Buf[1] = (byte)(Vddio * 10);
            SendCmd(ProCMD.USB_CMD_ORDER, ProCMD.USB_SPEC_ORDER_ARM_VOLTAGE, 0, 0, Buf);
            if (WaitForSingleObject(m_hOrderUpdate, 500) == true)
            {
                return false;
            }
            return true;
        }

        
        public bool UpdateBlock(byte[] buf, ushort len)
        {
	        byte[] outReport = new byte[64] ;
            byte[] tmp = new byte[64] ;
	        ushort currLen;
	        ushort totalCnt;
	        byte packLen;
            long BuffIndex = 0;
            SetVoltage(0,0);
            SetVoltage();
            Thread.Sleep(50);
	        outReport[ProCMD.OUT_INDEX_USB_CMD] = ProCMD.USB_CMD_FW_UPDATE;
            outReport[ProCMD.OUT_INDEX_SPEC] = ProCMD.USB_SPEC_FW_UPDATE_MILAN_TS;
            outReport[ProCMD.OUT_INDEX_TOTAL_H] = (byte)(len >> 8);
            outReport[ProCMD.OUT_INDEX_TOTAL_L] = (byte)(len & 0xFF);
	        currLen = 0;
	        totalCnt = 0;
	        while (true)
	        {
		        //???????
                outReport[ProCMD.OUT_INDEX_CURR_H] = (byte)(currLen >> 8);
                outReport[ProCMD.OUT_INDEX_CURR_L] = (byte)(currLen & 0xFF);

                packLen = (byte)min(ProCMD.OUT_DATA_LEN_MAX, len - totalCnt);

                outReport[ProCMD.OUT_INDEX_LEN] = packLen;

                Array.Copy(buf, BuffIndex, outReport, ProCMD.OUT_INDEX_DATA, packLen);

                Write(outReport, 0, ProCMD.USB_PACKET_SIZE, 500, out RealWriteLen);

		        currLen++;
		        totalCnt += packLen;
                BuffIndex += packLen;

		        if (totalCnt == len)
		        {
			        break;
		        }

		        Thread.Sleep(1);
	        }

	        int timeOut = 0;
	        //??????
	        while (true)
	        {
                Read(tmp, 1000, out RealReadLen);

                if (tmp[ProCMD.IN_INDEX_USB_CMD] == ProCMD.USB_CMD_FW_UPDATE
                    && tmp[ProCMD.IN_INDEX_SPEC] == ProCMD.USB_SPEC_FW_UPDATE_MILAN_TS)
		        {
                    if (tmp[ProCMD.IN_INDEX_STATUS] == ProCMD.USB_ACK_OK)
			        {
				        return true;
			        }
			        else
			        {
				        return false;
			        }
		        }

                Thread.Sleep(1);

		        timeOut++;

		        if (timeOut > 1000)
		        {
			        return false;
		        }
	        }
        }
        
    }
}
