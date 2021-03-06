﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INTROOS_Project
{
    public partial class ProcessManagerForm : Form
    {
        private PerformanceCounter pcProcess;
        private PerformanceCounter processMemory;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter availableRAMCounter;
        private PerformanceCounter diskUsage;
        private PerformanceCounter diskRead;
        private PerformanceCounter diskWrite;

        private string processorName;
        private string processorDescription;
        private string processorArchitecture;
        private string processorNumberOfCores;
        private string processorMaxClockSpeed;

        private string memoryManufacturer;
        private string memoryCapacity;
        private string memoryMemoryType;
        private string memoryDescription;
        private string memoryConfiguredClockSpeed;
        private string memoryFormFactor;
        private string memoryTypeDetail;

        private string diskModel;
        private string diskSize;
        private string diskDescription;
        private string diskPartitions;
        private string diskInterfaceType;

        private float[] CPUUsageValues;
        private float[] singleCPUUsageValues;
        private float[] memoryUsageValues;
        private float[] singleMemoryUsageValues;
        private float[] diskUsageValues;

        public ProcessManagerForm()
        {
            this.cpuCounter = new PerformanceCounter();
            this.cpuCounter.CategoryName = "Processor";
            this.cpuCounter.CounterName = "% Processor Time";
            this.cpuCounter.InstanceName = "_Total";
            this.availableRAMCounter = new PerformanceCounter("Memory", "Available MBytes");
            this.diskUsage = new PerformanceCounter("PhysicalDisk", "% Disk Time", "_Total");
            this.diskRead = new PerformanceCounter("PhysicalDisk", "Disk Read Bytes/sec", "_Total");
            this.diskWrite = new PerformanceCounter("PhysicalDisk", "Disk Write Bytes/sec", "_Total");

            InitializeComponent();
            refreshTimer.Enabled = false;
            //Console.WriteLine(this.GetComponent("Win32_DiskDrive", "InterfaceType"));
            //formatFloat(10);

        }

        #region Event Handlers

        private void ProcessManagerForm_Load(object sender, EventArgs e)
        {
            loadProcessorInformation();
            loadMemoryInformation();
            loadDiskInformation();

            initializeCPUChart();
            initializeMemoryChart();
            initializeDiskChart();

            initializeSingleProcessCPUChart();
            initializeSingleMemoryChart();

            loadProcessList();
            Console.WriteLine("Done Loading");
            refreshTimer.Enabled = true ;  
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            //Console.WriteLine("Refresh");
            int currentRow = processListGridView.FirstDisplayedScrollingRowIndex;
            int currentColumn = processListGridView.FirstDisplayedScrollingColumnIndex;
            int currentCell = processListGridView.CurrentCell.RowIndex;

            loadProcessList();

            processListGridView.CurrentCell = processListGridView.Rows[currentCell].Cells[0];
            processListGridView.FirstDisplayedScrollingRowIndex = currentRow;
            processListGridView.FirstDisplayedScrollingColumnIndex = currentColumn;

            updateCPUChart();
            updateMemoryChart();
            updateDiskChart();

            updateSingleProcessCPUChart();
            updateSingleMemoryChart();

            //Console.WriteLine(this.diskUsage.NextValue());
        }


        #endregion

        #region Button Click Events
        private void startBtn_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = processTxt.Text;
            process.Start();

            loadProcessList();
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            if (processListGridView.CurrentCell != null)
            {
                foreach (var process in Process.GetProcessesByName(processListGridView.Rows[processListGridView.CurrentCell.RowIndex].Cells[0].Value.ToString()))
                {
                    process.Kill();
                }
            }
            else
            {
                MessageBox.Show("Please select a process to kill", "Error");
            }

            loadProcessList();
        }

        private void viewUsageBtn_Click(object sender, EventArgs e)
        {
            if (processListGridView.CurrentCell != null)
            {
                pcProcess = new PerformanceCounter("Process", "% Processor Time", processListGridView.Rows[processListGridView.CurrentCell.RowIndex].Cells[0].Value.ToString());
                processMemory = new PerformanceCounter("Process", "Working Set", processListGridView.Rows[processListGridView.CurrentCell.RowIndex].Cells[0].Value.ToString());
            }
            else
            {
                MessageBox.Show("Please select a process", "Error");
            }
        }
        #endregion

        private void loadProcessList()
        {

            Process[] processList = Process.GetProcesses();
            DataTable dt = new DataTable();
            DataRow row;

            dt.Clear();
            initializeDataTable(dt);

            foreach (Process process in processList)
            {
                using (PerformanceCounter pcProcess = new PerformanceCounter("Process", "% Processor Time", process.ProcessName))
                {
                    row = dt.NewRow();

                    #region Process Name and ID
                    row["Process Name"] = process.ProcessName;
                    row["Process ID"] = process.Id;
                    #endregion

                    #region Disk Usage
                    row["Disk Usage"] = formatBytes(Convert.ToInt32(process.PagedMemorySize64));
                    #endregion

                    #region Memory Usage
                    row["Memory Usage  (Working Set)"] = formatBytes(Convert.ToInt32(process.WorkingSet64));
                    row["Memory Usage  (Private Working Set)"] = formatBytes(Convert.ToInt32(process.PrivateMemorySize64));
                    row["Memory Usage  (Working Set Peak)"] = formatBytes(Convert.ToInt32(process.PeakWorkingSet64));
                    #endregion

                    dt.Rows.Add(row);
                }
            }
            processListGridView.DataSource = dt;
            processListGridView.Sort(processListGridView.Columns["Process Name"], ListSortDirection.Ascending);
            processListGridView.Refresh();
        }

        #region Utility Functions

        private void initializeDataTable(DataTable table)
        {
            table.Columns.Add("Process Name");
            table.Columns.Add("Process ID");
            table.Columns.Add("Disk Usage");
            table.Columns.Add("Memory Usage  (Working Set)");
            table.Columns.Add("Memory Usage  (Private Working Set)");
            table.Columns.Add("Memory Usage  (Working Set Peak)");
        }

        public string formatBytes(int Bytes)
        {
            const int scale = 1024;
            string[] orders = new string[] { "GB", "MB", "KB", "Bytes" };
            long max = (long)Math.Pow(scale, orders.Length - 1);

            foreach (string order in orders)
            {
                if (Bytes > max)
                {
                    return string.Format("{0:##.##} {1}", decimal.Divide(Bytes, max), order);
                }
                max /= scale;
            }

            return "0 Bytes";
        }
        
        public float formatFloat(float bytes)
        {
            float x = 1.0f;
            if (Math.Floor(Math.Log10(bytes) + 1) == 0)
            {
                return bytes;
            }
            return x;
        }
        
        private void loadProcessorInformation()
        {
            Console.WriteLine("Loading " + "Processor Name");
            this.processorName = this.GetComponent("Win32_Processor", "Name");
            Console.WriteLine("Loading " + "Processor Description");
            this.processorDescription = this.GetComponent("Win32_Processor", "Description");
            Console.WriteLine("Loading " + "Processor Architecture");
            this.processorArchitecture = this.GetComponent("Win32_Processor", "Architecture");
            switch (this.processorArchitecture)
            {
                case "0": this.processorArchitecture = "x86"; break;
                case "1": this.processorArchitecture = "MIPS"; break;
                case "2": this.processorArchitecture = "Alpha"; break;
                case "3": this.processorArchitecture = "PowerPC"; break;
                case "5": this.processorArchitecture = "ARM"; break;
                case "6": this.processorArchitecture = "ia64"; break;
                case "9": this.processorArchitecture = "x64"; break;
                default: break;
            }
            Console.WriteLine("Loading " + "Processor Number of Cores");
            this.processorNumberOfCores = this.GetComponent("Win32_Processor", "NumberOfCores");
            Console.WriteLine("Loading " + "Processor Max Clock Speed");
            this.processorMaxClockSpeed = this.GetComponent("Win32_Processor", "MaxClockSpeed");
            this.processorMaxClockSpeed = (Convert.ToInt32(this.processorMaxClockSpeed) / 1000.0).ToString() + "GHz";

            procNameLbl.Text = this.processorName;
            procDescriptionLbl.Text = this.processorDescription;
            procArchitectureLbl.Text = this.processorArchitecture;
            procNumOfCoresLbl.Text = this.processorNumberOfCores;
            procMaxClockSpeedLbl.Text = this.processorMaxClockSpeed;
        }

        private string GetComponent(string hwclass, string syntax)
        {
            string str = "";
            ManagementObjectSearcher mos = new ManagementObjectSearcher("root\\CIMV2", "SELECT * FROM " + hwclass);
            foreach (ManagementObject mj in mos.Get())
            {
                //Console.WriteLine(Convert.ToString(mj[syntax]));
                str = Convert.ToString(mj[syntax]);
            }
            return str;
        }

        private void loadMemoryInformation()
        {
            Console.WriteLine("Loading " + "Memory Manufacturer");
            this.memoryManufacturer = this.GetComponent("Win32_PhysicalMemory", "Manufacturer");
            Console.WriteLine("Loading " + "Memory Capacity");
            this.memoryCapacity = this.GetComponent("Win32_PhysicalMemory", "Capacity");
            this.memoryCapacity = (Convert.ToInt64(this.memoryCapacity) / 1073741824).ToString();
            Console.WriteLine("Loading " + "Memory Type");
            this.memoryMemoryType = this.GetComponent("Win32_PhysicalMemory", "MemoryType");
            switch (this.memoryMemoryType)
            {
                case "0": this.memoryMemoryType = "Unknown"; break;
                case "1": this.memoryMemoryType = "Other"; break;
                case "2": this.memoryMemoryType = "DRAM"; break;
                case "3": this.memoryMemoryType = "Synchronous DRAM"; break;
                case "4": this.memoryMemoryType = "Cache DRAM"; break;
                case "5": this.memoryMemoryType = "EDO"; break;
                case "6": this.memoryMemoryType = "EDRAM"; break;
                case "7": this.memoryMemoryType = "VRAM"; break;
                case "8": this.memoryMemoryType = "SRAM"; break;
                case "9": this.memoryMemoryType = "RAM"; break;
                case "10": this.memoryMemoryType = "ROM"; break;
                case "11": this.memoryMemoryType = "Flash"; break;
                case "12": this.memoryMemoryType = "EEPROM"; break;
                case "13": this.memoryMemoryType = "FEPROM"; break;
                case "14": this.memoryMemoryType = "EPROM"; break;
                case "15": this.memoryMemoryType = "CDRAM"; break;
                case "16": this.memoryMemoryType = "3DRAM"; break;
                case "17": this.memoryMemoryType = "SDRAM"; break;
                case "18": this.memoryMemoryType = "SGRAM"; break;
                case "19": this.memoryMemoryType = "RDRAM"; break;
                case "20": this.memoryMemoryType = "DDR"; break;
                case "21": this.memoryMemoryType = "DDR2"; break;
                case "22": this.memoryMemoryType = "DDR2"; break;
                case "23": this.memoryMemoryType = "DDR2 FB-DIMM"; break;
                case "24": this.memoryMemoryType = "DDR3"; break;
                case "25": this.memoryMemoryType = "FBD2"; break;
                default: break;
            }
            Console.WriteLine("Loading " + "Memory Description");
            this.memoryDescription = this.GetComponent("Win32_PhysicalMemory", "Description");
            Console.WriteLine("Loading " + "Memory Configured Clock Speed");
            this.memoryConfiguredClockSpeed = this.GetComponent("Win32_PhysicalMemory", "ConfiguredClockSpeed") + "MHz";
            Console.WriteLine("Loading " + "Memory Form Factor");
            this.memoryFormFactor = this.GetComponent("Win32_PhysicalMemory", "FormFactor");
            switch (this.memoryFormFactor)
            {
                case "0": this.memoryFormFactor = "Unknown"; break;
                case "1": this.memoryFormFactor = "Other"; break;
                case "2": this.memoryFormFactor = "SIP"; break;
                case "3": this.memoryFormFactor = "DIP"; break;
                case "4": this.memoryFormFactor = "ZIP"; break;
                case "5": this.memoryFormFactor = "SOJ"; break;
                case "6": this.memoryFormFactor = "Proprietary"; break;
                case "7": this.memoryFormFactor = "SIMM"; break;
                case "8": this.memoryFormFactor = "DIMM"; break;
                case "9": this.memoryFormFactor = "TSOP"; break;
                case "10": this.memoryFormFactor = "PGA"; break;
                case "11": this.memoryFormFactor = "RIMM"; break;
                case "12": this.memoryFormFactor = "SODIMM"; break;
                case "13": this.memoryFormFactor = "SRIMM"; break;
                case "14": this.memoryFormFactor = "SMD"; break;
                case "15": this.memoryFormFactor = "SSMP"; break;
                case "16": this.memoryFormFactor = "QFP"; break;
                case "17": this.memoryFormFactor = "TQFP"; break;
                case "18": this.memoryFormFactor = "SOIC"; break;
                case "19": this.memoryFormFactor = "LCC"; break;
                case "20": this.memoryFormFactor = "PLCC"; break;
                case "21": this.memoryFormFactor = "BGA"; break;
                case "22": this.memoryFormFactor = "FPBGA"; break;
                case "23": this.memoryFormFactor = "LGA"; break;
                default: break;
            }
            Console.WriteLine("Loading " + "Memory Type Detail");
            this.memoryTypeDetail = this.GetComponent("Win32_PhysicalMemory", "TypeDetail");
            switch (this.memoryTypeDetail)
            {
                case "1": this.memoryTypeDetail = "Reserved"; break;
                case "2": this.memoryTypeDetail = "Other"; break;
                case "4": this.memoryTypeDetail = "Unknown"; break;
                case "8": this.memoryTypeDetail = "Fast-paged"; break;
                case "16": this.memoryTypeDetail = "Static column"; break;
                case "32": this.memoryTypeDetail = "Pseudo-static"; break;
                case "64": this.memoryTypeDetail = "RAMBUS"; break;
                case "128": this.memoryTypeDetail = "Synchronous"; break;
                case "256": this.memoryTypeDetail = "CMOS"; break;
                case "512": this.memoryTypeDetail = "EDO"; break;
                case "1024": this.memoryTypeDetail = "Window DRAM"; break;
                case "2048": this.memoryTypeDetail = "Cache DRAM"; break;
                case "4096": this.memoryTypeDetail = "Nonvolatile"; break;
                default: break;
            }

            memoryNameLbl.Text = this.memoryManufacturer + " " + this.memoryCapacity + "GB" + " " + this.memoryMemoryType;
            memoryDescriptionLbl.Text = this.memoryDescription;
            memoryTypeDetailLbl.Text = this.memoryTypeDetail;
            memorySpeedLbl.Text = this.memoryConfiguredClockSpeed;
            memoryFormFactLbl.Text = this.memoryFormFactor;
        }

        private void loadDiskInformation()
        {
            this.diskModel = this.GetComponent("Win32_DiskDrive", "Model");
            this.diskDescription = this.GetComponent("Win32_DiskDrive", "MediaType");
            this.diskSize = (Convert.ToInt64(this.GetComponent("Win32_DiskDrive", "Size")) * 1.0 / 1024 / 1024 / 1024).ToString("n2");
            this.diskPartitions = this.GetComponent("Win32_DiskDrive", "Partitions");
            this.diskInterfaceType = this.GetComponent("Win32_DiskDrive", "InterfaceType");

            diskNameLbl.Text = this.diskModel;
            diskDescriptionLbl.Text = this.diskDescription; 
            diskSizeLbl.Text = this.diskSize + " GB";
            diskPartitionsLbl.Text = this.diskPartitions;
            diskInterfaceTypeLbl.Text = this.diskInterfaceType;
        }

        #endregion

        #region Chart Functions

        private void initializeCPUChart()
        {
            this.CPUUsageValues = new float[50];
            for (int i = 0; i < 50; i++)
            {
                this.CPUUsageValues[i] = 0;
            }
            CPUChart.ChartAreas[0].AxisX.Minimum = 0;
            CPUChart.ChartAreas[0].AxisX.Maximum = 50;
            CPUChart.ChartAreas[0].AxisY.Minimum = 0;
            CPUChart.ChartAreas[0].AxisY.Maximum = 100;
            CPUChart.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100); // -15<= y <=15
            CPUChart.ChartAreas[0].AxisX.ScaleView.Zoom(1, 50); // -15 <= x <= 2
            CPUChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            CPUChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            CPUChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            CPUChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            CPUChart.Series[0].IsVisibleInLegend = false;
        }

        private void updateCPUChart()
        {
            float[] temp = new float[50];

            foreach (var series in CPUChart.Series)
            {
                series.Points.Clear();
            }

            temp[49] = this.cpuCounter.NextValue();
            if (temp[49].ToString("n2").Length < 5) procCPUUsageLbl.Text = temp[49].ToString("n3") +"%";
            else procCPUUsageLbl.Text = temp[49].ToString("n2") + "%";
            for (int i = 0; i < 49; i++)
			{
                temp[i] = this.CPUUsageValues[i + 1];
			}

            for (int i = 0; i < 50; i++)
            {
                this.CPUUsageValues[i] = temp[i];
            }

            for (int i = 0; i < 50; i++)
            {
                CPUChart.Series[0].Points.AddXY(i, this.CPUUsageValues[i]);
                CPUChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
        }

        private void initializeMemoryChart()
        {
            this.memoryUsageValues = new float[50];
            for (int i = 0; i < 50; i++)
            {
                this.memoryUsageValues[i] = 0;
            }
            memoryChart.ChartAreas[0].AxisX.Minimum = 0;
            memoryChart.ChartAreas[0].AxisX.Maximum = 50;
            memoryChart.ChartAreas[0].AxisY.Minimum = 0;
            memoryChart.ChartAreas[0].AxisY.Maximum = Convert.ToInt64(this.memoryCapacity);
            memoryChart.ChartAreas[0].AxisY.ScaleView.Zoom(0, Convert.ToInt64(this.memoryCapacity)); 
            memoryChart.ChartAreas[0].AxisX.ScaleView.Zoom(1, 50); 
            memoryChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            memoryChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            memoryChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            memoryChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            memoryChart.Series[0].IsVisibleInLegend = false;
        }

        private void updateMemoryChart()
        {
            float[] temp = new float[50];
            float availableMemory;
            float totalMemory;

            foreach (var series in memoryChart.Series)
            {
                series.Points.Clear();
            }

            availableMemory = this.availableRAMCounter.NextValue() / 1024;
            if (availableMemory.ToString("n2").Length < 5) memAvailableLbl.Text = availableMemory.ToString("n3");
            else memAvailableLbl.Text = availableMemory.ToString("n2");

            totalMemory = (float)new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory / 1024 / 1024 / 1024;
            if (totalMemory.ToString("n2").Length < 5) totalMemoryLbl.Text = totalMemory.ToString("n3");
            else totalMemoryLbl.Text = totalMemory.ToString("n2");

            temp[49] = totalMemory - availableMemory;
            if (temp[49].ToString("n2").Length < 5) memoryInUseLbl.Text = temp[49].ToString("n3");
            else memoryInUseLbl.Text = temp[49].ToString("n2");
            

            for (int i = 0; i < 49; i++)
            {
                temp[i] = this.memoryUsageValues[i + 1];
            }

            for (int i = 0; i < 50; i++)
            {
                this.memoryUsageValues[i] = temp[i];
            }

            for (int i = 0; i < 50; i++)
            {
                memoryChart.Series[0].Points.AddXY(i, this.memoryUsageValues[i]);
                memoryChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
        }

        private void initializeDiskChart()
        {
            this.diskUsageValues = new float[50];
            for (int i = 0; i < 50; i++)
            {
                this.diskUsageValues[i] = 0;
            }
            diskChart.ChartAreas[0].AxisX.Minimum = 0;
            diskChart.ChartAreas[0].AxisX.Maximum = 50;
            diskChart.ChartAreas[0].AxisY.Minimum = 0;
            diskChart.ChartAreas[0].AxisY.Maximum = 100;
            diskChart.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100);
            diskChart.ChartAreas[0].AxisX.ScaleView.Zoom(1, 50);
            diskChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            diskChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            diskChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            diskChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            diskChart.Series[0].IsVisibleInLegend = false;
        }

        private void updateDiskChart()
        {
            float[] temp = new float[50];
            float read;
            float write;

            foreach (var series in diskChart.Series)
            {
                series.Points.Clear();
            }

            temp[49] = this.diskUsage.NextValue();
            if (temp[49] >= 100) temp[49] = 100;
            if (temp[49].ToString("n2").Length < 5) diskTimeLbl.Text = temp[49].ToString("n3") + "%";
            else diskTimeLbl.Text = temp[49].ToString("n2") + "%";

            read = this.diskRead.NextValue();
            if (read.ToString("n3").Length == 5) diskReadLbl.Text = read.ToString("n3") + "  B/s";
            else if (read.ToString("n3").Length == 6) diskReadLbl.Text = read.ToString("n2") + "  B/s";
            else if (read.ToString("n3").Length == 7) diskReadLbl.Text = read.ToString("n1") + "  B/s";
            else if (read.ToString("n3").Length == 9)
            {
                read = read / 1024;
                diskReadLbl.Text = read.ToString("n3") + " KB/s";
            }
            else if (read.ToString("n3").Length == 10)
            {
                read = read / 1024;
                diskReadLbl.Text = read.ToString("n2") + " KB/s";
            }
            else if (read.ToString("n3").Length == 11)
            {
                read = read / 1024;
                diskReadLbl.Text = read.ToString("n1") + " KB/s";
            }
            else if (read.ToString("n3").Length == 13)
            {
                read = read / 1024 / 1024;
                diskReadLbl.Text = read.ToString("n3") + " MB/s";
            }
            else if (read.ToString("n3").Length == 14)
            {
                read = read / 1024 / 1024;
                diskReadLbl.Text = read.ToString("n2") + " MB/s";
            }
            else if (read.ToString("n3").Length == 15)
            {
                read = read / 1024 / 1024;
                diskReadLbl.Text = read.ToString("n1") + " MB/s";
            }
            else diskReadLbl.Text = read.ToString() + " B/s";

            write = this.diskWrite.NextValue();
            if (write.ToString("n3").Length == 5) diskWriteLbl.Text = write.ToString("n3") + "  B/s";
            else if (write.ToString("n3").Length == 6) diskWriteLbl.Text = write.ToString("n2") + "  B/s";
            else if (write.ToString("n3").Length == 7) diskWriteLbl.Text = write.ToString("n1") + "  B/s";
            else if (write.ToString("n3").Length == 9)
            {
                write = write / 1024;
                diskWriteLbl.Text = write.ToString("n3") + " KB/s";
            }
            else if (write.ToString("n3").Length == 10)
            {
                write = write / 1024;
                diskWriteLbl.Text = write.ToString("n2") + " KB/s";
            }
            else if (write.ToString("n3").Length == 11)
            {
                write = write / 1024;
                diskWriteLbl.Text = write.ToString("n1") + " KB/s";
            }
            else if (write.ToString("n3").Length == 13)
            {
                write = write / 1024 / 1024;
                diskWriteLbl.Text = write.ToString("n3") + " MB/s";
            }
            else if (write.ToString("n3").Length == 14)
            {
                write = write / 1024 / 1024;
                diskWriteLbl.Text = write.ToString("n2") + " MB/s";
            }
            else if (write.ToString("n3").Length == 15)
            {
                write = write / 1024 / 1024;
                diskWriteLbl.Text = write.ToString("n1") + " MB/s";
            }
            else diskWriteLbl.Text = write.ToString() + " B/s";

            for (int i = 0; i < 49; i++)
            {
                temp[i] = this.diskUsageValues[i + 1];
            }

            for (int i = 0; i < 50; i++)
            {
                this.diskUsageValues[i] = temp[i];
            }

            for (int i = 0; i < 50; i++)
            {
                diskChart.Series[0].Points.AddXY(i, this.diskUsageValues[i]);
                diskChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
        }

        private void initializeSingleProcessCPUChart()
        {
            this.singleCPUUsageValues = new float[50];
            for (int i = 0; i < 50; i++)
            {
                this.singleCPUUsageValues[i] = 0;
            }
            singleProcessCPUChart.ChartAreas[0].AxisX.Minimum = 0;
            singleProcessCPUChart.ChartAreas[0].AxisX.Maximum = 50;
            singleProcessCPUChart.ChartAreas[0].AxisY.Minimum = 0;
            singleProcessCPUChart.ChartAreas[0].AxisY.Maximum = 100;
            singleProcessCPUChart.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100); // -15<= y <=15
            singleProcessCPUChart.ChartAreas[0].AxisX.ScaleView.Zoom(1, 50); // -15 <= x <= 2
            singleProcessCPUChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            singleProcessCPUChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            singleProcessCPUChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            singleProcessCPUChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            singleProcessCPUChart.Series[0].IsVisibleInLegend = false;
        }

        private void updateSingleProcessCPUChart()
        {
            float[] temp = new float[50];

            foreach (var series in singleProcessCPUChart.Series)
            {
                series.Points.Clear();
            }
            if (pcProcess != null)
            {
                temp[49] = this.pcProcess.NextValue() / Convert.ToInt32(this.processorNumberOfCores);
                if (temp[49].ToString("n2").Length < 5) cpuUsageLbl.Text = temp[49].ToString("n3") + "%";
                else cpuUsageLbl.Text = temp[49].ToString("n2") + "%";
            }
            else temp[49] = 0;
            for (int i = 0; i < 49; i++)
            {
                temp[i] = this.singleCPUUsageValues[i + 1];
            }

            for (int i = 0; i < 50; i++)
            {
                this.singleCPUUsageValues[i] = temp[i];
            }

            for (int i = 0; i < 50; i++)
            {
                singleProcessCPUChart.Series[0].Points.AddXY(i, this.singleCPUUsageValues[i]);
                singleProcessCPUChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }

            
        }
      
        private void initializeSingleMemoryChart()
        {
            this.singleMemoryUsageValues = new float[50];
            for (int i = 0; i < 50; i++)
            {
                this.singleMemoryUsageValues[i] = 0;
            }
            singleProcessMemChart.ChartAreas[0].AxisX.Minimum = 0;
            singleProcessMemChart.ChartAreas[0].AxisX.Maximum = 50;
            singleProcessMemChart.ChartAreas[0].AxisY.Minimum = 0;
            singleProcessMemChart.ChartAreas[0].AxisY.Maximum = Convert.ToInt64(this.memoryCapacity);
            singleProcessMemChart.ChartAreas[0].AxisY.ScaleView.Zoom(0, Convert.ToInt64(this.memoryCapacity)); // -15<= y <=15
            singleProcessMemChart.ChartAreas[0].AxisX.ScaleView.Zoom(1, 50); // -15 <= x <= 2
            singleProcessMemChart.ChartAreas[0].CursorX.IsUserEnabled = true;
            singleProcessMemChart.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            singleProcessMemChart.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            singleProcessMemChart.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            singleProcessMemChart.Series[0].IsVisibleInLegend = false;
        }

        private void updateSingleMemoryChart()
        {
            float[] temp = new float[50];

            foreach (var series in singleProcessMemChart.Series)
            {
                series.Points.Clear();
            }

            if (pcProcess != null)
            {
                temp[49] = this.processMemory.NextValue() / 1024 / 1024;
                if (temp[49].ToString("n2").Length < 5) memoryUsageLbl.Text = temp[49].ToString("n3") + " MB";
                else memoryUsageLbl.Text = temp[49].ToString("n2") + " MB";
                temp[49] = temp[49] / 1024;
            }

            for (int i = 0; i < 49; i++)
            {
                temp[i] = this.singleMemoryUsageValues[i + 1];
            }

            for (int i = 0; i < 50; i++)
            {
                this.singleMemoryUsageValues[i] = temp[i];
            }

            for (int i = 0; i < 50; i++)
            {
                singleProcessMemChart.Series[0].Points.AddXY(i, this.singleMemoryUsageValues[i]);
                singleProcessMemChart.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            }
        }
        
        #endregion
    }
}
