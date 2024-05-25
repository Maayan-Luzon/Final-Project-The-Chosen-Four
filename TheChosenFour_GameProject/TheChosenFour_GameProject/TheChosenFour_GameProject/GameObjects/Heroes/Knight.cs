using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameServices;

namespace TheChosenFour_GameProject.GameObjects.Heroes
{
    public class Knight:Hero
    {
        public Knight(Scene scene, HeroState heroState, double placeX, double placeY)
            : base(scene, heroState, placeX, placeY)
        {
            _heroState = heroState;
            SetHeroImage();
            _health += GameManager.Player.Set.KnightShirt == null ? 0 : GameManager.Player.Set.KnightShirt.ProductLives;
            _health += GameManager.Player.Set.KnightShoe == null ? 0 : GameManager.Player.Set.KnightShoe.ProductLives;
            _health += GameManager.Player.Set.KnightShield == null ? 0 : GameManager.Player.Set.KnightShield.ProductLives;
            _strength += GameManager.Player.Set.KnightSword == null ? 0 : GameManager.Player.Set.KnightSword.ProductStrength;
            _strength += GameManager.Player.Set.KnightBow == null ? 0 : GameManager.Player.Set.KnightBow.ProductStrength;
            _textBlock.Text = _health.ToString() + "♥";
        }

        /// <summary>
        /// change the hero's gif according to its state. 
        /// </summary>
        protected override void SetHeroImage()
        {
            switch (_heroState)
            {
                case HeroState.Idle:
                    base.SetImage("Characters/Heros/Knight/knightIdle.gif");
                    Image.Height = 68 * 2;
                    Image.Width = 72 * 2;
                    break;

                case HeroState.Run:
                    base.SetImage("Characters/Heros/Knight/knightRun.gif");
                    Image.Height = 68 * 2;
                    Image.Width = 50 * 2;
                    break;

                case HeroState.Jump:
                    base.SetImage("Characters/Heros/Knight/knightJump.gif");
                    Image.Height = 88 * 2;
                    Image.Width = 78 * 2;
                    break;

                case HeroState.Fight:
                    base.SetImage("Characters/Heros/Knight/"); // FIX
                    break;

                case HeroState.Dead:
                    base.SetImage("Characters/Heros/Knight/knightIdle.gif"); // FIX
                    break;
            }
        }
    }
}
