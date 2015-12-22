using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading;
using System.Drawing.Drawing2D;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Management;
using Microsoft.Win32;
using System.Data.OleDb;
using System.Collections;
namespace LogicPaser
{
    class MilanFLogicSpiParser : LogicPaserBase
    {
        public struct ProStrut
        {
            public byte Cmd;
            public ushort Addr;
            public int Length;
            public List<byte> Data;
        }
        public struct LogicDataStruct
        {
            public double Time;
            public  int ID;
            public byte MOSI;
            public byte MISO;
            public double DiffTime;  //us
            public int IdLength;  
        }

        public int SENSOR_ROW = 88;
        public int SENSOR_COL = 108;
        public int NAV_ROW = 24;
        public int NAV_COL = 108;
        public int FONT_SIZE = 9;

        public int RealStrutNum = 0;
        public int ProStrutNum = 0;
        public int RowT = 0;
        public int ColT = 0;
        public RichTextBox MsgBox;
        LogicDataStruct[] LogicData;
        List<ProStrut> ProData;
        Hashtable SpiCmdHashTable;
        public byte[] SpiCmdData = new byte[7] { 0xC0, 0xC1, 0xC2, 0xC4, 0xC8, 0xCA, 0xA1 };
        public String[] SpiCmdDataStr = new String[7] { "Idle", "Fdt", "Image", "FingerFlash", "Sleep", "Nav", "TimerTrigle" };

        public MilanFLogicSpiParser(String FilePath)
        {
            //新建Hash表
            SpiCmdHashTable = new Hashtable();
            for (int i = 0; i < SpiCmdData.Length; i++)
                SpiCmdHashTable.Add(SpiCmdData[i], SpiCmdDataStr[i]);

            //读取数据
            List<string> Datalines = new List<string>(File.ReadAllLines(FilePath));
            Datalines.RemoveAt(0);
            
            //获取逻辑数据
            LogicData = new LogicDataStruct[Datalines.Count];
            GetLogicData(Datalines);

            ProData = new List<ProStrut>();
            if (ProData.Count>0)
                ProData.Clear();
            GetProcess(ref ProData);
                       
        }
        public void GetLogicData(List<string> ListStr)
        {
            int Offset_1 = 0;
            int CountID = 0;
            for (int i = 0; i < ListStr.Count;i++ )
            {
                Offset_1 = i - 1;
                if (Offset_1 < 0)
                    Offset_1 = 0;
                ListStr[i] = ListStr[i].Replace("0x", "");
                string[] DataString = ListStr[i].Split(',');
                LogicData[i].Time = Convert.ToDouble(DataString[0]);
                if (DataString[1] != "")
                    LogicData[i].ID = Convert.ToInt32(DataString[1]);
                else
                    LogicData[i].ID = LogicData[i-1].ID;
                LogicData[i].MOSI = Convert.ToByte(DataString[2], 16);
                LogicData[i].MISO = Convert.ToByte(DataString[3], 16);

                if (LogicData[i].ID != LogicData[Offset_1].ID)
                {
                    LogicData[i - CountID].IdLength = CountID;
                    CountID = 1;
                }
                else
                    CountID++;
            }
                
        }
        public void GetProcess(ref List<ProStrut> ListData)
        {
            int Offset3 = 0;
            for (int i = 0; i < LogicData.Length; i++)
            {

                if (LogicData[i].IdLength == 0)
                    continue;
                else
                {
                    Offset3 = i + 3;
                    if (Offset3 > LogicData.Length - 1)
                        Offset3 = 0;

                    ProStrut pStrut = new ProStrut();
                    if (LogicData[i].MOSI == 0xF0 && LogicData[Offset3].MOSI == 0xF1)  //至少剩余4个字节才解析
                    {
                        if (i >= LogicData.Length - 2)
                            break;
                        pStrut.Cmd = 0xF1;
                        pStrut.Addr = (ushort)(LogicData[i + 1].MOSI * 256 + LogicData[i + 2].MOSI);
                        pStrut.Length = LogicData[Offset3].IdLength - 1;
                        pStrut.Data = new List<byte>();
                        for (int j = 0; j < pStrut.Length; j++)
                            pStrut.Data.Add(LogicData[Offset3 + 1 + j].MISO);
                        i += LogicData[i].IdLength + LogicData[Offset3].IdLength-1;
                    }
                    else if (LogicData[i].MOSI == 0xF0)  //至少剩余6个字节才解析
                    {
                        if (i >= LogicData.Length - 6)
                            break;
                        pStrut.Cmd = 0xF0;
                        pStrut.Addr = (ushort)(LogicData[i + 1].MOSI * 256 + LogicData[i + 2].MOSI);
                        pStrut.Length = (ushort)(LogicData[i + 3].MOSI * 256 + LogicData[i + 4].MOSI);
                        pStrut.Length *= 2;

                        if (i + 5 + pStrut.Length - 1 >= LogicData.Length)
                            pStrut.Length = LogicData.Length - 5 - i;
                        pStrut.Data = new List<byte>();
                        for (int j = 0; j < pStrut.Length; j++)
                            pStrut.Data.Add(LogicData[i + 5 + j].MOSI);
                        i += LogicData[i].IdLength-1;
                    }
                    else
                    {
                        pStrut.Cmd = LogicData[i].MOSI;
                        pStrut.Length = 0;
                    }
                    ListData.Add(pStrut);
                }
            }
            
        }

