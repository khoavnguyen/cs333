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
        int time = -1;

        public DisplayForm()
        {
            InitializeComponent();
        }

        public DisplayForm(Scheduler scheduler)
        {
            //algo = algorithm;
            InitializeComponent();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            //if algo = round robin add combobox quantum
        }

        private void button3_Click(object sender, EventArgs e)
        {
            time++;
            scheduler.schedule(time);
        }

    }
}
