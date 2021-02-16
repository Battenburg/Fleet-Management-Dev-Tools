using BaseClients.Architecture;
using GACore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDT.Core.UI.ViewModel
{
    public abstract class AbstractCallbackClientViewModel<T> : AbstractViewModel<T> where T:class, ICallbackClient
    {
        public AbstractCallbackClientViewModel()
            :base()
        {
        }

        protected override void HandleModelUpdate(T oldValue, T newValue)
        {
            if (oldValue != null)
            {
                oldValue.Connected -= Model_Connected;
                oldValue.Disconnected -= Model_Disconnected;
            }

            if (newValue != null)
            {
                newValue.Connected += Model_Connected;
                newValue.Disconnected += Model_Disconnected;
            }

            HandleRefresh();

            base.HandleModelUpdate(oldValue, newValue);
        }

        protected virtual void HandleRefresh()
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
