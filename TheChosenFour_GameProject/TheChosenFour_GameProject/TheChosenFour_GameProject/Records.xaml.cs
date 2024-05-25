using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
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
    public sealed partial class Records : Page
    {
        public Records()
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
