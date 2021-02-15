using GACore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetClients.Core;

namespace FMDT.Core.UI.ViewModel
{
    public class FleetManagerViewModel : AbstractViewModel<IFleetManagerClient>
    {
        internal FleetManagerViewModel()
        {
        }

        protected override void HandleModelUpdate(IFleetManagerClient oldValue, IFleetManagerClient newValue)
        {
            if (oldValue != null)
            {
                oldValue.Disconnected -= Model_Disconnected;
                oldValue.Connected -= Model_Connected;
            }

            if (newValue != null)
            {
                newValue.Disconnected += Model_Disconnected;
                newValue.Connected += Model_Connected;
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


        private void Model_Disconnected(DateTime obj)
        {
            IsConnected = false;
        }

        private void Model_Connected(DateTime obj)
        {
            IsConnected = true;
        }
    }
}
