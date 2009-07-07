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
        int arriveTime;
        ArrayList timeList;
        Color color;
        int remainTime;
        int phase;

        public Process(string times)
        {
            timeList = new ArrayList();
            string[] time = times.Split(new char[]{' ','\t'});
            arriveTime = Int32.Parse(time[0]);
            for (int i = 1; i < time.Length; i++)
                timeList.Add(Int32.Parse(time[i]));
            phase = 1;
            remainTime = (int)timeList[phase];
        }
        
        public int ArriveTime
        {
            get
            {
                return arriveTime;
            }
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
        
        public int RemainTime
        {
            get
            {
                return remainTime;
            }
            set
            {
                remainTime = value;
            }
        }

        public void toNextPhase()
        {
            remainTime = (int)timeList[phase++];
        }
    }
}
