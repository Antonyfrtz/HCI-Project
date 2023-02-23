using BespokeFusion;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
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
        // Logged in constructor
        public UserHome1()
        {
            InitializeComponent(); // by defaults opens to homepage
            ListViewMenu.SelectedItem = ItemHome;
        }

        // Constructor callback from payment screen
        public UserHome1(string message, Uri uri)
        {
            InitializeComponent(); // by defaults opens to homepage\
            GridMain.Children.Clear();
            UserControl main = new UserControlRestaurant(message, uri);
            GridMain.Children.Add(main);
        }

        // Pass UserControlRestaurant down to UserOrderPanel
        public UIElement passControlDown()
        {
            return GridMain.Children[GridMain.Children.Count-1];
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
                    "You are currently in the Main menu. Here you can access all controls for room management.\n\n"+
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

    }
}