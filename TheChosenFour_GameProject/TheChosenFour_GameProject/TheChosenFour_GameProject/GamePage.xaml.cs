using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheChosenFour_GameProject.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheChosenFour_GameProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class GamePage : Page
    {
        GameManager _gameManger; // GameManager object
        public GamePage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonPlay_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            pauseImagePlay.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Pause2.png"));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonPlay_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            pauseImagePlay.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Pause1.png"));
        }

        /// <summary>
        /// paused the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonPlay_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            pauseButtonStop.Visibility = Visibility.Visible;
            _gameManger.Pause();
        }

        /// <summary>
        ///  change the cursor shape when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonStop_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        ///  change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonStop_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// resume the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void pauseButtonStop_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            pauseButtonStop.Visibility = Visibility.Collapsed;
            _gameManger.Resume();
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// pauses the game and shows the help popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            help_grid.Visibility = Visibility.Visible;
            pauseButtonPlay_Click(sender, e);
            
        }

        /// <summary>
        /// this method navigate the user to the MenuPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            menuImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home2.png"));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            menuImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home1.png"));
        }

        /// <summary>
        /// close the help popup and resume the game
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            help_grid.Visibility = Visibility.Collapsed;
            pauseButtonStop_Click(sender, e);
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// when exiting the page, disconnect the events' connections and replace the background music.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            if (MusicPlayer.IsOn)
            {
                MusicPlayer.IsOn = false;
                MusicPlayer.Play("let-the-mystery-unfold-122118.mp3");
            }

            _gameManger.UnsubscribeFunctions();
        }

        /// <summary>
        /// when entering this page, replaces the backroung music and connects several methods
        /// to the GameEvets and AppGameEvents' events. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            if (MusicPlayer.IsOn)
            {
                MusicPlayer.IsOn = false;
                MusicPlayer.Play("Run-Amok(chosic.com).mp3");
            }
            _gameManger = new GameManager(scene);
            Manager.GameEvent.OnKeyDown += StartPlay;
            Manager.GameEvent.OnScoreRefresh += UpdateScore;
            Manager.GameEvent.OnGameOver += GameOver;
            Manager.GameEvent.OnWin += GameWin;
            points.Text = GameManager.Player.Money.ToString();
            playerName.Text = "Player: " + GameManager.Player.UserName;
        }

        /// <summary>
        /// shows popup to declare that the player have won this level. 
        /// </summary>
        private void GameWin()
        {
            wonGame_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// shows popup to declare that the player have lost this level. 
        /// </summary>
        private void GameOver()
        {
            gameOver_grid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// update the player's money text according to the money he earned. 
        /// </summary>
        private void UpdateScore()
        {
            points.Text = GameManager.Player.Money.ToString();
        }

        /// <summary>
        /// start the game 
        /// </summary>
        /// <param name="key"></param>
        private void StartPlay(VirtualKey key)
        {
            if (key == VirtualKey.Right)
            {
                start_grid.Visibility = Visibility.Collapsed;
                Manager.GameEvent.OnKeyDown -= StartPlay;
                _gameManger.Start();
            }
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goHomeButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            goHomeImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home2.png"));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goHomeButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            goHomeImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home1.png"));
        }

        /// <summary>
        /// this method navigate the user to the MenuPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goHomeButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            gameOver_grid.Visibility = Visibility.Collapsed;
            wonGame_grid.Visibility = Visibility.Collapsed;
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// this method catches when the 'enter' or 'space' is being pushed down and mark them as handle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_KeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space || e.Key == VirtualKey.Enter /*&& FocusManager.GetFocusedElement() is Button*/)
            {
                // Cancel the KeyDown event to prevent activation of the Click method
                e.Handled = true;
            }
        }
    }
}
