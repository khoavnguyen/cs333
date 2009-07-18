using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Scheduling.Forms;
using Scheduling.Source_codes;
using System.IO;

namespace Scheduling
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        ArrayList files = new ArrayList();
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
                foreach(String path in openFileDialog1.FileNames)
                    files.Add(path);
                for (int i = 0; i < s.Length; i++)
                {
                    comboBox1.Items.Add(s[i]);
                }
            }
        }


        private void button2_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex == -1)
            {
                MessageBox.Show("You must select an input file first.");
                return;
            }
            if (comboBox2.SelectedIndex == -1)
            {
                MessageBox.Show("You must select a scheduling strategy first.");
                return;
            }
            int fileIndex = comboBox1.SelectedIndex;
            int algoIndex = comboBox2.SelectedIndex;
            InputForm i = new InputForm((String)files[fileIndex]);
            if (!i.checkFormat())
            {
                MessageBox.Show("The file you selected has a wrong format. Please edit it first.");
                i.External = false;
                i.ShowDialog();
                InputForm j = new InputForm((String)files[fileIndex]);
                if (!j.checkFormat())
                {
                    MessageBox.Show("Please choose another input file.");
                    return;
                }
            }
            if (textBox1.Text == "")
            {
                MessageBox.Show("Please choose an overhead for switching processes.");
                return;
            }
            try
            {
                int k = Int32.Parse(textBox1.Text);
                if (k <= 0)
                {
                    MessageBox.Show("Overhead must be positive.");
                    return;
                }
            }
            catch
            {
                MessageBox.Show("Overhead must be an integer.");
                return;
            }

            Algorithm algorithm = newAlgorithm(algoIndex);
            algorithm.loadProcesses((String)files[fileIndex]);
            algorithm.Overhead = Int32.Parse(textBox1.Text);

            
            String text = "";
            switch (algoIndex)
            {
                case 0:
                    text = "First come first serve strategy";
                    break;
                case 1:
                    text = "Shortest job first strategy";
                    break;
                case 2:
                    text = "Shortest remaining time strategy";
                    break;
                case 3:
                    text = "Round robin strategy";
                    break;
                default:
                    text = "";
                    break;
            }
            DisplayForm x = new DisplayForm(algorithm);
            x.Text = text;
            x.Show();
        }

        private Algorithm newAlgorithm(int type)
        {
            switch (type)
            {
                case 0:
                    return new FCFS();
                case 1:
                    return new SJF();
                case 2:
                    return new SRTF();
                case 3:
                    return new RR();
                default:
                    return null;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            InputForm x = new InputForm();
            x.ShowDialog();
        }

    }
}
