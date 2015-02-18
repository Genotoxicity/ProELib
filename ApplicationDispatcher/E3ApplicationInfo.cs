using System;
using System.Diagnostics;

namespace ProELib
{
    public class E3ApplicationInfo
    {
        private string title;
        private int processId;
        private SelectionStatus status;
        private ApplicationDispatcher applicationDispatcher;

        public string StatusReasonDescription
        {
            get
            {
                if (status == SelectionStatus.None)
                    return "Нет открытых проектов";
                if (status == SelectionStatus.NoSelected)
                    return "Нет выбранных проектов";
                return title;
            }
        }

        public int ProcessId
        {
            get
            {
                return processId;
            }
        }

        public string MainWindowTitle
        {
            get
            {
                return title;
            }
        }

        public SelectionStatus Status
        {
            get
            {
                return status;
            }
        }

        public E3ApplicationInfo()
        {
            applicationDispatcher = new ApplicationDispatcher();
            applicationDispatcher.Select();
            status = applicationDispatcher.SelectedStatus;
            if (status == SelectionStatus.Selected)
            {
                processId = applicationDispatcher.SelectedProcessId;
                title = applicationDispatcher.SelectedProjectTitle;
            }
            else
            {
                processId = 0;
                title = String.Empty;
            }
        }

        /*internal E3ApplicationInfo(SelectionStatus status, Process process)
        {
            this.status = status;
            if (process != null)
            {
                this.processId = process.Id;
                this.title = process.MainWindowTitle;
            }
            else
            {
                processId = 0;
                title = String.Empty;
            }
        }*/

    }
}
