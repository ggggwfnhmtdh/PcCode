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
using Microsoft.Office.Interop.Excel;
using System.Data.OleDb;
using HawkFileOperation;

namespace FdtDataPro
{
    public partial class MainForm : Form
    {
        private List<System.Windows.Forms.TextBox> AreaBox = new List<System.Windows.Forms.TextBox>();
        private List<ushort[]> AreaData = new List<ushort[]>();
        private List<ushort[]> AreaDataTouch = new List<ushort[]>();
        private List<ushort[]> AreaDataNoTouch = new List<ushort[]>();
        public int[] SelectRow = new int[3]{8,40,72};
        public int[] SelectCol = new int[4]{8,36,64,92};


        public static bool isLoadData = false;
        public static bool isLoadReg = false;
        public static int ShowType = 0;
        public static int Pixel_Row = 88;
        public static int Pixel_Col = 108;
        public static string ExcelFilePath;
        public ushort[] ShowData = new ushort[Pixel_Row * Pixel_Col];
        public ushort[] NoTouchData = new ushort[Pixel_Row * Pixel_Col];
        public ushort[] TouchData = new ushort[Pixel_Row * Pixel_Col];
        public ushort[] DiffData = new ushort[Pixel_Row * Pixel_Col];
        public string DataType = "D4";

        public ushort rg_fdt_delta = 0;
        public ushort[] rg_fdt_thr = new ushort[12];
        public ushort[] rg_area_cmp_num = new ushort[12];
        public ushort[] rg_area01_cmp_num = new ushort[6];

        public int[] fdt_cmp_flag = new int[12];
        public int[] fdt_touch_flag = new int[12];
        public int[] fdt_avg_flag = new int[12];
        public int[] Pixel_flag = new int[12];
        public ulong[] RawSum = new ulong[12];
        public ulong[] BaseSum = new ulong[12];
        
        public int[] fdt_avg_flag1 = new int[12];
        public int[] fdt_touch_flag1 = new int[12];
        public ulong[] BaseSum1 = new ulong[12];

        public int FdtSize = 8*8;
        public MainForm()
        {
            InitializeComponent();
        }



        public static DataSet LoadDataFromExcel(string filePath, string SheetName)
        {
            bool hasTitle = false;
            string fileType = System.IO.Path.GetExtension(filePath);
            if (string.IsNullOrEmpty(fileType)) return null;

            using (DataSet ds = new DataSet())
            {
                string strCon = string.Format("Provider=Microsoft.Jet.OLEDB.{0}.0;" +
                                "Extended Properties=\"Excel {1}.0;HDR={2};IMEX=1;\";" +
                                "data source={3};",
                                (fileType == ".xls" ? 4 : 12), (fileType == ".xls" ? 8 : 12), (hasTitle ? "Yes" : "NO"), filePath);
                string strCom = " SELECT * FROM [" + SheetName + "$]";
                using (OleDbConnection myConn = new OleDbConnection(strCon))
                using (OleDbDataAdapter myCommand = new OleDbDataAdapter(strCom, myConn))
                {
                    myConn.Open();
                    myCommand.Fill(ds);
                }
                if (ds == null || ds.Tables.Count <= 0) return null;
                return ds;
            }

        }

