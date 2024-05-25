using DB_Project.Models;
using GameEngine.GameObjects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Core;
using Windows.UI.UIAutomation.Core;
using Windows.UI.Xaml;
using static GameEngine.GameServices.Constants;

namespace GameEngine.GameServices
{
    /*
        Abstract class that needs to have a descendant.
        It contains the game state, a set of static events, two timers, and a stage.
        The class includes the game's start, stop, continue, and end actions in addition to creating two 
        timers and a bundle of static events.
        Running simultaneously, both timers cause two events—OnRun and OnClock—to occur continuously.
        Additionally, registering to push and leave the keys is done in class.
        Two things happen if the corresponding key is pressed or there is an exit. Anyone who signs up for 
        such events responds in a unique way.
    */
    public abstract class Manager
    {
        public Scene Scene {  get; private set; } // the game arena

        public static GameState GameState { get; set; } = GameState.Loaded; // game state

        private static DispatcherTimer _runTimer = new DispatcherTimer(); // OnRun will activate non-stop an event 

        public static GameEvents GameEvent {  get; } = new GameEvents(); // creating the events in GameEvents


        public Manager(Scene scene)
        {          
            Scene = scene;
            //_runTimer = new DispatcherTimer();
            _runTimer.Interval = TimeSpan.FromMilliseconds(1);
            _runTimer.Start();
            _runTimer.Tick += _runTimer_Tick;
            // כך נרשמים לשימוש במקלדת
            Window.Current.CoreWindow.KeyDown += CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp += CoreWindow_KeyUp;
        }

        // trigger the OnKeyUp event when a key in the keyboard is being release. 
        private void CoreWindow_KeyUp(CoreWindow sender, KeyEventArgs args)
        {
            if (GameEvent.OnKeyUp != null) // if there is anyone who registered to this event 
            {
                GameEvent.OnKeyUp(args.VirtualKey); // activate event
            }
        }

        // trigger the OnKeyDown event when a key in the keyboard is being pressed. 
        private void CoreWindow_KeyDown(CoreWindow sender, KeyEventArgs args)
        {
            if (GameEvent.OnKeyDown != null) // if there is anyone who registered to this event 
            {
                GameEvent.OnKeyDown(args.VirtualKey); // activate event
            }
        }

        // trigger the OnRun event for every timer's tick. every 1 milli second.
        protected virtual void _runTimer_Tick(object sender, object e)
        {
            if(GameEvent.OnRun != null) // is there anyone who registered to this event?
            {
                GameEvent.OnRun(); // activate non-stop OnRun event
            }
        }

        // start the game method. each subclass implements this method diffrently
        public virtual void Start()
        {
            GameState = GameState.Started;
        }

        // pause the game method. each subclass implements this method diffrently
        public virtual void Pause()
        {
            GameState = GameState.Paused;
            _runTimer.Tick -= _runTimer_Tick;
        }

        // resume the game method. each subclass implements this method diffrently
        public virtual void Resume() {
            GameState = GameState.Started;
            _runTimer.Tick += _runTimer_Tick;
        }

        // method that run when the user lost and the game is over
        public bool GameOver()
        {
            if(GameState != GameState.GameOver)
            {
                GameState = GameState.GameOver;
                return true;
            }
            return false;
        }

        // each time the game is being unloaded, clear all the events' connections and stop the timer. 
        public virtual void UnsubscribeFunctions()
        {
            _runTimer.Tick -= _runTimer_Tick;
            Window.Current.CoreWindow.KeyDown -= CoreWindow_KeyDown;
            Window.Current.CoreWindow.KeyUp -= CoreWindow_KeyUp;

            GameEvent.ClearEvent();

            GameState = GameState.Loaded;
        }     
    }
}
