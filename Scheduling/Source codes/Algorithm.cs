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
        public bool isDummy(Process x) { return x == Process.dummy; }
        protected ArrayList processes;
        protected ArrayList readyList;
        protected ArrayList waitingList;
        protected Process currCPUProc;
        protected Process currIOProc;

        public Algorithm()
        {
            processes = new ArrayList();
            readyList = new ArrayList();
            waitingList = new ArrayList();
            currCPUProc = null;
            currIOProc = null;
        }
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
            StreamReader file = new StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                Process p = new Process(line);
                processes.Add(p);
            }
            file.Close();
        }
        public virtual bool schedule(int t)
        {
            return false;
        }
        public int countProcesses()
        {
            return processes.Count;
        }
        public String getProcName(int index)
        {
            return ((Process)processes[index]).Name;
        }
    }
}
