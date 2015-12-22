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
using UsbProtecolSpace;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Drawing2D;
using LibUsbDotNet;
using LibUsbDotNet.Main;
using LibUsbDotNet.Info;
using LibUsbDotNet.DeviceNotify;
using LibUsbDotNet.LibUsb;
using System.Management;
using ProtocolSpace;
using CommunicateSpace;
using HawkFileOperation;
using HawkMathSpace;
using DataProSpace;
using DevExpress.XtraCharts;

namespace UsbComTools
{
    public partial class MainForm : Form
    {
        public HawkClassSaveFile Memory = new HawkClassSaveFile();
        public UsbRegDeviceList UsrUsbDev;
        public static IDeviceNotifier UsbDeviceNotifier = DeviceNotifier.OpenDeviceNotifier();
        public static byte[] UsbReadBuffer = new byte[64];
        public static byte[] UsbWriteBuffer = new byte[64];
        public static int RealReadLen;
        public static int RealWriteLen;
        public bool ThreadEnable = false;
        public static UsbEndpointReader CurReader;
        public static UsbEndpointWriter CurWriter;
        public static UsbDevice CurUsbDevice;
        public Thread CurReadThread;
        public Thread CurViewThread;
        public Thread CurMsgThread;
        public long CurRevDataLen = 0;
        public long LastRevDataLen = 0;
        public long TestTime = 0;
        public Communicate UsbCom = new Communicate();
        public DataProClass DataPro = new DataProClass();
        public bool TestDacEnable = false;
        public ushort ReadVer = 0;
        public bool UnlockEnable = false;
        public delegate void ChartInvoke(ushort[] Data);
        public delegate void ComBoxInvoke<T>(T[] Data,String Format);
        public delegate void MsgInvoke();
        public MainForm()
        {
            InitializeComponent();
            //this.DoubleBuffered = true;//ÉèÖÃ±¾´°Ìå
            //SetStyle(ControlStyles.UserPaint, true);
            //SetStyle(ControlStyles.AllPaintingInWmPaint, true); // ½ûÖ¹²Á³ý±³¾°.
            //SetStyle(ControlStyles.DoubleBuffer, true); // Ë«»º³å
        }

        private void button1_Click(object sender, EventArgs e)
        {
           
        }

        private void Debug_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ConfigPath.Text = Memory.LoadMemoryInformation("ConfigPath.Text");
            PatchPath.Text = Memory.LoadMemoryInformation("PatchPath.Text");
            CalcFwPath.Text = Memory.LoadMemoryInformation("CalcFwPath.Text");
            SavePath.Text = Memory.LoadMemoryInformation("SavePath.Text");
            if (SavePath.Text == "")
                SavePath.Text = System.Windows.Forms.Application.StartupPath;

            DataPro.MsgBox = Debug;
            DataPro.MsgChart = DataChart;
            toolStripStatusLabel1.Text = "123";

            UsbDeviceNotifier.OnDeviceNotify += OnDeviceNotifyEvent;
            UsbClass UsrUsbObj = new UsbClass(Communicate.VID, Communicate.PID);
            UsrUsbDev = UsrUsbObj.GetAllDevice();
            DeviceTree.Nodes.Clear();
            if (UsrUsbDev.Count > 0)
                DeviceTree.Nodes.Add("UsbDeviceList");
            else
                DeviceTree.Nodes.Clear();
            for (int i = 0; i < UsrUsbDev.Count; i++)
            {
                UsbRegistry UsbDev = UsrUsbDev[i];
                DeviceTree.Nodes[0].Nodes.Add(UsbDev.Name);
                DeviceTree.Nodes[0].Nodes[i].Nodes.Add(UsbDev.DeviceProperties["LocationInformation"].ToString());
            }
            DeviceTree.ExpandAll();

            CurViewThread = new Thread(new ParameterizedThreadStart(RawDataViewThread));
            CurViewThread.Start((object)this);
            CurMsgThread = new Thread(new ParameterizedThreadStart(MsgShowThread));
            CurMsgThread.Start((object)this);


           
        }

