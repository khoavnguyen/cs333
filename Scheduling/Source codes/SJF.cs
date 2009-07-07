using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    class SJF:Algorithm
    {
        public override bool schedule(int t)
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

            for(int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (p.ArriveTime == t)
                    readyList.Add(p);
            }
            
            if(currIOProc != null)
                if (currIOProc.RemainTime == 0)
                {
                    if(currIOProc.toNextPhase())
                        readyList.Add(currIOProc);
                    currIOProc = Process.dummy;
                }
            if (currCPUProc == null || currCPUProc == Process.dummy)
            {
                if (readyList.Count == 0)
                    return true;
                Process p = (Process)readyList[0];
                int totalTime = p.totalTime();
                int least = 0;
                for (int i = 0; i < readyList.Count; i++)
                {
                    Process q = (Process)readyList[i];
                    if (q.totalTime() < totalTime)
                    {
                        totalTime = q.totalTime();
                        least = i;
                    }
                }
                currCPUProc = (Process)readyList[least];
                currCPUProc.RemainTime--;
                readyList.RemoveAt(least);
            }
            else
            {
                if (currCPUProc.RemainTime == 0)
                {
                    currCPUProc.toNextPhase();
                    
                        waitingList.Add(currCPUProc);
                        currCPUProc = null;
                    
                }
                else
                {
                    currCPUProc.RemainTime--;
                }
            }
            if (currIOProc == null || currIOProc == Process.dummy)
            {
                if (waitingList.Count == 0)
                    return true;
                Process p = (Process)waitingList[0];
                int totalTime = p.totalTime();
                int least = 0;
                for (int i = 0; i < waitingList.Count; i++)
                {
                    Process q = (Process)waitingList[i];
                    if (q.totalTime() < totalTime)
                    {
                        totalTime = q.totalTime();
                        least = i;
                    }
                }
                currIOProc = (Process)waitingList[least];
                currIOProc.RemainTime--;
                waitingList.RemoveAt(least);
            }
            else
            {
                currIOProc.RemainTime--;
            }
            return true;
        }
    }
}
