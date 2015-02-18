using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Runtime.InteropServices;
using e3;

namespace ProELib
{
    internal class ApplicationDispatcher
    {
        private const int undefinedSelectionIndex = -1;
        private CT.Dispatcher e3Dispatcher;

        internal SelectionStatus SelectedStatus { get; private set; }

        internal int SelectedProcessId { get; private set; }

        internal string SelectedProjectTitle { get; private set; }

        internal ApplicationDispatcher()
        {
            e3Dispatcher = new CT.Dispatcher();
        }

        internal void Select()
        {
            List<Process> e3Processes = Process.GetProcessesByName("E3.series").ToList<Process>();
            e3Processes.RemoveAll(process => !IsAppropriateProcess(process));
            SelectedProcessId = 0;
            SelectedProjectTitle = String.Empty;
            e3Application app;
            e3Job job;
            switch (e3Processes.Count)
            {
                case 0:
                    SelectedStatus = SelectionStatus.None;
                    break;
                case 1:
                    SelectedStatus = SelectionStatus.Selected;
                    SelectedProcessId = e3Processes[0].Id;
                    app = e3Dispatcher.GetE3ByProcessId(SelectedProcessId) as e3Application;
                    job = app.CreateJobObject();
                    SelectedProjectTitle = job.GetName();
                    Marshal.FinalReleaseComObject(job);
                    break;
                default:
                    int selectedIndex = undefinedSelectionIndex;
                    List<string> projectNames = new List<string>(e3Processes.Count);
                    foreach (Process process in e3Processes)
                    {
                        app = e3Dispatcher.GetE3ByProcessId(process.Id) as e3Application;
                        job = app.CreateJobObject();
                        projectNames.Add(job.GetName());
                        Marshal.FinalReleaseComObject(job);
                    }
                    ApplicationSelectingWindow window = new ApplicationSelectingWindow(projectNames, new Action<int>(index=>selectedIndex = index));
                    window.ShowDialog();
                    if (selectedIndex != undefinedSelectionIndex)
                    {
                        SelectedStatus = SelectionStatus.Selected;
                        SelectedProcessId = e3Processes[selectedIndex].Id;
                        SelectedProjectTitle = projectNames[selectedIndex];
                    }
                    else
                        SelectedStatus = SelectionStatus.NoSelected;
                    break;
            }
        }

        private bool IsAppropriateProcess(Process process)
        {
            e3Application app = e3Dispatcher.GetE3ByProcessId(process.Id) as e3Application;
            if (app == null)  // на случай открытой БД
                return false;
            int jobCount = app.GetJobCount();
            if (jobCount == 0)  // на случай приложения без открытого проекта
                return false;
            return true;
        }


    }
}
