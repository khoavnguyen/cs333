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
        Algorithm algo = null;
        public DisplayForm()
        {
            InitializeComponent();
        }

        public DisplayForm(Algorithm algorithm)
        {
            algo = algorithm;
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            //if algo = round robin add combobox quantum
        }

    }
}
