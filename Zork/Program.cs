using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;



namespace Zork
{
    class Program
    {
        private static Room CurrentRoom
        {
            get
            {
                return Rooms[Location.Row, Location.Column];
            }
        }

        private enum CommandLineArguments
        {
            RoomsFilename = 0
        }

        private static readonly Dictionary<string, Room> RoomMap;
        static Program()
        {
            RoomMap = new Dictionary<string, Room>();
            foreach (Room room in Rooms)
            {
                RoomMap[room.Name] = room;
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Zork!");


            const string defaultRoomsFilename = "Rooms.txt";
            string roomDescriptionsFilename = (args.Length > 0 ? args[(int)CommandLineArguments.RoomsFilename] : defaultRoomsFilename);

            //const string roomDescriptionsFilename = "Rooms.txt";
            InitializeRoomDescriptions(roomDescriptionsFilename);

            Room previousRoom = null;
            Commands command = Commands.UNKNOWN;
            while (command != Commands.QUIT)
            {
                Console.WriteLine(CurrentRoom);
                if (previousRoom != CurrentRoom)
                {
                    Console.WriteLine(CurrentRoom.Description);
                    previousRoom = CurrentRoom;
                }

                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());

                switch (command)
                {
                    case Commands.QUIT:
                        Console.WriteLine("Thank you for playing!");
                        break;

                    case Commands.LOOK:
                        Console.WriteLine(CurrentRoom.Description);
                        break;

                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.EAST:
                    case Commands.WEST:
                        if (Move(command) == false)
                        {
                            Console.WriteLine("The way is shut!");
                        }
                        break;

                    default:
                        Console.WriteLine("Unrecognized Command.");
                        break;
                }
            }
        }


        private static bool Move(Commands command)
        {
            Assert.IsTrue(IsDirection(command), "Invalid direction.");

            bool isValidMove = true;
            switch (command)
            {
                case Commands.NORTH when Location.Row > 0:
                    Location.Row--;
                    break;

                case Commands.SOUTH when Location.Row < Rooms.GetLength(1) - 1:
                    Location.Row++;
                    break;

                case Commands.EAST when Location.Column < Rooms.GetLength(1) - 1:
                    Location.Column++;
                    break;

                case Commands.WEST when Location.Column > 0:
                    Location.Column--;
                    break;

                default:
                    isValidMove = false;
                    break;
            }

            return isValidMove;
        }

        private static Commands ToCommand(string commandString)
        {
            return Enum.TryParse<Commands>(commandString, true, out Commands command) ? command : Commands.UNKNOWN;
        }

        private static bool IsDirection(Commands command)
        {
            return Directions.Contains(command);
        }

        private enum Fields
        {
            Name = 0,
            Description
        }



        private static void InitializeRoomDescriptions(string roomDescriptionsFilename)
        {
            const string fieldDelimiter = "##";
            const int expectedFieldCount = 2;

            var roomQuery = from line in File.ReadLines(roomDescriptionsFilename)            
                            let fields = line.Split(fieldDelimiter)
                            where fields.Length == expectedFieldCount
                            select (Name: fields[(int)Fields.Name],
                                    Description: fields[(int)Fields.Description]);
            
            foreach (var (Name, Description) in roomQuery)
            {
                RoomMap[Name].Description = Description;
            }

        }

        private static readonly Room[,] Rooms = {

            { new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") },
            { new Room("Forest"), new Room("West of House"), new Room("Behind House") },
            { new Room("Rocky Trail"), new Room("South of House"), new Room("Canyon View") },

        };

        private static readonly List<Commands> Directions = new List<Commands>
        {
            Commands.NORTH,
            Commands.SOUTH,
            Commands.EAST,
            Commands.WEST
        };

        private static (int Row, int Column) Location = (1, 1);
    }
}
