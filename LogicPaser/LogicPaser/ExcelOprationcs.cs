using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel= Microsoft.Office.Interop.Excel;
using System.Diagnostics;
namespace LogicPaser
{
    public class ExcelHelper
    {
        private Excel._Application excelApp;
        private string fileName = string.Empty;
        private Excel.WorkbookClass wbclass;
        public ExcelHelper(string _filename)
        {
            excelApp = new Excel.Application();
            object objOpt = System.Reflection.Missing.Value;
            wbclass = (Excel.WorkbookClass)excelApp.Workbooks.Open(_filename, objOpt, false, objOpt, objOpt, objOpt, true, objOpt, objOpt, true, objOpt, objOpt, objOpt, objOpt, objOpt);
        }
        /// <summary>
        /// ����sheet�������б�
        /// </summary>
        /// <returns></returns>
        public List<string> GetSheetNames()
        {
            List<string> list = new List<string>();
            Excel.Sheets sheets = wbclass.Worksheets;
            string sheetNams = string.Empty;
            foreach (Excel.Worksheet sheet in sheets)
            {
                list.Add(sheet.Name);
            }
            return list;
        }
        public Excel.Worksheet GetWorksheetByName(string name)
        {
            Excel.Worksheet sheet = null;
            Excel.Sheets sheets = wbclass.Worksheets;
            foreach (Excel.Worksheet s in sheets)
            {
                if (s.Name == name)
                {
                    sheet = s;
                    break;
                }
            }
            return sheet;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sheetName">sheet����</param>
        /// <returns></returns>
        public Array GetContent(string sheetName)
        {
            Excel.Worksheet sheet = GetWorksheetByName(sheetName);
            //��ȡA1 ��AM24��Χ�ĵ�Ԫ��
            Excel.Range rang = sheet.get_Range("A1", "AM24");
            //��һ����Ԫ������
            //sheet.get_Range("A1", Type.Missing);
            //��Ϊ�յ�����,��,����Ŀ
            //   int l = sheet.UsedRange.Columns.Count;
            // int w = sheet.UsedRange.Rows.Count;
            //  object[,] dell = sheet.UsedRange.get_Value(Missing.Value) as object[,];
            System.Array values = (Array)rang.Cells.Value2;
            return values;
        }

        public void Close()
        {
            excelApp.Quit();
            excelApp = null;
        }

    }
}
