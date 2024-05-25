using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameServices;

namespace TheChosenFour_GameProject.GameObjects.Heroes
{
    public class Wizard:Hero
    {
        public Wizard(Scene scene, HeroState heroState, double placeX, double placeY)
            : base(scene, heroState, placeX, placeY)
        {
            _heroState = heroState;
            SetHeroImage();
            _health += GameManager.Player.Set.WizardShirt == null ? 0 : GameManager.Player.Set.WizardShirt.ProductLives;
            _health += GameManager.Player.Set.WizardShoe == null ? 0 : GameManager.Player.Set.WizardShoe.ProductLives;
            _health += GameManager.Player.Set.WizardShield == null ? 0 : GameManager.Player.Set.WizardShield.ProductLives;
            _strength += GameManager.Player.Set.WizardSword == null ? 0 : GameManager.Player.Set.WizardSword.ProductStrength;
            _strength += GameManager.Player.Set.WizardBow == null ? 0 : GameManager.Player.Set.WizardBow.ProductStrength;
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
                    base.SetImage("Characters/Heros/Wizard/wizardIdle.gif");
                    Image.Height = 76 * 2;
                    Image.Width = 48 * 2;
                    break;

                case HeroState.Run:
                    base.SetImage("Characters/Heros/Wizard/wizardRun.gif");
                    Image.Height = 71 * 2;
                    Image.Width = 49 * 2;
                    break;

                case HeroState.Jump:
                    base.SetImage("Characters/Heros/Wizard/wizardJump.gif");
                    Image.Height = 81 * 2;
                    Image.Width = 48 * 2;
                    break;

                case HeroState.Fight:
                    base.SetImage("Characters/Heros/Wizard/"); // FIX
                    break;

                case HeroState.Dead:
                    base.SetImage("Characters/Heros/Wizard/wizardIdle.gif"); // FIX
                    break;
            }
        }

        
    }
}
