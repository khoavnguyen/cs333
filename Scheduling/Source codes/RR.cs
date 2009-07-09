using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class RR:Algorithm
    {
        int quantum = 3, cpuQuantum, ioQuantum;

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
                if (quantum < currCPUProc.RemainTime)
                    cpuQuantum = quantum - 1;
                else
                    cpuQuantum = currCPUProc.RemainTime - 1;
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
                    if (cpuQuantum == 0)
                    {
                        readyList.Add(currCPUProc);
                        CurrCPUProc = null;
                    }
                    else
                    {
                        currCPUProc.RemainTime--;
                        cpuQuantum--;
                    }
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
