using DB_Project;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.ServiceModel.Channels;
using TheChosenFour_GameProject.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Popups;
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
    public sealed partial class SignPage : Page
    {
        public SignPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        ///  this method navigate the user to the MenuPage
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
        /// change the cursor shape when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forgotPasswordGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forgotPasswordGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);

        }

        /// <summary>
        /// TODO: show the user its password after he enters his user name and email. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void forgotPasswordGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            passwordRecoveryGrid.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void okGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);

        }

        /// <summary>
        /// makes sure all of the new user's data is valid and then creates and saves new user. 
        /// In case the data is not good enough, a suitable message will appear.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private /*async*/ void okGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");

            if (userNameUp.Text == "" || passwordBoxUp.Password == "" || passwordBox2Up.Password == ""
                || emailUp.Text == "")
            {
                errorGrid.Visibility = Visibility.Visible;
                emptyInfoBox.Visibility = Visibility.Visible;
                //await new MessageDialog("there are empty boxes. fill them all", "TheChosenFour_GameProject").ShowAsync();
            }
            else if (passwordBoxUp.Password != passwordBox2Up.Password)
            {
                //messageText.Text = "the passwords do not match";
                //messageGrid.Visibility = Visibility.Visible;
                errorGrid.Visibility = Visibility.Visible;
                passwordsNotEqual.Visibility = Visibility.Visible;
            }
            else if (!CheckStrongPassword(passwordBoxUp.Password))
            {
                errorGrid.Visibility = Visibility.Visible;
                weekPassword.Visibility = Visibility.Visible;
            }
            else if (!CheckValidateEmail(emailUp.Text))
            {
                errorGrid.Visibility = Visibility.Visible;
                emailUnvalid.Visibility = Visibility.Visible;
            }
            else
            {
                // present message that all of the data recived successfully
                DataRecivedSuccessfully();

                // 'Trim' delete spaces from string from the start and the end
                int? userId = Server.ValidateUser(userNameUp.Text.Trim(), passwordBoxUp.Password.Trim());
                if (userId == null)
                {
                    // adding the user to the data base
                     GameManager.Player = Server.AddNewUser(userNameUp.Text.Trim(), passwordBoxUp.Password.Trim(), emailUp.Text.Trim());
                    // present message that the user added proparly 
                    goodNewUser_grid.Visibility = Visibility.Visible;
                    welcomeWarrior.Visibility = Visibility.Visible;
                    //Frame.Navigate(typeof(MenuPage)); 
                }
                else
                {
                    // the user already exist. present a message to tell him to go to sign in  
                    errorGrid.Visibility = Visibility.Visible;
                    existingUser.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// close all of the error messages
        /// </summary>
        private void DataRecivedSuccessfully()
        {
            //goodNewUser_grid.Visibility = Visibility.Visible;
            emptyInfoBox.Visibility = Visibility.Collapsed;
            passwordsNotEqual.Visibility = Visibility.Collapsed;
            weekPassword.Visibility = Visibility.Collapsed;
            emailUnvalid.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// validate the email. Check if it conatain @ in it
        /// </summary>
        /// <param name="email"> the email to validat</param>
        /// <returns> is the email valid?</returns>
        private bool CheckValidateEmail(string email)
        {
            if(!email.Contains('@'))
                return false;
            return true;
        }

        /// <summary>
        /// check if the password is strong enough by checking several conditions. 
        /// </summary>
        /// <param name="password">the password</param>
        /// <returns> if the password answer all of the requirments</returns>
        private bool CheckStrongPassword(string password)
        {
            if (password.Length < 8 ||
                !password.Any(char.IsDigit) ||
                !password.Any(char.IsUpper) ||
                !password.Any(char.IsLower) ||
                !password.Any(ch => !char.IsLetterOrDigit(ch))
                //!password.Any(char.IsSymbol)
                )
                return false;

            return true;
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeErrorButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeErrorButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// close all of the error messages by clicking a button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void closeErrorButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            errorGrid.Visibility = Visibility.Collapsed;
            emptyInfoBox.Visibility = Visibility.Collapsed;
            passwordsNotEqual.Visibility = Visibility.Collapsed;
            weekPassword.Visibility = Visibility.Collapsed;
            emailUnvalid.Visibility = Visibility.Collapsed;
            existingUser.Visibility = Visibility.Collapsed;
            wrongUserData.Visibility = Visibility.Collapsed;
        }

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goodNewUserOkButton_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goodNewUserOkButton_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// close the welcome grid 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void goodNewUserOkButton_Click(object sender, RoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            goodNewUser_grid.Visibility = Visibility.Collapsed;
            welcomeWarrior.Visibility = Visibility.Collapsed;
            welcomeBackWarrior.Visibility = Visibility.Collapsed;
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

        /// <summary>
        /// change the cursor shape when the cursor enters the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterSignInGrid_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// change the cursor shape when the cursor exits the button's borders.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterSignInGrid_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
        }

        /// <summary>
        /// enter the user to his old account as long as the details are right.
        /// In case they are not, a meesage will show up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void enterSignInGrid_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            int? userId = Server.ValidateUser(UserNameIn.Text.Trim(), PasswordBoxIn.Password.Trim());
            if(userId.HasValue )
            {
                GameManager.Player = Server.GetUser(userId.Value);
                // welcom back message
                goodNewUser_grid.Visibility = Visibility.Visible;
                welcomeBackWarrior.Visibility = Visibility.Visible;
            }
            else
            {
                // the datat is incorrect.
                errorGrid.Visibility = Visibility.Visible;
                wrongUserData.Visibility = Visibility.Visible;
            }
        }

        /// <summary>
        /// recover the user's password
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void findPassword_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            string password = Server.FindPassword(recoveyUserNameBox.Text.Trim(), recoveyEmailBox.Text.Trim());
            if(password == null)
                recoveryPassword.Text = "Invalid data. \n Create new account";
            else 
                recoveryPassword.Text = password;
        }

        /// <summary>
        /// close the password's recovery
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitRecveryPassword_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            recoveryPassword.Text = "";
            passwordRecoveryGrid.Visibility = Visibility.Collapsed;
            recoveyUserNameBox.Text = "";
            recoveyEmailBox.Text = "";
        }
    }
}
