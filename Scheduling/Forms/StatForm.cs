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
    public partial class StatForm : Form
    {
        public StatForm()
        {
            InitializeComponent();
        }
        
        public StatForm(ListView lv)
        {
            InitializeComponent();
            this.Controls.Add(lv);
        }
    }
}
