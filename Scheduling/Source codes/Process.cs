using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Scheduling
{
    class Process
    {
        int arrive;
        public ArrayList timeList;

        public Process(string times)
        {
            timeList = new ArrayList();
            string[] time = times.Split(new char[]{' ','\t'});
            arrive = Int32.Parse(time[0]);
            for (int i = 1; i < time.Length; i++)
            {
                timeList.Add(Int32.Parse(time[i]));
                MessageBox.Show(time[i]);
            }
        }
    }
}