        public void RegEdit()
        {
            //int RegValue;
            //RegistryKey hklm = Registry.LocalMachine;
            //RegistryKey RegSoftWare = hklm.OpenSubKey("SOFTWARE", true);
            //RegistryKey RegMicrosoft = RegSoftWare.OpenSubKey("Microsoft", true);
            //RegistryKey Regjet = RegMicrosoft.OpenSubKey("Jet", true);
            //RegistryKey Reg4 = Regjet.OpenSubKey("4.0", true);
            //RegistryKey RegEng = Reg4.OpenSubKey("Engines", true);
            //RegistryKey RegExcel = RegEng.OpenSubKey("Excel", true);
            //RegValue = Convert.ToInt32(RegExcel.GetValue("TypeGuessRows"));
            //if (RegValue != 0)
            //{
            //    RegExcel.SetValue("TypeGuessRows", 0);
            //    MessageBox.Show("注册表修改成功（用于读取Excel）！");

            //}
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            AreaBox.Add(AreaBox0);
            AreaBox.Add(AreaBox1);
            AreaBox.Add(AreaBox2);
            AreaBox.Add(AreaBox3);
            AreaBox.Add(AreaBox4);
            AreaBox.Add(AreaBox5);
            AreaBox.Add(AreaBox6);
            AreaBox.Add(AreaBox7);
            AreaBox.Add(AreaBox8);
            AreaBox.Add(AreaBox9);
            AreaBox.Add(AreaBox10);
            AreaBox.Add(AreaBox11);
            if (TypeHexCheck.Checked == true)
                DataType = "X4";
            else
                DataType = "D4";
            for (int i = 0; i < AreaBox.Count; i++)
                AreaBox[i].Text = i.ToString();
        }

