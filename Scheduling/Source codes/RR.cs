using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class RR:Algorithm
    {
        int quantum = 3, cpuQuantum;

        public override bool scheduleCPU(int t)
        {
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
            return true;
        }
    }
}
