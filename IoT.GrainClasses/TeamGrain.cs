using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IoT.GrainInterfaces;
using Orleans;
using static Game.Model.OperationResults;

namespace IoT.GrainClasses
{
    /// <summary>
    /// Grain implementation class TeamGrain.
    /// </summary>
    public class TeamGrain : Grain, ITeamGrain
    {
        public override Task OnActivateAsync()
        {
            Players = new List<string>();

            return base.OnActivateAsync();
        }

        public Task<ServiceCallResult> AddPlayer(string playerId)
        {
            if (Players.Count >= 5)
            {
                // throw new ApplicationException("Can't add to team, team is full");
                return Task.FromResult(ServiceCallResult.ERROR);
            }

            // add to data
            Players.Add(playerId);

            return Task.FromResult(ServiceCallResult.OK);
        }

        public Task<ServiceCallResult> RemovePlayer(string playerId)
        {
            // remove from data
            Players.Remove(playerId);

            return Task.FromResult(ServiceCallResult.OK);
        }

        public Task<List<string>> GetPlayers()
        {
            return Task.FromResult(Players);
        }


        private List<string> Players { get; set; }
    }
}
