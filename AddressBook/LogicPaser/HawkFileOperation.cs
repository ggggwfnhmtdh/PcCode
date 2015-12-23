using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;


namespace HawkFileOperation
{
    /// <summary>
    /// ??.hex??
    /// </summary>
    public class HawkClassHexFile
    {
        /// <summary>
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// </summary>
        /// 

        /// <summary>
        /// </summary>
        public UInt16 StartAdd = 0;

        /// <summary>
        /// </summary>
        public UInt16 EndAdd = 0xFFFF;

        /// <summary>
        /// </summary>
        public Int32 Length = 1024;

        /// <summary>
        /// </summary>
        public bool DebugFlag = false;

        /// <summary>
        /// </summary>
        public Int32 ReadCodeNum = 0;


        /// <summary>
        /// </summary>
        virtual public byte[] ReadFile()
        {
            byte[] DataArray = new byte[Length];
            FileStream DataPoint = new FileStream(SourceFilePathWithName, FileMode.Open);
            StreamReader DataStream = new StreamReader(DataPoint);
            string LineDataString = "";
            string LineCodeString = "";
            UInt16 LineAdd = 0;
            UInt16 LastLineAdd = 0;
            byte LineCodeLength;
            byte LineCheckSum;
            byte DataType;
            int i = 0;
            int LineNum = 0;
            bool CodeReadFinish = false;
            while (true) //???hex
            {
                if ((LineDataString = DataStream.ReadLine()) != null)
                {
                    if (LineDataString.Contains(":"))
                    {
                        LastLineAdd = LineAdd;
                        LineAdd = Convert.ToUInt16(LineDataString.Substring(3, 4), 16); ;
                        LineCodeLength = Convert.ToByte(LineDataString.Substring(1, 2), 16);
                        LineCheckSum = Convert.ToByte(LineDataString.Substring(LineDataString.Length - 2, 2), 16);
                        LineCodeString = LineDataString.Substring(9, 2 * LineCodeLength);
                        DataType = Convert.ToByte(LineDataString.Substring(7, 2), 16);


                        if (DebugFlag == true)
                        {
                            MessageBox.Show("LineAdd:" + LineAdd.ToString("X"));
                            MessageBox.Show("LineCodeLength:" + LineCodeLength.ToString("X"));
                            MessageBox.Show("LineCheckSum:" + LineCheckSum.ToString("X"));
                            MessageBox.Show(LineCodeString);
                            MessageBox.Show("LineNum:" + LineNum.ToString());
                            MessageBox.Show("DataType:" + DataType.ToString());
                            LineNum++;
                        }

                        if (LineAdd < LastLineAdd)
                        {
                            CodeReadFinish = true;
                        }

                        if (DataType == 0 && CodeReadFinish == false && (LineAdd >= StartAdd && LineAdd <= EndAdd))   //????????,?????????
                        {
                            ReadCodeNum = LineAdd + LineCodeLength - StartAdd;
                            for (i = 0; i < LineCodeLength; i++)
                            {
                                DataArray[LineAdd - StartAdd + i] = Convert.ToByte(LineCodeString.Substring(i * 2, 2), 16);
                                if (DebugFlag == true)
                                {
                                    MessageBox.Show("i:" + (LineAdd - StartAdd + i).ToString());
                                    MessageBox.Show(DataArray[LineAdd - StartAdd + i].ToString("X"));

                                }
                            }
                        }




                    }
                }
                else
                    break;
            }
            DataPoint.Close();
            DataStream.Close();
            return DataArray;

        }
    }
    /// <summary>
    /// ??.bin??
    /// </summary>
    public class HawkClassBinFile
    {
        /// <summary>
        /// ?????(?????)
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// ??????(?????)
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// ?????(????)
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// ??????(????)
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// ????
        /// </summary>
        public Int32 Length = 1024;

