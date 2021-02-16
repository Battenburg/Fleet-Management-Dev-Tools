using GACore;
using SchedulingClients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDT.Core.UI.ViewModel
{
    public class TaskStateViewModel : AbstractCallbackClientViewModel<ITaskStateClient>
    {
        public TaskStateViewModel()
            :base()
        {
        }
    }
}
