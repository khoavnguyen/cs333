using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class FCFS : Algorithm
    {
        public override bool scheduleCPU(int t)
        {
            if (currCPUProc == null || currCPUProc == Process.dummy)
            {
                if (currCPUProc == null)
                {
                    remainOH--;
                    if (remainOH <= 0)
                    {
                        currCPUProc = Process.dummy;
                        remainOH = overhead;
                    }
                }
                if (currCPUProc == null || readyList.Count == 0)
                    return true;
                Process p = (Process)readyList[0];
                currCPUProc = p;
                currCPUProc.RemainTime--;
                readyList.RemoveAt(0);
                readyListStack.Add(p);
                readyListStack.Add(0);
                readyListStack.Add(false);
                readyListStack.Add(t);
            }
            else
            {
                if (currCPUProc.RemainTime == 0)
                {
                    if (currCPUProc.toNextPhase())
                    {
                        waitingList.Add(currCPUProc);
                        waitingListStack.Add(currCPUProc);
                        waitingListStack.Add(true);
                        waitingListStack.Add(t);
                    }
                    currCPUProc = null;

                }
                else
                {
                    currCPUProc.RemainTime--;
                }
            }
            return true;
        }

    }
}
