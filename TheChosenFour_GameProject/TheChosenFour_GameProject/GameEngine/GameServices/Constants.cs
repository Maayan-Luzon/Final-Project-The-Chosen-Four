using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameEngine.GameServices
{
    public static class Constants
    {
        public enum GameState // defind the game's state
        {
            Loaded,
            Started,
            Paused,
            GameOver
        }

        // the speed in which the object will move. (pixel per movment)
        public static double SpeedUnit = 6;
    }
}
