using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;

namespace INTROOS_Project
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        #region Event Handlers
        private void main_Load(object sender, EventArgs e)
        {
            loadProcessList();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            Process process = new Process();
            process.StartInfo.FileName = processTxt.Text;
            process.Start();
                     
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

    }
}
