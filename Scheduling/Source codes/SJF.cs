using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    class SJF:Algorithm
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
                int Time = p.RemainTime;
                int least = 0;
                for (int i = 0; i < readyList.Count; i++)
                {
                    Process q = (Process)readyList[i];
                    if (q.RemainTime < Time)
                    {
                        Time = q.RemainTime;
                        least = i;
                    }
                }
                currCPUProc = (Process)readyList[least];
                currCPUProc.RemainTime--;
                readyList.RemoveAt(least);
                readyListStack.Add(currCPUProc);
                readyListStack.Add(least);
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
