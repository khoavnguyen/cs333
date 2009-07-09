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
            return true;
        }

    }
}
