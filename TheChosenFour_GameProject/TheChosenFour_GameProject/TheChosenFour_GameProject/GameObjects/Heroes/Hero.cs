using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Diagnostics;
using TheChosenFour_GameProject.GameObjects.Lands;
using TheChosenFour_GameProject.GameObjects.Villains;
using TheChosenFour_GameProject.GameServices;
using Windows.Foundation;
using Windows.System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using static TheChosenFour_GameProject.GameObjects.Villains.Monster;

namespace TheChosenFour_GameProject.GameObjects.Heroes
{
    public class Hero : AppGameObject
    {
        public enum HeroState // the hero states
        {
            Idle,
            Run,
            Fight,
            Dead,
            Jump
        }

        private double _speed; // the hero speed

        public HeroState _heroState; // the hero's state
        protected double _health = 30; // the hero's default health
        private bool _isStart; // boolean to check if the game has started
        protected TextBlock _textBlock; // text block that presents the hero's health
        private bool _isOnLog; // boolean to determain if the hero is on log or not
        private Land _currentLand; // the current log the hero is on. 
        protected double _strength = 1; // the hero's strength
        protected bool _isPaused; // boolean to check if the game has paused


        public Hero(Scene scene, HeroState heroState, double placeX, double placeY)
            : base(scene, string.Empty, placeX, placeY)
        {
            SetHeroImage();
            _heroState = heroState;
            _isStart = false;
            _Y = scene.ActualHeight - Height;
            _dY = 0;
            _speed = 1;
            _isOnLog = false;
            /*_strength = 1;
            _health = 30;*/
            _isPaused = false;
            _textBlock = new TextBlock();            
            _textBlock.FontSize = 30;
            _textBlock.Foreground = new SolidColorBrush(Colors.YellowGreen);
            Canvas.SetLeft(_textBlock, _X + 35);
            Canvas.SetTop(_textBlock, _Y - 35);
            _scene.Children.Add( _textBlock);

            Debug.WriteLine(this.GetType().Name + ": hey");
        }

        /// <summary>
        /// method that will be activated every time a key will be release 
        /// </summary>
        /// <param name="key"> the released key</param>
        private void KeyUp(VirtualKey key)
        {
            if (_heroState == HeroState.Dead) return;

            switch (key)
            {
                case VirtualKey.Down:
                    Collisional = true;
                    break;

                case VirtualKey.Right:

                    break;
                case VirtualKey.Up:

                    break;
            }
        }

        /// <summary>
        /// method that will be activated every time a key will be pressed 
        /// </summary>
        /// <param name="key"> the pressed key</param>
        private void KeyDown(VirtualKey key)
        {
            if (_heroState == HeroState.Dead) return;

            switch (key)
            {
                case VirtualKey.Up:
                    if (_heroState != HeroState.Jump)
                    {
                        _Y -= 10;
                        _dY = -28;
                        _ddY = 1;
                        _heroState = HeroState.Jump;
                        _isOnLog = false;
                        SetHeroImage();
                    }
                    break;

                case VirtualKey.Down:
                    Collisional = false;
                    _isOnLog = false;
                    _ddY = 1;
                    break;
            }
        }

        /// <summary>
        /// what will happen every time the game start
        /// </summary>
        public override void Start()
        {
            _isStart = true;
            _heroState = HeroState.Run;
            SetHeroImage();
            _dX = 0;
            _ddX = 0;
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
        }

        /// <summary>
        /// what will happen every time the game pause
        /// </summary>
        public override void Pause()
        {
            if (_heroState != HeroState.Dead)
                _heroState = HeroState.Idle;
            SetHeroImage();
            Manager.GameEvent.OnKeyDown -= KeyDown;
            Manager.GameEvent.OnKeyUp -= KeyUp;
        }

        /// <summary>
        /// what will happen every time the game resume
        /// </summary>
        public override void Resume()
        {
            if(_heroState != HeroState.Dead)
                _heroState = HeroState.Run;
            SetHeroImage();
            if (!_isInCombat) { 
            Manager.GameEvent.OnKeyDown += KeyDown;
            Manager.GameEvent.OnKeyUp += KeyUp;
            }
        }
        
        /// <summary>
        /// method that determain what will happen to the hero when he enter
        /// a combat.
        /// </summary>
        /// <param name="isCombatant"> whether the hero is the combatant or not</param>
        /// <param name="enemy"> the enemy monster in this combat</param>
        public override void EnterCombat(bool isCombatant, GameObject enemy)
        {
            if (_heroState == HeroState.Dead) {
                Stop();
                return;
            } 

            base.EnterCombat(isCombatant, enemy);
            Manager.GameEvent.OnKeyDown -= KeyDown;
            Manager.GameEvent.OnKeyUp -= KeyUp;
        }

        /// <summary>
        /// method that returns the hero to its original place after a combat have been over.
        /// </summary>
        public override void FinishingCombat()
        {
            base.FinishingCombat();
            Stop();

            if (_heroState == HeroState.Dead)
            {
                return;
            }

            if (isInPlace())
            {
                _X = _placeX;
                Debug.WriteLine("Hero already in place: " + GetType().Name);
                GameManager.AppGameEvents.OnFinishCombatInPlace();
            }
            else
            {
                _dX = Math.Sign(_placeX - _X) * 5;
            }
        }

