using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class FCFS:Algorithm
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

            for (int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (p.ArriveTime == t)
                    readyList.Add(p);
            }

            if (currIOProc != null && currIOProc != Process.dummy)
                if (currIOProc.RemainTime == 0)
                {
                    if (currIOProc.toNextPhase())
                        readyList.Add(currIOProc);
                    currIOProc = Process.dummy;
                }
            if (currCPUProc == null || currCPUProc == Process.dummy)
            {
                if (readyList.Count == 0)
                    return true;
                Process p = (Process)readyList[0];
                currCPUProc = p;
                currCPUProc.RemainTime--;
                readyList.RemoveAt(0);
            }
            else
            {
                if (currCPUProc.RemainTime == 0)
                {
                    if (currCPUProc.toNextPhase())
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
                currIOProc = p;
                currIOProc.RemainTime--;
                waitingList.RemoveAt(0);
            }
            else
            {
                currIOProc.RemainTime--;
            }
            return true;
        }

    }
}
