using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
namespace Scheduling.Forms
{
    public partial class DisplayForm : Form
    {
        Algorithm algo = null;

        ArrayList colorList = null;
        ArrayList color = null;


        int time = -1;


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
                color.Add(Color.FromKnownColor((KnownColor)colorList[x.Next(173)]));
            }
        }

        private void DisplayForm_Load(object sender, EventArgs e)
        {
            //if algo = round robin add combobox quantum
           // if (scheduler.Algorithm is RR)
           //     textBox1.Enabled = true;
            listView1.Columns.Add("Timeline", 80);
            listView1.Items.Add("");
            listView1.Items.Add("Cpu time"); 
            listView1.Items.Add("");
            listView1.Items.Add("IO time");
            listView1.Items[1].UseItemStyleForSubItems = false;
            listView1.Items[3].UseItemStyleForSubItems = false;
            listView1.Columns.Add("0");
        }


        private void DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        {
            TextFormatFlags flag = e.ColumnIndex > 0 ?  TextFormatFlags.HorizontalCenter : TextFormatFlags.Left;
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
                for (int i = 1; i <= time + 1; i++)
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
            step();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            step();
        }

        private bool step()
        {
            time++;

            if(!algo.schedule(time))
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
    }
}