        /// <summary>
        /// ????
        /// </summary>
        virtual public byte[] ReadFile()
        {
            byte[] DataArray;
            FileStream BinStream = new FileStream(SourceFilePathWithName, FileMode.OpenOrCreate);
            BinaryReader BinReader = new BinaryReader(BinStream);
            BinReader.BaseStream.Seek(0, SeekOrigin.Begin);
            Length = Convert.ToInt32(BinReader.BaseStream.Length);
            DataArray = BinReader.ReadBytes(Length);
            BinReader.Close();
            BinStream.Close();
            return DataArray;
        }
        virtual public void WriteFile(byte[] DataArray)
        {
            FileStream BinStream = new FileStream(DestinationFilePathWithName, FileMode.OpenOrCreate);
            BinaryWriter BinWriter = new BinaryWriter(BinStream);
            BinWriter.Write(DataArray, 0, DataArray.Length);
            BinWriter.Close();
            BinStream.Close();
        }
    }
    /// <summary>
    /// ??.dat??
    /// </summary>
    public class HawkClassDatFile
    {
        /// <summary>
        /// ?????(?????)
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// ??????(?????)
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// ?????(????)
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// ??????(????)
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// ????
        /// </summary>
        virtual public void ReadFile()
        {
            MessageBox.Show(SourceFilePath);
        }
    }
    /// <summary>
    /// ??.txt??
    /// </summary>
    public class HawkClassTxtFile
    {
        /// <summary>
        /// ?????(?????)
        /// </summary>
        public string SourceFilePath;

        /// <summary>
        /// ??????(?????)
        /// </summary>
        public string DestinationFilePath;

        /// <summary>
        /// ?????(????)
        /// </summary>
        public string SourceFilePathWithName;

        /// <summary>
        /// ??????(????)
        /// </summary>
        public string DestinationFilePathWithName;

        /// <summary>
        /// ????
        /// </summary>
        virtual public void ReadFile()
        {
            MessageBox.Show(SourceFilePath);
        }


    }

    public class HawkClassOutPutFile
    {

        public int Raw = 10;
        public int Column = 10;
        public int Length = 10;
        public string IsolateString = " ";
        public bool IsContain0x = false;
        public string AlphabetFormat = "D";
        public bool OpenFileFlag = false;
        /// <summary>
        /// ??????(?????)
        /// </summary>
        public string DestinationFilePathName;

        /// <summary>
        /// Debug??
        /// </summary>
        public bool DebugFlag = false;


        /// <summary>
        /// ?????????
        /// </summary>
        /// 
        virtual public void OutFile(byte[] ArrayData)
        {
            int i = 0;
            string TempString = "";
            String FileType = Path.GetExtension(DestinationFilePathName);
            FileStream SavePoint = new FileStream(DestinationFilePathName, FileMode.Create);
            StreamWriter SaveStream = new StreamWriter(SavePoint);
            for (i = 0; i < Length; i++)
            {

                if (AlphabetFormat.ToLower() == "x" && IsContain0x == true)
                    TempString += "0x";
                TempString += ArrayData[i].ToString(AlphabetFormat);
                TempString += IsolateString;
                if ((i + 1) % Column == 0 || i == Length - 1)
                {
                    TempString += "\r";
                    SaveStream.WriteLine(TempString);
                    TempString = "";
                }

            }
            if (DebugFlag == true)
                MessageBox.Show(TempString);
            SaveStream.Close();
            SavePoint.Close();

            if (OpenFileFlag == true)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = DestinationFilePathName;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start(); //????
            }
        }

