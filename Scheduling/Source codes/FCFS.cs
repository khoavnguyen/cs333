using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class FCFS:Algorithm
    {
        int overhead = 1;
        int remainOvh;

        public override void schedule(int t)
        {
            if (currCPUProc == null) 
            {
                if (remainOvh == 0)
                {
                    Process p = (Process)readyList[0];
                    readyList.RemoveAt(0);
                    currCPUProc = p;
                    currCPUProc.RemainTime--;
                }
                else
                    remainOvh--;
            }
            else
            {
                if (currCPUProc.RemainTime == 0)
                {
                    remainOvh = overhead;
                    currCPUProc.toNextPhase();
                    if (CurrIOProc != null)
                        waitingList.Add(currCPUProc);
                    else
                        CurrIOProc = currCPUProc;
                    currCPUProc = null;
                }
                else
                {
                    currCPUProc.RemainTime--;
                }
            }

            if(currIOProc != null)
                if (currIOProc.RemainTime == 0)
                {
                    currIOProc.toNextPhase();
                    if (CurrCPUProc != null)
                        readyList.Add(currIOProc);
                    else
                        CurrCPUProc = currIOProc;
                    CurrIOProc = (Process)waitingList[0];
                    waitingList.RemoveAt(0);
                }
                else
                {
                    currIOProc.RemainTime--;
                }

            for (int i = 0; i < processes.Count; i++)
            {
                Process p = (Process)processes[i];
                if (p.ArriveTime == t)
                    readyList.Add(p);
            }
        }

    }
}
