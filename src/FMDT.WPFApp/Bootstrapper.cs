using FMDT.Architecture;
using FMDT.Core;
using FMDT.Core.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FMDT.WPFApp
{
    internal static class Bootstrapper
    {
        public static void Activate()
        {
            IClientsStack clientsStack = new ClientsStack();

            ViewModelLocator.FleetManagerViewModel.Model = clientsStack.FleetManagerClient;
        }
    }
}
