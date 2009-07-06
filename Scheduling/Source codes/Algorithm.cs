using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Scheduling
{
    public class Algorithm
    {
        ArrayList processes;
        ArrayList readyList;
        ArrayList waitingList;

        public void loadProcesses(string fileName)
        {
            processes = new ArrayList();
            StreamReader file = new StreamReader(fileName);
            string line;
            while ((line = file.ReadLine()) != null)
            {
                Process p = new Process(line);
                processes.Add(p);
            }
            file.Close();
        }
    }
}
