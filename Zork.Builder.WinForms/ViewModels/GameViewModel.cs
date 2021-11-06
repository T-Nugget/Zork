using System;
using System.ComponentModel;
using Zork;

namespace Zork.Builder.WinForms
{
    public class GameViewModel
    {
        public BindingList<Room> Rooms { get; set; }

        public Game Game
        {
            set
            {
                if(_game != value)
                {
                    _game = value;
                    if (_game != null)
                    {
                        Rooms = new BindingList<Room>(_game.Rooms);
                    }
                    else
                    {
                        Rooms = new BindingList<Room>(Array.Empty<Room>());
                    }
                }
            }
        }
        private Game _game;
    }
}
