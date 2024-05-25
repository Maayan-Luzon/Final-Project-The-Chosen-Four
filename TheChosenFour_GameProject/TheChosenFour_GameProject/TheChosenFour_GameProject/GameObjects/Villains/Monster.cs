using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameObjects.Heroes;
using TheChosenFour_GameProject.GameObjects.Lands;
using TheChosenFour_GameProject.GameServices;
using Windows.Graphics.Printing.PrintTicket;
using Windows.UI.Xaml;
using static TheChosenFour_GameProject.GameObjects.Heroes.Hero;

namespace TheChosenFour_GameProject.GameObjects.Villains
{
    public class Monster : AppGameObject
    {
        public enum MonsterState
        {
            Idle,
            Fight,
            Dead
        }

        public MonsterState _state; // the monster's state
        public double _health; // the monster's health
        private double _strength; // the monster's strength
        private int _worth; // the monster's woth - how much money the player
                            // can get for killing it
        private bool _isOnLog; // whether or not the monster is on log
        private Land _currentLand; // the log the monster is on

        public Monster(Scene scene, MonsterState monstaerState, double width, double placeX, double placeY, double strength, double health)
            : base(scene, string.Empty, placeX, placeY + 20)
        {
            Image.Width = width;
            Image.Height = width;
            _state = monstaerState;
            //_Y = placeY - Height + 5;
            _health = health;
            _strength = strength;
            SetImage();
            _worth = (int)_strength / 2;
            _isOnLog = true;
        }

        /// <summary>
        /// what will happen every time the game start
        /// </summary>
        public override void Start()
        {
            _dX = -5;
            _ddX = 0;
        }

        /// <summary>
        /// the hero attacks the monster and reduce its health. 
        /// </summary>
        /// <param name="attacker"> the hero the monster fights</param>
        /// <param name="strength"> the hero strength</param>
        public void attack(Hero attacker, double strength)// (GameObject attacker, double strength)
        {
            if (_state == MonsterState.Dead)
                return;

            // remove health
            _health -= strength;
            if (_health <= 0)
            {
                Stop();
                _state = MonsterState.Dead;
                SetImage();

                // if is dead, stop combat
                GameManager.AppGameEvents.OnFinishingCombat();
                GameManager.Player.Money += _worth;
                if (Manager.GameEvent.OnScoreRefresh != null)
                    Manager.GameEvent.OnScoreRefresh();
            }
            else
            {
                // get distance from attacker
                attacker.attack(this, _strength);
                _ddX = -1;
                _dX = 7;
            }

        }

        /// <summary>
        /// the method to change the monster's gif according to its state
        /// </summary>
        private void SetImage()
        {
            switch (_state)
            {
                case MonsterState.Idle:
                    base.SetImage("Characters/PurpleMonster/purpleMonsterIdleLeft.gif");
                    break;

                case MonsterState.Fight:
                    base.SetImage("Characters/PurpleMonster/purpleMonsterAttackLeft.gif");
                    break;

                case MonsterState.Dead:
                    base.SetImage("Characters/PurpleMonster/purpleMonsterDeathLeftS.gif");
                    break;
            }
        }

        /// <summary>
        /// sets the monster's type
        /// </summary>
        public void SetType()
        {
            switch (_state)
            {
                case MonsterState.Idle:
                    _state = MonsterState.Fight;
                    break;


                case MonsterState.Fight:
                    _state = MonsterState.Dead;

                    _scene.RemoveObject(this);
                    break;

            }
            SetImage();
        }

        /// <summary>
        /// returns the monster's parameters to usual after the combat is finished
        /// </summary>
        public override void ExitCombat()
        {
            base.ExitCombat();
            _dX = -5;
        }

        /// <summary>
        /// the method that enable the monster move in the scene depending on the
        /// monster's state and position and whether it in a combat or not. 
        /// </summary>
        public override void Render()
        {
            base.Render();

            if (_isInCombat && _isCombatant)
            {
                if (_dX <= -5)
                {
                    _dX = -5;
                    _ddX = 0;
                }
            }
            if (_dX <= -6)
            {
                _dX = -6;
            }

            if (_X <= -Width) //if the monster crossed the left border
            {
                if (_isInCombat && _isCombatant && !_isFinishingCombat)
                {
                    GameManager.AppGameEvents.OnFinishingCombat();
                }

                _scene.RemoveObject(this);
            }


            if (_isOnLog)
            {
                if(_currentLand == null)
                {
                    _dY = 0;
                    _ddY = 0;
                }
                else if (_dY < 0)
                {
                    _isOnLog = false;
                }
                else
                {
                    _dY = 0;
                    _ddY = 1;
                    _Y = _currentLand.Rect.Top - Height + (_currentLand.Rect.Height * 0.2); //0.3
                }
            }
            else
            {
                _ddY = 1;
            }


            if (Rect.Bottom > _scene?.ActualHeight && _dY >= 0)// + Height * 0.3
            {
                _dY = 0;
                _ddY = 1;
                _Y = _scene.ActualHeight - Height;
        }


        }

        /// <summary>
        /// this method stops the monster's movement
        /// </summary>
        public override void Stop()
        {
            base.Stop();
        }

        /// <summary>
        /// method that deal with collisional between the monster and other gameObject. 
        /// it can collide with a log and thus stand on it or with a hero and
        /// fight with it.
        /// </summary>
        /// <param name="gameObject">the other gameObject in this collisional</param>
        public override void Collide(GameObject gameObject)
        {
            if (_state == MonsterState.Dead) return;

            if (gameObject != null)
            {
                if (gameObject is Land land && _X <= _scene.ActualWidth)
                {
                    var rect = RectHelper.Intersect(Rect, land.Rect);

                    if (rect.Width > rect.Height) // vertical touch
                    {
                        if (_dY >= 0 && Rect.Bottom < land.Rect.Top + (land.Rect.Height * 0.5)) // && Rect.Bottom < land.Rect.Top + 2
                        {
                            if (!_isOnLog)
                            {
                                Debug.WriteLine(this.GetType().Name + ": ON LOG");
                                _dY = 0;
                            }
                            _dY = 0;
                            _currentLand = land;
                            _isOnLog = true;
                        }
                    }

                    if (rect.Width <= 40) //10
                    {
                        if (_isOnLog)
                        {
                            Debug.WriteLine(this.GetType().Name + ": END OF LOG");
                        }
                        _isOnLog = false;
                    }
                }
            }
        }
    }
}

    