        private  void OnDeviceNotifyEvent(object sender, DeviceNotifyEventArgs e)
        {
            

                if (e.EventType == EventType.DeviceRemoveComplete)
                {
                    e.Device.IdVendor == 1;

                    RealeaseUsb();
                    toolStripDropDownButton1.Image = Properties.Resources.Disconnect;
                }
                else if(e.EventType == EventType.DeviceArrival)
                {
                    UsbClass UsrUsbObj = new UsbClass(Communicate.VID, Communicate.PID);
                    UsrUsbDev = UsrUsbObj.GetAllDevice();
                    DeviceTree.Nodes.Clear();
                    if (UsrUsbDev.Count > 0)
                        DeviceTree.Nodes.Add("UsbDeviceList");
                    else
                    {
                        RealeaseUsb();
                        DeviceTree.Nodes.Clear();
                        toolStripDropDownButton1.Image = Properties.Resources.Disconnect;
                        return;
                    }
                    for (int i = 0; i < UsrUsbDev.Count; i++)
                    {
                        UsbRegistry UsbDev = UsrUsbDev[i];
                        DeviceTree.Nodes[0].Nodes.Add(UsbDev.Name);
                        DeviceTree.Nodes[0].Nodes[i].Nodes.Add(UsbDev.DeviceProperties["LocationInformation"].ToString());
                    }
                    DeviceTree.ExpandAll();
                }

        }

        private void RealeaseUsb()
        {
            ThreadEnable = false;
            if (CurReadThread != null)
                CurReadThread.Abort();
            if (!ReferenceEquals(CurUsbDevice, null))
                CurUsbDevice.Close();
            if (!ReferenceEquals(CurReader, null))
                CurReader.Dispose();
            if (!ReferenceEquals(CurWriter, null))
                CurWriter.Dispose();
            
        }

        public void UsbReadThread(object pForm)
        {
            MainForm mForm = (MainForm)pForm;
            byte[] LastData = new byte[64];
            while (ThreadEnable)
            {
                UsbCom.Read(UsbReadBuffer, 1000, out RealReadLen);
                CurRevDataLen += RealReadLen;
                UsbCom.ThreadDataProc(UsbReadBuffer, (byte)RealReadLen);
                if (RealReadLen > 0 && ComTest.Checked==true)
                    mForm.UpdateDebugText(UsbReadBuffer); 
            }
        }

        public void UpdateDebugText<T>(T[] Data)
        {
            if (Debug.InvokeRequired)
            {
                if (Data is byte[])
                {
                    ComBoxInvoke<byte> CallBackFun = new ComBoxInvoke<byte>(DataPro.ShowMsgBox);
                    DataChart.Invoke(CallBackFun, new object[] { Data, "X2" });
                }
                else if (Data is ushort[])
                {
                    ComBoxInvoke<ushort> CallBackFun = new ComBoxInvoke<ushort>(DataPro.ShowMsgBox);
                    DataChart.Invoke(CallBackFun, new object[] { Data, "X2" });
                }

            }
        }

        public void RawDataViewThread(object pForm)
        {
            MainForm mForm = (MainForm)pForm;
            ushort[] RawData = new ushort[72 * 128];
            ushort[] Buf = new ushort[64];
            while (true)
            {
                if (DataProClass.RawDataBuff.Count > 0)
                {
                    if (DataProClass.GetViewData(ref RawData))
                    {
                        if (ChartEnable.Checked==true)
                        mForm.UpdateDataChart(RawData);
  
                    }
                }
                //Thread.Sleep(100);
            }
        }

        public void UpdateDataChart(ushort[] Data)
        {
            
            if(DataChart.InvokeRequired)
            {
                ChartInvoke CallBackFun = new ChartInvoke(DataPro.ShowMsgChart);
                DataChart.Invoke(CallBackFun, new object[] { Data });
            }
        }

        public void MsgShowThread(object pForm)
        {
            MainForm mForm = (MainForm)pForm;
            ushort[] RawData = new ushort[72 * 128];
            while (true)
            {
                mForm.UpdateMsg("Speed");
                Thread.Sleep(500);
            }
        }

