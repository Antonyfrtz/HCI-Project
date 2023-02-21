using BespokeFusion;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using WpfApp1.UserMenuItems;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserHome1.xaml
    /// </summary>
    public partial class UserHome1 : Window
    {
        UserOrderPanel lunch = new UserOrderPanel(menuPages,_imageFiles);
        UserOrderPanel wine = new UserOrderPanel(winemenuPages, W_imageFiles);
        public UserHome1()
        {
            InitializeComponent(); // by defaults opens to homepage
            //UserControl main = new UserControlHome();
            //ListViewMenu.SelectedItem = ItemHome;
            //GridMain.Children.Add(main);
            DataContext = this;
            CreateResponseCard("Hello! My name is Demetra, the goddess of harvest and agriculture, presiding over crops, grains, food, and the fertility of the earth.");
            CreateResponseCard("I welcome you to the Palace of Zeus. How may I assist you today?");
        }

        public UserHome1(string message)
        {
            InitializeComponent();
            CreateResponseCard("Why are u gay");
        }

        // Various images for different menus
        private static string[] _imageFiles = { "../Assets/menus/menu_main_1.png", "../Assets/menus/menu_main_2.png", "../Assets/menus/menu_main_3.png", "../Assets/menus/menu_main_4.png" };
        private static string[] W_imageFiles = { "../Assets/wines/wine_1.png", "../Assets/wines/wine_2.png", "../Assets/wines/wine_3.png" , "../Assets/wines/wine_4.png", "../Assets/wines/wine_5.png" };
        
        // this function will provide text to display for card depending on what the button text is
        private async void DisplayCardByBtnTextAsync(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            // text of button's inner textblock
            string btnText = (string)((StackPanel)button.Content).Children[0].GetValue(TextBlock.TextProperty);
            switch (btnText)
            {
                case "Say Hello":
                    CreateUserCard("Hi! I am the user");
                    break;
                case "Lunch Menu":
                    Vokis.Source = new Uri("../../../Assets/voki/Menu.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("I'd like to view the lunch menu, please");
                    CreateResponseCard("Sure! Have a look.");
                    await Task.Delay(1500);
                    DrawerHost.RightDrawerContent = lunch;
                    DrawerHost.IsRightDrawerOpen = true;
                    CreateResponseCard("You can order any items you wish by selecting them in the panel below the menu, which you can navigate using the arrows.");
                    break;
                case "Breakfast Menu":
                    Vokis.Source = new Uri("../../../Assets/voki/Dishes.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("What are my options for breakfast?");
                    CreateResponseCard("We have a great variety of dishes available!");
                    await Task.Delay(1500);
                    DrawerHost.IsRightDrawerOpen = true;
                    break;
                case "Cocktails & Spirits":
                    Vokis.Source = new Uri("../../../Assets/voki/Drinks.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("I'd like to have a drink");
                    CreateResponseCard("Select any drink you would like from our vast selection.");
                    break;
                case "Wine Catalog":
                    Vokis.Source = new Uri("../../../Assets/voki/Wines.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("Show me the wine catalog please.");
                    CreateResponseCard("Here is a list of our available wines.");
                    await Task.Delay(1500);
                    DrawerHost.RightDrawerContent = wine;
                    DrawerHost.IsRightDrawerOpen = true;
                    break;
                default:
                    CreateUserCard("An error has occured. Please contact front desk for more info");
                    break;
            }
            chatScroll.ScrollToEnd();
        }

        private void CreateUserCard(string query)
        {
            // Card properties
            Card userCard = new(){
                Padding = new Thickness(8),
                Margin = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = new SolidColorBrush(Color.FromRgb(219, 84, 97)),
                Foreground = (Brush)FindResource("PrimaryHueDarkForegroundBrush"),
                UniformCornerRadius = 6,
                Content = new TextBlock{
                    Text = query, // user query text passed as a parameter
                    TextWrapping = TextWrapping.Wrap,
                    TextAlignment = TextAlignment.Right
                }
            };
            // Add the user card to the stack panel
            chat.Children.Add(userCard);

            // Create a fade-in animation for the user card
            var fadeInAnimation = new DoubleAnimation
            {
                From = 0,
                To = 1,
                Duration = new Duration(TimeSpan.FromSeconds(1))
            };

            // Set up the storyboard for the animation
            var storyboard = new Storyboard();
            Storyboard.SetTarget(fadeInAnimation, userCard);
            Storyboard.SetTargetProperty(fadeInAnimation, new PropertyPath(UIElement.OpacityProperty));
            storyboard.Children.Add(fadeInAnimation);

            // Start the animation
            storyboard.Begin();
        }

        private void CreateResponseCard(string reply)
        {
            // Card properties
            Card replyCard = new()
            {
                Width = 200,
                Padding = new Thickness(8),
                Margin = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Left,
                Background = new SolidColorBrush(Color.FromRgb(177, 133, 167)),
                Foreground = (Brush)FindResource("PrimaryHueDarkForegroundBrush"),
                UniformCornerRadius = 6,
                Content = new TextBlock
                {
                    Text = reply, // user query text passed as a parameter
                    TextWrapping = TextWrapping.Wrap,
                }
            };

            // Use a DispatcherTimer to delay the display of the card
            var timer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(1) };
            timer.Tick += (sender, args) =>
            {
                // Stop the timer
                timer.Stop();

                // Start the FadeInAnimation storyboard
                var fadeInAnimation = (Storyboard)FindResource("FadeInAnimation");
                fadeInAnimation.Begin(replyCard);
                // Add the user card to the stack panel
                chat.Children.Add(replyCard);
            };
            timer.Start();
        }

        private void ButtonOpenMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Visible;
            ButtonOpenMenu.Visibility = Visibility.Collapsed;
        }

        private void ButtonCloseMenu_Click(object sender, RoutedEventArgs e)
        {
            ButtonCloseMenu.Visibility = Visibility.Collapsed;
            ButtonOpenMenu.Visibility = Visibility.Visible;
        }

        int menu=0;
        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridMain.Children.Clear(); // remove current selection from main grid
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    menu = 0;
                    GridMain.Children.Add(new UserControlHome());
                    break;
                case "ItemPool":
                    menu = 1;
                    GridMain.Children.Add(new UserControlPool());
                    break;
                case "ItemTrojanHorse":
                    menu = 2;
                    GridMain.Children.Add(new UserControlTrojanHorse());
                    break;
                case "ItemRestaurant":
                    menu = 3;
                    GridMain.Children.Add(new UserControlRestaurant());
                    break;
                default:
                    break;
            }
            if (ButtonCloseMenu.Visibility == Visibility.Visible)
            {
                ButtonCloseMenu.RaiseEvent(new RoutedEventArgs(System.Windows.Controls.Primitives.ButtonBase.ClickEvent));
            }
        }

        private void InfoBtn_Click(object sender, RoutedEventArgs e)
        {
            CustomMaterialMessageBox msg = new CustomMaterialMessageBox
            {
                TxtMessage = { Text = Message(), Foreground = Brushes.Black },
                TxtTitle = { Text = "Information", Foreground = Brushes.White },
                BtnOk = { Content = "Okay", Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546"), BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546") },
                BtnCancel = { Content = "Cancel", Visibility = Visibility.Collapsed },
                MainContentControl = { Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#FFAEAEAE") },
                TitleBackgroundPanel = { Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546") },

                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546")
            };

            msg.Show();
            MessageBoxResult results = msg.Result;
        }

        private string Message()
        {
            switch (menu)
            {
                case 0: // Main menu
                    return
                    "You are currently in the Main menu. Here you can access all controls for the room.\n\n"+
                    "On the room Control Card you can turn the lights on and off using the button. You are informed about the lighting status with a message that tells you if you have turned the lights on or off, the image also changes.Also you can turn on or off the Television or the Air-Conditioner using the relevant button.You are informed of their status with a message and the icon changes.\n\n"+
                    "In the left part of the screen there is a menu which includes all the utilities you have access to. \n\n" +
                    "To enter the pool managment system click the pool icon from the menu. \n" +
                    "To use the Trojan Horse RV you rented click the trojan horse icon from the menu. \n" +
                    "If you want to enter the restaurant and order food or drinks click the restaurant icon from the menu. \n" +
                    "If you want to return to the home screen click the home icon from the menu. \n\n" +
                    "In the right top part of the screen there is a three dot button. If you click it you can logout and you can also click on Help which will open the User Manual. \n\n" +
                    "Thank you for using The Palace of Zeus app. We hope this user manual helps you enjoy your stay at our electronic theme hotel-park!\n\n" +
                    "Created by Antonios Fritzelas and Dimitrios Sidiropoulos.\n";
                case 1: // Pool
                    return
                    "You are currently in the pool management area. Here you can access all controls for the pool.\n\n" +
                    "You can activate or deactivate the alarm by clicking the alarm button.\n" +
                    "You can turn on and off the lights of the pool by clicking the lights button.\n" +
                    "You can raise or lower the temperature of the pool using the slider. If the temperature goes above 18 degrees Celsius then the slider changes color to red.\n" +
                    "For the water level of the pool you can use the slider.You can fill, half - fill or empty the pool.\n" +
                    "It is also reported if the hotel's central pool is open and if there is a person in the private pool then this is reported in the window.\n"+
                    "If you want to return to the home screen click the home icon from the menu. \n\n" +
                    "In the right top part of the screen there is a three dot button. If you click it you can logout and you can also click on Help which will open the User Manual. \n\n" +
                    "Thank you for using The Palace of Zeus app. We hope this user manual helps you enjoy your stay at our electronic theme hotel-park!\n\n" +
                    "Created by Antonios Fritzelas and Dimitrios Sidiropoulos.\n";
                case 2: // Trojan
                    return
                    "You are currently in the Trojan horse GPS. Here you can navigate using the on-screen buttons.\n\n" +
                    "The map opens showing your position and the two destinations: the gardens and the palace of Zeus.\n" +
                    "You can drive the Trojan horse with the controls or otherwise with the arrows from your keyboard.\n" +
                    "Pressing the button of the gardens or the palace of Zeus starts the navigation which shows the course you must follow to reach your destination.\n" +
                    "To park the trojan horse you must not be in navigation. Pressing the Park button opens the four modes.\n"+
                    "Open the door, half - open the door, close the door and activate the ladder. When you select one of the options, a Related message appears And the image changes.\n" +
                    "If you want to return to the home screen click the home icon from the menu. \n\n" +
                    "In the right top part of the screen there is a three dot button. If you click it you can logout and you can also click on Help which will open the User Manual. \n\n" +
                    "Thank you for using The Palace of Zeus app. We hope this user manual helps you enjoy your stay at our electronic theme hotel-park!\n\n" +
                    "Created by Antonios Fritzelas and Dimitrios Sidiropoulos.\n";
                case 3: // Restaurant
                    return
                    "You are currently in the restaurant ordering page. Here you can order from our facilities by interacting with our chatbot.\n\n" +
                    "By pressing the buttons of breakfast, lunch, cocktail or wine menu the relevant menu appears on the right side and Goddess Dimitra guides you.\n" +
                    "There are arrows so you can navigate in the pages of the menu.\n" +
                    "At the bottom of the menu you can choose what you want to order by pressing the relevant buttons.\n" +
                    "Pressing the save button closes the menu. And in the lower right part of the window, your order, the dishes you have chosen as well as the total amount are displayed.\n" +
                    "If you have selected at least one dish then the payment button is activated and by pressing it you are taken to the payment window.\n" +
                    "The payment window shows the total amount you have to pay as well as the appropriate fields to fill in your credit card information.\n" +
                    "By pressing the pay now button, your order is completed and Goddess Dimitra informs you of its success. Otherwise if you choose to close the payment window or press the cancel your payment button, a relevant message will appear informing you that your order will be lost and you will have to do it from the beginning.\n" +
                    "Otherwise if you press the Clear button in the menu then the selections you have made are deleted.\n" +
                    "If you want to return to the home screen click the home icon from the menu. \n\n" +
                    "In the right top part of the screen there is a three dot button. If you click it you can logout and you can also click on Help which will open the User Manual. \n\n" +
                    "Thank you for using The Palace of Zeus app. We hope this user manual helps you enjoy your stay at our electronic theme hotel-park!\n\n" +
                    "Created by Antonios Fritzelas and Dimitrios Sidiropoulos.\n";
            }
            return "";
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToLogin = new MainWindow();
            backToLogin.Show();
            this.Close();
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            string path = @".\..\..\..\Resources\UserManual.pdf";
            Process.Start(
            new ProcessStartInfo{
                FileName = path,
                UseShellExecute = true
            });
        }
        private void paybtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow paymentWindow = new PaymentWindow(Total.Text);
            paymentWindow.Show();
            this.Close();
        }

        static List<MenuPage> menuPages = new List<MenuPage>
        {
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Kappadokia", Price = 19 },
                    new MenuItem { Name = "Mount Zas", Price = 18 },
                    new MenuItem { Name = "The Caldera", Price = 33 },
                    new MenuItem { Name = "The Classic", Price = 21 },
                    new MenuItem { Name = "Mediterranean", Price = 35 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Mountaineer", Price = 24 },
                    new MenuItem { Name = "Aegean", Price = 32 },
                    new MenuItem { Name = "Ionian", Price = 29 },
                    new MenuItem { Name = "Navagio", Price = 28 },
                    new MenuItem { Name = "Kavouri", Price = 34 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Macedonian", Price = 29 },
                    new MenuItem { Name = "Fish of the day", Price = 53 },
                    new MenuItem { Name = "Scordalia", Price = 54 },
                    new MenuItem { Name = "Sea & Pasta Mix", Price = 54 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Kotariz", Price = 38 },
                    new MenuItem { Name = "Athenian", Price = 49 },
                    new MenuItem { Name = "Olympian", Price = 57 },
                    new MenuItem { Name = "Chips", Price = 0 },
                    new MenuItem { Name = "Puree", Price = 0 },
                    new MenuItem { Name = "Greens", Price = 0 }
                }
            }
        };

        static List<MenuPage> winemenuPages = new List<MenuPage>
        {
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Zacharias Winery", Price = 25 },
                    new MenuItem { Name = "Karipidis Estate", Price = 35 },
                    new MenuItem { Name = "Vientzi Papagiannakos", Price = 38 },
                    new MenuItem { Name = "Ktima Gerovassilou", Price = 44 },
                    new MenuItem { Name = "Domaine Kikones", Price = 50 },
                    new MenuItem { Name = "Nykteri Reserve Santo Wines", Price = 71}
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Driopi Nemea Domaine Tselepos", Price = 28 },
                    new MenuItem { Name = "Chatzivaritis Estate Goumenissa", Price = 39 },
                    new MenuItem { Name = "Oenops Wines", Price = 43 },
                    new MenuItem { Name = "Cyrus One Kyros Melas", Price = 46 },
                    new MenuItem { Name = "Emphasis, Pavlidi Estate", Price = 52 },
                    new MenuItem { Name = "Taos Paparousi Wines" , Price = 60}
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Vissinokipos,Palivou Estate", Price = 27 },
                    new MenuItem { Name = "Theopetra Estate", Price = 39 },
                    new MenuItem { Name = "Idylle D'Achinos, La Tour Melas", Price = 43 },
                    new MenuItem { Name = "Alpha Estate", Price = 54 },
                    new MenuItem { Name = "Dianthos, Boutari Estate", Price = 52 },
                    new MenuItem { Name = "Mavrose 2022, Tiniakoi Ampelones", Price = 69 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Akakies Kir-Yanni Sparkling", Price = 38 },
                    new MenuItem { Name = "Karanika Brut Cuvee Speciale", Price = 43 },
                    new MenuItem { Name = "Douroufakis Sparkling Brut", Price = 48 },
                    new MenuItem { Name = "Santo White Sparkling Brut", Price = 59 },
                    new MenuItem { Name = "Muscat Rio Patras Parparousis", Price = 42 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Moet & Chandon Brut Imperial", Price = 120 },
                    new MenuItem { Name = "Taittinger Nocturne City Lights", Price = 160 },
                    new MenuItem { Name = "Dom Perignon Vintage", Price = 450 },
                    new MenuItem { Name = "Cristal Brut Louis Roederer", Price = 620 }
                }
            }
        };

    }
}