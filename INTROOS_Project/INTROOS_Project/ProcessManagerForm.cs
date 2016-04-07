using System;
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
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        private string processorName;
        private string processorDescription;
        private string processorArchitecture;
        private string processorNumberOfCores;
        private string processorMaxClockSpeed;

        private float[] CPUUsageValues;

        public ProcessManagerForm()
        {
            this.cpuCounter = new PerformanceCounter();
            this.cpuCounter.CategoryName = "Processor";
            this.cpuCounter.CounterName = "% Processor Time";
            this.cpuCounter.InstanceName = "_Total";
            this.ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 
            InitializeComponent();
        }

        #region Event Handlers

        private void ProcessManagerForm_Load(object sender, EventArgs e)
        {
            loadProcessorInformation();
            initializeCPUChart();
            loadProcessList();
        }

        private void refreshTimer_Tick(object sender, EventArgs e)
        {
            int currentRow = processListGridView.FirstDisplayedScrollingRowIndex;
            int currentColumn = processListGridView.FirstDisplayedScrollingColumnIndex;
            int currentCell = processListGridView.CurrentCell.RowIndex;

            loadProcessList();

            processListGridView.CurrentCell = processListGridView.Rows[currentCell].Cells[0];
            processListGridView.FirstDisplayedScrollingRowIndex = currentRow;
            processListGridView.FirstDisplayedScrollingColumnIndex = currentColumn;

            if (pcProcess != null)
            {
                cpuUsageLbl.Text = "CPU usage: " + pcProcess.NextValue() + "%";
            }

            updateCPUChart();
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

        private void loadProcessorInformation()
        {
            this.processorName = this.GetComponent("Win32_Processor", "Name");
            this.processorDescription = this.GetComponent("Win32_Processor", "Description");
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
            this.processorNumberOfCores = this.GetComponent("Win32_Processor", "NumberOfCores");
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

        #endregion
    }
}
