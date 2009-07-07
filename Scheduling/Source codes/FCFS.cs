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
            if (currCPUProc == null) // currCPU=null thi ve cai cell overhead ra
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
                    currCPUProc = null;
                    remainOvh = overhead;
                    if (CurrIOProc != null)
                    {
                        currCPUProc.toNextPhase();
                        waitingList.Add(currCPUProc);
                    }
                    else
                        CurrIOProc = currCPUProc;
                }
                else
                {
                    currCPUProc.RemainTime--;
                }
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
