using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    class Scheduler
    {
        private Algorithm schedulingAlgo;
        public Algorithm Algorithm
        {
            get
            {
                return schedulingAlgo;
            }
            set
            {
                schedulingAlgo = value;
            }
        }
        public void loadProcesses(string fileName)
        {
            schedulingAlgo.loadProcesses(fileName);
        }
    }
}
