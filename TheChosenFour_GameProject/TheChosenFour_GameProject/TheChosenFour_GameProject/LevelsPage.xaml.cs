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
using Windows.UI;
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
    public sealed partial class LevelsPage : Page
    {
        public LevelsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// this method navigate the user to the MenuPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(MenuPage));
            SoundPlayer.Play("click.wav");
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
        ///  change the cursor shape and the image source when the cursor exits the button's borders. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            menuImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Buttons/Home1.png"));
        }

        /// <summary>
        /// change the cursor shape and the color of the level's name when the cursor enters the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level1Grid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            var grid = (Grid)sender;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            switch (grid.Name)
            {
                case "level1Grid":                   
                    level1Text.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;

                case "level2Grid":
                    level2Text.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;

                case "level3Grid":
                    level3Text.Foreground = new SolidColorBrush(Colors.Yellow);
                    break;
            }
            
            
        }

        /// <summary>
        /// change the cursor shape and the color of the level's name when the cursor exits the button's borders
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level1Grid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
            var grid = (Grid)sender;
            switch (grid.Name)
            {
                case "level1Grid":
                    /*SolidColorBrush color = level1Text.Foreground as SolidColorBrush;
                    if (color.Color == Colors.Aqua)*/
                    if(GameManager.Level.LevelID == 1)
                        level1Text.Foreground = new SolidColorBrush(Colors.Aqua);
                    else
                        level1Text.Foreground = new SolidColorBrush(Colors.White);
                    break;

                case "level2Grid":
                    if(GameManager.Level.LevelID == 2)
                        level2Text.Foreground = new SolidColorBrush(Colors.Aqua);
                    else
                        level2Text.Foreground = new SolidColorBrush(Colors.White);
                    break;

                case "level3Grid":
                    if (GameManager.Level.LevelID == 3) 
                        level3Text.Foreground = new SolidColorBrush (Colors.Aqua);
                    else
                        level3Text.Foreground = new SolidColorBrush(Colors.White);
                    break;
            }
        }

        /// <summary>
        /// choose the first level to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level1Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            GameManager.Level.LevelID = 1;
            GameManager.Player.CurrentLevel.LevelID = 1;
            GameManager.Player.CurrentLevel.LevelNumber = 1;
            level1Text.Foreground = new SolidColorBrush(Colors.Aqua);
            level2Text.Foreground = new SolidColorBrush(Colors.White);
            level3Text.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// choose the second level to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level2Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            GameManager.Level.LevelID = 2;
            GameManager.Player.CurrentLevel.LevelID = 2;
            GameManager.Player.CurrentLevel.LevelNumber = 2;
            level2Text.Foreground = new SolidColorBrush(Colors.Aqua);
            level1Text.Foreground = new SolidColorBrush(Colors.White);
            level3Text.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// choose the third level to play
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void level3Grid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            GameManager.Level.LevelID = 3;
            GameManager.Player.CurrentLevel.LevelID = 3;
            GameManager.Player.CurrentLevel.LevelNumber = 3;
            level3Text.Foreground = new SolidColorBrush(Colors.Aqua);
            level2Text.Foreground = new SolidColorBrush(Colors.White);
            level1Text.Foreground = new SolidColorBrush(Colors.White);
        }

        /// <summary>
        /// this method navigate the user to the MenuPage
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            Frame.Navigate(typeof(MenuPage));
        }

        /// <summary>
        /// changes the levels' name colors according to the wanted level
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            switch (GameManager.Level.LevelID)
            {
                case 1:
                    level1Text.Foreground = new SolidColorBrush (Colors.Aqua);
                    break;

                case 2:
                    level2Text.Foreground= new SolidColorBrush (Colors.Aqua);
                    break;

                case 3:
                    level3Text.Foreground = new SolidColorBrush(Colors.Aqua);
                    break;
            }
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
