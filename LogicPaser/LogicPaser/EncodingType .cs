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
    class EncodingType
    {
        /// <summary> 
        /// �����ļ���·������ȡ�ļ��Ķ��������ݣ��ж��ļ��ı������� 
        /// </summary> 
        /// <param name="FILE_NAME">�ļ�·��</param> 
        /// <returns>�ļ��ı�������</returns> 
        public static System.Text.Encoding GetType(string FILE_NAME)
        {
            FileStream fs = new FileStream(FILE_NAME, FileMode.Open, FileAccess.Read);
            Encoding r = GetType(fs);
            fs.Close();
            return r;
        }

        /// <summary> 
        /// ͨ���������ļ������ж��ļ��ı������� 
        /// </summary> 
        /// <param name="fs">�ļ���</param> 
        /// <returns>�ļ��ı�������</returns> 
        public static System.Text.Encoding GetType(FileStream fs)
        {
            byte[] Unicode = new byte[] { 0xFF, 0xFE, 0x41 };
            byte[] UnicodeBIG = new byte[] { 0xFE, 0xFF, 0x00 };
            byte[] UTF8 = new byte[] { 0xEF, 0xBB, 0xBF }; //��BOM 
            Encoding reVal = Encoding.Default;

            BinaryReader r = new BinaryReader(fs, System.Text.Encoding.Default);
            int i;
            int.TryParse(fs.Length.ToString(), out i);
            byte[] ss = r.ReadBytes(i);
            if (IsUTF8Bytes(ss) || (ss[0] == 0xEF && ss[1] == 0xBB && ss[2] == 0xBF))
            {
                reVal = Encoding.UTF8;
            }
            else if (ss[0] == 0xFE && ss[1] == 0xFF && ss[2] == 0x00)
            {
                reVal = Encoding.BigEndianUnicode;
            }
            else if (ss[0] == 0xFF && ss[1] == 0xFE && ss[2] == 0x41)
            {
                reVal = Encoding.Unicode;
            }
            r.Close();
            return reVal;

        }

        /// <summary> 
        /// �ж��Ƿ��ǲ��� BOM �� UTF8 ��ʽ 
        /// </summary> 
        /// <param name="data"></param> 
        /// <returns></returns> 
        private static bool IsUTF8Bytes(byte[] data)
        {
            int charByteCounter = 1;
            //���㵱ǰ���������ַ�Ӧ���е��ֽ��� 
            byte curByte; //��ǰ�������ֽ�. 
            for (int i = 0; i < data.Length; i++)
            {
                curByte = data[i];
                if (charByteCounter == 1)
                {
                    if (curByte >= 0x80)
                    {
                        //�жϵ�ǰ 
                        while (((curByte <<= 1) & 0x80) != 0)
                        {
                            charByteCounter++;
                        }
                        //���λ��λ��Ϊ��0 ��������2��1��ʼ ��:110XXXXX...........1111110X�� 
                        if (charByteCounter == 1 || charByteCounter > 6)
                        {
                            return false;
                        }
                    }
                }
                else
                {
                    //����UTF-8 ��ʱ��һλ����Ϊ1 
                    if ((curByte & 0xC0) != 0x80)
                    {
                        return false;
                    }
                    charByteCounter--;
                }
            }
            if (charByteCounter > 1)
            {
                throw new Exception("��Ԥ�ڵ�byte��ʽ");
            }
            return true;
        }
    } 
}
