using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Orleans;
using Game.Model;
using static Game.Model.OperationResults;

namespace IoT.GrainInterfaces
{
    /// <summary>
    /// Grain interface ITeamGrain
    /// </summary>
    public interface ITeamGrain : IGrainWithStringKey
    {
        Task<ServiceCallResult> AddPlayer(string playerId);
        Task<ServiceCallResult> RemovePlayer(string playerId);
        Task<List<string>> GetPlayers();
    }
}
