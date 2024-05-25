using DB_Project;
using DB_Project.Models;
using GameEngine.GameServices;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using TheChosenFour_GameProject.GameServices;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Store;
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
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace TheChosenFour_GameProject
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Storage : Page
    {
        private string currentHero = "Musketeer"; // the current hero that on the screen
        private string currentGrid = "simpleWoodAndStrongWoodGrid"; // the current grid that on the screen
        private List<Image> productsImages = new List<Image>(); // the list of all the products in the store
       
        public Storage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// fill the productsImages list with all the products. 
        /// </summary>
        private void FillProductsImages()
        {
            this.productsImages.Add(simpleWoodShoe);
            this.productsImages.Add(simpleWoodShirt);
            this.productsImages.Add(simpleWoodSword);
            this.productsImages.Add(simpleWoodShield);
            this.productsImages.Add(simpleWoodBow);

            this.productsImages.Add(strongWoodShoe);
            this.productsImages.Add(strongWoodShirt);
            this.productsImages.Add(strongWoodSword);
            this.productsImages.Add(strongWoodShield);
            this.productsImages.Add(strongWoodBow);

            this.productsImages.Add(simpleIronShoe);
            this.productsImages.Add(simpleIronShirt);
            this.productsImages.Add(simpleIronSword);
            this.productsImages.Add(simpleIronShield);
            this.productsImages.Add(simpleIronBow);

            this.productsImages.Add(strongSilverShoe);
            this.productsImages.Add(strongSilverShirt);
            this.productsImages.Add(strongSilverSword);
            this.productsImages.Add(strongSilverShield);
            this.productsImages.Add(strongSilverBow);

            this.productsImages.Add(goldShoe);
            this.productsImages.Add(goldShirt);
            this.productsImages.Add(goldSword);
            this.productsImages.Add(goldShield);
            this.productsImages.Add(goldBow);
        }

        /// <summary>
        /// method to change the cursor shape when the cursor enters the button's borders
        /// </summary>
        private void pointerEntered()
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        /// <summary>
        /// method to change the cursor shape when the cursor exits the button's borders
        /// </summary>
        private void pointerExited()
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 1);
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
        /// calls the pointerEnterd method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leftIronArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerEntered();
        }

        /// <summary>
        /// calls the pointerExited method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leftIronArrow_PointerEnxited(object sender, PointerRoutedEventArgs e)
        {
            pointerExited();
        }

        /// <summary>
        /// replace the current hero. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void leftIronArrow_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            switch (currentHero)
            {
                case "Musketeer":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Knight/knightIdle.gif"));
                    currentHero = "Knight";
                    break;

                case "Knight":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Archer/archerIdle.gif"));
                    currentHero = "Archer";
                    break;

                case "Archer":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Wizard/wizardIdle.gif"));
                    currentHero = "Wizard";
                    break;

                case "Wizard":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Musketeer/musketeerIdle.gif"));
                    currentHero = "Musketeer";
                    break;
            }
            LoadProductsForAllConnectedObjects();
        }

        /// <summary>
        /// calls the pointerEnterd method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightIronArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerEntered();
        }

        /// <summary>
        /// calls the pointerExited method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightIronArrow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pointerExited();
        }

        /// <summary>
        /// replace the current hero.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rightIronArrow_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            switch (currentHero)
            {
                case "Archer":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Knight/knightIdle.gif"));
                    currentHero = "Knight";
                    break;

                case "Wizard":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Archer/archerIdle.gif"));
                    currentHero = "Archer";
                    break;

                case "Musketeer":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Wizard/wizardIdle.gif"));
                    currentHero = "Wizard";
                    break;

                case "Knight":
                    heroImage.Source = new BitmapImage(new Uri("ms-appx:///Assets/Characters/Heros/Musketeer/musketeerIdle.gif"));
                    currentHero = "Musketeer";
                    break;
            }
            LoadProductsForAllConnectedObjects();
        }

        /// <summary>
        /// calls the pointerEnterd method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteLeftArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerEntered();
        }

        /// <summary>
        /// calls the pointerExited method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteLeftArrow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pointerExited();
        }

        /// <summary>
        /// replcae the current products grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteLeftArrow_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            switch (currentGrid)
            {
                case "simpleWoodAndStrongWoodGrid":
                    simpleWoodAndStrongWoodGrid.Visibility = Visibility.Collapsed;
                    currentGrid = simpleIronAndStrongSilverGrid.Name;
                    simpleIronAndStrongSilverGrid.Visibility = Visibility.Visible;
                    break;

                case "simpleIronAndStrongSilverGrid":
                    simpleIronAndStrongSilverGrid.Visibility = Visibility.Collapsed;
                    currentGrid = goldGrid.Name;
                    goldGrid.Visibility = Visibility.Visible;
                    break;

                case "goldGrid":
                    goldGrid.Visibility = Visibility.Collapsed;
                    currentGrid = simpleWoodAndStrongWoodGrid.Name;
                    simpleWoodAndStrongWoodGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        /// <summary>
        /// calls the pointerEnterd method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteRightArrow_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerEntered();
        }

        /// <summary>
        /// calls the pointerExited method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteRightArrow_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pointerExited();
        }

        /// <summary>
        /// replcae the current products grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void whiteRightArrow_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            switch (currentGrid)
            {
                case "simpleWoodAndStrongWoodGrid":
                    simpleWoodAndStrongWoodGrid.Visibility = Visibility.Collapsed;
                    currentGrid = goldGrid.Name;
                    goldGrid.Visibility = Visibility.Visible;
                    break;

                case "simpleIronAndStrongSilverGrid":
                    simpleIronAndStrongSilverGrid.Visibility = Visibility.Collapsed;
                    currentGrid = simpleWoodAndStrongWoodGrid.Name;
                    simpleWoodAndStrongWoodGrid.Visibility = Visibility.Visible;
                    break;

                case "goldGrid":
                    goldGrid.Visibility = Visibility.Collapsed;
                    currentGrid = simpleIronAndStrongSilverGrid.Name;
                    simpleIronAndStrongSilverGrid.Visibility = Visibility.Visible;
                    break;
            }
        }

        // <summary>
        /// calls the pointerEnterd method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clothingItem_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            pointerEntered();
        }

        // <summary>
        /// calls the pointerExited method. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clothingItem_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            pointerExited();
        }

        /// <summary>
        /// buying the product. shows suitable messages for cases of not enough money or
        /// when the user have already bought the product. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clothingItem_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            SoundPlayer.Play("click.wav");
            // to show an message if the user have alredy bought that cloth
            // if not, to show an message "are you sure you want to buy?"
            // to show an message if does not have enough money
            // if all good, let him buy the item. 

            Product desireProduct = Server.GetProduct(((Image)sender).Name);
            messageGrid.Visibility = Visibility.Visible;
            List<int> idOwnList = new List<int>();
            idOwnList = Server.GetOwnProductsId(GameManager.Player); // List of own Fitchers
            idOwnList = idOwnList == null ? new List<int>() : idOwnList;
            if (idOwnList.Contains(desireProduct.ProductId))
            {
                purchasedProductMessage.Visibility = Visibility.Visible;
            }           
            else
            {
                if (desireProduct.ProductPrice > GameManager.Player.Money) // not enough money
                {
                    notEnoughMoneyMessage.Visibility = Visibility.Visible;
                }
                else
                {
                    Server.AddProductToSet(GameManager.Player, currentHero, desireProduct.ProductName); // Add the object to the set
                    GameManager.Player.Money -= desireProduct.ProductPrice;
                    amountOfMoney.Text = GameManager.Player.Money.ToString();
                    Server.AddUserProduct(GameManager.Player.UserId, desireProduct.ProductId);
                   // Price_Loaded(FindChild<TextBlock>(((Image)sender).Parent, desireProduct.ProductName + "Price"), e);
                    Price_Loaded(((Grid)(((Image)sender).Parent)).FindName(desireProduct.ProductName + "Price"), e);
                    LoadProductsForAllConnectedObjects();
                    boughtTheItemMessage.Visibility = Visibility.Visible;
                }
            }
        }

        /// <summary>
        /// when the page is being loaded, the amount of money on the screen will be updated,
        /// the productsImages will be filled and all the bought product will be marked. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            amountOfMoney.Text = GameManager.Player.Money.ToString();
            FillProductsImages();
            LoadProductsForAllConnectedObjects();
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
        /// load the price for each product. if the user bought the product,
        /// it price will be 0. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Price_Loaded(object sender, RoutedEventArgs e)
        {
            var product=(TextBlock)sender; 
            product.Text = Server.GetProduct(product.Name.Replace("Price", "")).ProductPrice.ToString();

            Product desireProduct = Server.GetProduct((product.Name.Replace("Price", "")));
            List<int> idOwnList = new List<int>();
            idOwnList = Server.GetOwnProductsId(GameManager.Player); // List of own Fitchers
            idOwnList = idOwnList == null ? new List<int>() : idOwnList;
            if (idOwnList.Contains(desireProduct.ProductId))
            {
                product.Text = "0";
            }

            
        }

        /// <summary>
        /// mark all the use product of the current hero with blue rectangle.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Product_Loaded(object sender, RoutedEventArgs e)
        {
            List<string> heroProducts = Server.HeroProducts(GameManager.Player, currentHero) == null ?
                new List<string>() : Server.HeroProducts(GameManager.Player, currentHero);
            if (heroProducts.Contains(((Image)sender).Name))
            {
                Rectangle rectangle = new Rectangle();
                rectangle.Name = "rectangle" + ((Image)sender).Name;
                Grid.SetRow(rectangle, Grid.GetRow((Image)sender));
                Grid.SetColumn(rectangle, Grid.GetColumn((Image)sender));
                rectangle.Stroke = new SolidColorBrush(Colors.CadetBlue); // Set the color of the border
                rectangle.StrokeThickness = 5;

                Grid parentGrid = FindParentGrid((UIElement)sender);
                if (parentGrid != null)
                {
                    parentGrid.Children.Add(rectangle);
                }
            }
            else
            {
                Grid parentGrid = FindParentGrid((UIElement)sender);
                Rectangle rectangle = (Rectangle)parentGrid.FindName("rectangle" + ((Image)sender).Name);
                parentGrid.Children.Remove(rectangle);
            }
        }

        /// <summary>
        /// calls the Product_Loaded() for all the products. 
        /// </summary>
        private void LoadProductsForAllConnectedObjects()
        {
            foreach (var image in productsImages)
            {
                Product_Loaded(image, null);
            }
        }

        /// <summary>
        /// shows explenation how to use the store.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void helpButton_Click(object sender, RoutedEventArgs e)
        {
            messageGrid.Visibility = Visibility.Visible; 
            helpMessage.Visibility = Visibility.Visible;
        }

        /// <summary>
        /// close all explenations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Image_PointerPressed(object sender, PointerRoutedEventArgs e)
        {
            messageGrid.Visibility = Visibility.Collapsed;
            helpMessage.Visibility = Visibility.Collapsed;
            notEnoughMoneyMessage.Visibility = Visibility.Collapsed;
            purchasedProductMessage.Visibility = Visibility.Collapsed;
            boughtTheItemMessage.Visibility = Visibility.Collapsed;
        }


        /// <summary>
        /// Finds a Child of a given item in the visual tree. 
        /// Source: https://stackoverflow.com/questions/636383/how-can-i-find-wpf-controls-by-name-or-type/1759923#1759923
        /// </summary>
        /// <param name="parent">A direct parent of the queried item.</param>
        /// <typeparam name="T">The type of the queried item.</typeparam>
        /// <param name="childName">x:Name or Name of child. </param>
        /// <returns>The first parent item that matches the submitted type parameter. 
        /// If not matching item can be found, 
        /// a null parent is being returned.</returns>
        public static T FindChild<T>(DependencyObject parent, string childName)
           where T : DependencyObject
        {
            // Confirm parent and childName are valid. 
            if (parent == null) return null;

            T foundChild = null;

            int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
            for (int i = 0; i < childrenCount; i++)
            {
                var child = VisualTreeHelper.GetChild(parent, i);
                // If the child is not of the request child type child
                T childType = child as T;
                if (childType == null)
                {
                    // recursively drill down the tree
                    foundChild = FindChild<T>(child, childName);

                    // If the child is found, break so we do not overwrite the found child. 
                    if (foundChild != null) break;
                }
                else if (!string.IsNullOrEmpty(childName))
                {
                    var frameworkElement = child as FrameworkElement;
                    // If the child's name is set for search
                    if (frameworkElement != null && frameworkElement.Name == childName)
                    {
                        // if the child's name is of the request name
                        foundChild = (T)child;
                        break;
                    }
                }
                else
                {
                    // child element found.
                    foundChild = (T)child;
                    break;
                }
            }
            return foundChild;
        }

        /// <summary>
        /// find the parent grid of a known product. 
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        private Grid FindParentGrid(UIElement element)
        {
            DependencyObject parent = VisualTreeHelper.GetParent(element);

            while (parent != null && !(parent is Grid))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as Grid;
        }
    }
}
