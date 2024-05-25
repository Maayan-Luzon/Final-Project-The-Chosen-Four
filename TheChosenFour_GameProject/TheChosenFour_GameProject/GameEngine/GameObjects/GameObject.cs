using GameEngine.GameServices;
using System;
using Windows.Foundation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

namespace GameEngine.GameObjects
{
    // the class is not ment to create objects, but to be the base
    // for other classes we will make in the futers.
    // this class contains all of the share members (attributes and methods)
    // that will be in the project. 
    public abstract class GameObject
    {
        public double _X; // current position
        public double _Y; // current position

        protected double _placeX; // first position
        protected double _placeY; // first position

        public Image Image { get; set; } // look

        protected string _fileName; // the image's file name

        public double Width => Image.ActualWidth; // short and easier writing
        public double Height => Image.ActualHeight; // short and easier writing

        public virtual Rect Rect => new Rect(_X, _Y, Width, Height); // the rectangle which surrounds the object
        
        public Rectangle rectangle;
        public bool Collisional { get; set; } = true; // It can be punped into, not transparent

        protected Scene _scene; // the playing field

        
        public GameObject(Scene scene, string fileName, double placeX, double placeY)
        {
            _scene = scene;
            _fileName = fileName;
            _X = placeX; 
            _Y = placeY;
            _placeX = placeX; // כך יזכור עצם היכן נוצר
            _placeY = placeY; // כך יזכור עצם היכן נוצר
            Image = new Image();
            SetImage(fileName);
            Render();
        }

        // The method draws the object on the screen
        public virtual void Render() 
        {
            Canvas.SetLeft(Image, _X);
            Canvas.SetTop(Image, _Y); 
        }

        // The method determines the image of the object
        protected void SetImage(string fileName)  
        {
            Image.Source = new BitmapImage(new Uri($"ms-appx:///Assets/{fileName}"));

        }

        // The method returns the object to its initial position
        public virtual void Init() 
        {
            _X = _placeX;
            _Y = _placeY;
        }

        // intended for being override. what will happen when two objects collide
        public virtual void Collide(GameObject gameObject) 
        {
        }

        // intended for being override. what will happen to the object when the game start
        public virtual void Start()
        {

        }

        // intended for being override. what will happen to the object when the game pause
        public virtual void Pause()
        {

        }

        // intended for being override. what will happen to the object when the game resume
        public virtual void Resume()
        {

        }
    }
}
