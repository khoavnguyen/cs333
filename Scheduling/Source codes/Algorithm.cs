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
        protected ArrayList readyListStack;
        protected ArrayList waitingListStack;
        protected ArrayList currProcs;
        public Algorithm()
        {
            processes = new ArrayList();
            readyList = new ArrayList();
            waitingList = new ArrayList();
            readyListStack = new ArrayList();
            waitingListStack = new ArrayList();
            currProcs = new ArrayList();
            currCPUProc = Process.dummy;
            currIOProc = Process.dummy;
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
            int i = 0;
            while ((line = file.ReadLine()) != null)
            {
                Process p = new Process(line, i++);
                processes.Add(p);
            }
            file.Close();
        }
        public bool schedule(int t)
        {
            bool finished = true;
            for (int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (!p.Finished)
                    finished = false;
            }
            if (finished)
                return false;
            currProcs.Add(currCPUProc);
            currProcs.Add(currIOProc);
            for (int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (p.ArriveTime == t)
                {
                    readyList.Add(p);
                    readyListStack.Add(p);
                    readyListStack.Add(true);
                    readyListStack.Add(t);
                }
            }

            if (currIOProc != null && currIOProc != Process.dummy)
                if (currIOProc.RemainTime == 0)
                {
                    if (currIOProc.toNextPhase())
                    {
                        readyList.Add(currIOProc);
                        readyListStack.Add(currIOProc);
                        readyListStack.Add(true);
                        readyListStack.Add(t);
                    }
                    currIOProc = Process.dummy;
                }

            scheduleCPU(t);

            if (currIOProc == null || currIOProc == Process.dummy)
            {
                if (waitingList.Count == 0)
                    return true;
                currIOProc = (Process)waitingList[0];
                currIOProc.RemainTime--;
                waitingList.RemoveAt(0);
                waitingListStack.Add(currIOProc);
                waitingListStack.Add(false);
                waitingListStack.Add(t);
            }
            else
            {
                currIOProc.RemainTime--;
            }
            return true;
        }
        public virtual bool scheduleCPU(int t)
        {
            return false;
        }
        public bool undoSchedule(int t)
        {
            if (currProcs.Count == 0)
                return false;

            Process p = (Process)currProcs[currProcs.Count - 1];
            if ((currIOProc == null || currIOProc == Process.dummy)
                 && (p != null && p != Process.dummy))
                p.toPrevPhase();
            else if ((p == null || p == Process.dummy) &&
                (currIOProc != null && currIOProc != Process.dummy))
                currIOProc.RemainTime++;
            else if ((currIOProc != null && currIOProc != Process.dummy)
                && (p != null && p != Process.dummy))
                if (p == currIOProc)
                    p.RemainTime++;
                else
                {
                    p.toPrevPhase();
                    currIOProc.RemainTime++;
                }
            currIOProc = p;
            currProcs.RemoveAt(currProcs.Count - 1);

            p = (Process)currProcs[currProcs.Count - 1];
            if ((currCPUProc == null || currCPUProc == Process.dummy)
                 && (p != null && p != Process.dummy))
                p.toPrevPhase();
            else if ((p == null || p == Process.dummy) &&
                (currCPUProc != null && currCPUProc != Process.dummy))
                currCPUProc.RemainTime++;
            else if ((currCPUProc != null && currCPUProc != Process.dummy)
                && (p != null && p != Process.dummy))
                if (p == currCPUProc)
                    p.RemainTime++;
                else
                {
                    p.toPrevPhase();
                    currCPUProc.RemainTime++;
                }
            currCPUProc = p;
            currProcs.RemoveAt(currProcs.Count - 1);

            while(waitingListStack.Count > 0 && (int)waitingListStack[waitingListStack.Count - 1] == t)
                {
                    waitingListStack.RemoveAt(waitingListStack.Count - 1);
                    if ((bool)waitingListStack[waitingListStack.Count - 1] == true)
                        waitingList.Remove(waitingListStack[waitingListStack.Count - 2]);
                    else
                        waitingList.Add(waitingListStack[waitingListStack.Count - 2]);
                    
                    waitingListStack.RemoveAt(waitingListStack.Count - 1);
                    waitingListStack.RemoveAt(waitingListStack.Count - 1);
                }
            while(readyListStack.Count > 0 && (int)readyListStack[readyListStack.Count - 1] == t)
                {
                    readyListStack.RemoveAt(readyListStack.Count - 1);
                    if ((bool)readyListStack[readyListStack.Count - 1] == true)
                        readyList.Remove(readyListStack[readyListStack.Count - 2]);
                    else
                        readyList.Add(readyListStack[readyListStack.Count - 2]);
                    
                    readyListStack.RemoveAt(readyListStack.Count - 1);
                    readyListStack.RemoveAt(readyListStack.Count - 1);
                }
            
            return true;
        }
        public int countProcesses()
        {
            return processes.Count;
        }
        public String getProcName(int index)
        {
            return ((Process)processes[index]).Name;
        }
        public void reloadProcesses()
        {
            for (int i = 0; i < processes.Count; i++)
                ((Process)processes[i]).reload();
            readyList.Clear();
            waitingList.Clear();
            readyListStack.Clear();
            waitingListStack.Clear();
            currProcs.Clear();
            currCPUProc = null;
            currIOProc = null;
        }
        public bool isFinished()
        {
            bool finished = true;
            for (int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (!p.Finished)
                    finished = false;
            }
            if (!finished)
                return false;

            return true;
        }
    }
}
