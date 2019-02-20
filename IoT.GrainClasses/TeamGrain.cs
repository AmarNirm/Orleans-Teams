using IoT.GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<List<string>> GetPlayers()
        {
            var playerNames = new List<string>();
            foreach (var player in Players)
            {
                var playerGrain = GrainFactory.GetGrain<IPlayerGrain>(player);
                var name = await playerGrain.GetName();
                playerNames.Add(name);
            }
            return playerNames;
        }


        private List<string> Players { get; set; }
    }
}
