namespace UsbComTools
{
    partial class MainForm
    {
        /// <summary>
        /// ?????????
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// ????????????
        /// </summary>
        /// <param name="disposing">?????????,? true;??? false?</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows ??????????

        /// <summary>
        /// ?????????? - ??
        /// ????????????????
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend1 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            DevExpress.XtraCharts.XYDiagram3D xyDiagram3D1 = new DevExpress.XtraCharts.XYDiagram3D();
            DevExpress.XtraCharts.Series series1 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ManhattanBarSeriesView manhattanBarSeriesView1 = new DevExpress.XtraCharts.ManhattanBarSeriesView();
            DevExpress.XtraCharts.Series series2 = new DevExpress.XtraCharts.Series();
            DevExpress.XtraCharts.ManhattanBarSeriesView manhattanBarSeriesView2 = new DevExpress.XtraCharts.ManhattanBarSeriesView();
            DevExpress.XtraCharts.ManhattanBarSeriesView manhattanBarSeriesView3 = new DevExpress.XtraCharts.ManhattanBarSeriesView();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.TreeMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disConnectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.anasisToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.ComTest = new System.Windows.Forms.CheckBox();
            this.Debug = new System.Windows.Forms.RichTextBox();
            this.CbSaveData = new System.Windows.Forms.CheckBox();
            this.CmdBox = new System.Windows.Forms.TextBox();
            this.DeviceTree = new System.Windows.Forms.TreeView();
            this.Start = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.ChartEnable = new System.Windows.Forms.CheckBox();
            this.DataChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.button4 = new System.Windows.Forms.Button();
            this.DataView = new System.Windows.Forms.DataGridView();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.button6 = new System.Windows.Forms.Button();
            this.SavePath = new System.Windows.Forms.TextBox();
            this.button5 = new System.Windows.Forms.Button();
            this.CalcFwPath = new System.Windows.Forms.TextBox();
            this.button3 = new System.Windows.Forms.Button();
            this.ConfigPath = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.PatchPath = new System.Windows.Forms.TextBox();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.toolStripProgressBar1 = new System.Windows.Forms.ToolStripProgressBar();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.toolStripStatusLabel1 = new System.Windows.Forms.ToolStripStatusLabel();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.editToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.calibDacToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.chartControl1 = new DevExpress.XtraCharts.ChartControl();
            this.TreeMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataChart)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).BeginInit();
            this.tabPage3.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.MainMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3D1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView3)).BeginInit();
            this.SuspendLayout();
            // 
            // TreeMenu
            // 
            this.TreeMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ConnectToolStripMenuItem,
            this.disConnectToolStripMenuItem});
            this.TreeMenu.Name = "TreeMenu";
            this.TreeMenu.Size = new System.Drawing.Size(166, 48);
            // 
            // ConnectToolStripMenuItem
            // 
            this.ConnectToolStripMenuItem.Name = "ConnectToolStripMenuItem";
            this.ConnectToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.ConnectToolStripMenuItem.Text = "Connect Device";
            this.ConnectToolStripMenuItem.Click += new System.EventHandler(this.ConnectToolStripMenuItem_Click);
            // 
            // disConnectToolStripMenuItem
            // 
            this.disConnectToolStripMenuItem.Name = "disConnectToolStripMenuItem";
            this.disConnectToolStripMenuItem.Size = new System.Drawing.Size(165, 22);
            this.disConnectToolStripMenuItem.Text = "DisConnect";
            this.disConnectToolStripMenuItem.Click += new System.EventHandler(this.disConnectToolStripMenuItem_Click);
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(39, 21);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // editToolStripMenuItem
            // 
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            this.editToolStripMenuItem.Size = new System.Drawing.Size(42, 21);
            this.editToolStripMenuItem.Text = "Edit";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.viewToolStripMenuItem.Text = "View";
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(52, 21);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // debugToolStripMenuItem
            // 
            this.debugToolStripMenuItem.Name = "debugToolStripMenuItem";
            this.debugToolStripMenuItem.Size = new System.Drawing.Size(59, 21);
            this.debugToolStripMenuItem.Text = "Debug";
            // 
            // anasisToolStripMenuItem
            // 
            this.anasisToolStripMenuItem.Name = "anasisToolStripMenuItem";
            this.anasisToolStripMenuItem.Size = new System.Drawing.Size(66, 21);
            this.anasisToolStripMenuItem.Text = "Analysis";
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Font = new System.Drawing.Font("Cambria", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.Location = new System.Drawing.Point(2, 30);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(967, 530);
            this.tabControl1.TabIndex = 6;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.ComTest);
            this.tabPage1.Controls.Add(this.Debug);
            this.tabPage1.Controls.Add(this.CbSaveData);
            this.tabPage1.Controls.Add(this.CmdBox);
            this.tabPage1.Controls.Add(this.DeviceTree);
            this.tabPage1.Controls.Add(this.Start);
            this.tabPage1.Location = new System.Drawing.Point(4, 23);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(959, 503);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Main";
            this.tabPage1.UseVisualStyleBackColor = true;
            this.tabPage1.Click += new System.EventHandler(this.tabPage1_Click);
            // 
            // ComTest
            // 
            this.ComTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ComTest.AutoSize = true;
            this.ComTest.Location = new System.Drawing.Point(804, 7);
            this.ComTest.Name = "ComTest";
            this.ComTest.Size = new System.Drawing.Size(74, 18);
            this.ComTest.TabIndex = 13;
            this.ComTest.Text = "ComTest";
            this.ComTest.UseVisualStyleBackColor = true;
            // 
            // Debug
            // 
            this.Debug.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.Debug.Font = new System.Drawing.Font("SimHei", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.Debug.ForeColor = System.Drawing.Color.Olive;
            this.Debug.Location = new System.Drawing.Point(151, 6);
            this.Debug.Name = "Debug";
            this.Debug.Size = new System.Drawing.Size(636, 359);
            this.Debug.TabIndex = 12;
            this.Debug.Text = "";
            this.Debug.DoubleClick += new System.EventHandler(this.Debug_DoubleClick);
            // 
            // CbSaveData
            // 
            this.CbSaveData.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.CbSaveData.AutoSize = true;
            this.CbSaveData.Location = new System.Drawing.Point(812, 398);
            this.CbSaveData.Name = "CbSaveData";
            this.CbSaveData.Size = new System.Drawing.Size(74, 18);
            this.CbSaveData.TabIndex = 11;
            this.CbSaveData.Text = "SaveData";
            this.CbSaveData.UseVisualStyleBackColor = true;
            // 
            // CmdBox
            // 
            this.CmdBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.CmdBox.Location = new System.Drawing.Point(151, 371);
            this.CmdBox.Multiline = true;
            this.CmdBox.Name = "CmdBox";
            this.CmdBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.CmdBox.Size = new System.Drawing.Size(636, 126);
            this.CmdBox.TabIndex = 9;
            // 
            // DeviceTree
            // 
            this.DeviceTree.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.DeviceTree.ContextMenuStrip = this.TreeMenu;
            this.DeviceTree.Location = new System.Drawing.Point(6, 6);
            this.DeviceTree.Name = "DeviceTree";
            this.DeviceTree.Size = new System.Drawing.Size(139, 359);
            this.DeviceTree.TabIndex = 8;
            this.DeviceTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.DeviceTree_AfterSelect);
            // 
            // Start
            // 
            this.Start.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.Start.Font = new System.Drawing.Font("Microsoft Sans Serif", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start.Location = new System.Drawing.Point(804, 422);
            this.Start.Name = "Start";
            this.Start.Size = new System.Drawing.Size(147, 67);
            this.Start.TabIndex = 6;
            this.Start.Text = "Start";
            this.Start.UseVisualStyleBackColor = true;
            this.Start.Click += new System.EventHandler(this.button1_Click_1);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.chartControl1);
            this.tabPage2.Controls.Add(this.ChartEnable);
            this.tabPage2.Controls.Add(this.DataChart);
            this.tabPage2.Controls.Add(this.button4);
            this.tabPage2.Controls.Add(this.DataView);
            this.tabPage2.Location = new System.Drawing.Point(4, 23);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(959, 503);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ChartEnable
            // 
            this.ChartEnable.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ChartEnable.AutoSize = true;
            this.ChartEnable.Location = new System.Drawing.Point(574, 302);
            this.ChartEnable.Name = "ChartEnable";
            this.ChartEnable.Size = new System.Drawing.Size(93, 18);
            this.ChartEnable.TabIndex = 12;
            this.ChartEnable.Text = "ChartEnable";
            this.ChartEnable.UseVisualStyleBackColor = true;
            // 
            // DataChart
            // 
            this.DataChart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            chartArea1.Name = "ChartArea1";
            this.DataChart.ChartAreas.Add(chartArea1);
            legend1.Enabled = false;
            legend1.Name = "Legend1";
            this.DataChart.Legends.Add(legend1);
            this.DataChart.Location = new System.Drawing.Point(535, 6);
            this.DataChart.Name = "DataChart";
            this.DataChart.RightToLeft = System.Windows.Forms.RightToLeft.No;
            series3.ChartArea = "ChartArea1";
            series3.Legend = "Legend1";
            series3.Name = "Series1";
            this.DataChart.Series.Add(series3);
            this.DataChart.Size = new System.Drawing.Size(395, 299);
            this.DataChart.TabIndex = 11;
            this.DataChart.Text = "chart1";
            // 
            // button4
            // 
            this.button4.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.button4.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button4.Location = new System.Drawing.Point(806, 391);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(140, 88);
            this.button4.TabIndex = 7;
            this.button4.Text = "Debug";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // DataView
            // 
            this.DataView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DataView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DataView.Location = new System.Drawing.Point(6, 6);
            this.DataView.Name = "DataView";
            this.DataView.RowTemplate.Height = 23;
            this.DataView.Size = new System.Drawing.Size(245, 127);
            this.DataView.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.button6);
            this.tabPage3.Controls.Add(this.SavePath);
            this.tabPage3.Controls.Add(this.button5);
            this.tabPage3.Controls.Add(this.CalcFwPath);
            this.tabPage3.Controls.Add(this.button3);
            this.tabPage3.Controls.Add(this.ConfigPath);
            this.tabPage3.Controls.Add(this.button2);
            this.tabPage3.Controls.Add(this.PatchPath);
            this.tabPage3.Location = new System.Drawing.Point(4, 23);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(959, 503);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Config";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // button6
            // 
            this.button6.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button6.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button6.Location = new System.Drawing.Point(451, 107);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(87, 24);
            this.button6.TabIndex = 7;
            this.button6.Text = "SavePath";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // SavePath
            // 
            this.SavePath.Location = new System.Drawing.Point(22, 107);
            this.SavePath.Name = "SavePath";
            this.SavePath.Size = new System.Drawing.Size(428, 22);
            this.SavePath.TabIndex = 6;
            // 
            // button5
            // 
            this.button5.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button5.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button5.Location = new System.Drawing.Point(452, 78);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(87, 24);
            this.button5.TabIndex = 5;
            this.button5.Text = "DacCalibCode";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // CalcFwPath
            // 
            this.CalcFwPath.Location = new System.Drawing.Point(22, 78);
            this.CalcFwPath.Name = "CalcFwPath";
            this.CalcFwPath.Size = new System.Drawing.Size(428, 22);
            this.CalcFwPath.TabIndex = 4;
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button3.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button3.Location = new System.Drawing.Point(452, 48);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(87, 24);
            this.button3.TabIndex = 3;
            this.button3.Text = "Config";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // ConfigPath
            // 
            this.ConfigPath.Location = new System.Drawing.Point(22, 49);
            this.ConfigPath.Name = "ConfigPath";
            this.ConfigPath.Size = new System.Drawing.Size(428, 22);
            this.ConfigPath.TabIndex = 2;
            // 
            // button2
            // 
            this.button2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.button2.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.button2.Location = new System.Drawing.Point(451, 19);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(87, 24);
            this.button2.TabIndex = 1;
            this.button2.Text = "PatchCode";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // PatchPath
            // 
            this.PatchPath.Location = new System.Drawing.Point(22, 20);
            this.PatchPath.Name = "PatchPath";
            this.PatchPath.Size = new System.Drawing.Size(428, 22);
            this.PatchPath.TabIndex = 0;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 200;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.statusStrip1.AutoSize = false;
            this.statusStrip1.Dock = System.Windows.Forms.DockStyle.None;
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripProgressBar1,
            this.toolStripDropDownButton1,
            this.toolStripStatusLabel1});
            this.statusStrip1.Location = new System.Drawing.Point(0, 561);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(952, 26);
            this.statusStrip1.TabIndex = 7;
            this.statusStrip1.Text = "statusStrip1";
            this.statusStrip1.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this.statusStrip1_ItemClicked);
            // 
            // toolStripProgressBar1
            // 
            this.toolStripProgressBar1.AutoSize = false;
            this.toolStripProgressBar1.Margin = new System.Windows.Forms.Padding(2, 3, 1, 3);
            this.toolStripProgressBar1.Name = "toolStripProgressBar1";
            this.toolStripProgressBar1.Size = new System.Drawing.Size(700, 20);
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.toolStripDropDownButton1.Image = global::UsbComTools.Properties.Resources.Disconnect;
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(29, 24);
            this.toolStripDropDownButton1.Text = "toolStripDropDownButton1";
            // 
            // toolStripStatusLabel1
            // 
            this.toolStripStatusLabel1.Name = "toolStripStatusLabel1";
            this.toolStripStatusLabel1.Size = new System.Drawing.Size(205, 21);
            this.toolStripStatusLabel1.Spring = true;
            this.toolStripStatusLabel1.Text = "100Kb/s";
            // 
            // MainMenu
            // 
            this.MainMenu.Font = new System.Drawing.Font("Cambria", 10.5F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem1,
            this.editToolStripMenuItem1,
            this.viewToolStripMenuItem1,
            this.toolsToolStripMenuItem1,
            this.optionToolStripMenuItem,
            this.helpToolStripMenuItem1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MainMenu.Size = new System.Drawing.Size(969, 24);
            this.MainMenu.TabIndex = 8;
            this.MainMenu.Text = "MainMenu";
            // 
            // fileToolStripMenuItem1
            // 
            this.fileToolStripMenuItem1.Name = "fileToolStripMenuItem1";
            this.fileToolStripMenuItem1.Size = new System.Drawing.Size(43, 20);
            this.fileToolStripMenuItem1.Text = "File";
            // 
            // editToolStripMenuItem1
            // 
            this.editToolStripMenuItem1.Name = "editToolStripMenuItem1";
            this.editToolStripMenuItem1.Size = new System.Drawing.Size(45, 20);
            this.editToolStripMenuItem1.Text = "Edit";
            // 
            // viewToolStripMenuItem1
            // 
            this.viewToolStripMenuItem1.Name = "viewToolStripMenuItem1";
            this.viewToolStripMenuItem1.Size = new System.Drawing.Size(51, 20);
            this.viewToolStripMenuItem1.Text = "View";
            // 
            // toolsToolStripMenuItem1
            // 
            this.toolsToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.calibDacToolStripMenuItem});
            this.toolsToolStripMenuItem1.Name = "toolsToolStripMenuItem1";
            this.toolsToolStripMenuItem1.Size = new System.Drawing.Size(54, 20);
            this.toolsToolStripMenuItem1.Text = "Tools";
            // 
            // calibDacToolStripMenuItem
            // 
            this.calibDacToolStripMenuItem.Name = "calibDacToolStripMenuItem";
            this.calibDacToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.calibDacToolStripMenuItem.Text = "CalibDac";
            this.calibDacToolStripMenuItem.Click += new System.EventHandler(this.calibDacToolStripMenuItem_Click);
            // 
            // optionToolStripMenuItem
            // 
            this.optionToolStripMenuItem.Name = "optionToolStripMenuItem";
            this.optionToolStripMenuItem.Size = new System.Drawing.Size(63, 20);
            this.optionToolStripMenuItem.Text = "Option";
            // 
            // helpToolStripMenuItem1
            // 
            this.helpToolStripMenuItem1.Name = "helpToolStripMenuItem1";
            this.helpToolStripMenuItem1.Size = new System.Drawing.Size(49, 20);
            this.helpToolStripMenuItem1.Text = "Help";
            // 
            // chartControl1
            // 
            this.chartControl1.Cursor = System.Windows.Forms.Cursors.Default;
            xyDiagram3D1.RotationMatrixSerializable = "0.766044443118978;-0.219846310392954;0.604022773555054;0;0;0.939692620785908;0.34" +
    "2020143325669;0;-0.642787609686539;-0.262002630229385;0.719846310392954;0;0;0;0;" +
    "1";
            this.chartControl1.Diagram = xyDiagram3D1;
            this.chartControl1.Location = new System.Drawing.Point(53, 202);
            this.chartControl1.Name = "chartControl1";
            series1.Name = "Series 1";
            series1.View = manhattanBarSeriesView1;
            series2.Name = "Series 2";
            series2.View = manhattanBarSeriesView2;
            this.chartControl1.SeriesSerializable = new DevExpress.XtraCharts.Series[] {
        series1,
        series2};
            this.chartControl1.SeriesTemplate.View = manhattanBarSeriesView3;
            this.chartControl1.Size = new System.Drawing.Size(300, 200);
            this.chartControl1.TabIndex = 13;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(969, 587);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.tabControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(400, 300);
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Usb Com 1.0 ";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.SizeChanged += new System.EventHandler(this.MainForm_SizeChanged);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.TreeMenu.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.DataChart)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.DataView)).EndInit();
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(xyDiagram3D1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(series2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(manhattanBarSeriesView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chartControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem anasisToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ContextMenuStrip TreeMenu;
        private System.Windows.Forms.ToolStripMenuItem ConnectToolStripMenuItem;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TreeView DeviceTree;
        private System.Windows.Forms.Button Start;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ToolStripMenuItem disConnectToolStripMenuItem;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripProgressBar toolStripProgressBar1;
        private System.Windows.Forms.ToolStripDropDownButton toolStripDropDownButton1;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel1;
        private System.Windows.Forms.TextBox CmdBox;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem optionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem1;
        private System.Windows.Forms.DataGridView DataView;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox ConfigPath;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.TextBox PatchPath;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox CalcFwPath;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.TextBox SavePath;
        private System.Windows.Forms.CheckBox CbSaveData;
        private System.Windows.Forms.ToolStripMenuItem calibDacToolStripMenuItem;
        private System.Windows.Forms.DataVisualization.Charting.Chart DataChart;
        private System.Windows.Forms.RichTextBox Debug;
        private System.Windows.Forms.CheckBox ComTest;
        private System.Windows.Forms.CheckBox ChartEnable;
        private DevExpress.XtraCharts.ChartControl chartControl1;
    }
}

