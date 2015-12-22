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
namespace LogicPaser
{
    public class CSVFileHelper
    {
        /// <summary>
        /// ��DataTable������д�뵽CSV�ļ���
        /// </summary>
        /// <param name="dt">�ṩ�������ݵ�DataTable</param>
        /// <param name="fileName">CSV���ļ�·��</param>
        public static void SaveCSV(System.Data.DataTable dt, string fullPath)
        {
            FileInfo fi = new FileInfo(fullPath);
            if (!fi.Directory.Exists)
            {
                fi.Directory.Create();
            }
            FileStream fs = new FileStream(fullPath, System.IO.FileMode.Create, System.IO.FileAccess.Write);
            //StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.Default);
            StreamWriter sw = new StreamWriter(fs, System.Text.Encoding.UTF8);
            string data = "";
            //д��������
            for (int i = 0; i < dt.Columns.Count; i++)
            {
                data += dt.Columns[i].ColumnName.ToString();
                if (i < dt.Columns.Count - 1)
                {
                    data += ",";
                }
            }
            sw.WriteLine(data);
            //д����������
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                data = "";
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    string str = dt.Rows[i][j].ToString();
                    str = str.Replace("\"", "\"\"");//�滻Ӣ��ð�� Ӣ��ð����Ҫ��������ð��
                    if (str.Contains(',') || str.Contains('"')
                        || str.Contains('\r') || str.Contains('\n')) //������ ð�� ���з�����Ҫ�ŵ�������
                    {
                        str = string.Format("\"{0}\"", str);
                    }

                    data += str;
                    if (j < dt.Columns.Count - 1)
                    {
                        data += ",";
                    }
                }
                sw.WriteLine(data);
            }
            sw.Close();
            fs.Close();
            DialogResult result = MessageBox.Show("CSV�ļ�����ɹ���");
        }

        /// <summary>
        /// ��CSV�ļ������ݶ�ȡ��DataTable��
        /// </summary>
        /// <param name="fileName">CSV�ļ�·��</param>
        /// <returns>���ض�ȡ��CSV���ݵ�DataTable</returns>
        public static System.Data.DataTable OpenCSV(string filePath)
        {
            Encoding encoding = EncodingType.GetType(filePath); //Encoding.ASCII;//
            System.Data.DataTable dt = new System.Data.DataTable();
            FileStream fs = new FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);

            //StreamReader sr = new StreamReader(fs, Encoding.UTF8);
            StreamReader sr = new StreamReader(fs, encoding);
            //string fileContent = sr.ReadToEnd();
            //encoding = sr.CurrentEncoding;
            //��¼ÿ�ζ�ȡ��һ�м�¼
            string strLine = "";
            //��¼ÿ�м�¼�еĸ��ֶ�����
            string[] aryLine = null;
            string[] tableHead = null;
            //��ʾ����
            int columnCount = 0;
            //��ʾ�Ƿ��Ƕ�ȡ�ĵ�һ��
            bool IsFirst = true;
            //���ж�ȡCSV�е�����
            while ((strLine = sr.ReadLine()) != null)
            {
                //strLine = Common.ConvertStringUTF8(strLine, encoding);
                //strLine = Common.ConvertStringUTF8(strLine);

                if (IsFirst == true)
                {
                    tableHead = strLine.Split(',');
                    IsFirst = false;
                    columnCount = tableHead.Length;
                    //������
                    for (int i = 0; i < columnCount; i++)
                    {
                        DataColumn dc = new DataColumn(tableHead[i]);
                        dt.Columns.Add(dc);
                    }
                }
                else
                {
                    aryLine = strLine.Split(',');
                    DataRow dr = dt.NewRow();
                    for (int j = 0; j < columnCount; j++)
                    {
                        dr[j] = aryLine[j];
                    }
                    dt.Rows.Add(dr);
                }
            }
            if (aryLine != null && aryLine.Length > 0)
            {
                dt.DefaultView.Sort = tableHead[0] + " " + "asc";
            }

            sr.Close();
            fs.Close();
            return dt;
        }
    }
}
