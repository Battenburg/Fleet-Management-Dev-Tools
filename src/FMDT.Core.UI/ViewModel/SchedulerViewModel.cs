using SchedulingClients.Core;
using SchedulingClients.Core.SchedulingServiceReference;

namespace FMDT.Core.UI.ViewModel
{
    public class SchedulerViewModel : AbstractCallbackClientViewModel<ISchedulingClient>
    {
        public SchedulerViewModel()
        {
        }

        protected override void HandleModelUpdate(ISchedulingClient oldValue, ISchedulingClient newValue)
        {
            if (oldValue != null)
                oldValue.Updated -= Model_Updated;

            if (newValue != null)           
                newValue.Updated += Model_Updated;

            base.HandleModelUpdate(oldValue, newValue);
        }

        private SchedulerStateDto schedulerState = null;

        public SchedulerStateDto SchedulerState
        {
            get { return schedulerState; }
            private set
            {
                if (schedulerState != value)
                {
                    schedulerState = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private void Model_Updated(SchedulerStateDto schedulerStateDto)
        {
            SchedulerState = schedulerStateDto;
        }
    }
}
