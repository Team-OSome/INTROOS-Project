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

namespace INTROOS_Project
{
    public partial class main : Form
    {
        public main()
        {
            InitializeComponent();
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            string processString = processTxt.Text;
            Process process = new Process();
            process.StartInfo.FileName = processString;
            process.Start();
            loadProcessList();
        }

        private void main_Load(object sender, EventArgs e)
        {
            loadProcessList();
        }

        private void loadProcessList(){

            processListView.Items.Clear();
            Process[] processList = Process.GetProcesses();
            foreach (Process process in processList){

                ListViewItem item = new ListViewItem(process.ProcessName);
                item.SubItems.Add(process.ProcessName);
                item.Tag = process;
                processListView.Items.Add(item);
                processListView.View = View.Details;

                }
        }

        private void stopBtn_Click(object sender, EventArgs e)
        {
            ListViewItem item = processListView.SelectedItems[0];
            Process process = (Process)item.Tag;
            process.Kill();
            loadProcessList();
        }
    }
}
