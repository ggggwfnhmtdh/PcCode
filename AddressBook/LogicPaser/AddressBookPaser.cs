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

namespace AddressBook
{
    class AddressBookPaser
    {
        public struct PersonInf
        {
            public string Name;
            public string TelephoneNumber;
            public int JobId;
            public string Email;
            public string MobileNumber;
            public string Department;
            public string Position;
        };
        public List<PersonInf> ListAddressBook;
        public RichTextBox MsgBox;
        public int FONT_SIZE = 9;
        public Hashtable DepartmentTable;
        public Hashtable DepartmentTableRev;
        public AddressBookPaser(String FilePath,RichTextBox TextBox)
        {
            MsgBox = TextBox;
            List<string> Datalines = new List<string>(File.ReadAllLines(FilePath));
            ListAddressBook = new List<PersonInf>();
            for(int i = 0;i<Datalines.Count;i++)
            {
                string[] LineItemstr = Datalines[i].Trim().Split(';');
                if (LineItemstr.Length >= 7 && LineItemstr[0].Trim().Length >= 2 && LineItemstr[0].Trim().Length <= 4)
                {
                   
                    
                    PersonInf TempPerson = new PersonInf();
                    TempPerson.Name = LineItemstr[0].Trim();
                    TempPerson.TelephoneNumber = LineItemstr[1].Trim().Replace("电话：", "");
                     try 
                     {
                         TempPerson.JobId = Convert.ToInt32(LineItemstr[2].Replace("工号：", "").Trim());
                     }
                     catch 
                     {
                         TempPerson.JobId = 0;
                         ShowMsg(Datalines[i], Color.Green, FONT_SIZE, true);
                     }
                     TempPerson.Email = LineItemstr[3].Replace(" 邮箱：", "").Trim();
                    TempPerson.MobileNumber = LineItemstr[4].Replace("手机：", "").Trim();
                    TempPerson.Department = LineItemstr[5].Replace("部门：", "").Trim();
                    TempPerson.Position = LineItemstr[6].Replace("职位：", "").Trim();

                    if (TempPerson.Department == "")
                        TempPerson.Department = "未知";
                    ListAddressBook.Add(TempPerson);
                }
                else
                    ShowMsg(Datalines[i], Color.Red, FONT_SIZE, true);               

            }
            GetDepartmentData();
        }

        public void GetDepartmentData()
        {
            int CountTime = 0;
            DepartmentTable = new Hashtable();
            DepartmentTableRev = new Hashtable();
            for (int i = 0; i < ListAddressBook.Count; i++)
            {
                if (!DepartmentTable.ContainsValue(ListAddressBook[i].Department))
                {
                    DepartmentTable.Add(CountTime, ListAddressBook[i].Department);
                    DepartmentTableRev.Add(ListAddressBook[i].Department, CountTime);
                    CountTime++;
                }
            }
        }
        public void ShowMsg(string Msg, Color Fontcolor, float FontSize, bool IsBoldFont)
        {        
                MsgBox.SelectionStart = MsgBox.Text.Length;//设置插入符位置为文本框末
                MsgBox.SelectionColor = Fontcolor;//设置文本颜色
                if (IsBoldFont)
                    MsgBox.SelectionFont = new Font(MsgBox.Font.Name, FontSize, FontStyle.Bold);
                else
                    MsgBox.SelectionFont = new Font(MsgBox.Font.Name, FontSize, FontStyle.Regular);
                MsgBox.AppendText(Msg+"\r\n");//输出文本，换行
            
        }

        public void ShowImage(byte[] Data, int Width, int Hight)
        {
            Bitmap TempBmp = new Bitmap(Width, Hight);     //新建位图b1
            if (Data.Length == TempBmp.Width * TempBmp.Height)
            {
                for (int i = 0; i < TempBmp.Width; i++)
                {
                    for (int j = 0; j < TempBmp.Height; j++)
                    {
                        TempBmp.SetPixel(i, j, Color.FromArgb(255, 0, Data[i * TempBmp.Height + j], 0));
                    }
                }
                Clipboard.SetImage(TempBmp);
                MsgBox.Paste();
            }
        }

        public void ShowPic(string FilePath)
        {
            int W=400, H=400,P=0;
            Bitmap TempBmp = new Bitmap(FilePath);
            if (TempBmp.Width > W && TempBmp.Width >= TempBmp.Height)
            {
                P = TempBmp.Width / W;
                H = TempBmp.Height / P;
                Bitmap newBmp = new Bitmap(W,H);
                for (int i = 0; i < W; i++)
                {
                    for (int j = 0; j < H; j++)
                    {
                        int x, y;
                        x = i * TempBmp.Width / W;
                        y = j * TempBmp.Height / H;
                        newBmp.SetPixel(i, j, TempBmp.GetPixel(x, y));
                    }
                }
                Clipboard.SetImage(newBmp);
            }
            else if (TempBmp.Height > H && TempBmp.Height >= TempBmp.Width)
            {
                P = TempBmp.Height / H;
                W = TempBmp.Width / P;
                Bitmap newBmp = new Bitmap(W, H);
                for (int i = 0; i < W; i++)
                {
                    for (int j = 0; j < H; j++)
                    {
                        int x, y;
                        x = i * TempBmp.Width / W;
                        y = j * TempBmp.Height / H;
                        newBmp.SetPixel(i, j, TempBmp.GetPixel(x, y));
                    }
                }
                Clipboard.SetImage(newBmp);
            }
            else
                Clipboard.SetImage(TempBmp);
            MsgBox.Paste();

        }
    }

}
