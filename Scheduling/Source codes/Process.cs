using System;
using System.Drawing;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class Process
    {
        int arrive;
        ArrayList timeList;
        Color color;
        int remainTime;

        public Process(string times)
        {
            timeList = new ArrayList();
            string[] time = times.Split(new char[]{' ','\t'});
            arrive = Int32.Parse(time[0]);
            for (int i = 1; i < time.Length; i++)
                timeList.Add(Int32.Parse(time[i]));
        }
        public Color Color
        {
            get
            {
                return color;
            }
            set
            {
                color = value;
            }
        }
    }
}
