using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameServices;
using Windows.UI.Xaml.Controls;

namespace TheChosenFour_GameProject.GameObjects.Heroes
{
    public class Archer:Hero
    {
        public Archer(Scene scene, HeroState heroState,double placeX, double placeY)
            : base(scene, heroState, placeX, placeY)
        {
            _heroState = heroState;
            SetHeroImage();
            _health += GameManager.Player.Set.ArcherShirt == null ? 0 : GameManager.Player.Set.ArcherShirt.ProductLives;
            _health += GameManager.Player.Set.ArcherShoe == null ? 0 : GameManager.Player.Set.ArcherShoe.ProductLives;
            _health += GameManager.Player.Set.ArcherShield == null ? 0 : GameManager.Player.Set.ArcherShield.ProductLives;
            _strength += GameManager.Player.Set.ArcherSword == null ? 0 : GameManager.Player.Set.ArcherSword.ProductStrength;
            _strength += GameManager.Player.Set.ArcherBow == null ? 0 : GameManager.Player.Set.ArcherBow.ProductStrength;
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
                    base.SetImage("Characters/Heros/Archer/archerIdle.gif");
                    Image.Height = 75 * 2;
                    Image.Width = 65 * 2;
                    break;

                case HeroState.Run:
                    base.SetImage("Characters/Heros/Archer/archerRun.gif");
                    Image.Height = 72 * 2;
                    Image.Width = 67 * 2;
                    break;

                case HeroState.Jump:
                    base.SetImage("Characters/Heros/Archer/archerJump.gif");
                    Image.Height = 76 * 2;
                    Image.Width = 67 * 2;
                    break;

                case HeroState.Fight:
                    base.SetImage("Characters/Heros/Archer/"); // FIX
                    break;

                case HeroState.Dead:
                    base.SetImage("Characters/Heros/Archer/archerIdle.gif"); // FIX
                    break;
            }
        }       
    }
}
