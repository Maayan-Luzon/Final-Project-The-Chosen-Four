using DB_Project;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheChosenFour_GameProject.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
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
    public sealed partial class MenuPage : Page
    {
        public MenuPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            exitImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Cross2.png"));
            assistingMessage.Text = "Select to exit";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            exitImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Cross1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// opening popup to ask if the player sure if he want to leave. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitButton_Click(object sender, RoutedEventArgs e)
        {
            areYouSureYouWantToLeave_grid.Visibility = Visibility.Visible;
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            soundImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Sound2.png"));
            assistingMessage.Text = "Music control";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            soundImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Sound1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// opening popup to control the music
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void soundButton_Click(object sender, RoutedEventArgs e)
        {
            musicGrid.Visibility = Visibility.Visible;
            backgroundMusicSw.IsOn = MusicPlayer.IsOn;
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            gameImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Play2.png"));
            assistingMessage.Text = "Start playing Level " + GameManager.Level.LevelID;
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            gameImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Play1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the GamePage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            string dbPath = ApplicationData.Current.LocalFolder.Path;
            Frame.Navigate(typeof(GamePage));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signInButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            signInImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SignIn2.png"));
            assistingMessage.Text = "Sign in / Log in";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signInButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            signInImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/SignIn1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the SignPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SignPage));
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void levelsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            levelsImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Levels2.png"));
            assistingMessage.Text = "Choose your level";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void levelsButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            levelsImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Levels1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the LevelsPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void levelsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof (LevelsPage));
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storyButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            storyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Story2.png"));
            assistingMessage.Text = "Story and how to play";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storyButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            storyImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Story1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the HelpPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storyButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(HelpPage));
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recodsButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            recordsImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Record2.png"));
            assistingMessage.Text = "Records";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recodsButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            recordsImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Records1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the Records page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void recodsButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            Frame.Navigate(typeof(Records));
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storageButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            storageImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Storage2.png"));
            assistingMessage.Text = "Game Storage";
        }

        /// <summary>
        /// change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storageButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            storageImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Storage1.png"));
            assistingMessage.Text = string.Empty;
        }

        /// <summary>
        /// this method navigate the user to the Storage page
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void storageButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            Frame.Navigate(typeof(Storage));
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders
        /// </summary> 
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToLeaveButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToLeaveButton_PointerExited(object sender, PointerRoutedEventArgs e) 
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// save the data base and shut sown the program
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToLeaveButton_Click(object sender, RoutedEventArgs e)
        {
            Server.SaveChanges(GameManager.Player);
            Application.Current.Exit();            
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToStayButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToStayButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// closing the "are you sure you want to leave" popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void wantToStayButton_Click(object sender, RoutedEventArgs e)
        {
            areYouSureYouWantToLeave_grid.Visibility = Visibility.Collapsed;
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// start and stop the background music when toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void backgroundMusicSw_Toggled(object sender, RoutedEventArgs e)
        {
            ToggleSwitch sw = (ToggleSwitch)sender;
            if (sw != null)
            {
                if (sw.IsOn)
                {
                    MusicPlayer.Play("let-the-mystery-unfold-122118.mp3");
                }
                else
                {
                    MusicPlayer.Pause();
                }
            }
        }

        /// <summary>
        /// start and stop the sound effects when toggled
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void effectSoundSw_Toggled(object sender, RoutedEventArgs e)
        {
            SoundPlayer.IsOn = effectSoundSw.IsOn;
        }

        /// <summary>
        /// change the music's volume according to the volume the user sets. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void volumeSlider_ValueChanged(object sender, RangeBaseValueChangedEventArgs e)
        {
            MusicPlayer.Volume = volumeSlider.Value;
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okMusicImg_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okMusicImg_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// close the music popup
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okMusicImg_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            musicGrid.Visibility = Visibility.Collapsed;
            SoundPlayer.Play("click.wav");
        }

        /// <summary>
        /// all the methods and values that need to be loaded. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            backgroundMusicSw.IsOn = MusicPlayer.IsOn;
            volumeSlider.Value = MusicPlayer.Volume;

            effectSoundSw.IsOn = SoundPlayer.IsOn;

            playerName.Text = "Hello " + GameManager.Player.UserName;
        }

        /// <summary>
        /// this method catches when the 'enter' or 'space' is being pushed down and mark them as handle. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_PreviewKeyDown(object sender, KeyRoutedEventArgs e)
        {
            if (e.Key == VirtualKey.Space || e.Key == VirtualKey.Enter /*&& FocusManager.GetFocusedElement() is Button*/)
            {
                // Cancel the KeyDown event to prevent activation of the Click method
                e.Handled = true;
            }
        }
    }
}
