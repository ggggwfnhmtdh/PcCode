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
    class FpcLogicSpiParser : LogicPaserBase
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

        public int SENSOR_ROW = 160;
        public int SENSOR_COL = 160;
        public int NAV_ROW = 40;
        public int NAV_COL = 20;
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

        public FpcLogicSpiParser(String FilePath)
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
            for (int i = 0; i < LogicData.Length; i++)
            {

                if (LogicData[i].IdLength == 0)
                    continue;
                else
                {
                    ProStrut pStrut = new ProStrut();
                    if(LogicData[i].IdLength==1)
                    {
                        pStrut.Cmd = 0x02;
                        pStrut.Addr = LogicData[i].MOSI;
                    }
                    else if ((LogicData[i].MOSI&0x80) == 0)  //写
                    {
                        if (i >= LogicData.Length - 2)
                            break;
                        pStrut.Cmd = 0x00;
                        pStrut.Addr = LogicData[i].MOSI;
                        pStrut.Length = LogicData[i].IdLength - 1;
                        pStrut.Data = new List<byte>();
                        for (int j = 0; j < pStrut.Length; j++)
                            pStrut.Data.Add(LogicData[i + 1 + j].MOSI);
                        i += LogicData[i].IdLength-1;
                    }
                    else //读
                    {
                        if (i >= LogicData.Length - 2)
                            break;
                        pStrut.Cmd = 0x01;
                        pStrut.Addr = LogicData[i].MOSI;
                        pStrut.Length = LogicData[i].IdLength - 1;
                        pStrut.Data = new List<byte>();
                        for (int j = 0; j < pStrut.Length; j++)
                            pStrut.Data.Add(LogicData[i + 1 + j].MISO);
                        i += LogicData[i].IdLength-1;
                        
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
                if (ProData[i].Cmd == 0)
                {
                    TempS = "W:0x" + ProData[i].Addr.ToString("X2") + " = 0x";
                    for (int j = 0; j < ProData[i].Length ; j++)
                    {
                        TempS += (ProData[i].Data[j]).ToString("X2") + " ";
                    }
                    TempS += "\r\n";
                    ShowMsg(TempS, Color.Red, FONT_SIZE, false);
                }
                else if (ProData[i].Cmd == 1)
                {
                    if (ProData[i].Data.Count >= SENSOR_ROW * SENSOR_COL)
                    {
                        TempS = "R:0x" + ProData[i].Addr.ToString("X2") + " = "+ ProData[i].Data[0].ToString("X2");
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);

                        ProData[i].Data.RemoveAt(0);
                        ShowImage(ProData[i].Data.ToArray(),SENSOR_COL,SENSOR_ROW);
                        TempS = "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);
                    }
                    else if (ProData[i].Data.Count >= NAV_ROW * NAV_COL)
                    {
                        TempS = "R:0x" + ProData[i].Addr.ToString("X2") + " = " + ProData[i].Data[0].ToString("X2");
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);

                        ProData[i].Data.RemoveAt(0);
                        ShowImage(ProData[i].Data.ToArray(), NAV_ROW, NAV_COL);
                        TempS = "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);
                    }
                    else
                    {
                        TempS = "R:0x" + ProData[i].Addr.ToString("X2") + " = 0x";
                        for (int j = 0; j < ProData[i].Length; j++)
                        {
                            TempS += (ProData[i].Data[j]).ToString("D3") + " ";
                        }
                        TempS += "\r\n";
                        ShowMsg(TempS, Color.Blue, FONT_SIZE, false);
                    }
                }
                else
                {
                    TempS = "SpiCmd:0x" + ProData[i].Addr.ToString("X2");
                    TempS += "\r\n";
                    ShowMsg(TempS, Color.Black, FONT_SIZE,true);
                }
                
            }
            if (ShowLevel==2)
               System.Diagnostics.Process.Start(SaveDataPath); 
           
        }

        public void ShowMsg(string Msg,Color Fontcolor,float FontSize,bool IsBoldFont)
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
               
            
            //MsgBox.ScrollToCaret();//滚动条滚到到最新插入行
        }

        public void ShowImage(byte[] Data,int Width,int Hight)
        {
            Bitmap TempBmp = new Bitmap(Width, Hight);     //新建位图b1
            if (Data.Length == TempBmp.Width * TempBmp.Height)
            {
                for(int i=0;i<TempBmp.Width;i++)
                {
                    for (int j = 0; j < TempBmp.Height; j++)
                    {
                        TempBmp.SetPixel(i, j, Color.FromArgb(255, 0, Data[i * TempBmp.Height+j], 0));
                    }
                }
                Clipboard.SetImage(TempBmp);
                MsgBox.Paste();
            }   
        }
    }
}
