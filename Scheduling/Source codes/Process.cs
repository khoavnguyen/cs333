using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    class Process
    {
        int arrive;
        ArrayList timeList;

        public Process(string times)
        {
            string[] time = times.Split(new char[]{' ','\t'});
            arrive = Int32.Parse(time[0]);
            for (int i = 1; i < time.Length; i++)
                timeList.Add(Int32.Parse(time[i]));
        }
    }
}
