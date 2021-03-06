﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
namespace Scheduling.Forms
{
    public partial class InputForm : Form
    {
        public InputForm()
        {
            InitializeComponent();
        }

        bool external = false;
        String filepath = null;
        public bool External
        {
            get
            {
                return external;
            }
            set
            {
                external = value;
            }
        }
        public InputForm(String file)
        {
            InitializeComponent();
            Text = "Edit file";
            button3.Enabled = false;
            external = true;
            filepath = file;
            loadFile(file);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                MessageBox.Show("You must type something first.");
                return;
            }

            if(!checkFormat())
                return;

            if (filepath == null)
                save();
            else
                overwrite();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void textBox1_MouseEnter(object sender, EventArgs e)
        {
            toolTip1.Show("Format: ProcessName ArriveTime CPUBurst IOBurst ... CPUBurst", (IWin32Window)sender);
        }


        private void button3_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            if (open.ShowDialog() == DialogResult.OK)
                loadFile(open.FileName);
        }

        public void loadFile(String filepath)
        {
            StreamReader file = new StreamReader(filepath);
            String line;
            textBox1.Text = "";
            while ((line = file.ReadLine()) != null)
            {
                textBox1.Text += line + "\r\n";
            }
            file.Close();
        }

        public bool checkFormat()
        {
            String[] processes = textBox1.Text.Split(new String [] {"\n", "\r"}, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < processes.GetLength(0); i++)
            {
                String[] process = processes[i].Split(new String[] { " ", "\t" }, StringSplitOptions.RemoveEmptyEntries);
                int count = process.GetLength(0);
                if (count < 3 || (count % 2) == 0)
                {
                    if(external == false)
                        MessageBox.Show("Wrong format on line " + (i + 1).ToString() + ".");
                    return false;
                }

                for (int j = 1; j < count; j++)
                    try
                    {
                        Int32.Parse(process[j]);
                    }
                    catch
                    {
                        if (external == false)
                            MessageBox.Show("Times must be integers. Wrong format on line " + (i + 1).ToString() + ".");
                        return false;
                    }
            }
            return true;
        }

        private void save()
        {
            String[] processes = textBox1.Text.Split(new String[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);

            SaveFileDialog save = new SaveFileDialog();
            save.DefaultExt = "txt";
            save.AddExtension = true;
            save.Filter = "Text file (*.txt)|*.txt|All files (*.*)|*.*";
            save.RestoreDirectory = true;
            save.Title = "Save input file to";

            Stream file = null;
            if (save.ShowDialog() == DialogResult.OK)
            {
                if ((file = save.OpenFile()) != null)
                {
                    StreamWriter write = new StreamWriter(file);
                    foreach (String s in processes)
                    {
                        write.WriteLine(s);
                        write.Flush();
                    }
                    file.Close();
                }
                MessageBox.Show("File saved.");
            }
            else
            {
                MessageBox.Show("The file was not saved.");
            }
        }
        private void overwrite()
        {
            String[] processes = textBox1.Text.Split(new String[] { "\n", "\r" }, StringSplitOptions.RemoveEmptyEntries);
            
            StreamWriter write = new StreamWriter(filepath);
            foreach (String s in processes)
            {
                write.WriteLine(s);
                write.Flush();
            }
            write.Close();
            MessageBox.Show("File saved.");
        }
    }
}
