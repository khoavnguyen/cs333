using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using Scheduling.Source_codes;
namespace Scheduling.Forms
{
    public partial class DisplayForm : Form
    {
        Algorithm algo = null;

        ArrayList colorList = null;
        ArrayList color = null;

        int time = -1;

        ListView resultLv;

        public DisplayForm()
        {
            InitializeComponent();
        }

        public DisplayForm(Algorithm algorithm)
        {
            InitializeComponent();
            this.algo = algorithm;

            listView1.OwnerDraw = true;
            colorList = GetColors();
            color = new ArrayList();
            Random x = new Random();
            for (int i = 0; i < algo.countProcesses(); i++)
            {
                comboBox1.Items.Add(algo.getProcName(i));
                color.Add(Color.FromKnownColor((KnownColor)colorList[x.Next(colorList.Count - 1)]));
            }
            reloadForm();
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            if (algo is RR)
            {
                label1.Visible = true;
                textBox1.Visible = true;
                RR rr = (RR)algo;
                rr.Quantum = Int32.Parse(textBox1.Text);
            }
            else
            {
                label1.Visible = false;
                textBox1.Visible = false;
            }
        }

        private void DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flag = e.ColumnIndex > 0 ? TextFormatFlags.HorizontalCenter : TextFormatFlags.Left;
            e.DrawBackground();
            e.DrawText(flag);
        }

        private void DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            e.DrawDefault = true;
            e.DrawBackground();
            e.DrawText();
        }

        private ArrayList GetColors()
        {
            ArrayList colors = new ArrayList();
            foreach (KnownColor known in Enum.GetValues(typeof(KnownColor)))
                colors.Add(known);
            return colors;
        }



        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            ColorDialog colorDialog = new ColorDialog();
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                String s = comboBox1.SelectedItem.ToString();
                for (int i = 1; i < listView1.Items[1].SubItems.Count; i++)
                {
                    if (listView1.Items[1].SubItems[i].Text == s)
                    {
                        listView1.Items[1].SubItems[i].BackColor = colorDialog.Color;
                        color[comboBox1.SelectedIndex] = colorDialog.Color;
                    }
                    if (listView1.Items[3].SubItems[i].Text == s)
                    {
                        listView1.Items[3].SubItems[i].BackColor = colorDialog.Color;
                        color[comboBox1.SelectedIndex] = colorDialog.Color;
                    }
                }
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (!step())
                MessageBox.Show("Finished scheduling.");
            ArrayList ready = algo.ReadyList;
            ArrayList wait = algo.WaitingList;
            listBox1.Items.Clear();
            for (int i = 0; i < ready.Count; i++)
                listBox1.Items.Add(((Process)ready[i]).Name);
            listBox2.Items.Clear();
            for (int j = 0; j < wait.Count; j++)
                listBox2.Items.Add(((Process)wait[j]).Name);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            while (step()) ;
            MessageBox.Show("Finished scheduling.");
        }

        public bool step()
        {
            time++;
            if (!algo.schedule(time))
                return false;

            Process cpu = algo.CurrCPUProc;
            Process io = algo.CurrIOProc;

            if (cpu == null)
                listView1.Items[1].SubItems.Add("OH");
            else if (algo.isDummy(cpu))
                listView1.Items[1].SubItems.Add("");
            else
                listView1.Items[1].SubItems.Add(new ListViewItem.ListViewSubItem(listView1.Items[1],
                    cpu.Name, Color.Black, (Color)color[cpu.ID], listView1.Items[1].Font));
            if (io == null || algo.isDummy(io))
                listView1.Items[3].SubItems.Add("");
            else
                listView1.Items[3].SubItems.Add(new ListViewItem.ListViewSubItem(listView1.Items[3],
                    io.Name, Color.Black, (Color)color[io.ID], listView1.Items[3].Font));

            listView1.Columns.Add((time + 1).ToString());
            return true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            time = -1;
            algo.reloadProcesses();
            listView1.Clear();
            reloadForm();
            MessageBox.Show("Finished undo scheduling.");
        }

        public void reloadForm()
        {
            listView1.Columns.Add("Timeline", 80);
            listView1.Items.Add("");
            listView1.Items.Add("Cpu time");
            listView1.Items.Add("");
            listView1.Items.Add("IO time");
            listView1.Items[1].UseItemStyleForSubItems = false;
            listView1.Items[3].UseItemStyleForSubItems = false;
            listView1.Columns.Add("0");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (algo.undoSchedule(time))
            {
                time--;
                listView1.Items[1].SubItems.RemoveAt(listView1.Items[1].SubItems.Count - 1);
                listView1.Items[3].SubItems.RemoveAt(listView1.Items[3].SubItems.Count - 1);
                listView1.Columns.RemoveAt(listView1.Columns.Count - 1);
                ArrayList ready = algo.ReadyList;
                ArrayList wait = algo.WaitingList;
                listBox1.Items.Clear();
                for (int i = 0; i < ready.Count; i++)
                    listBox1.Items.Add(((Process)ready[i]).Name);
                listBox2.Items.Clear();
                for (int j = 0; j < wait.Count; j++)
                    listBox2.Items.Add(((Process)wait[j]).Name);
            }
            else
                MessageBox.Show("Finished undo scheduling.");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (!algo.isFinished())
                MessageBox.Show("Must finish scheduling first.");
            else
            {               
                setupResult();
                StatForm stats = new StatForm(resultLv, algo);
                stats.Show();
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            RR rr = (RR)algo;
            try
            {
                rr.Quantum = Int32.Parse(textBox1.Text);
            }
            catch
            {
                MessageBox.Show("Quantum must be an integer.");
                return;
            }
            time = -1;
            algo.reloadProcesses();
            listView1.Clear();
            reloadForm();
        }

        int[] wts, tts;
        public float waitingTime()
        {
            int sum = 0, i;
            int n = algo.countProcesses();
            wts = new int[n];
            for (i = 0; i < n; i++)
            {
                string name = algo.getProcName(i);
                int count = 0;
                int j = listView1.Items[1].SubItems.Count - 1;
                while (listView1.Items[1].SubItems[j].Text != name)
                    j--;
                for (; j >= algo.getProcArriveT(i) + 1; j--)
                    if (listView1.Items[1].SubItems[j].Text != name &&
                       listView1.Items[3].SubItems[j].Text != name)
                        count++;
                wts[i] = count;
                sum += count;
            }
            float avgWT = (float)sum / n;
            return avgWT;
        }

        public float turnaroundTime()
        {
            int sum = 0, i;
            int n = algo.countProcesses();
            tts = new int[n];
            for (i = 0; i < algo.countProcesses(); i++)
            {
                string name = algo.getProcName(i);
                int t;
                int j = listView1.Items[1].SubItems.Count - 1;
                while (listView1.Items[1].SubItems[j].Text != name)
                    j--;
                t = j - algo.getProcArriveT(i);
                tts[i] = t;
                sum += t;
            }
            float avgTT = (float)sum / n;
            return avgTT;
        }

        private void setupResult()
        {
            resultLv = new ListView();
            float avgWT = waitingTime();
            float avgTT = turnaroundTime();
            for (int i = 0; i < wts.Length; i++)
            {
                ListViewItem lvi = resultLv.Items.Add(algo.getProcName(i));
                lvi.SubItems.Add(wts[i].ToString());
                lvi.SubItems.Add(tts[i].ToString());
            }      
            ListViewItem l = resultLv.Items.Add("Avg");
            l.SubItems.Add(avgWT.ToString());
            l.SubItems.Add(avgTT.ToString());
        }

    }
}
