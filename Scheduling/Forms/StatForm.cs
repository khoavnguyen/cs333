﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduling.Source_codes;

namespace Scheduling.Forms
{
    public partial class StatForm : Form
    {
        Algorithm algo;

        
        public StatForm()
        {
            InitializeComponent();
        }
        
        public StatForm(ListView lv, Algorithm algo)
        {
            InitializeComponent();
            for (int i = 0; i < lv.Items.Count; i++)
                listView2.Items.Add((ListViewItem)lv.Items[i].Clone());
            this.algo = algo;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Algorithm []algos = { new FCFS(), new SJF(), new SRTF(), new RR() };
            for (int i = 0; i < algos.Length; i++)
            {
                float avgWT, avgTT;
                if (algos[i].GetType() != algo.GetType())
                {
                    string fileName = algo.FileName;
                    algos[i].loadProcesses(fileName);
                    DisplayForm x = new DisplayForm(algos[i]);
                    while (x.step()) ;
                    avgWT = x.waitingTime(); 
                    avgTT = x.turnaroundTime();
                }
                else
                {
                    int n = listView2.Items.Count - 1; 
                    avgWT = float.Parse(listView2.Items[n].SubItems[1].Text);
                    avgTT = float.Parse(listView2.Items[n].SubItems[2].Text);
                }
                listView1.Items[0].SubItems.Add(avgWT.ToString());
                listView1.Items[1].SubItems.Add(avgTT.ToString());
            }
        }
    }
}
