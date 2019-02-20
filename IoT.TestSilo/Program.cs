using Orleans;
using Orleans.Runtime.Configuration;
using Orleans.Runtime.Host;
using System;
using Game.Model;

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



            var game = new GameController();

            // mock
            var nir = game.CreatePlayer("p1", "p1name").Result;
            nir = game.CreatePlayer("p2", "p2name").Result;
            nir = game.CreatePlayer("p3", "p3name").Result;
            nir = game.CreatePlayer("p4", "p4name").Result;
            game.CreateTeam("t1");
            nir = game.AddPlayerToTeam("p1", "t1").Result;
            nir = game.AddPlayerToTeam("p2", "t1").Result;
            nir = game.AddPlayerToTeam("p3", "t1").Result;
            var players2 = game.ListPlayers("t1").Result;
            Console.WriteLine(String.Join(", ", players2));

            while (true)
            {
                var line = double.Parse(Console.ReadLine());
                OperationResults.ServiceCallResult status = OperationResults.ServiceCallResult.ERROR;
                switch (line)
                {
                    case 1: // CreatePlayer
                        Console.WriteLine("CreatePlayer");
                        Console.WriteLine("Enter Player ID:");
                        var id = Console.ReadLine();
                        Console.WriteLine("Enter Player Name:");
                        var name = Console.ReadLine();

                        status = game.CreatePlayer(id,name).Result;
                        if (status == OperationResults.ServiceCallResult.ERROR)
                            Console.WriteLine("error creating player");
                        break;

                    case 2: // CreateTeam
                        Console.WriteLine("CreateTeam");
                        Console.WriteLine("Enter Team ID:");
                        game.CreateTeam(Console.ReadLine());
                        status = OperationResults.ServiceCallResult.OK;
                        break;

                    case 3: // AddPlayerToTeam
                        Console.WriteLine("AddPlayerToTeam");
                        Console.WriteLine("Player ID:");
                        var playerId = Console.ReadLine();
                        Console.WriteLine("Team ID:");
                        var teamId = Console.ReadLine();
                        status = game.AddPlayerToTeam(playerId, teamId).Result;
                        if (status == OperationResults.ServiceCallResult.ERROR)
                            Console.WriteLine("error AddPlayerToTeam");

                        break;

                    case 4: // RemovePlayerFromTeam
                        Console.WriteLine("RemovePlayerFromTeam");
                        Console.WriteLine("Player ID:");
                        Console.WriteLine("Team ID:");
                        status = game.RemovePlayerFromTeam(Console.ReadLine(), Console.ReadLine()).Result;
                        if (status == OperationResults.ServiceCallResult.ERROR)
                            Console.WriteLine("error RemovePlayerFromTeam");

                        break;

                    case 5: // ListPlayers
                        Console.WriteLine("ListPlayers");
                        Console.WriteLine("Enter Team ID:");
                        var players = game.ListPlayers(Console.ReadLine()).Result;
                        Console.WriteLine(String.Join(", ", players));
                        status = OperationResults.ServiceCallResult.OK;
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
