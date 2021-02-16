using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDT.Core.UI.ViewModel
{
    public static class ViewModelLocator
    {
        public static FleetManagerViewModel FleetManagerViewModel { get; } = new FleetManagerViewModel();

        public static SchedulerViewModel SchedulerViewModel { get; } = new SchedulerViewModel();

        public static TaskStateViewModel TaskStateViewModel { get; } = new TaskStateViewModel();
    }
}
