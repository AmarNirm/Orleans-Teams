using Game.Model;
using IoT.GrainInterfaces;
using Orleans;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace IoT.TestSilo
{
    public class GameController
    {

        public async Task<OperationResults.ServiceCallResult> CreatePlayer(string id, string name)
        {
            //var guid = Guid.NewGuid();
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(id);
            var result = await player.Setname(name);
            return result;
        }

        public void CreateTeam(string key)
        {
            GrainClient.GrainFactory.GetGrain<ITeamGrain>(key);
        }

        public async Task<OperationResults.ServiceCallResult> AddPlayerToTeam(string playerKey, string teamId)
        {
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(playerKey);
            var result = await player.AddToTeam(teamId);
            return result;
        }

        public async Task<OperationResults.ServiceCallResult> RemovePlayerFromTeam(string playerKey, string teamId)
        {
            var player = GrainClient.GrainFactory.GetGrain<IPlayerGrain>(playerKey);
            var result = await player.LeaveTeam(teamId);
            return result;
        }

        public async Task<List<string>> ListPlayers(string teamId)
        {
            var team = GrainClient.GrainFactory.GetGrain<ITeamGrain>(teamId);
            var players = await team.GetPlayers();
            return players;
        }
    }
}
