using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DB_Project.Models
{
    public class GameUser
    {
        public int UserId { get; set; } = 0; // the user's id
        public string UserName { get; set; } = "Anonymous"; // the user's userName
        public string UserPassword { get; set; } = "000"; // the user's password
        public string UserMail { get; set; } = "None Email"; // the user's email
        public GameLevel CurrentLevel { get; set; } = new GameLevel(); // the user's current level
        public int MaxLevel { get; set; } = 1; // the max level the user won in
        public int Money { get; set; } = 20; // the amount of money the user has     
        public Set Set { get; set; } = new Set(); // the set of products and clothes the user got
    }
}
