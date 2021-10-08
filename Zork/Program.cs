using System;


namespace Zork
{
    class Program
    {
        static void Main(string[] args)
        {
            const string defaultRoomsFilename = "Zork.json";
            string roomDescriptionsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            Game game = Game.Load(roomDescriptionsFilename);
            Console.WriteLine("Welcome to Zork!");
            game.Run();
            Console.WriteLine("Thank you for playing!");
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

    }
}
