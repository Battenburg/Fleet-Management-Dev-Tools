using GACore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetClients.Core;
using System.Net;
using GAAPICommon.Core.Dtos;
using System.Windows.Input;
using GACore.Command;

namespace FMDT.Core.UI.ViewModel
{
    public class FleetManagerViewModel : AbstractViewModel<IFleetManagerClient>
    {
        internal FleetManagerViewModel()
        {
            HandleLoadCommands();
        }

        public ICommand FleetManagerCommand { get; private set; }

        private void HandleLoadCommands()
        {
            FleetManagerCommand = new CustomCommand(FleetManagerCommandClick, CanFleetManagerCommandClick);
        }

        private bool CanFleetManagerCommandClick(object obj) => true;

        private Dictionary<FleetManagerOption, Action<FleetManagerViewModel>> optionDictionary
            = new Dictionary<FleetManagerOption, Action<FleetManagerViewModel>>()
            {
                { FleetManagerOption.CreateVirtualVehicle, (e) => e.HandleCreateVirtualVehicle() },
                { FleetManagerOption.RemoveVehicle, (e) => e.HandleRemoveVehicle() }
            };

        private void FleetManagerCommandClick(object obj)
        {
            try
            {
                FleetManagerOption option = (FleetManagerOption)obj;
                optionDictionary[option](this);
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        public void HandleRemoveVehicle()
        {
            IPAddress ipAddress = IPAddress.Parse(IPV4String);
            Model.RemoveVehicle(ipAddress);
        }

        public void HandleCreateVirtualVehicle()
        {
            IPAddress ipAddress = IPAddress.Parse(IPV4String);
            PoseDtoFactory.TryParseString(PoseString, out PoseDto pose);

            Model.CreateVirtualVehicle(ipAddress, pose);
        }

        private string poseString = "0,0,0";

        public string PoseString
        {
            get { return poseString; }
            set
            {
                if (poseString != value)
                {
                    poseString = value;
                    OnNotifyPropertyChanged();
                }
            }
        }

        private string ipV4String = "192.168.0.1";

        public string IPV4String
        {
            get { return ipV4String; }
            set
            {
                if (ipV4String != value)
                {
                    ipV4String = value;
                    OnNotifyPropertyChanged();
                }  
            }
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
