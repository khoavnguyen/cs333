using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class Algorithm
    {
        protected ArrayList processes;
        protected ArrayList readyList;
        protected ArrayList waitingList;
        protected Process currCPUProc;
        protected Process currIOProc;

        public Process CurrCPUProc
        {
            get
            {
                return currCPUProc;
            }
            set
            {
                currCPUProc = value;
            }
        }
        public Process CurrIOProc
        {
            get
            {
                return currIOProc;
            }
            set
            {
                currIOProc = value;
            }
        }

        public void loadProcesses(string fileName)
        {
            processes = new ArrayList();
            StreamReader file = new StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                Process p = new Process(line);
                processes.Add(p);
            }
            file.Close();
        }
        public virtual void schedule(int t)
        {
            
        }
        public int countProcesses()
        {
            return processes.Count;
        }
    }
}