        /// <summary>
        /// method that returns the game to work as usual after the combat is over and every hero
        /// is in its place.
        /// </summary>
        public override void ExitCombat()
        {
            base.ExitCombat();

            _dX = 0;
            _ddX = 0;

            if (_heroState == HeroState.Dead)
            {
                _dX = -5;
                return;
            }

            if (!_isPaused)
            {
                Manager.GameEvent.OnKeyDown += KeyDown;
                Manager.GameEvent.OnKeyUp += KeyUp;
            }
        }


        /// <summary>
        /// method to return whether the hero is back in his place or not. 
        /// </summary>
        /// <returns>whether the hero is back in his place or not</returns>
        public bool isInPlace()
        {
            return Math.Abs(_X - _placeX) < 3;
        }

        /// <summary>
        /// base method to set the hero image according to his state.
        /// </summary>
        protected virtual void SetHeroImage()
        { }


        /// <summary>
        /// the method that enable the hero move in the scene depending on the
        /// hero's state and position and whether he in a combat or not. 
        /// </summary>
        public override void Render()
        {
            base.Render();
            if(_textBlock != null) 
            {
                Canvas.SetLeft(_textBlock, _X + 35);
                Canvas.SetTop(_textBlock, _Y - 35);
            }
            

            if (_X <= -Width) //if the hero crossed the left border
            {
                _scene.RemoveObject(this);
            }

            if (_heroState == HeroState.Dead)
            {
                return;
            }

            if (_isInCombat && _isCombatant && !_isFinishingCombat)
            { 
                if (_dX >= 5)
                {
                    _dX = 5;
                    _ddX = 0;
                }
                
                if (_X > _enemy._X + _enemy.Width + 10)
                {
                    GameManager.AppGameEvents.OnFinishingCombat();
                }
            }

            if (_dX != 0 && _isFinishingCombat && _heroState != HeroState.Dead)
            {
                if (isInPlace())
                {
                    Stop();
                    Debug.WriteLine("Hero reached place: " + GetType().Name);
                    _X = _placeX;
                    GameManager.AppGameEvents.OnFinishCombatInPlace();
                }
                else
                {
                    _dX = Math.Sign(_placeX - _X) * 5;
                }
            }

            if (_isOnLog)
            {
                if (_dY < 0)
                {
                    _isOnLog = false;
                }
                else
                {
                    _dY = 0;
                    _ddY = 1;
                    _Y = _currentLand.Rect.Top - Height + (_currentLand.Rect.Height * 0.2);
                    if (_heroState != HeroState.Run)
                    {
                        _heroState = HeroState.Run;
                        SetHeroImage();
                    }
                }
            }
            else
            {
                _ddY = 1;
            }

            if (Rect.Bottom > _scene?.ActualHeight && _dY >= 0)// + Height * 0.3
            {
                if (_heroState == HeroState.Jump)
                {
                    _heroState = HeroState.Run;
                    SetHeroImage();
                }

                _dY = 0;
                _ddY = 1;
                _Y = _scene.ActualHeight - Height;
            }

           
        }

        /// <summary>
        /// method that deal with collisional between the hero and other gameObject. 
        /// it can collide with a log and thus stand on it or with a monster and
        /// fight with it. 
        /// </summary>
        /// <param name="gameObject"> the other gameObject in this collisional</param>
        public override void Collide(GameObject gameObject)
        {
            if (_heroState == HeroState.Dead) return;

            if (gameObject != null)
            {
                if (gameObject is Land land)
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
                    if (rect.Width <= 20) 
                    {
                        if (_isOnLog)
                        {
                            Debug.WriteLine(this.GetType().Name + ": END OF LOG");
                        }
                        _isOnLog = false;
                    }
                }


                else if (gameObject is Monster monster && monster._state != MonsterState.Dead 
                    && _heroState!= HeroState.Dead)
                {
                    SoundPlayer.Play("sword-blade-lashes-chainmail-armor.wav");
                    if (_isInCombat && !_isFinishingCombat)
                        {
                        if (_isCombatant && ReferenceEquals(monster, _enemy))
                        {                           
                            Debug.WriteLine("hello " + this.GetType().Name);
                            monster.attack(this, _strength);
                            if (_isInCombat && !_isFinishingCombat)
                            {
                                _ddX = 1;
                                _dX = -7;                               
                            }
                        }
                    }
                    else if (!_isFinishingCombat)
                    {
                        GameManager.AppGameEvents.OnEnterCombat(this, monster);
                    }
                }
            }
        }

        /// <summary>
        /// the monster attacks the hero and reduce its health. 
        /// </summary>
        /// <param name="monster"> the momster the hero fights</param>
        /// <param name="strength"> the monster strength</param>
        public void attack(Monster monster, double strength)
        {
            if (_heroState == HeroState.Dead)
                return;

            // remove health
            _health -= strength;
            if (_health <= 0)
            {
                Stop();
                _heroState = HeroState.Dead;
                SetHeroImage();
                _textBlock.Text = "0";

                Manager.GameEvent.OnKeyDown -= KeyDown;
                Manager.GameEvent.OnKeyUp -= KeyUp;

                // if is dead, stop combat
                _appScene.OnHeroDeath();
                GameManager.AppGameEvents.OnFinishingCombat();
            }
            else
                _textBlock.Text = _health.ToString() + "♥";           
        }
    }
}
