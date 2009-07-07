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
        ArrayList processes;
        ArrayList readyList;
        ArrayList waitingList;
        Process currCPUProc;
        Process currIOProc;

        public Process CurrCPUProc
        {
            get
            {
                return currCPUProc;
            }
        }
        public Process CurrIOProc
        {
            get
            {
                return currIOProc;
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
    }
}
