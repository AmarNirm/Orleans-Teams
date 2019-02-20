using System;
using System.Threading.Tasks;
using Orleans;
using static Game.Model.OperationResults;

namespace IoT.GrainInterfaces
{
    /// <summary>
    /// Grain interface IPlayerGrain
    /// </summary>
    public interface IPlayerGrain : IGrainWithStringKey
    {
        Task<ServiceCallResult> AddToTeam(string teamId);
        Task<ServiceCallResult> LeaveTeam(string teamId);

        Task<ServiceCallResult> Setname(string name);
    }
}
