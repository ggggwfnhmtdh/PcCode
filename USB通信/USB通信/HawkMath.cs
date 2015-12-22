using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HawkMathSpace
{
    static class HawkMath
    {
        /// <summary>
        /// 计算算数平均数:（x1+x2+...+xn）/n
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>算术平均数</returns>
        public static double Mean<T>(T[] InData)
        {
            double result = 0;          
                if (InData is float[] || InData is double[])
                {
                    foreach (T num in InData)
                    {
                        result += Convert.ToDouble(num);
                    }
                }
                else
                {
                    foreach (T num in InData)
                    {
                        result += Convert.ToInt32(num);
                    }
                }

                return result / InData.Length;
        }

        public static double GetStatistic<T>(T[] InData)
        {
            double result = 0;

            if (InData is float[] || InData is double[])
            {
                foreach (T num in InData)
                {
                    result += Convert.ToDouble(num);
                }
            }
            else
            {
                foreach (T num in InData)
                {
                    result += Convert.ToInt32(num);
                }
            }

            return result / InData.Length;
        }


        public static ushort[] GetDacAvrRaw(ushort[] RawData, int Row, int Col)
        {
            int i, j;
            long[] Sum = new long[4];
            ushort[] Avr = new ushort[4];
            Array.Clear(Sum, 0, Sum.Length);
            for (i = 0; i < Row; i++)
            {
                for (j = 0; j < Col; j++)
                    Sum[i % 4] += RawData[i * Col + j];
            }
            Avr[0] = (ushort)(Sum[0] * 4 / (Row * Col));
            Avr[1] = (ushort)(Sum[1] * 4 / (Row * Col));
            Avr[2] = (ushort)(Sum[2] * 4 / (Row * Col));
            Avr[3] = (ushort)(Sum[3] * 4 / (Row * Col));
            return Avr;
        }


        /// <summary>
        /// 几何平均数：(x1*x2*...*xn)^(1/n)
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>几何平均数</returns>
        public static double GeometricMean(double[] arr)
        {
            double result = 1;
            foreach (double num in arr)
            {
                result *= Math.Pow(num, 1.0 / arr.Length);
            }
            return result;
        }

        /// <summary>
        /// 调和平均数：n/((1/x1)+(1/x2)+...+(1/xn))
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns>调和平均数</returns>
        public static double HarmonicMean(double[] arr)
        {
            double temp = 0;
            foreach (double num in arr)
            {
                temp += (1.0 / num);
            }
            return arr.Length / temp;
        }

        /// <summary>
        /// 计算中位数
        /// </summary>
        /// <param name="arr">数组</param>
        /// <returns></returns>
        public static double Median(double[] arr)
        {
            //为了不修改arr值，对数组的计算和修改在tempArr数组中进行
            double[] tempArr = new double[arr.Length];
            arr.CopyTo(tempArr, 0);

            //对数组进行排序
            double temp;
            for (int i = 0; i < tempArr.Length; i++)
            {
                for (int j = i; j < tempArr.Length; j++)
                {
                    if (tempArr[i] > tempArr[j])
                    {
                        temp = tempArr[i];
                        tempArr[i] = tempArr[j];
                        tempArr[j] = temp;
                    }
                }
            }

            //针对数组元素的奇偶分类讨论
            if (tempArr.Length % 2 != 0)
            {
                return tempArr[arr.Length / 2 + 1];
            }
            else
            {
                return (tempArr[tempArr.Length / 2] +
                    tempArr[tempArr.Length / 2 + 1]) / 2.0;
            }
        }



    }
}
