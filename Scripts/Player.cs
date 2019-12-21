using Godot;
using System.Collections.Generic;
using Godot.Collections;
namespace RiskGame.Scripts
{
    public partial class Game
    {
        public class Player
        {
            public int id { get; set; }
            public Color color { get; set; }
            public int[] card = new int[6] { 0,0,0,0,0,0};
            public int hasCard = 0;
           
            public int notusedTroops { get; set; }
           
            public int usedTroops { get; set; }
            // public  string name;
            public int contents { get; set; }
            public int countries { get; set; }
            public Player()
            {

                notusedTroops = 35;
                usedTroops = 35;
                countries = 0;
            }

        }
    }
}
