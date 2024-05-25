using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System;

namespace GameEngine.GameServices
{
    public class GameEvents
    {
        public Action OnRun; // the event that thanks to the moving objects, will move 
        public Action<VirtualKey> OnKeyDown; // with this event the objects can listen to key press
        public Action<VirtualKey> OnKeyUp; // with this event the objects can listen to key release
        public Action<int> OnRemoveLife; // with this event we will be able to remove heart from the game page 
        public Action OnScoreRefresh; // with this event we can display the updated score      
        public Action OnGameOver; // this event happens when the player lost in the game
        public Action OnWin; // this event happens when the player won in the game

        // An action that disconnects the connections between the events and the methods
        public virtual void ClearEvent()
        {
            OnWin = null;
            OnRemoveLife = null;
            OnScoreRefresh = null;
            OnKeyDown = null;
            OnRun = null;
            OnKeyUp = null;
            OnGameOver = null;
        }
    }
}
