using GameEngine.GameObjects;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheChosenFour_GameProject.GameServices;
using static TheChosenFour_GameProject.GameObjects.Villains.Monster;

namespace TheChosenFour_GameProject.GameObjects.Lands
{
    public class Land : AppGameObject
    {
        public Land(Scene scene, double width, double placeX, double placeY, string fileName)
            : base(scene, "Lands/" + fileName , placeX, placeY)
        {
            Image.Width = width;
            Image.Height = width / 2.87;         
        }

        /// <summary>
        /// what will happen every time the game start
        /// </summary>
        public override void Start()
        {
            _dX = -5;
        }

        /// <summary>
        /// returns the land's parameters to usual after the combat is finished
        /// </summary>
        public override void ExitCombat()
        {
            base.ExitCombat();
            _dX = -5;
        }

        /// <summary>
        /// the method that enable the land move in the scene depending on
        /// whether it in a combat or not. 
        /// </summary>
        public override void Render()
        {
            base.Render();
            if (_X <= -Width) //if the land crossed the left border
            {
                _scene.RemoveObject(this);
            }
        }
    }
}
