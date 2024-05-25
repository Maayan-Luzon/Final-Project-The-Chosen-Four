using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls.Maps;

namespace TheChosenFour_GameProject.GameServices
{
    public class AppGameObject : GameMovingObject
    {
        protected bool _isInCombat = false; // wheter or not the object is in combat
        protected bool _isCombatant = false; // wheter or not the object is the combatant object
        protected GameObject _enemy = null; // the enemy in the conbat
        protected bool _isFinishingCombat = false; // is the object in 'finishing combat' state?
        protected GameScene _appScene; // the scene of the game

        public AppGameObject(Scene scene, string fileName, double placeX, double placeY) : base(scene, fileName, placeX, placeY)
        {
            _appScene = (GameScene)scene;
        }

        /// <summary>
        /// the base method to preper the object to enter a combat.
        /// </summary>
        /// <param name="isCombatant"> wheter or not the object is the combatant object</param>
        /// <param name="enemy">the enemy in the conbat</param>
        public virtual void EnterCombat(bool isCombatant, GameObject enemy)
        {
            Debug.WriteLine(this.GetType().Name + ": ENTER COMBAT, iscombatant=" + isCombatant + ", enemy=" + enemy?.GetType().Name);

            _isInCombat = true;
            _isCombatant = isCombatant;
            _enemy = enemy;
            _dX = 0;
            _ddX = 0;
        }

        /// <summary>
        /// this base method will enter the object into 'finishing combat' state
        /// </summary>
        public virtual void FinishingCombat()
        {
            _isFinishingCombat = true;
            Stop();
        }

        /// <summary>
        /// this base method returns the object state to usual after finishing the combat 
        /// </summary>
        public virtual void ExitCombat()
        {
            Debug.WriteLine(this.GetType().Name + ": EXIT COMBAT");
            _isFinishingCombat = false;
            _isInCombat = false;
        }
    }
}
