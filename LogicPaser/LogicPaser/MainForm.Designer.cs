namespace LogicPaser
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.StartBtn = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.ShowBox = new System.Windows.Forms.RichTextBox();
            this.ChipComBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.eDITToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tOOLSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.oPTIONToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.hELPToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.ShowSpeedComBox = new System.Windows.Forms.ComboBox();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // StartBtn
            // 
            this.StartBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.StartBtn.BackColor = System.Drawing.Color.Red;
            this.StartBtn.Font = new System.Drawing.Font("Microsoft YaHei", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.StartBtn.Location = new System.Drawing.Point(806, 564);
            this.StartBtn.Name = "StartBtn";
            this.StartBtn.Size = new System.Drawing.Size(160, 56);
            this.StartBtn.TabIndex = 12;
            this.StartBtn.Text = "LoadData";
            this.StartBtn.UseVisualStyleBackColor = false;
            this.StartBtn.Click += new System.EventHandler(this.button3_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(3, 626);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(962, 23);
            this.progressBar1.TabIndex = 14;
            // 
            // ShowBox
            // 
            this.ShowBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ShowBox.Location = new System.Drawing.Point(3, 32);
            this.ShowBox.Name = "ShowBox";
            this.ShowBox.Size = new System.Drawing.Size(789, 588);
            this.ShowBox.TabIndex = 15;
            this.ShowBox.Text = "";
            this.ShowBox.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.ShowBox_MouseDoubleClick);
            // 
            // ChipComBox
            // 
            this.ChipComBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ChipComBox.FormattingEnabled = true;
            this.ChipComBox.Items.AddRange(new object[] {
            "MilanF",
            "FPC"});
            this.ChipComBox.Location = new System.Drawing.Point(872, 31);
            this.ChipComBox.Name = "ChipComBox";
            this.ChipComBox.Size = new System.Drawing.Size(93, 20);
            this.ChipComBox.TabIndex = 16;
            this.ChipComBox.Text = "MilanF";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(804, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 17;
            this.label1.Text = "Chip Name";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.eDITToolStripMenuItem,
            this.tOOLSToolStripMenuItem,
            this.oPTIONToolStripMenuItem,
            this.hELPToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(978, 25);
            this.menuStrip1.TabIndex = 18;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(43, 21);
            this.fileToolStripMenuItem.Text = "FILE";
            this.fileToolStripMenuItem.Click += new System.EventHandler(this.fileToolStripMenuItem_Click);
            // 
            // eDITToolStripMenuItem
            // 
            this.eDITToolStripMenuItem.Name = "eDITToolStripMenuItem";
            this.eDITToolStripMenuItem.Size = new System.Drawing.Size(47, 21);
            this.eDITToolStripMenuItem.Text = "EDIT";
            // 
            // tOOLSToolStripMenuItem
            // 
            this.tOOLSToolStripMenuItem.Name = "tOOLSToolStripMenuItem";
            this.tOOLSToolStripMenuItem.Size = new System.Drawing.Size(60, 21);
            this.tOOLSToolStripMenuItem.Text = "TOOLS";
            // 
            // oPTIONToolStripMenuItem
            // 
            this.oPTIONToolStripMenuItem.Name = "oPTIONToolStripMenuItem";
            this.oPTIONToolStripMenuItem.Size = new System.Drawing.Size(68, 21);
            this.oPTIONToolStripMenuItem.Text = "OPTION";
            // 
            // hELPToolStripMenuItem
            // 
            this.hELPToolStripMenuItem.Name = "hELPToolStripMenuItem";
            this.hELPToolStripMenuItem.Size = new System.Drawing.Size(49, 21);
            this.hELPToolStripMenuItem.Text = "HELP";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(804, 58);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(59, 12);
            this.label2.TabIndex = 19;
            this.label2.Text = "ShowSpeed";
            // 
            // ShowSpeedComBox
            // 
            this.ShowSpeedComBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.ShowSpeedComBox.FormattingEnabled = true;
            this.ShowSpeedComBox.Items.AddRange(new object[] {
            "Level0",
            "Level1",
            "Level2"});
            this.ShowSpeedComBox.Location = new System.Drawing.Point(873, 55);
            this.ShowSpeedComBox.Name = "ShowSpeedComBox";
            this.ShowSpeedComBox.Size = new System.Drawing.Size(93, 20);
            this.ShowSpeedComBox.TabIndex = 20;
            this.ShowSpeedComBox.Text = "Level0";
            this.ShowSpeedComBox.SelectedIndexChanged += new System.EventHandler(this.ShowSpeedComBox_SelectedIndexChanged);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(978, 652);
            this.Controls.Add(this.ShowSpeedComBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ChipComBox);
            this.Controls.Add(this.ShowBox);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.StartBtn);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LogicPaser";
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button StartBtn;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.RichTextBox ShowBox;
        private System.Windows.Forms.ComboBox ChipComBox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem eDITToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tOOLSToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem oPTIONToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem hELPToolStripMenuItem;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox ShowSpeedComBox;
    }
}

