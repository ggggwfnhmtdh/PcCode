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
namespace LogicPaser
{
    class LogicPaserBase
    {
        public int ShowLevel = 0;
        public string SaveDataPath = "LogicData.txt";
    }
}
