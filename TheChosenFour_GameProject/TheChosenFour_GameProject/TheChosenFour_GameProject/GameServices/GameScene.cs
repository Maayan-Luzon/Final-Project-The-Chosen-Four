using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameObjects.Lands;

namespace TheChosenFour_GameProject.GameServices
{
    public class GameScene:Scene
    {
        private bool _inCombatFinish; // is the game state is finishing combat?
        private int _expectInPlace; // how many heroes are expected to be in place
        private int _aliveHeros; // how many alive heroes are there.

        public GameScene() : base()
        {
            Manager.GameEvent.OnRun += checkEnd;
            _inCombatFinish = false;
            _aliveHeros = 4;
        }

        /// <summary>
        /// this method checks if there are no more logs in the game. if this is the case, the player
        /// won this level. *every log that pass the left border of the page is being deleted. 
        /// </summary>
        private void checkEnd()
        {
            if (!hasLands() && Manager.GameState == Constants.GameState.Started)
            {
                Manager.GameEvent.OnWin();
                GameManager.Player.MaxLevel = GameManager.Player.MaxLevel < GameManager.Player.CurrentLevel.LevelNumber ?
                    GameManager.Player.CurrentLevel.LevelNumber : GameManager.Player.MaxLevel;
            }
        }

        /// <summary>
        /// this method return if there are no more logs in the game
        /// </summary>
        /// <returns> is there any log left in the game? </returns>
        private bool hasLands()
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                if (gameObject is Land)
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// being called on hero death. If all the heroes have being killed, the player
        /// lost in this level. 
        /// </summary>
        public void OnHeroDeath()
        {
            _aliveHeros--;
            if (_aliveHeros == 0)
            {
                if (Manager.GameEvent.OnGameOver != null)
                    Manager.GameEvent.OnGameOver();
            }
            Debug.WriteLine("OnHeroDeath: " + _aliveHeros);
        }

        /// <summary>
        /// activates the EnterCombat method for each object. 
        /// </summary>
        /// <param name="combatant1"> the first combatant</param>
        /// <param name="combatant2"> the second combatant</param>
        public void EnterCombat(GameObject combatant1, GameObject combatant2)
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                bool isCombatant = false;
                GameObject enemy = null;
                if (ReferenceEquals(gameObject, combatant1))
                {
                    isCombatant = true;
                    enemy = combatant2;
                }
                else if (ReferenceEquals(gameObject, combatant2))
                {
                    isCombatant = true;
                    enemy = combatant1;
                }

                if(gameObject is AppGameObject appGameObject)
                    appGameObject.EnterCombat(isCombatant, enemy);
                
            }
        }

        /// <summary>
        /// activates the FinishCombat method for each object. 
        /// </summary>
        public void FinishingCombat()
        {
            _inCombatFinish = true;
            _expectInPlace = _aliveHeros;
            Debug.WriteLine("OnFinishingCombat: alive=" + _expectInPlace);

            foreach (var gameObject in _gameObjectsSnapshot)
            {
                if (gameObject is AppGameObject appGameObject)
                    appGameObject.FinishingCombat();
            }
        }

        /// <summary>
        /// activates the ExitCombat method for each object. 
        /// </summary>
        public void ExitCombat()
        {
            Debug.WriteLine("OnExitCombat");
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                if (gameObject is AppGameObject appGameObject)
                    appGameObject.ExitCombat();
            }
        }

        /// <summary>
        /// this method counting how many heros still need to go back to thier place after 
        /// a combat. If all the heroes got their place, this method call the OnExitCombat
        /// event.
        /// </summary>
        public void FinishCombatInPlace()
        {
            _expectInPlace--;
            Debug.WriteLine("OnFinishCombatInPlace: expected=" + _expectInPlace);

            if (_expectInPlace < 1)
            {
                Debug.WriteLine("OnFinishCombatInPlace: done");
                _inCombatFinish = false;
                GameManager.AppGameEvents.OnExitCombat();
            }
        }
    }
}
