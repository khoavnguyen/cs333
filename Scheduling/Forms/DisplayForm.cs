using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduling.Forms
{
    public partial class DisplayForm : Form
    {
        Scheduler scheduler = null;
        public DisplayForm()
        {
            InitializeComponent();
        }

        public DisplayForm(Scheduler scheduler)
        {
            this.scheduler = scheduler;
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            //if algo = round robin add combobox quantum
            Algorithm a = scheduler.Algorithm;
            for (int i = 0; i < a.processes.Count; i++)
            {
                string s="";
                Process p = (Process)a.processes[i];
                for (int j = 0; j < p.timeList.Count; j++)
                    s += (int)p.timeList[i];
                MessageBox.Show(s);
            }
        }

    }
}