        public void UpdateMsg(String Type)
        {
            if (Type == "Speed")
            {
                if (statusStrip1.InvokeRequired)
                {
                    MsgInvoke CallBackFun = new MsgInvoke(StatusShowSpeed);
                    DataChart.Invoke(CallBackFun);
                }
            }
        }
        public void StatusShowSpeed()
        {
            toolStripStatusLabel1.Text = "   ComSpd:" + (UsbCom.ComSpeed / 1024).ToString("D3") + "kb/s";
            toolStripStatusLabel1.Text += "   RepSpd:" + (UsbCom.ReportFre).ToString("D2") + "/s";
        }


        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            RealeaseUsb();
            if(CurViewThread!=null)
               CurViewThread.Abort();
            if (CurMsgThread != null)
                CurMsgThread.Abort();
        }

        private void disConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeviceTree.SelectedNode.Level == 1)
            {
                if (ThreadEnable == false)
                    return;
                String LocatInf = DeviceTree.SelectedNode.Nodes[0].Text;
                if (CurUsbDevice.UsbRegistryInfo.DeviceProperties["LocationInformation"].ToString() == LocatInf)
                {
                    RealeaseUsb();
                    toolStripDropDownButton1.Image = Properties.Resources.Disconnect;
                }
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            byte[] Buf = new byte[2];
            Buf[0] = 0x00;
            Buf[1] = 0x00;
            //??????
            
            LastRevDataLen = CurRevDataLen;
        }

        private void MainMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {

            if (ThreadEnable == true)
            {
                TestDacData();
            }
            else
            {
                MessageBox.Show("Device don't be connected!!");
            }
         
        }

        private void TestDacData()
        {
            String sMuduleID = "";
            String sDacValue = "";
            String sAvrRawa = "";
            TestTime++;
            DataPro.ClearMsgBox();
            DataPro.ShowMsgBox(TestTime);
            byte[] Buf = new byte[2];
            if (ThreadEnable == false)
            {
                MessageBox.Show("?????!!");
                return;
            }
            DataPro.ShowMsgBox(UsbCom.ReadEVKVersion());
            toolStripProgressBar1.Value = 10;
            //????ID
            sMuduleID = UsbCom.ReadModulID();
            DataPro.ShowMsgBox(sMuduleID);
            //??DAC
            byte[] DacValue = new byte[4];
            UsbCom.ReadDac(ref DacValue);
            sDacValue += DacValue[0].ToString("D2") + "-";
            sDacValue += DacValue[1].ToString("D2") + "-";
            sDacValue += DacValue[2].ToString("D2") + "-";
            sDacValue += DacValue[3].ToString("D2");
            DataPro.ShowMsgBox(DacValue, "D2");
            toolStripProgressBar1.Value = 20;

            //????
            bool ret = UpdateFw();
            if (ret)
                DataPro.ShowMsgBox("App Update Successful!!");
            else
                DataPro.ShowMsgBox("App Update Fail!!");
            //??Debug??
            Buf[0] = 0x56;
            Buf[1] = 0x00;
            UsbCom.ChipRegWrite(0x3034, ref Buf, 2, 500);
            Buf[0] = 0x00;
            UsbCom.ChangeToRawDataMode();
            Thread.Sleep(500);
            toolStripProgressBar1.Value = 30;
            ushort[] RawData = new ushort[72 * 128];
            ulong[] SumRaw = new ulong[72 * 128];
            ushort[] AvrRaw = new ushort[4];
            int AvrNum = 30;
            Array.Clear(SumRaw, 0, SumRaw.Length);
            for (int i = 0; i < AvrNum; i++)
            {
                Application.DoEvents();
                DataProClass.ReadDataBuff(ref RawData);
                for (int j = 0; j < RawData.Length; j++)
                {
                    SumRaw[j] += RawData[j];
                    if (i == AvrNum - 1)
                    {
                        RawData[j] = (ushort)(SumRaw[j] / (ulong)AvrNum);
                    }
                }
            }
            toolStripProgressBar1.Value = 50;
            AvrRaw = HawkMath.GetDacAvrRaw(RawData, Communicate.PixelRow, Communicate.PixelCol);
            sAvrRawa = "-";
            sAvrRawa += AvrRaw[0].ToString("D4") + "-";
            sAvrRawa += AvrRaw[1].ToString("D4") + "-";
            sAvrRawa += AvrRaw[2].ToString("D4") + "-";
            sAvrRawa += AvrRaw[3].ToString("D4");
            DataPro.ShowMsgBox(AvrRaw, "D4");
            toolStripProgressBar1.Value = 60;

            ushort[] SaveData = new ushort[72 * 128 + 4];
            Array.Copy(RawData, 0, SaveData, 0, RawData.Length);
            Array.Copy(AvrRaw, 0, SaveData, RawData.Length, AvrRaw.Length);
            HawkClassOutPutFile FileOut = new HawkClassOutPutFile();
            FileOut.Length = SaveData.Length;
            FileOut.Column = 128;
            FileOut.Raw = 72;
            //FileOut.IsolateString = ",";
            FileOut.DestinationFilePathName = SavePath.Text + "\\" + sMuduleID + "-" + sDacValue + ".txt";
            FileOut.AlphabetFormat = "D4";

            UnlockEnable = true;
            if (CbSaveData.Checked == true)
            {
                if (UnlockEnable == true)
                    FileOut.OutFile(SaveData);
                else
                    MessageBox.Show("??????,????????,???????!");
            }

            toolStripProgressBar1.Value = 100;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "?????|*.bin|????|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                PatchPath.Text = openFileDialog1.FileName;
                if (File.Exists(PatchPath.Text))
                    Memory.SaveInformation("PatchPath.Text", PatchPath.Text);
            }  
        }

        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "????|*.cfg|????|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ConfigPath.Text = openFileDialog1.FileName;
                if (File.Exists(ConfigPath.Text))
                    Memory.SaveInformation("ConfigPath.Text", ConfigPath.Text);
            }  
        }

        public bool UpdateFw()
        {
            int LenCount = 0;
            int Offset = 0;
            byte[] Data;
            byte[] Code = new byte[ProCMD.GF12_FIRMWARE_LEN + ProCMD.GF12_CONFIGFILE_LEN];
            HawkClassBinFile BinFile = new HawkClassBinFile();
            if (File.Exists(PatchPath.Text) && File.Exists(ConfigPath.Text))
            {
                BinFile.SourceFilePathWithName = PatchPath.Text;
                Data = BinFile.ReadFile();
                if (Data.Length > ProCMD.GF12_FIRMWARE_LEN)
                    Offset = Data.Length - ProCMD.GF12_FIRMWARE_LEN;
                else
                    Offset = 0;
                Array.Copy(Data, Offset, Code, 0, Data.Length - Offset);
                LenCount = Data.Length - Offset;
                BinFile.SourceFilePathWithName = ConfigPath.Text;
                Data = BinFile.ReadFile();
                Array.Copy(Data, 0, Code, LenCount, Data.Length);
                bool ret = UsbCom.UpdateBlock(Code, (ushort)(ProCMD.GF12_FIRMWARE_LEN + ProCMD.GF12_CONFIGFILE_LEN));
                return ret;
            }
            else
            {
                MessageBox.Show("???????????!!!");
                return false;
            }
        }

        public bool DacCalc(ref byte[] DacValue)
        {
            int LenCount = 0;
            int Offset = 0;
            byte[] Data;
            byte[] Code = new byte[ProCMD.GF12_FIRMWARE_LEN + ProCMD.GF12_CONFIGFILE_LEN];
            HawkClassBinFile BinFile = new HawkClassBinFile();
            if (File.Exists(PatchPath.Text) && File.Exists(ConfigPath.Text))
            {
                BinFile.SourceFilePathWithName = CalcFwPath.Text;
                Data = BinFile.ReadFile();
                if (Data.Length > ProCMD.GF12_FIRMWARE_LEN)
                    Offset = Data.Length - ProCMD.GF12_FIRMWARE_LEN;
                else
                    Offset = 0;

                Array.Copy(Data, Offset, Code, 0, Data.Length - Offset);
                LenCount = Data.Length - Offset;
                BinFile.SourceFilePathWithName = ConfigPath.Text;
                Data = BinFile.ReadFile();
                Array.Copy(Data, 0, Code, LenCount, Data.Length);
                bool ret = UsbCom.UpdateBlock(Code, (ushort)(ProCMD.GF12_FIRMWARE_LEN + ProCMD.GF12_CONFIGFILE_LEN));
                if (ret)
                {
                    Thread.Sleep(100);
                    byte[] Buf = new byte[16];
                    UsbCom.ChipRegRead(0x3194, ref Buf, 16, 500);
                    for (int i = 0; i < 4; i++)
                    {
                        if (Buf[4 * i] == 1)
                        {
                            DacValue[i] = Buf[4 * i + 2];
                        }
                        else
                            return false;
                    }
                    return true;
                }
                else
                    return false;
            }
            else
            {
                MessageBox.Show("Device is not connected!!!");
                return false;
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "?????|*.bin|????|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                CalcFwPath.Text = openFileDialog1.FileName;
                if (File.Exists(CalcFwPath.Text))
                    Memory.SaveInformation("CalcFwPath.Text", CalcFwPath.Text);
            }  
        }

        private void button6_Click(object sender, EventArgs e)
        {
            System.Windows.Forms.FolderBrowserDialog folderBrowserDialog = new System.Windows.Forms.FolderBrowserDialog();
            System.Windows.Forms.DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == System.Windows.Forms.DialogResult.OK)
            {
                SavePath.Text = folderBrowserDialog.SelectedPath;
                if (Directory.Exists(SavePath.Text))
                    Memory.SaveInformation("SavePath.Text", SavePath.Text);
            }  
        }

        private void DeviceTree_AfterSelect(object sender, TreeViewEventArgs e)
        {

        }

        private void statusStrip1_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {

        }

        private void ConnectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (DeviceTree.SelectedNode.Level == 1)
            {
                String LocatInf = DeviceTree.SelectedNode.Nodes[0].Text;
                UsbRegistry MyRegUsb = UsbClass.UsbFilter(UsrUsbDev, "LocationInformation", LocatInf);
                MyRegUsb.Open(out CurUsbDevice);
                ((LibUsbDevice)CurUsbDevice).SetConfiguration(1);
                ((LibUsbDevice)CurUsbDevice).ClaimInterface(0);
                CurReader = CurUsbDevice.OpenEndpointReader(ReadEndpointID.Ep01);
                CurWriter = CurUsbDevice.OpenEndpointWriter(WriteEndpointID.Ep03);
                UsbCom.InitWriteFun(CurWriter.Write);
                UsbCom.InitReadFun(CurReader.Read);
                ThreadEnable = true;
                CurReadThread = new Thread(new ParameterizedThreadStart(UsbReadThread));
                CurReadThread.Start((object)this);
                toolStripDropDownButton1.Image = Properties.Resources.Connect;
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                
            }
            else if (e.KeyCode == Keys.F5)
            {
                button1_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F4)
            {
                
            }
            else if (e.KeyCode == Keys.F6)
            {
               
            }
            else if (e.KeyCode == Keys.F10)
            {
               
            }
            else if (e.Control && e.KeyCode >= Keys.NumPad0 && e.KeyCode <= Keys.NumPad9)
            {

              
            }

        }

        private void calibDacToolStripMenuItem_Click(object sender, EventArgs e)
        {
            byte[] DacValue = new byte[4];
            if (ThreadEnable == false)
            {
                MessageBox.Show("?????!!");
                return;
            }
            bool ret = DacCalc(ref DacValue);
            if (ret)
                DataPro.ShowMsgBox(DacValue);
            else
                DataPro.ShowMsgBox("Dac Calibrate Fail");
        }

        private void Debug_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Text = "";
            
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void MainForm_SizeChanged(object sender, EventArgs e)
        {
            this.toolStripProgressBar1.Size = new System.Drawing.Size(this.Size.Width-300, toolStripProgressBar1.Size.Height); 
        }

        private void Debug_DoubleClick(object sender, EventArgs e)
        {
            ((RichTextBox)sender).Text = "";
        }


  
    }
}
