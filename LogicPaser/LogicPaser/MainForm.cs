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

namespace LogicPaser
{
    public partial class MainForm : Form
    {
        public int ShowLevel = 0;
        public MainForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ShowBox.Text = "";
            StartBtn.Enabled = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "Csv文件|*.csv|所有文件|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                System.Data.DataTable CsvDataTable = new System.Data.DataTable();
                String CsvFilePath = openFileDialog1.FileName;
                ShowBox.AppendText("正在加载数据，请稍等.....\r\n");
                if (ChipComBox.Text.ToUpper() == "FPC")
                {
                    FpcLogicSpiParser MyParser = new FpcLogicSpiParser(CsvFilePath);
                    MyParser.MsgBox = ShowBox;
                    MyParser.ShowData(ShowLevel);
                }
                if (ChipComBox.Text.ToUpper() == "MILANF")
                {
                    MilanFLogicSpiParser MyParser = new MilanFLogicSpiParser(CsvFilePath);
                    MyParser.MsgBox = ShowBox;
                    MyParser.ShowData(ShowLevel);
                }
                ShowBox.AppendText("加载完成\r\n");

            }
            StartBtn.Enabled = true;
        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void ShowBox_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ShowBox.Text = "";
        }

        private void ShowSpeedComBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ShowSpeedComBox.Text == "Level0")
                ShowLevel = 0;
            else if (ShowSpeedComBox.Text == "Level1")
                ShowLevel = 1;
            else if (ShowSpeedComBox.Text == "Level2")
                ShowLevel = 2;
        }
    }
}
