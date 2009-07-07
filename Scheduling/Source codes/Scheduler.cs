using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class Scheduler
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
        
        public void schedule(int t)
        {
            schedulingAlgo.schedule(t);
        }
    }
}
