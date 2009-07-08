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
            /*
            if(currCPUProc != null && currCPUProc != Process.dummy)
                if (currCPUProc.RemainTime + 1 == currCPUProc.currentPhase())
                    currCPUProc.toPrevPhase();
                else
                    currCPUProc.RemainTime++;
            if (currIOProc != null && currIOProc != Process.dummy)
                if (currIOProc.RemainTime + 1 == currIOProc.currentPhase())
                    currIOProc.toPrevPhase();
                else
                    currIOProc.RemainTime++;

            Process p = (Process)currProcs[currProcs.Count - 1];
            if (currIOProc == null || currIOProc == Process.dummy)
                if(p != null && p != Process.dummy)
                    p.toPrevPhase();
            currIOProc = p;
            currProcs.RemoveAt(currProcs.Count - 1);

            Process q = (Process)currProcs[currProcs.Count - 1];
            if (currCPUProc == null || currCPUProc == Process.dummy)
                if (q != null && q != Process.dummy)
                    q.toPrevPhase();
            currCPUProc = q;
            currProcs.RemoveAt(currProcs.Count - 1);
            */
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
    }
}
