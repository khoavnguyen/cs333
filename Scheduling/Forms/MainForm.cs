﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduling.Forms;
using Scheduling.Source_codes;

namespace Scheduling
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        String[] files;
        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            openFileDialog1.Multiselect = true;
            openFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
           
            openFileDialog1.Title = "Select a text file";
            String[] s = null;
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                s = openFileDialog1.SafeFileNames;
                files = openFileDialog1.FileNames;
                for (int i = 0; i < s.Length; i++)
                {
                    comboBox1.Items.Add(s[i]);
                }
            }
            
        }

        Algorithm[] algos = { new FCFS(), new SJF() };

        private void button2_Click(object sender, EventArgs e)
        {
            int algoIndex = comboBox2.SelectedIndex;
            Scheduler scheduler = new Scheduler();
            scheduler.Algorithm = algos[algoIndex];
            int fileIndex = comboBox1.SelectedIndex;
            scheduler.loadProcesses(files[fileIndex]);

            DisplayForm x = new DisplayForm(scheduler);
            
            x.Show();
        }
        
        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            
        }
    }
}
