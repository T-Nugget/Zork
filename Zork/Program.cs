using System;

namespace Zork
{
    class Program
    {
        private static string CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");

            while (true)
            {
                Console.Write($"{CurrentRoom}\n> ");
                Commands command = ToCommand(Console.ReadLine().Trim());
                if (command == Commands.QUIT)
                {
                    break;
                }

                string outputString;
                switch (command)
                {
                    case Commands.QUIT:
                        outputString = "Thank you for playing!";
                        break;
                    case Commands.LOOK:
                        outputString = "This is an open field west of a white house, with a boarded front door.\nA rubber mat saying 'Welcome to Zork!' lies by the door.";
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        outputString = Move(command) ? $"You moved {command}." : "The way is shut!";
                        break;

                    default:
                        outputString = "Unrecognized Command.";
                        break;
                }

                Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {
            bool didMove = false;

            switch (command)
            {
                case Commands.NORTH:
                    if (Location.Row > 0)
                    {
                        Location.Row--;
                        didMove = true;
                    }
                    break;

                case Commands.SOUTH:
                    if (Location.Row < Rooms.GetLength(1) - 1)
                    {
                        Location.Row++;
                        didMove = true;
                    }
                    break;

                case Commands.EAST:
                    if (Location.Column < Rooms.GetLength(1) - 1)
                    {
                        Location.Column++;
                        didMove = true;
                    }
                     break;

                case Commands.WEST:
                    if (Location.Column > 0)
                    {
                        Location.Column--;
                        didMove = true;
                    }
                    break;

            }

            return didMove;
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
        }

        private static readonly string[,] Rooms = {

            { "Dense Woods", "North of House", "Clearing" },
            { "Forest", "West of House", "Behind House" },
            { "Rocky Trail", "South of House", "Canyon View" },

        };

        private static (int Row, int Column) Location = (1, 1);

    }


}