        public bool IsCmd(byte InCmd)
        {
            for (int i = 0; i < SpiCmdData.Length; i++)
            {
                if(SpiCmdData[i]==InCmd)
                {
                    return true;
                }
            }
            return false;
        }

        public void ShowData(int Level)
        {
            
            String TempS = "";
            ShowLevel = Level;
            for (int i = 0; i < ProData.Count; i++)
            {
                if (ProData[i].Cmd == 0xF0)   //写
                {
                    TempS = "W:0x" + ProData[i].Addr.ToString("X4") + " = 0x";
                    for (int j = 0; j < ProData[i].Length / 2; j++)
                    {
                        TempS += (ProData[i].Data[j * 2] * 256 + ProData[i].Data[j * 2 + 1]).ToString("X4") + " ";
                    }
                    TempS += "\r\n";
                    ShowMsg(TempS, Color.Red, FONT_SIZE, false);
                }
                else if (ProData[i].Cmd == 0xF1)
                {
                    
                    if (ProData[i].Addr == 0xAAAA)
                    {
                        TempS = "R:0x" + ProData[i].Addr.ToString("X4") + " ";
                        TempS += "Read FIFO Data(" + ProData[i].Length.ToString()+"Bytes)";
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);
                    }
                    else
                    {
                        TempS = "R:0x" + ProData[i].Addr.ToString("X4") + " = 0x";
                        for (int j = 0; j < ProData[i].Length / 2; j++)
                        {
                            TempS += (ProData[i].Data[j * 2] * 256 + ProData[i].Data[j * 2 + 1]).ToString("X4") + " ";
                        }
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);
                    }
                }
                else
                {
                    if (SpiCmdHashTable.Contains((object)ProData[i].Cmd))
                    {
                        TempS = "SpiCmd:";
                        TempS += SpiCmdHashTable[(object)ProData[i].Cmd].ToString();
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Black, FONT_SIZE, true);
                    }
                }
                
            }
            if (ShowLevel == 2)
                System.Diagnostics.Process.Start(SaveDataPath); 
           
        }

        public void ShowMsg(string Msg, Color Fontcolor, float FontSize, bool IsBoldFont)
        {
            if (ShowLevel == 0)
                MsgBox.AppendText(Msg);//输出文本，换行
            else if (ShowLevel == 1)
            {
                MsgBox.SelectionStart = MsgBox.Text.Length;//设置插入符位置为文本框末
                MsgBox.SelectionColor = Fontcolor;//设置文本颜色
                if (IsBoldFont)
                    MsgBox.SelectionFont = new Font(MsgBox.Font.Name, FontSize, FontStyle.Bold);
                else
                    MsgBox.SelectionFont = new Font(MsgBox.Font.Name, FontSize, FontStyle.Regular);
                MsgBox.AppendText(Msg);//输出文本，换行
            }
            else if (ShowLevel == 2)
            {
                FileStream fs = new FileStream(SaveDataPath, FileMode.Append);
                StreamWriter sw = new StreamWriter(fs, Encoding.Default);
                sw.Write(Msg);
                sw.Close();
                fs.Close();
            }
        }
    }
}
