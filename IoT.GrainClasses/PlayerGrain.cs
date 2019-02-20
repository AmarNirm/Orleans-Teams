using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using IoT.GrainInterfaces;
using Orleans;
using static Game.Model.OperationResults;

namespace IoT.GrainClasses
{
    /// <summary>
    /// Grain implementation class PlayerGrain.
    /// </summary>
    public class PlayerGrain : Grain, IPlayerGrain
    {
        public override Task OnActivateAsync()
        {
            Teams = new List<string>();

            return base.OnActivateAsync();
        }

        public async Task<ServiceCallResult> AddToTeam(string teamId)
        {
            if (Teams.Count >= 3)
            {
                //throw new ApplicationException("Can't join team, already have 3 teams");
                return ServiceCallResult.ERROR;
            }
            
            // add to the team grain
            var teamGrain = GrainFactory.GetGrain<ITeamGrain>(teamId);
            string myKey;
            this.GetPrimaryKey(out myKey);
            await teamGrain.AddPlayer(myKey);

            // add to cache
            Teams.Add(teamId);
            return ServiceCallResult.OK;
        }

        public async Task<ServiceCallResult> LeaveTeam(string teamId)
        {
            // remove from the team grain
            var teamGrain = GrainFactory.GetGrain<ITeamGrain>(teamId);
            string myKey;
            this.GetPrimaryKey(out myKey);
            await teamGrain.RemovePlayer(myKey);

            // remove from cache
            Teams.Remove(teamId);
            return ServiceCallResult.OK;
        }

        public Task<ServiceCallResult> Setname(string name)
        {
            Name = name;
            return Task.FromResult(ServiceCallResult.OK);
        }

        public string Name { get; set; }
        private List<string> Teams { get; set; }
    }
}
