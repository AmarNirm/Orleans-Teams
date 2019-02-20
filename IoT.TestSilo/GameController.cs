using IoT.GrainInterfaces;
using Orleans;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoT.TestSilo
{
    public class GameController
    {

        public async void CreatePlayer(string id, string name)
        {
            //var guid = Guid.NewGuid();
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(id);
            await player.Setname(name);
        }

        public void CreateTeam(string key)
        {
            GrainClient.GrainFactory.GetGrain<ITeamGrain>(key);
        }

        public async void AddPlayerToTeam(string playerKey, string teamId)
        {
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(playerKey);
            await player.AddToTeam(teamId);
        }

        public async void RemovePlayerFromTeam(string playerKey, string teamId)
        {
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(playerKey);
            await player.LeaveTeam(teamId);
        }

        public async Task<List<string>> ListPlayers(string teamId)
        {
            var team = GrainClient.GrainFactory.GetGrain<ITeamGrain>(teamId);
            var players = await team.GetPlayers();
            return players;
        }
    }
}
