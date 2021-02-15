﻿using BaseClients.Architecture;
using FleetClients.Core;
using FMDT.Architecture;
using SchedulingClients.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FMDT.Core
{
    public class ClientsStack : IClientsStack
    {
        public IPAddress IPAddress { get; }

        public ClientsStack(IPAddress ipAddress = null)
        {
            IPAddress = ipAddress 
                ?? IPAddress.Loopback;

            HandleConfigureClients();
        }

        private List<IClient> clients = new List<IClient>();

        ~ClientsStack()
        {
            Dispose(false);
        }
        
        private void HandleConfigureClients()
        {
            clients.Add(SchedulingClients.Core.ClientFactory.CreateTcpAgentClient(IPAddress));
            clients.Add(FleetClients.Core.ClientFactory.CreateTcpFleetManagerClient(IPAddress));
        }

        public void Dispose() => Dispose(true);

        private void Dispose(bool isDisposing)
        {
            if (isDisposed)
                return;

            if (isDisposing)
            {
                clients.ForEach(e => e.Dispose());
            }

            isDisposed = true;
        }

        private bool isDisposed = false;

        public IFleetManagerClient FleetManagerClient 
            => (IFleetManagerClient)clients.FirstOrDefault(e => e is IFleetManagerClient);

        public IAgentClient AgentClient
            => (IAgentClient)clients.FirstOrDefault(e => e is IAgentClient);
    }
}