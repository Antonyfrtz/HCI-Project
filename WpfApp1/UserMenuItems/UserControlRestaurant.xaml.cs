using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WpfApp1.UserMenuItems
{
    /// <summary>
    /// Interaction logic for UserControlRestaurant.xaml
    /// </summary>
    public partial class UserControlRestaurant : UserControl
    {
        // Various images for different menus
        private static string[] _imageFiles = { "../Assets/menus/menu_main_1.png", "../Assets/menus/menu_main_2.png", "../Assets/menus/menu_main_3.png", "../Assets/menus/menu_main_4.png" };
        private static string[] W_imageFiles = { "../Assets/wines/wine_1.png", "../Assets/wines/wine_2.png", "../Assets/wines/wine_3.png", "../Assets/wines/wine_4.png", "../Assets/wines/wine_5.png" };
        private static string[] D_imageFiles = { "../Assets/drinks/drink_1.png", "../Assets/drinks/drink_2.png", "../Assets/drinks/drink_3.png", "../Assets/drinks/drink_4.png", "../Assets/drinks/drink_5.png", "../Assets/drinks/drink_6.png", "../Assets/drinks/drink_7.png", "../Assets/drinks/drink_8.png" };
        private static string[] DE_imageFiles = { "../Assets/dessert/dessert_1.png", "../Assets/dessert/dessert_2.png" };
        private static string[] B_imageFiles = { "../Assets/breakfast/coffee.png", "../Assets/breakfast/breakfast_1.png", "../Assets/breakfast/breakfast_2.png", "../Assets/breakfast/breakfast_3.png" };
        // Various panels to show
        UserOrderPanel lunch = new UserOrderPanel(menuPages, _imageFiles, "maindishes");
        UserOrderPanel wine = new UserOrderPanel(winemenuPages, W_imageFiles, "wines");
        UserOrderPanel drink = new UserOrderPanel(drinkmenuPages, D_imageFiles, "drinks");
        UserOrderPanel dessert = new UserOrderPanel(dessertmenuPages, DE_imageFiles, "desserts");
        UserOrderPanel breakfast = new UserOrderPanel(breakfastmenuPages, B_imageFiles, "breakfasts");

        public UserControlRestaurant()
        {
            InitializeComponent();
            DataContext = this;
            CreateResponseCard("Hello! My name is Demetra, the goddess of harvest and agriculture, presiding over crops, grains, food, and the fertility of the earth.");
            CreateResponseCard("I welcome you to the Palace of Zeus. How may I assist you today?");
        }

        public UserControlRestaurant(string message, Uri uri)
        {
            UserOrderPanel.total = 0;
            InitializeComponent();
            DataContext = this;
            CreateResponseCard(message);
            Vokis.Source = uri;
        }

        // this function will provide text to display for card depending on what the button text is
        private async void DisplayCardByBtnTextAsync(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            // text of button's inner textblock
            string btnText = (string)((StackPanel)button.Content).Children[0].GetValue(TextBlock.TextProperty);
            switch (btnText)
            {
                case "Say Hello":
                    CreateUserCard("Hi Demetra!");
                    CreateResponseCard("Hello! My name is Demetra, the goddess of harvest and agriculture, presiding over crops, grains, food, and the fertility of the earth.");
                    Vokis.Source = new Uri("../../../Assets/voki/Welcome.mp4", UriKind.RelativeOrAbsolute);
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
                    DrawerHost.RightDrawerContent = breakfast;
                    DrawerHost.IsRightDrawerOpen = true;
                    break;
                case "Drinks":
                    Vokis.Source = new Uri("../../../Assets/voki/Drinks.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("I'd like to have a drink");
                    CreateResponseCard("Select any drink you would like from our vast selection.");
                    await Task.Delay(1500);
                    DrawerHost.RightDrawerContent = drink;
                    DrawerHost.IsRightDrawerOpen = true;
                    break;
                case "Wine Catalog":
                    Vokis.Source = new Uri("../../../Assets/voki/Wines.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("Show me the wine catalog please.");
                    CreateResponseCard("Here is a list of our available wines.");
                    await Task.Delay(1500);
                    DrawerHost.RightDrawerContent = wine;
                    DrawerHost.IsRightDrawerOpen = true;
                    break;
                case "Desserts Menu":
                    Vokis.Source = new Uri("../../../Assets/voki/Dishes.mp4", UriKind.RelativeOrAbsolute);
                    CreateUserCard("What are my options for dessert?");
                    CreateResponseCard("We have a great variety of dishes available!");
                    await Task.Delay(1500);
                    DrawerHost.RightDrawerContent = dessert;
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
            Card userCard = new()
            {
                Padding = new Thickness(8),
                Margin = new Thickness(8),
                HorizontalAlignment = HorizontalAlignment.Right,
                VerticalAlignment = VerticalAlignment.Bottom,
                Background = new SolidColorBrush(Color.FromRgb(219, 84, 97)),
                Foreground = (Brush)FindResource("PrimaryHueDarkForegroundBrush"),
                UniformCornerRadius = 6,
                Content = new TextBlock
                {
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

        private void paybtn_Click(object sender, RoutedEventArgs e)
        {
            PaymentWindow paymentWindow = new PaymentWindow(Total.Text);
            paymentWindow.Show();
            Window.GetWindow(this).Close();
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
                    new MenuItem { Name = "Zacharias", Price = 25 },
                    new MenuItem { Name = "Karipidis", Price = 35 },
                    new MenuItem { Name = "Vientzi", Price = 38 },
                    new MenuItem { Name = "Gerovassilou", Price = 44 },
                    new MenuItem { Name = "Kikones", Price = 50 },
                    new MenuItem { Name = "Nykteri", Price = 71}
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Driopi Nemea", Price = 28 },
                    new MenuItem { Name = "Chatzivaritis", Price = 39 },
                    new MenuItem { Name = "Oenops", Price = 43 },
                    new MenuItem { Name = "Cyrus One", Price = 46 },
                    new MenuItem { Name = "Emphasis", Price = 52 },
                    new MenuItem { Name = "Taos Paparousi" , Price = 60}
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Vissinokipos", Price = 27 },
                    new MenuItem { Name = "Theopetra", Price = 39 },
                    new MenuItem { Name = "Idylle", Price = 43 },
                    new MenuItem { Name = "Alpha", Price = 54 },
                    new MenuItem { Name = "Dianthos", Price = 52 },
                    new MenuItem { Name = "Mavrose 2022", Price = 69 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Akakies", Price = 38 },
                    new MenuItem { Name = "Karanika", Price = 43 },
                    new MenuItem { Name = "Douroufakis", Price = 48 },
                    new MenuItem { Name = "Santo White", Price = 59 },
                    new MenuItem { Name = "Muscat Rio", Price = 42 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Moet & Chandon", Price = 120 },
                    new MenuItem { Name = "Taittinger", Price = 160 },
                    new MenuItem { Name = "Dom Perignon", Price = 450 },
                    new MenuItem { Name = "Cristal Brut", Price = 620 }
                }
            }
        };

        static List<MenuPage> drinkmenuPages = new List<MenuPage>
        {
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Ouzito", Price = 10 },
                    new MenuItem { Name = "Thalassaki", Price = 12 },
                    new MenuItem { Name = "Goddess", Price = 12 },
                    new MenuItem { Name = "Kafeneion", Price = 10 },
                    new MenuItem { Name = "Cretan", Price = 8 },
                    new MenuItem { Name = "Sidecar", Price = 8 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Siris", Price = 5 },
                    new MenuItem { Name = "Fresh Chios", Price = 6 },
                    new MenuItem { Name = "Sol", Price = 6 },
                    new MenuItem { Name = "Eza", Price = 5 },
                    new MenuItem { Name = "Corfu Red Ale", Price = 8 },
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Grey Goose", Price = 9 },
                    new MenuItem { Name = "Ciroc", Price = 9 },
                    new MenuItem { Name = "Belvedere", Price = 10 },
                    new MenuItem { Name = "Stoli Elit", Price = 17 },
                    new MenuItem { Name = "Beluga Gold", Price = 20 },
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Johnnie Walker", Price = 9 },
                    new MenuItem { Name = "Lambay", Price = 11 },
                    new MenuItem { Name = "Gentlemen", Price = 14 },
                    new MenuItem { Name = "Harmony", Price = 14 },
                    new MenuItem { Name = "Ardbeg", Price = 21 },
                    new MenuItem { Name = "Famous Grouse", Price = 21 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Bacardi Heritage", Price = 9 },
                    new MenuItem { Name = "El Dorado", Price = 9 },
                    new MenuItem { Name = "Dictator", Price = 10 },
                    new MenuItem { Name = "Lost Spirits", Price = 14 },
                    new MenuItem { Name = "Havana Club Union", Price = 15 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Bombay Sapphire", Price = 7 },
                    new MenuItem { Name = "Concepts Votanikon", Price = 9 },
                    new MenuItem { Name = "Roku Suntory", Price = 11 },
                    new MenuItem { Name = "Nordes", Price = 13 },
                    new MenuItem { Name = "Hendrick's", Price = 15 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Jose Quervo", Price = 9 },
                    new MenuItem { Name = "El Jimador", Price = 9 },
                    new MenuItem { Name = "Reposado", Price = 11 },
                    new MenuItem { Name = "Don Julio", Price = 13 },
                    new MenuItem { Name = "Casa Dragones", Price = 37 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Palaiothen", Price = 10 },
                    new MenuItem { Name = "Metaxa", Price = 9 },
                    new MenuItem { Name = "Roots", Price = 11 },
                    new MenuItem { Name = "Stoupaki", Price = 8 },
                }
            }
        };

        static List<MenuPage> dessertmenuPages = new List<MenuPage>
        {
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Bougatsa", Price = 12 },
                    new MenuItem { Name = "Tsoureki", Price = 12 },
                    new MenuItem { Name = "Greek cheesecake", Price = 12 },
                    new MenuItem { Name = "Seasonal fresh fruit", Price = 10 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Say thanks to the chef", Price = 0 },
                }
            }
        };

        static List<MenuPage> breakfastmenuPages = new List<MenuPage>
        {
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Espresso", Price = 3 },
                    new MenuItem { Name = "Espresso 2x", Price = 4 },
                    new MenuItem { Name = "Espresso F", Price = 4 },
                    new MenuItem { Name = "Cap. 2x", Price = 3 },
                    new MenuItem { Name = "Cap. F", Price = 3 },
                    new MenuItem { Name = "Greek", Price = 3 },
                    new MenuItem { Name = "Macha", Price = 3 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Yogurt", Price = 9 },
                    new MenuItem { Name = "Porridge", Price = 8 },
                    new MenuItem { Name = "Fruit salad", Price = 7 },
                    new MenuItem { Name = "Chocolate", Price = 4 },
                    new MenuItem { Name = "Tea", Price = 5 },
                    new MenuItem { Name = "Juice", Price = 5 },
                    new MenuItem { Name = "Water", Price = 3 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Red pancake", Price = 9 },
                    new MenuItem { Name = "Maple pancake", Price = 9 },
                    new MenuItem { Name = "French toast", Price = 9 },
                    new MenuItem { Name = "Loukoumades", Price = 8 },
                    new MenuItem { Name = "Brioche", Price = 10 }
                }
            },
            new MenuPage
            {
                Items = new List<MenuItem>{
                    new MenuItem { Name = "Double egg", Price = 8 },
                    new MenuItem { Name = "Double egg salmon", Price = 9 },
                    new MenuItem { Name = "Potato lover", Price = 11 },
                    new MenuItem { Name = "Greek athlete", Price = 8 }
                }
            }
        };

        private bool _ratingBarClicked = false;
        private void OnRatingBarClick(object sender, RoutedEventArgs e)
        {
            if (!_ratingBarClicked)
            {
                CreateResponseCard("Thank you so much for sharing your experience with us. We hope to see you again soon.");
                Vokis.Source = new Uri("../../../Assets/voki/ThankYou.mp4", UriKind.RelativeOrAbsolute);

                // Set the _ratingBarClicked flag to true to prevent further clicks
                _ratingBarClicked = true;
            }

        }
    }
}
