using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameServices;

namespace TheChosenFour_GameProject.GameObjects.Heroes
{
    public class Musketeer: Hero
    {
        public Musketeer(Scene scene, HeroState heroState,  double placeX, double placeY)
            : base(scene, heroState, placeX, placeY)
        {
            _heroState = heroState;
            SetHeroImage();
            _health += GameManager.Player.Set.MusketeerShirt == null ? 0 : GameManager.Player.Set.MusketeerShirt.ProductLives;
            _health += GameManager.Player.Set.MusketeerShoe == null ? 0 : GameManager.Player.Set.MusketeerShoe.ProductLives;
            _health += GameManager.Player.Set.MusketeerShield == null ? 0 : GameManager.Player.Set.MusketeerShield.ProductLives;
            _strength += GameManager.Player.Set.MusketeerSword == null ? 0 : GameManager.Player.Set.MusketeerSword.ProductStrength;
            _strength += GameManager.Player.Set.MusketeerBow == null ? 0 : GameManager.Player.Set.MusketeerBow.ProductStrength;
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
                    base.SetImage("Characters/Heros/Musketeer/musketeerIdle.gif");
                    Image.Height = 72 * 2;
                    Image.Width = 48 * 2;
                    break;

                case HeroState.Run:
                    base.SetImage("Characters/Heros/Musketeer/musketeerRun.gif");
                    Image.Height = 73 * 2;
                    Image.Width = 48 * 2;
                    break;

                case HeroState.Jump:
                    base.SetImage("Characters/Heros/Musketeer/musketeerJump.gif");
                    Image.Height = 93 * 2;
                    Image.Width = 54 * 2;
                    break;

                case HeroState.Fight:
                    base.SetImage("Characters/Heros/Musketeer/musketeerAttack.gif"); // FIX
                    break;

                case HeroState.Dead:
                    base.SetImage("Characters/Heros/Musketeer/musketeerIdle.gif"); // FIX
                    break;
            }
        }
    }
}