        virtual public void OutFile(ushort[] ArrayData)
        {
            int i = 0;
            String TempString = "";
            FileStream SavePoint = new FileStream(DestinationFilePathName, FileMode.Create);
            StreamWriter SaveStream = new StreamWriter(SavePoint);
            for (i = 0; i < Length; i++)
            {

                if (AlphabetFormat.ToLower() == "x" && IsContain0x == true)
                    TempString += "0x";
                TempString += ArrayData[i].ToString(AlphabetFormat);
                TempString += IsolateString;
                if ((i + 1) % Column == 0 || i == Length - 1)
                {
                    TempString += "\r";
                    SaveStream.WriteLine(TempString);
                    TempString = "";
                }

            }
            if (DebugFlag == true)
                MessageBox.Show(TempString);
            SaveStream.Close();
            SavePoint.Close();

            if (OpenFileFlag == true)
            {
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.FileName = DestinationFilePathName;
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                process.Start(); //????
            }
        }

    }
    public class HawkClassSaveFile
    {
        public string DefaultSavePatch = ".";
        public string DefaultSaveName = "Save.txt";
        public string LoadMemoryInformation(string ItemString)
        {
            int i = 0;
            string TempS = "";
            string NameString = "";
            string ContentString = "";
            if (File.Exists(DefaultSavePatch + "\\" + DefaultSaveName))
            {
                FileStream Temp = new FileStream(DefaultSavePatch + "\\" + DefaultSaveName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(Temp);
                string[] LineSring = sr.ReadToEnd().Split("\n".ToCharArray());
                sr.Close();
                Temp.Close();
                for (i = 0; i < LineSring.Length; i++)
                {
                    if (LineSring[i].Contains(":"))
                    {
                        NameString = LineSring[i].Trim().Substring(0, LineSring[i].IndexOf(":"));
                        ContentString = LineSring[i].Trim().Substring(LineSring[i].IndexOf(":") + 1, LineSring[i].IndexOf(";") - LineSring[i].IndexOf(":") - 1);

                        if (NameString.Trim() == ItemString.Trim())
                        {
                            return ContentString;
                        }


                    }
                }
                return null;

            }
            else
            {
                FileStream Temp = new FileStream(DefaultSavePatch + "\\" + DefaultSaveName, FileMode.Create, FileAccess.Write);
                StreamWriter sw = new StreamWriter(Temp);
                TempS = "DefaultSavePatch:" + DefaultSavePatch + ";";
                sw.WriteLine(TempS);//????? 
                TempS = "DefaultSaveName:" + DefaultSaveName + ";";
                sw.WriteLine(TempS);//????? 

                sw.Close();
                Temp.Close();
                return null;
            }
        }

        public void SaveInformation(string ItemString, string SaveString)
        {
            int i = 0;
            string NameString = "";
            string ContentString = "";
            bool IsContainFlag = false;

            if (File.Exists(DefaultSavePatch + "\\" + DefaultSaveName))
            {
                FileStream Temp = new FileStream(DefaultSavePatch + "\\" + DefaultSaveName, FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(Temp);
                string[] LineSring = sr.ReadToEnd().Split("\n".ToCharArray());
                sr.Close();
                Temp.Close();
                for (i = 0; i < LineSring.Length; i++)
                {
                    if (LineSring[i].Contains(":"))
                    {
                        NameString = LineSring[i].Trim().Substring(0, LineSring[i].IndexOf(":"));
                        ContentString = LineSring[i].Trim().Substring(LineSring[i].IndexOf(":") + 1, LineSring[i].IndexOf(";") - LineSring[i].IndexOf(":") - 1);
                        if (ItemString.Trim() == NameString.Trim())
                        {
                            if (SaveString != string.Empty)
                                LineSring[i] = LineSring[i].Replace(":" + ContentString, ":" + SaveString);

                            else
                                LineSring[i] = string.Empty;
                            IsContainFlag = true;
                        }
                    }

                }

                if (IsContainFlag == true)
                {
                    FileStream Temp1 = new FileStream(DefaultSavePatch + "\\" + DefaultSaveName, FileMode.Create, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(Temp1);
                    for (i = 0; i < LineSring.Length; i++)
                    {
                        if (LineSring[i].Contains(":"))
                            sw.WriteLine(LineSring[i]);
                    }
                    sw.Close();
                    Temp1.Close();
                }
                else
                {
                    FileStream Temp2 = new FileStream(DefaultSavePatch + "\\" + DefaultSaveName, FileMode.Append, FileAccess.Write);
                    StreamWriter sw = new StreamWriter(Temp2);
                    sw.WriteLine(ItemString + ":" + SaveString + ";");
                    sw.Close();
                    Temp2.Close();

                }
            }
        }
    }
}