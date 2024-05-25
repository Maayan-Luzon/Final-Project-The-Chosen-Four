using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheChosenFour_GameProject.GameServices
{
    public class AppGameEvents
    {
        public Action<GameObject, GameObject> OnEnterCombat; // the event happens when one character get inyo an combat
                                                             // it will couse every other object enter to combat mode.
                                                             
        public Action OnFinishingCombat; // this event happens when the combat is being finished and the heroes need to get
                                         // back to their place.

        public Action OnExitCombat; // this event happens when the combat is finished completly and it gets every other object 
                                    // out of the finishing combat state.

        public Action OnFinishCombatInPlace; // every hero declare on itself that he have return to its place.  

        /// <summary>
        ///  An action that disconnects the connections between the events and the methods
        /// </summary>
        public void ClearEvent()
        {
            OnEnterCombat = null;
            OnFinishingCombat = null;
            OnFinishCombatInPlace = null;
            OnExitCombat = null;
        }
        
    }
}