        private void LoadDataBtn1_Click(object sender, EventArgs e)
        {
            int i = 0, j = 0,ErrorCount = 0;
            RegEdit();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Excel文件|*.xls|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelFilePath = openFileDialog1.FileName;
                System.Data.DataTable MyTable = new System.Data.DataTable();
                DataSet MyDataSet = LoadDataFromExcel(ExcelFilePath, "Sheet1");
                MyTable = MyDataSet.Tables[0];
                for (i = 0; i < MyTable.Rows.Count; i++)
                {
                    for (j = 0; j < MyTable.Columns.Count; j++)
                    {
                        NoTouchData[i * Pixel_Col + j] = Convert.ToUInt16(MyTable.Rows[i][j].ToString());
                    }
                }
            }
            else return;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelFilePath = openFileDialog1.FileName;
                System.Data.DataTable MyTable = new System.Data.DataTable();
                DataSet MyDataSet = LoadDataFromExcel(ExcelFilePath, "Sheet1");
                MyTable = MyDataSet.Tables[0];
                for (i = 0; i < MyTable.Rows.Count; i++)
                {
                    for (j = 0; j < MyTable.Columns.Count; j++)
                    {
                        TouchData[i * Pixel_Col + j] = Convert.ToUInt16(MyTable.Rows[i][j].ToString());
                    }
                }
            }
            else return;
            for (i = 0; i < Pixel_Row; i++)
            {
                for (j = 0; j < Pixel_Col; j++)
                {
                    if (NoTouchData[i * Pixel_Col + j] >= TouchData[i * Pixel_Col + j])
                        DiffData[i * Pixel_Col + j] = (ushort)(NoTouchData[i * Pixel_Col + j] - TouchData[i * Pixel_Col + j]);
                    else
                    {
                        DiffData[i * Pixel_Col + j] = (ushort)(TouchData[i * Pixel_Col + j] - NoTouchData[i * Pixel_Col + j]);
                        ErrorCount++;
                    }
                }
            }
            if(ErrorCount>0)
                MessageBox.Show("Error RawData Number:"+ErrorCount.ToString());
            isLoadData = true;
            GetAreaData();
            GetStaticsData();
            ShowAllData();
            
        }
        
        private void SowRawData(int Type)
        {

            
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {          
            ShowType = comboBox1.SelectedIndex;
            ShowAllData();
        }
        private bool GetSelectRow()
        {
            string[] Strs = SetRowBox.Text.Split(' ');
            if (Strs.Length != 3)
                return false;
            for (int i = 0; i < Strs.Length;i++)
            {
                try
                {
                    SelectRow[i] = Convert.ToInt32(Strs[i]);
                    if(SelectRow[i]<0||SelectRow[i]>Pixel_Row-8)
                        return false;
                }
                catch
                {
                    return false;
                }
            }
            return true;
            
        }
        private void ShowAllData()
        {
            if (isLoadData == false)
            {
                MessageBox.Show("No Data!");
                return;
            }
            if (ShowType == 0)
                Array.Copy(NoTouchData, ShowData, ShowData.Length);
            else if(ShowType==1)
                Array.Copy(TouchData, ShowData, ShowData.Length);
            else if (ShowType == 2)
                Array.Copy(DiffData, ShowData, ShowData.Length);

            if (GetSelectRow())
            {
                toolStripStatusLabel1.Text = "";
                AreaData.Clear();
                int Index = 0;
                for(int i = 0;i<3;i++)
                {
                    for(int j = 0;j<4;j++)
                    {
                        ushort[] TempData = new ushort[FdtSize];
                        

                        if (FdtSize == 8 * 8)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                for (int n = 0; n < 8; n++)
                                {
                                    Index = (SelectRow[i] + m) * Pixel_Col + SelectCol[j] + n;
                                    TempData[m * 8 + n] = ShowData[Index];
                                }
                            }
                        }
                        else if (FdtSize == 8 * 1)
                        {
                            for (int n = 0; n < 8; n++)
                            {
                                Index = (SelectRow[i]) * Pixel_Col + SelectCol[j] + n;
                                TempData[n] = ShowData[Index];
                            }
                        }

                        if(CheckSort.Checked==true)
                           Array.Sort(TempData);
                        AreaData.Add(TempData);
                    }
                }
                ShowListData(AreaData);
                ShowGetData(GetCom.Text);
            }
            else
                toolStripStatusLabel1.Text = "Get row error!";
        }


        private void GetAreaData()
        {
            if (isLoadData == false)
            {
                MessageBox.Show("No Data!");
                return;
            }
            if (GetSelectRow())
            {
                toolStripStatusLabel1.Text = "";
                AreaData.Clear();
                AreaDataTouch.Clear();
                AreaDataNoTouch.Clear();
                int Index = 0;
                for (int i = 0; i < 3; i++)
                {
                    for (int j = 0; j < 4; j++)
                    {
                        ushort[] TempData = new ushort[FdtSize];
                        ushort[] TempDataX = new ushort[FdtSize];

                        if (FdtSize == 8 * 8)
                        {
                            for (int m = 0; m < 8; m++)
                            {
                                for (int n = 0; n < 8; n++)
                                {
                                    Index = (SelectRow[i] + m) * Pixel_Col + SelectCol[j] + n;
                                    TempData[m * 8 + n] = TouchData[Index];
                                    TempDataX[m * 8 + n] = NoTouchData[Index];
                                }
                            }
                        }
                        else if(FdtSize == 8 * 1)
                        {
                            for (int n = 0; n < 8; n++)
                                {
                                    Index = (SelectRow[i]) * Pixel_Col + SelectCol[j] + n;
                                    TempData[n] = TouchData[Index];
                                    TempDataX[n] = NoTouchData[Index];
                                }
                        }

                        if (CheckSort.Checked == true)
                        {
                            Array.Sort(TempData);
                            Array.Sort(TempDataX);
                        }
                        AreaDataTouch.Add(TempData);
                        AreaDataNoTouch.Add(TempDataX);
                    }
                }
            }
            else
                toolStripStatusLabel1.Text = "Get row error!";
        }

        private void ShowListData(List<ushort[]> sListData)
        {
           string str = "";
           if(sListData.Count==12)
           {
               
               for(int i = 0;i<12;i++)
               {
                   str = "";
                   for (int j = 0; j < sListData[i].Length; j++)
                   {
                       str += sListData[i][j].ToString(DataType)+" ";
                   }
                   if (isLoadReg == true)
                   {
                       str += "fdt_cmp_flag=" + fdt_cmp_flag[i].ToString() + "\r\n";
                       str += "fdt_touch_flag=" + fdt_touch_flag[i].ToString() + "\r\n";
                       str += "fdt_avg_flag=" + fdt_avg_flag[i].ToString() + "\r\n";
                       str += "Pixel_flag=" + Pixel_flag[i].ToString() + "\r\n";
                       str += "RawSum=0x" + RawSum[i].ToString("X") + "\r\n";
                       str += "BaseSum=0x" + BaseSum[i].ToString("X") + "\r\n";
                   }
                   AreaBox[i].Text = str;
                   
               }
           }
           else
           {
               MessageBox.Show("ListData count error!");
           }
        }
        private void GetStaticsData()
        {
            if (isLoadData != true || isLoadReg != true)
            {
                toolStripStatusLabel1.Text = "Get Statics Data error!";
                return;
            }
            string str = "";
            Array.Clear(fdt_cmp_flag, 0, fdt_cmp_flag.Length);
            Array.Clear(fdt_touch_flag, 0, fdt_touch_flag.Length);
            Array.Clear(fdt_avg_flag, 0, fdt_avg_flag.Length);
            Array.Clear(Pixel_flag, 0, Pixel_flag.Length);
            Array.Clear(RawSum, 0, RawSum.Length);
            Array.Clear(BaseSum, 0, BaseSum.Length);
            toolStripStatusLabel1.Text = "Get Statics Data OK!";
            for (int i = 0; i < 12; i++)
            {
                ushort rg_cmp_thr_delta = (ushort)((rg_fdt_delta&0x00FF)<<4) ;
                ushort rg_cmp_thr_base = (ushort)((rg_fdt_thr[i]&0x00FF)<<4) ;
                ushort Max = (ushort)(rg_cmp_thr_base + rg_cmp_thr_delta);
                ushort Min = (ushort)(rg_cmp_thr_base - rg_cmp_thr_delta);
   
                ushort rg_raw_avg_thr_delta = (ushort)((rg_fdt_delta >> 8) << 4);
                ushort rg_raw_avg_thr_base = (ushort)((rg_fdt_thr[i]>>8) << 4);
                if(i==11)
                {
                    MaxBox.Text = Max.ToString();
                    MinBox.Text = Min.ToString();
                    DiffThrBox.Text = rg_raw_avg_thr_delta.ToString();
                    CmpNumBox.Text = rg_area_cmp_num[i].ToString();
                }
                str = "";
                for (int j = 0; j < AreaDataTouch[i].Length; j++)
                {
                    if (AreaDataTouch[i][j] < Max && AreaDataTouch[i][j] > Min)
                    {
                        RawSum[i] += AreaDataTouch[i][j];
                        BaseSum[i] += (ushort)(rg_raw_avg_thr_base - rg_raw_avg_thr_delta);
                        BaseSum1[i] += (ulong)(rg_raw_avg_thr_base + rg_raw_avg_thr_delta);
                        Pixel_flag[i]++;
                    }      
                }
                if (Pixel_flag[i] > rg_area_cmp_num[i])
                    fdt_cmp_flag[i] = 1;
                RawSum[i] >>=  6;
                BaseSum[i] >>= 6;
                BaseSum1[i] >>= 6;
                if (RawSum[i]<=BaseSum[i])
                    fdt_avg_flag[i] = 1;
                if (RawSum[i] >= BaseSum1[i])
                    fdt_avg_flag1[i] = 1;

                if (fdt_cmp_flag[i] == 1 && fdt_avg_flag[i] == 1)
                    fdt_touch_flag[i] = 1;
                if (fdt_cmp_flag[i] == 1 && fdt_avg_flag1[i] == 1)
                    fdt_touch_flag1[i] = 1;
            }
        }


        private void AreaBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CheckSort_CheckedChanged(object sender, EventArgs e)
        {
            ShowAllData();
        }

        private void SetRowBox_TextChanged(object sender, EventArgs e)
        {
            GetAreaData();
            ShowAllData();
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowGetData(GetCom.Text);
        }

        private void ShowGetData(string InStr)
        {

            if (InStr == "GetAvr")
            {
                Debug.Text = "";
                for (int i = 0; i < AreaData.Count; i++)
                {
                    Debug.Text += ArithmeticMean(AreaData[i]).ToString(DataType) + "\r\n";
                }
            }
        }

        private static ushort ArithmeticMean(ushort[] arr)
        {
            ulong result = 0;
            foreach (ushort num in arr)
            {
                result += num;
            }
            return (ushort)(result / (ulong)arr.Length);
        }

        private void TypeHexCheck_CheckedChanged(object sender, EventArgs e)
        {
            if (TypeHexCheck.Checked == true)
                DataType = "X4";
            else
                DataType = "D4";
            ShowAllData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Debug.Text = "";
            int i = 0, j = 0;
            RegEdit();
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Excel文件|*.xls|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                ExcelFilePath = openFileDialog1.FileName;
                System.Data.DataTable MyTable = new System.Data.DataTable();
                DataSet MyDataSet = LoadDataFromExcel(ExcelFilePath, "Sheet1");
                MyTable = MyDataSet.Tables[0];
                
                rg_fdt_delta = Convert.ToUInt16(MyTable.Rows[2][3].ToString(),16);
                Debug.Text += "rg_fdt_delta:" + rg_fdt_delta.ToString("X") + "\r\n";
                for (i = 0; i < 12; i++)
                {
                    rg_fdt_thr[i] = Convert.ToUInt16(MyTable.Rows[3 + i][3].ToString(),16);
                    Debug.Text += "rg_fdt_thr:" + rg_fdt_thr[i].ToString("X") + "\r\n";
                }

                for (i = 0; i < 6; i++)
                {
                    rg_area01_cmp_num[i] = Convert.ToUInt16(MyTable.Rows[32 + i][3].ToString(),16);
                    rg_area_cmp_num[2*i] = (ushort)(rg_area01_cmp_num[i]&0x3F);
                    rg_area_cmp_num[2 * i + 1] = (ushort)((rg_area01_cmp_num[i]>>8) & 0x3F);
                    Debug.Text += "rg_area01_cmp_num:" + rg_area01_cmp_num[i].ToString("X") + "\r\n";
                }
                isLoadReg = true;
                GetStaticsData();
                ShowAllData();
            }
            else return;
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void ConfigBtn_Click(object sender, EventArgs e)
        {
            if(isLoadData==false||isLoadReg==false)
            {
                MessageBox.Show("No Data!");
                return;
            }
            ushort rg_cmp_thr_delta = 0;
            ushort rg_cmp_thr_base = 0 ;
            ushort rg_raw_avg_thr_delta = 0;
            ushort config0 = Convert.ToUInt16(MaxBox.Text);
            ushort config1 = Convert.ToUInt16(MinBox.Text);
            ushort config2 = Convert.ToUInt16(DiffThrBox.Text);
            ushort config3 = Convert.ToUInt16(CmpNumBox.Text);
            rg_cmp_thr_delta = (ushort)((config0 - config1)>>1);
            rg_fdt_delta = (ushort)((rg_cmp_thr_delta & 0xFF00) | (rg_cmp_thr_delta>>4));
            rg_cmp_thr_base = (ushort)((config0 + config1) >> 1);
            for (int i = 0; i < 12;i++ )
            {
                rg_fdt_thr[i] = (ushort)((rg_fdt_thr[i] & 0xFF00) | (rg_cmp_thr_base>>4));
            }
                rg_raw_avg_thr_delta = (ushort)((config2 >> 4) << 8);
            rg_fdt_delta = (ushort)((rg_fdt_delta & 0x00FF) | rg_raw_avg_thr_delta);
            for (int i = 0; i < 12; i++)
            {
                rg_area_cmp_num[i] = config3;
            }

            GetStaticsData();
            ShowAllData();
        }

        private void Pixel8x1Check_CheckedChanged(object sender, EventArgs e)
        {
            if(Pixel8x1Check.Checked==true)
                FdtSize = 8 * 1;               
            else
                FdtSize = 8 * 8;     
            GetAreaData();
            GetStaticsData();
            ShowAllData();
        }

    }
}

