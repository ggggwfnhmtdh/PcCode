using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using CommunicateSpace;
using System.Threading;
using System.Windows.Forms.DataVisualization.Charting;
using HawkMathSpace;
namespace DataProSpace
{
    public class DataProClass : Communicate
    {
        public ushort[] RowData = new ushort[Communicate.PixelRow];
        public ushort[] ColData = new ushort[Communicate.PixelCol];
        public RichTextBox MsgBox;
        public Chart MsgChart;
        public SeriesChartType ChartType = SeriesChartType.FastLine;
        public static int  BUFF_MAX = 10;
        public static List<ushort[]> RawDataBuff = new List<ushort[]>();
        public static int ThreadSig = 0;

        public static int ViewDataAvr = 0;

        public static void LockThread()
        {
            object lockThis = new object();
            lock (lockThis)
            {
                while (ThreadSig == 1) ;
                ThreadSig = 1;
            }
        }

        public static void UnLockThread()
        {
            object lockThis = new object();
            lock (lockThis)
            {
                ThreadSig = 0;
            }
        }

        public static void AppendDataBuff(ushort[] buf)
        {
            LockThread();
            if (RawDataBuff.Count >= BUFF_MAX)
                RawDataBuff.RemoveAt(0);
            RawDataBuff.Add(buf);
            UnLockThread();
        }
        public static void ClearDataBuff()
        {
            LockThread();
            RawDataBuff.Clear();
            UnLockThread();
        }

        public static bool ReadDataBuff(ref ushort[] buf)
        {
            int OverTime = 20;
            LockThread();
            while (RawDataBuff.Count == 0 && OverTime!=0)
            {
                OverTime--;
                Thread.Sleep(10);
            }
            if (OverTime != 0)
            {
                Array.Copy(RawDataBuff[0], 0, buf, 0, buf.Length);
                RawDataBuff.RemoveAt(0);
                UnLockThread();
                return true;
            }
            else
            {
                UnLockThread();
                return false;
            }
        }

        public static bool GetViewData(ref ushort[] buf)
        {
            int OverTime = 20;
            while (!m_hViewUpdate.WaitOne(10));
            m_hViewUpdate.Reset();
            LockThread();
            while (RawDataBuff.Count == 0 && OverTime != 0)
            {
                OverTime--;
                Thread.Sleep(10);
            }
            if (OverTime != 0)
            {                
                Array.Copy(RawDataBuff[0], 0, buf, 0, buf.Length);
                ViewDataAvr = (int)HawkMath.Mean(buf);
                UnLockThread();
                return true;
            }
            else
            {
                UnLockThread();
                return false;
            }
        }

        public void ClearMsgBox()
        {
            MsgBox.Text = "";
        }
        public void ShowMsgBox<T>(T Msg)
        {
            String TempS = "";
            String Type = typeof(T).ToString();
            if (Msg is String)
                TempS = Convert.ToString(Msg);
            else if (Msg is float||Msg is double)
                TempS = Convert.ToDouble(Msg).ToString();
            else
                TempS = Convert.ToInt32(Msg).ToString();
            MsgBox.AppendText(TempS + "\r\n");
            MsgBox.HideSelection = false;
        }

        public void ShowMsgBox<T>(T[] Msg)
        {
            String TempS = "";
            String Format = "D2";
            int i;
            if (Msg is String[])
            {
                for (i = 0; i < Msg.Length; i++)
                    TempS += Convert.ToString(Msg[i]) + " ";
            }
            else if (Msg is float[] || Msg is double[])
            {
                for (i = 0; i < Msg.Length; i++)
                    TempS += Convert.ToDouble(Msg[i]).ToString(Format) + " ";
            }
            else
            {
                for (i = 0; i < Msg.Length; i++)
                    TempS += Convert.ToInt32(Msg[i]).ToString(Format) + " ";
            }
            MsgBox.AppendText(TempS + "\r\n");
            MsgBox.HideSelection = false;
        }


        public void ShowMsgBox<T>(T Msg,String Format = "D2")
        {
            String TempS = "";
            String Type = typeof(T).ToString();
            if (Msg is String)
                TempS = Convert.ToString(Msg);
            else if (Msg is float || Msg is double)
                TempS = Convert.ToDouble(Msg).ToString(Format);
            else
                TempS = Convert.ToInt32(Msg).ToString(Format);
            MsgBox.AppendText(TempS + "\r\n");
            MsgBox.HideSelection = false;
        }


        public void ShowMsgBox<T>(T[] Msg, String Format = "X2")
        {
            String TempS = "";
            int i;
            if (Msg is String[])
            {
                for(i=0;i<Msg.Length;i++)
                    TempS += Convert.ToString(Msg[i])+" ";
            }
            else if (Msg is float[] || Msg is double[])
            {
                for (i = 0; i < Msg.Length; i++)
                    TempS += Convert.ToDouble(Msg[i]).ToString(Format) + " ";
            }
            else
            {
                for (i = 0; i < Msg.Length; i++)
                    TempS += Convert.ToInt32(Msg[i]).ToString(Format) + " ";
            }
            MsgBox.AppendText(TempS + "\r\n");
            MsgBox.HideSelection = false;
        }

        public void ShowMsgChart<T>(T[] Data)
        {
            int i;
            double[] X = new double[Data.Length];
            double[] Y = new double[Data.Length];
            MsgChart.Series["Series1"].ChartType = ChartType;
            for (i = 0; i < Data.Length; i++)
            {
                X[i] = (double)i;
                Y[i] = Convert.ToDouble(Data[i]);
            }
            MsgChart.ChartAreas[0].AxisY.LabelStyle.Format = "D";
            MsgChart.ChartAreas[0].AxisX.LabelStyle.Format = "D";
            MsgChart.ChartAreas[0].AxisY.Minimum = 0;
            MsgChart.ChartAreas[0].AxisY.Maximum = 4096;
            MsgChart.ChartAreas[0].AxisX.Minimum = 0;
            MsgChart.ChartAreas[0].AxisX.Maximum = Data.Length;
            MsgChart.Series["Series1"].Points.DataBindXY(X, Y);
        }

    }
}
