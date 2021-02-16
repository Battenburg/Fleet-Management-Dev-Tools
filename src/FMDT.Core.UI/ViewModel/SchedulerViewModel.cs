using GACore;
using SchedulingClients.Core;
using SchedulingClients.Core.SchedulingServiceReference;
using System;

namespace FMDT.Core.UI.ViewModel
{
    public class SchedulerViewModel : AbstractViewModel<ISchedulingClient>
    {
        public SchedulerViewModel()
        {
        }

        protected override void HandleModelUpdate(ISchedulingClient oldValue, ISchedulingClient newValue)
        {
            if (oldValue != null)
            {
                oldValue.Disconnected -= Model_Disconnected;
                oldValue.Connected -= Model_Connected;
                oldValue.Updated -= Model_Updated;
            }

            if (newValue != null)
            {
                newValue.Disconnected += Model_Disconnected;
                newValue.Connected += Model_Connected;
                newValue.Updated += Model_Updated;
            }

            HandleRefresh();

            base.HandleModelUpdate(oldValue, newValue);
        }

        private void HandleRefresh()
        {
            IsConnected = (Model != null) && Model.IsConnected;
        }

        private bool isConnected = false;

        public bool IsConnected
        {
            get { return isConnected; }
            private set
            {
                if (isConnected != value)
                {
                    isConnected = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private void Model_Disconnected(DateTime dateTime)
        {
            IsConnected = false;
        }

        private void Model_Connected(DateTime dateTime)
        {
            IsConnected = true;
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
