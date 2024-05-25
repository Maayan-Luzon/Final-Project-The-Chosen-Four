using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Project.Models
{
    public class GameLevel
    {
        public int LevelID { get; set; } = 1; // the level's id
        public int LevelNumber { get; set; } // the level's number
        public int CountMonsters { get; set; } // the amount of monsters in the level
        public int CountLogs { get; set; } // the amount of land logs in the level
        
    }
}
