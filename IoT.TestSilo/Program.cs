using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;

namespace IoT.TestSilo
{
    /// <summary>
    /// Orleans test silo host
    /// </summary>
    public class Program
    {
        static void Main(string[] args)
        {
            // First, configure and start a local silo
            var siloConfig = ClusterConfiguration.LocalhostPrimarySilo();
            var silo = new SiloHost("TestSilo", siloConfig);
            silo.InitializeOrleansSilo();
            silo.StartOrleansSilo();

            Console.WriteLine("Silo started.");

            // Then configure and connect a client.
            //var clientConfig = ClientConfiguration.LocalhostSilo();
            //var client = new ClientBuilder().UseConfiguration(clientConfig).Build();
            //client.Connect().Wait();
            GrainClient.Initialize(ClientConfiguration.LocalhostSilo());

            Console.WriteLine("Client connected.");

            //
            // This is the place for your test code.
            //
            //Orleans.GrainClient.Initialize();
            //var grain = GrainClient.GrainFactory.GetGrain<IDeviceGrain>(0);
            var game = new GameController();
            while (true)
            {
                var line = double.Parse(Console.ReadLine());
                switch (line)
                {
                    case 1: // CreatePlayer
                        Console.WriteLine("CreatePlayer");
                        Console.WriteLine("Enter Player ID:");
                        var id = Console.ReadLine();
                        Console.WriteLine("Enter Player Name:");
                        var name = Console.ReadLine();
                        game.CreatePlayer(id,name);
                        break;
                    case 2: // CreateTeam
                        Console.WriteLine("CreateTeam");
                        Console.WriteLine("Enter Team ID:");
                        game.CreateTeam(Console.ReadLine());
                        break;
                    case 3: // AddPlayerToTeam
                        Console.WriteLine("AddPlayerToTeam");
                        Console.WriteLine("Player ID:");
                        var playerId = Console.ReadLine();
                        Console.WriteLine("Team ID:");
                        var teamId = Console.ReadLine();
                        game.AddPlayerToTeam(playerId, teamId);
                        break;
                    case 4: // RemovePlayerFromTeam
                        Console.WriteLine("RemovePlayerFromTeam");
                        Console.WriteLine("Player ID:");
                        Console.WriteLine("Team ID:");
                        game.RemovePlayerFromTeam(Console.ReadLine(), Console.ReadLine());
                        break;
                    case 5: // ListPlayers
                        Console.WriteLine("ListPlayers");
                        Console.WriteLine("Enter Team ID:");
                        var players = game.ListPlayers(Console.ReadLine()).Result;
                        Console.WriteLine(String.Join(", ", players));
                        break;
                    default:
                        Console.WriteLine("no such command");
                        break;

                }
            }


            //// End
            //Console.WriteLine("\nPress Enter to terminate...");
            //Console.ReadLine();

            // Shut down
            //client.Close();
            silo.ShutdownOrleansSilo();
        }

    }
}
