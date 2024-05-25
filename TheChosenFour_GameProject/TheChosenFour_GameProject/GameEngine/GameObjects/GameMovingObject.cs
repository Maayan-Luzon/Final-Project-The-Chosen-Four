using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.System.Update;

namespace GameEngine.GameObjects
{
    public abstract class GameMovingObject:GameObject
    {
        protected double _dX; // horizontal speed
        protected double _dY; // vertical speed
        protected double _ddX; // horizontal acceleration
        protected double _ddY; // vertical acceleration
        protected double _toX; // Target location in horizontal axis
        protected double _toY; // Target location in vertical axis

        protected GameMovingObject(Scene scene, string fileName, double placeX, double placeY)
            :base(scene, fileName, placeX, placeY)
        {
            
        }

        // runs all the time. updating the object's place on screen
        public override void Render() 
        {
            _dX += _ddX; // שינוי מהירות אופקית
            _dY += _ddY; // שינוי מהירות אנכית

            _X += _dX; // שינוי מיקום אופקי
            _Y += _dY; // שינוי מיקום אנכי

            if(Math.Abs(_X - _toX)< 4 && Math.Abs(_Y - _toY)< 4) // האם האובייקט הגיע ליעדו
            {
                Stop(); // עצירת האובייקט
                _X = _toX; // הזזה קטנה לקיזוז חוסר דיוק בעצירת האובייקט
                _Y = _toY;
            }

            base.Render(); // draw on the screen
        }


        // method to stop the object's movement
        public virtual void Stop()
        {
            _dX = _dY = _ddX = _ddY = 0;          
        }


        // method to move an object to specific place on the screen
        public void MoveTo(double toX, double toY, double speed = 1, double acceleration = 0) 
        {
            _toX = toX;
            _toY = toY;

            var len = Math.Sqrt(Math.Pow(_toX - _X, 2) +  Math.Pow(_toY - _Y, 2));
            var cos = (_toX - _X) / len;
            var sin = (_toY - _Y) / len;

            speed *= Constants.SpeedUnit;
            _dX = speed * cos;
            _dY = speed * sin;

            _ddX = acceleration * cos;
            _ddY = acceleration * sin;
        }
    }
}
