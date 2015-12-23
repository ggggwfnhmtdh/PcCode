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
using System.Collections; 

namespace AddressBook
{
    public partial class MainForm : Form
    {
        public int ShowLevel = 0;
        ArrayList FileList = new ArrayList();
        public MainForm()
        {
            InitializeComponent();
        }
        AddressBookPaser MyAddressBook;
        private void button3_Click(object sender, EventArgs e)
        {
            StartBtn.Enabled = false;
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = false;
            openFileDialog1.Filter = "txt�ļ�|*.txt|�����ļ�|*.*";
            openFileDialog1.RestoreDirectory = true;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                String FilePath = openFileDialog1.FileName;
                ShowBox.AppendText("���ڼ������ݣ����Ե�.....\r\n");
                MyAddressBook = new AddressBookPaser(FilePath, ShowBox);
                �����ŷ���ToolStripMenuItem_Click(sender, e);
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
           
        }

        private void BookTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //ShowBox.Text += BookTree.SelectedNode.Index.ToString() + "\r\n";
            ShowBox.Text = "";
            for (int i = 0; i < MyAddressBook.ListAddressBook.Count; i++)
            {
                if (MyAddressBook.ListAddressBook[i].Name == BookTree.SelectedNode.Text)
                {
                    MyAddressBook.ShowMsg("������" + MyAddressBook.ListAddressBook[i].Name, Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("�ֻ��ţ�" + MyAddressBook.ListAddressBook[i].MobileNumber,Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("���䣺" + MyAddressBook.ListAddressBook[i].Email, Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("���ţ�" + MyAddressBook.ListAddressBook[i].JobId.ToString("D4"), Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("���ţ�" + MyAddressBook.ListAddressBook[i].Department, Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("ְλ��" + MyAddressBook.ListAddressBook[i].Position, Color.Black, MyAddressBook.FONT_SIZE, true);
                    MyAddressBook.ShowMsg("�绰��" + MyAddressBook.ListAddressBook[i].TelephoneNumber, Color.Black, MyAddressBook.FONT_SIZE, true);
                    string FileInf = SearchFile(System.Environment.CurrentDirectory, MyAddressBook.ListAddressBook[i].Name, ".jpg");
                    if (FileInf != "")
                    {
                        MyAddressBook.ShowMsg("ͼƬ·����" + FileInf, Color.Black, MyAddressBook.FONT_SIZE, true);
                        MyAddressBook.ShowPic(FileInf);
                    }
                    
                }
            }
            
        }

        private void ����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //for (int i = 0; i < BookTree.Nodes[0].Nodes.Count; i++)
            //    BookTree.Nodes[0].Nodes[i];
        }

        private void �����ŷ���ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MyAddressBook==null)
                return;
            int[] CountNum = new int[MyAddressBook.DepartmentTable.Count];
            BookTree.Nodes.Clear();
            BookTree.Nodes.Add("�㶥ͨѶ¼");
            for (int i = 0; i < MyAddressBook.DepartmentTable.Count; i++)
            {
                BookTree.Nodes[0].Nodes.Add(MyAddressBook.DepartmentTable[(object)i].ToString());
            }

            for (int i = 0; i < MyAddressBook.ListAddressBook.Count; i++)
            {
                int index = (int)MyAddressBook.DepartmentTableRev[MyAddressBook.ListAddressBook[i].Department];
                BookTree.Nodes[0].Nodes[index].Nodes.Add(MyAddressBook.ListAddressBook[i].Name);
                CountNum[index]++;
            }

            for (int i = 0; i < MyAddressBook.DepartmentTable.Count; i++)
            {
                BookTree.Nodes[0].Nodes[i].Text = BookTree.Nodes[0].Nodes[i].Text + "(" + CountNum[i].ToString()+")";
            }

            BookTree.Nodes[0].Expand();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        string SearchFile(string PathDirect, string Name,string FileType)//�����ļ����е��ļ�
        {
            DirectoryInfo dir = new DirectoryInfo(PathDirect);
            FileList.Clear();
            ArrayList Flst = GetAll(dir);
            //FileInfo[] inf = dir.GetFiles();
            foreach (FileInfo finf in Flst)
            {
                if (finf.Extension.ToUpper().Equals(FileType.ToUpper()) && finf.FullName.Contains(Name))
                    return finf.FullName;                
            }
            return "";
        }

        ArrayList GetAll(DirectoryInfo dir)//�����ļ����е��ļ�
        {
            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                FileList.Add(fi);
            }

            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAll(d);
            }
            return FileList;
        }
    }
}
