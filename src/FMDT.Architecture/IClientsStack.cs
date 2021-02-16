using FleetClients.Core;
using SchedulingClients;
using SchedulingClients.Core;
using System;

namespace FMDT.Architecture
{
    public interface IClientsStack : IDisposable
    {
        ISchedulingClient SchedulingClient { get; }

        IFleetManagerClient FleetManagerClient { get; }

        IAgentClient AgentClient { get; }

        ITaskStateClient TaskStateClient { get; }
    }
}
