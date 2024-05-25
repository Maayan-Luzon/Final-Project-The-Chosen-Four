using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace GameEngine.GameServices
{
    public abstract class Scene : Canvas
    {
        private List<GameObject> _gameObjects = new List<GameObject>(); // A repository of all game objects
        protected List<GameObject> _gameObjectsSnapshot => _gameObjects.ToList();// copy
        public double Ground { get; set; } // floor

        public Scene()
        {
            Manager.GameEvent.OnRun += Run; // this how you register to 'OnRun' event
            Manager.GameEvent.OnRun += CheckCollisional;     
        }

        // method to activate the Run method of each GameMovingObject
        private void Run()
        {

            foreach (var gameObject in _gameObjectsSnapshot)
            {
                if (gameObject is GameMovingObject obj)
                {
                    obj.Render();
                }
            }
        }

        // The operation returns all objects to the initial position
        public void Init() 
        {
            foreach (GameObject obj in _gameObjects)
            {
                obj.Init();
            }
        }

        // this method remove the object 
        public void RemoveObject(GameObject gameObject) 
        {
            if (_gameObjects.Contains(gameObject)) // does the gameObjects contain the gameObject?
            {
                _gameObjects.Remove(gameObject); // remove from gameObjects
                Children.Remove(gameObject.Image); // remove from screen
            }
        }

        //this method remove all of the object 
        public void RemoveAllObjects() 
        {
            foreach (GameObject obj in _gameObjects)
            {
                RemoveObject(obj);
            }
        }

        // add object to list and screen
        public void AddObject(GameObject gameObject) 
        {
            _gameObjects.Add(gameObject);
            // _gameObjectsSnapshot.Add(gameObject);
            Children.Add(gameObject.Image);
        }

        // check for each object if it collide with another object
        private void CheckCollisional()
        {
            foreach (var gameObject in _gameObjectsSnapshot) // go through all objects
            {
                if (gameObject.Collisional)// if the object isn't transparent
                {
                    /* 
                     if the two objects are different, AND g is ISN'T transparent, AND if the meshutaf rectangle (the one between the two
                    objects's rect) ISN'T empty (meaning there is one), 
                    THEN the g var is equals to the otherObject var.
                     */

                    foreach (var otherGameObject in _gameObjectsSnapshot)
                    {
                        if (ReferenceEquals(gameObject, otherGameObject))
                            continue;

                        if (!otherGameObject.Collisional)
                            continue;

                        var collisionRect = RectHelper.Intersect(otherGameObject.Rect, gameObject.Rect);
                        if (collisionRect.IsEmpty)
                            continue;

                        gameObject.Collide(otherGameObject);
                    }
                }
            }
        }

        // method to activate the Start method of each GameMovingObject
        public void Start()
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                gameObject.Start();
            }
        }

        // method to activate the Pause method of each GameMovingObject
        public void Pause()
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                gameObject.Pause();
            }
        }

        // method to activate the Resume method of each GameMovingObject
        public void Resume()
        {
            foreach (var gameObject in _gameObjectsSnapshot)
            {
                gameObject.Resume();
            }
        }

        
    }
}
