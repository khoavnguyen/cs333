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
        String name;
        int id;
        int arriveTime;
        ArrayList timeList;
        int remainTime;
        int phase;
        bool finished;

        public static Process dummy = new Process();
        
        public Process()
        {
            phase = 0;
            finished = true;
            timeList = new ArrayList();
        }

        public Process(string times, int Id)
        {
            timeList = new ArrayList();
            string[] time = times.Split(new char[]{' ','\t'});
            name = time[0];
            arriveTime = Int32.Parse(time[1]);
            for (int i = 2; i < time.Length; i++)
                timeList.Add(Int32.Parse(time[i]));
            phase = 0;
            remainTime = (int)timeList[phase++];
            finished = false;
            id = Id;
        }
        
        public int ArriveTime
        {
            get
            {
                return arriveTime;
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
        public String Name
        {
            get
            {
                return name;
            }
        }
        public int ID
        {
            get
            {
                return id;
            }
        }
        public bool toNextPhase()
        {
            if (phase == timeList.Count)
                finished = true;
            if (finished)
                return false;
            remainTime = (int)timeList[phase++];
            return true;
        }
        public bool toPrevPhase()
        {
            if (phase == 0)
                return false;
            if (phase == 1)
                remainTime = (int)timeList[0];
            if (phase == timeList.Count && finished == true)
                finished = false;
            else
            {
                phase--;
                remainTime = 0;
            }
            return true;
        }
        public int currentPhase()
        {
            return (int)timeList[phase - 1];
        }
        public bool Finished
        {
            get
            {
                return finished;
            }
        }
        public void reload()
        {
            phase = 0;
            remainTime = (int)timeList[phase++];
            finished = false;
        }
    }
}
