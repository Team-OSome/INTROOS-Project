namespace INTROOS_Project
{
    partial class ProcessManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea9 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Legend legend9 = new System.Windows.Forms.DataVisualization.Charting.Legend();
            System.Windows.Forms.DataVisualization.Charting.Series series9 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ProcessManagerForm));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.ProcessesTab = new System.Windows.Forms.TabPage();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cpuUsageLbl = new System.Windows.Forms.Label();
            this.viewUsageBtn = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.processListGridView = new System.Windows.Forms.DataGridView();
            this.processTxt = new System.Windows.Forms.TextBox();
            this.stopBtn = new System.Windows.Forms.Button();
            this.startBtn = new System.Windows.Forms.Button();
            this.CPUTab = new System.Windows.Forms.TabPage();
            this.procNumOfCoresLbl = new System.Windows.Forms.Label();
            this.procMaxClockSpeedLbl = new System.Windows.Forms.Label();
            this.procArchitectureLbl = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.procCPUUsageLbl = new System.Windows.Forms.Label();
            this.procNumOfCores = new System.Windows.Forms.Label();
            this.procMaxClockSpeed = new System.Windows.Forms.Label();
            this.procArchitecture = new System.Windows.Forms.Label();
            this.procDescriptionLbl = new System.Windows.Forms.Label();
            this.procNameLbl = new System.Windows.Forms.Label();
            this.CPUChart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.MemoryTab = new System.Windows.Forms.TabPage();
            this.imageList = new System.Windows.Forms.ImageList(this.components);
            this.refreshTimer = new System.Windows.Forms.Timer(this.components);
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.tabControl.SuspendLayout();
            this.ProcessesTab.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processListGridView)).BeginInit();
            this.CPUTab.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CPUChart)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Alignment = System.Windows.Forms.TabAlignment.Left;
            this.tabControl.Controls.Add(this.ProcessesTab);
            this.tabControl.Controls.Add(this.CPUTab);
            this.tabControl.Controls.Add(this.MemoryTab);
            this.tabControl.ImageList = this.imageList;
            this.tabControl.ItemSize = new System.Drawing.Size(150, 150);
            this.tabControl.Location = new System.Drawing.Point(12, 12);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(875, 507);
            this.tabControl.SizeMode = System.Windows.Forms.TabSizeMode.Fixed;
            this.tabControl.TabIndex = 0;
            // 
            // ProcessesTab
            // 
            this.ProcessesTab.BackColor = System.Drawing.Color.Transparent;
            this.ProcessesTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.ProcessesTab.Controls.Add(this.groupBox1);
            this.ProcessesTab.Controls.Add(this.label2);
            this.ProcessesTab.Controls.Add(this.label1);
            this.ProcessesTab.Controls.Add(this.processListGridView);
            this.ProcessesTab.Controls.Add(this.processTxt);
            this.ProcessesTab.Controls.Add(this.stopBtn);
            this.ProcessesTab.Controls.Add(this.startBtn);
            this.ProcessesTab.Cursor = System.Windows.Forms.Cursors.Default;
            this.ProcessesTab.ImageKey = "PROCESS_PNG.png";
            this.ProcessesTab.Location = new System.Drawing.Point(154, 4);
            this.ProcessesTab.Name = "ProcessesTab";
            this.ProcessesTab.Padding = new System.Windows.Forms.Padding(3);
            this.ProcessesTab.Size = new System.Drawing.Size(717, 499);
            this.ProcessesTab.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.tabControl1);
            this.groupBox1.Controls.Add(this.cpuUsageLbl);
            this.groupBox1.Controls.Add(this.viewUsageBtn);
            this.groupBox1.Location = new System.Drawing.Point(174, 17);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(521, 164);
            this.groupBox1.TabIndex = 14;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " Properties ";
            // 
            // cpuUsageLbl
            // 
            this.cpuUsageLbl.AutoSize = true;
            this.cpuUsageLbl.Location = new System.Drawing.Point(7, 69);
            this.cpuUsageLbl.Name = "cpuUsageLbl";
            this.cpuUsageLbl.Size = new System.Drawing.Size(0, 13);
            this.cpuUsageLbl.TabIndex = 2;
            // 
            // viewUsageBtn
            // 
            this.viewUsageBtn.Location = new System.Drawing.Point(6, 19);
            this.viewUsageBtn.Name = "viewUsageBtn";
            this.viewUsageBtn.Size = new System.Drawing.Size(120, 34);
            this.viewUsageBtn.TabIndex = 1;
            this.viewUsageBtn.Text = "View CPU Usage";
            this.viewUsageBtn.UseVisualStyleBackColor = true;
            this.viewUsageBtn.Click += new System.EventHandler(this.viewUsageBtn_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 140);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(121, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Select a process to stop";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Process Name";
            // 
            // processListGridView
            // 
            this.processListGridView.AllowUserToAddRows = false;
            this.processListGridView.AllowUserToDeleteRows = false;
            this.processListGridView.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.processListGridView.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.processListGridView.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.processListGridView.Location = new System.Drawing.Point(15, 187);
            this.processListGridView.Name = "processListGridView";
            this.processListGridView.ReadOnly = true;
            this.processListGridView.RowHeadersVisible = false;
            this.processListGridView.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.processListGridView.Size = new System.Drawing.Size(680, 290);
            this.processListGridView.TabIndex = 11;
            // 
            // processTxt
            // 
            this.processTxt.Location = new System.Drawing.Point(15, 33);
            this.processTxt.Name = "processTxt";
            this.processTxt.Size = new System.Drawing.Size(132, 20);
            this.processTxt.TabIndex = 10;
            // 
            // stopBtn
            // 
            this.stopBtn.Location = new System.Drawing.Point(12, 156);
            this.stopBtn.Name = "stopBtn";
            this.stopBtn.Size = new System.Drawing.Size(132, 23);
            this.stopBtn.TabIndex = 9;
            this.stopBtn.Text = "Stop";
            this.stopBtn.UseVisualStyleBackColor = true;
            this.stopBtn.Click += new System.EventHandler(this.stopBtn_Click);
            // 
            // startBtn
            // 
            this.startBtn.Location = new System.Drawing.Point(15, 59);
            this.startBtn.Name = "startBtn";
            this.startBtn.Size = new System.Drawing.Size(132, 23);
            this.startBtn.TabIndex = 8;
            this.startBtn.Text = "Start Process";
            this.startBtn.UseVisualStyleBackColor = true;
            this.startBtn.Click += new System.EventHandler(this.startBtn_Click);
            // 
            // CPUTab
            // 
            this.CPUTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.CPUTab.Controls.Add(this.procNumOfCoresLbl);
            this.CPUTab.Controls.Add(this.procMaxClockSpeedLbl);
            this.CPUTab.Controls.Add(this.procArchitectureLbl);
            this.CPUTab.Controls.Add(this.label3);
            this.CPUTab.Controls.Add(this.procCPUUsageLbl);
            this.CPUTab.Controls.Add(this.procNumOfCores);
            this.CPUTab.Controls.Add(this.procMaxClockSpeed);
            this.CPUTab.Controls.Add(this.procArchitecture);
            this.CPUTab.Controls.Add(this.procDescriptionLbl);
            this.CPUTab.Controls.Add(this.procNameLbl);
            this.CPUTab.Controls.Add(this.CPUChart);
            this.CPUTab.ImageKey = "CPU_PNG.png";
            this.CPUTab.Location = new System.Drawing.Point(154, 4);
            this.CPUTab.Name = "CPUTab";
            this.CPUTab.Padding = new System.Windows.Forms.Padding(3);
            this.CPUTab.Size = new System.Drawing.Size(717, 499);
            this.CPUTab.TabIndex = 1;
            // 
            // procNumOfCoresLbl
            // 
            this.procNumOfCoresLbl.AutoSize = true;
            this.procNumOfCoresLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procNumOfCoresLbl.Location = new System.Drawing.Point(134, 139);
            this.procNumOfCoresLbl.Name = "procNumOfCoresLbl";
            this.procNumOfCoresLbl.Size = new System.Drawing.Size(35, 20);
            this.procNumOfCoresLbl.TabIndex = 10;
            this.procNumOfCoresLbl.Text = "asd";
            // 
            // procMaxClockSpeedLbl
            // 
            this.procMaxClockSpeedLbl.AutoSize = true;
            this.procMaxClockSpeedLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procMaxClockSpeedLbl.Location = new System.Drawing.Point(168, 109);
            this.procMaxClockSpeedLbl.Name = "procMaxClockSpeedLbl";
            this.procMaxClockSpeedLbl.Size = new System.Drawing.Size(35, 20);
            this.procMaxClockSpeedLbl.TabIndex = 9;
            this.procMaxClockSpeedLbl.Text = "asd";
            // 
            // procArchitectureLbl
            // 
            this.procArchitectureLbl.AutoSize = true;
            this.procArchitectureLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procArchitectureLbl.Location = new System.Drawing.Point(103, 80);
            this.procArchitectureLbl.Name = "procArchitectureLbl";
            this.procArchitectureLbl.Size = new System.Drawing.Size(35, 20);
            this.procArchitectureLbl.TabIndex = 8;
            this.procArchitectureLbl.Text = "asd";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(562, 129);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(97, 17);
            this.label3.TabIndex = 7;
            this.label3.Text = "% CPU Usage";
            // 
            // procCPUUsageLbl
            // 
            this.procCPUUsageLbl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.procCPUUsageLbl.AutoSize = true;
            this.procCPUUsageLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 30F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procCPUUsageLbl.Location = new System.Drawing.Point(555, 83);
            this.procCPUUsageLbl.Name = "procCPUUsageLbl";
            this.procCPUUsageLbl.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.procCPUUsageLbl.Size = new System.Drawing.Size(154, 46);
            this.procCPUUsageLbl.TabIndex = 6;
            this.procCPUUsageLbl.Text = "99.99%";
            this.procCPUUsageLbl.TextAlign = System.Drawing.ContentAlignment.TopRight;
            // 
            // procNumOfCores
            // 
            this.procNumOfCores.AutoSize = true;
            this.procNumOfCores.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procNumOfCores.Location = new System.Drawing.Point(9, 142);
            this.procNumOfCores.Name = "procNumOfCores";
            this.procNumOfCores.Size = new System.Drawing.Size(119, 17);
            this.procNumOfCores.TabIndex = 5;
            this.procNumOfCores.Text = "Number of Cores:";
            // 
            // procMaxClockSpeed
            // 
            this.procMaxClockSpeed.AutoSize = true;
            this.procMaxClockSpeed.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procMaxClockSpeed.Location = new System.Drawing.Point(9, 112);
            this.procMaxClockSpeed.Name = "procMaxClockSpeed";
            this.procMaxClockSpeed.Size = new System.Drawing.Size(153, 17);
            this.procMaxClockSpeed.TabIndex = 4;
            this.procMaxClockSpeed.Text = "Maximum Clock Speed:";
            // 
            // procArchitecture
            // 
            this.procArchitecture.AutoSize = true;
            this.procArchitecture.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procArchitecture.Location = new System.Drawing.Point(9, 83);
            this.procArchitecture.Name = "procArchitecture";
            this.procArchitecture.Size = new System.Drawing.Size(88, 17);
            this.procArchitecture.TabIndex = 3;
            this.procArchitecture.Text = "Architecture:";
            // 
            // procDescriptionLbl
            // 
            this.procDescriptionLbl.AutoSize = true;
            this.procDescriptionLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procDescriptionLbl.Location = new System.Drawing.Point(9, 50);
            this.procDescriptionLbl.Name = "procDescriptionLbl";
            this.procDescriptionLbl.Size = new System.Drawing.Size(86, 20);
            this.procDescriptionLbl.TabIndex = 2;
            this.procDescriptionLbl.Text = "description";
            // 
            // procNameLbl
            // 
            this.procNameLbl.AutoSize = true;
            this.procNameLbl.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.procNameLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.procNameLbl.Location = new System.Drawing.Point(7, 7);
            this.procNameLbl.Name = "procNameLbl";
            this.procNameLbl.Size = new System.Drawing.Size(152, 31);
            this.procNameLbl.TabIndex = 1;
            this.procNameLbl.Text = "sample text";
            // 
            // CPUChart
            // 
            this.CPUChart.BackColor = System.Drawing.Color.Transparent;
            chartArea9.BackColor = System.Drawing.Color.Transparent;
            chartArea9.Name = "ChartArea1";
            this.CPUChart.ChartAreas.Add(chartArea9);
            legend9.Name = "Legend1";
            this.CPUChart.Legends.Add(legend9);
            this.CPUChart.Location = new System.Drawing.Point(6, 175);
            this.CPUChart.Name = "CPUChart";
            series9.BorderColor = System.Drawing.Color.Green;
            series9.BorderWidth = 3;
            series9.ChartArea = "ChartArea1";
            series9.Color = System.Drawing.Color.Green;
            series9.Legend = "Legend1";
            series9.Name = "Series1";
            this.CPUChart.Series.Add(series9);
            this.CPUChart.Size = new System.Drawing.Size(703, 316);
            this.CPUChart.TabIndex = 0;
            this.CPUChart.Text = "chart1";
            // 
            // MemoryTab
            // 
            this.MemoryTab.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.MemoryTab.ImageKey = "RAM_PNG.png";
            this.MemoryTab.Location = new System.Drawing.Point(154, 4);
            this.MemoryTab.Name = "MemoryTab";
            this.MemoryTab.Padding = new System.Windows.Forms.Padding(3);
            this.MemoryTab.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.MemoryTab.Size = new System.Drawing.Size(717, 499);
            this.MemoryTab.TabIndex = 2;
            // 
            // imageList
            // 
            this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
            this.imageList.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList.Images.SetKeyName(0, "process.jpg");
            this.imageList.Images.SetKeyName(1, "RAM_PNG.png");
            this.imageList.Images.SetKeyName(2, "CPU_PNG.png");
            this.imageList.Images.SetKeyName(3, "PROCESS_PNG.png");
            // 
            // refreshTimer
            // 
            this.refreshTimer.Enabled = true;
            this.refreshTimer.Interval = 1500;
            this.refreshTimer.Tick += new System.EventHandler(this.refreshTimer_Tick);
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(133, 16);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(388, 148);
            this.tabControl1.TabIndex = 3;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(380, 122);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(192, 74);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // ProcessManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(899, 531);
            this.Controls.Add(this.tabControl);
            this.Name = "ProcessManagerForm";
            this.Text = "ProcessManagerForm";
            this.Load += new System.EventHandler(this.ProcessManagerForm_Load);
            this.tabControl.ResumeLayout(false);
            this.ProcessesTab.ResumeLayout(false);
            this.ProcessesTab.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.processListGridView)).EndInit();
            this.CPUTab.ResumeLayout(false);
            this.CPUTab.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.CPUChart)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage ProcessesTab;
        private System.Windows.Forms.TabPage CPUTab;
        private System.Windows.Forms.ImageList imageList;
        private System.Windows.Forms.TabPage MemoryTab;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label cpuUsageLbl;
        private System.Windows.Forms.Button viewUsageBtn;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGridView processListGridView;
        private System.Windows.Forms.TextBox processTxt;
        private System.Windows.Forms.Button stopBtn;
        private System.Windows.Forms.Button startBtn;
        private System.Windows.Forms.Label procDescriptionLbl;
        private System.Windows.Forms.Label procNameLbl;
        private System.Windows.Forms.DataVisualization.Charting.Chart CPUChart;
        private System.Windows.Forms.Label procArchitecture;
        private System.Windows.Forms.Label procArchitectureLbl;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label procCPUUsageLbl;
        private System.Windows.Forms.Label procNumOfCores;
        private System.Windows.Forms.Label procMaxClockSpeed;
        private System.Windows.Forms.Label procNumOfCoresLbl;
        private System.Windows.Forms.Label procMaxClockSpeedLbl;
        private System.Windows.Forms.Timer refreshTimer;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}