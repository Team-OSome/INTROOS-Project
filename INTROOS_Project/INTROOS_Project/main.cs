﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace INTROOS_Project
{
    public partial class main : Form
    {
        private PerformanceCounter pcProcess;
        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;
        private float[] values;
        private float[] RAMValues;

        public main()
        {
            this.cpuCounter = new PerformanceCounter();
            ramCounter = new PerformanceCounter("Memory", "Available MBytes"); 
            values = new float[11];
            RAMValues = new float[11];
            for (int i = 0; i < 11; i++)
            {
                values[i] = 0;
            }
            for (int i = 0; i < 11; i++)
            {
                RAMValues[i] = 0;
            }
            InitializeComponent();
            
        }

        #region Event Handlers
        private void main_Load(object sender, EventArgs e)
        {
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
            updateChart();
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
        #endregion

        #region line graph functions

        private void setValues()
        {
            float[] temp = new float[11];
            float cpuUsage;
            float ramUsage;

            cpuUsage = this.getCurrentCpuUsage();
            TotalCPUUsageLbl.Text = "Total CPU Usage: " + cpuUsage.ToString() + "%";
            temp[0] = cpuUsage;
            temp[1] = values[0];
            temp[2] = values[1];
            temp[3] = values[2];
            temp[4] = values[3];
            temp[5] = values[4];
            temp[6] = values[5];
            temp[7] = values[6];
            temp[8] = values[7];
            temp[9] = values[8];
            temp[10] = values[9];
            for (int i = 0; i < 11; i++)
            {
                this.values[i] = temp[i];
            }
        }

        private void updateChart()
        {
            int j = 0;
            foreach (var series in chartGraphic.Series)
            {
                series.Points.Clear();
            }
            chartGraphic.ChartAreas[0].AxisY.Minimum = 0;
            chartGraphic.ChartAreas[0].AxisY.Maximum = 100;
            chartGraphic.ChartAreas[0].AxisY.ScaleView.Zoom(0, 100); // -15<= y <=15
            chartGraphic.ChartAreas[0].AxisX.ScaleView.Zoom(1, 10); // -15 <= x <= 2
            chartGraphic.ChartAreas[0].CursorX.IsUserEnabled = true; 
            chartGraphic.ChartAreas[0].CursorX.IsUserSelectionEnabled = true;
            chartGraphic.ChartAreas[0].AxisX.ScaleView.Zoomable = true;
            chartGraphic.ChartAreas[0].AxisX.LabelStyle.Enabled = false;
            chartGraphic.Series[0].IsVisibleInLegend = false;
            this.setValues();
            for (int i = 0; i < 11; i++)
            {
                chartGraphic.Series[0].Points.AddXY(i, this.values[i]);
                chartGraphic.Series[0].ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
                j++;
            }
            Console.WriteLine("Total RAM: " + new Microsoft.VisualBasic.Devices.ComputerInfo().TotalPhysicalMemory);
            Console.WriteLine("Available RAM: " + this.getAvailableRAM());

        }


        public float getCurrentCpuUsage()
        {
            cpuCounter.CategoryName = "Processor";
            cpuCounter.CounterName = "% Processor Time";
            cpuCounter.InstanceName = "_Total";

            return this.cpuCounter.NextValue();
        }

        public float getAvailableRAM()
        {
            return ramCounter.NextValue();
        } 

        #endregion

    }
}
