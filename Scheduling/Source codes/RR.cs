using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling.Source_codes
{
    class RR:Algorithm
    {
        int quantum; 
        int cpuQuantum;

        public int Quantum
        {
            set
            {
                quantum = value;
            }
        }

        public override bool scheduleCPU(int t)
        {
            if (currCPUProc == null || currCPUProc == Process.dummy)
            {
                if (currCPUProc == null)
                {
                    remainOH--;
                    if (remainOH == 0)
                    {
                        currCPUProc = Process.dummy;
                        remainOH = overhead;
                    }
                }
                if (currCPUProc == null || readyList.Count == 0)
                    return true;
                Process p = (Process)readyList[0];
                currCPUProc = p;
                if (quantum < currCPUProc.RemainTime)
                    cpuQuantum = quantum - 1;
                else
                    cpuQuantum = currCPUProc.RemainTime - 1;
                currCPUProc.RemainTime--;
                p = (Process)readyList[0];
                readyListStack.Add(p);
                readyListStack.Add(0);
                readyListStack.Add(false);
                readyListStack.Add(t);
                readyList.RemoveAt(0);
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
                    if (cpuQuantum == 0)
                    {
                        if (readyList.Count != 0)
                        {
                            readyList.Add(currCPUProc);
                            readyListStack.Add(currCPUProc);
                            readyListStack.Add(true);
                            readyListStack.Add(t);
                            CurrCPUProc = null;
                        }
                        else
                        {
                            currCPUProc.RemainTime--;
                            cpuQuantum = quantum - 1;
                        }
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
