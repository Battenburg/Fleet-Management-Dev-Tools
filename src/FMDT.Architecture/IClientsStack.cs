using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FleetClients.Core;
using SchedulingClients.Core;

namespace FMDT.Architecture
{
    public interface IClientsStack : IDisposable
    {
        ISchedulingClient SchedulingClient { get; }

        IFleetManagerClient FleetManagerClient { get; }

        IAgentClient AgentClient { get; }
    }
}
