using BespokeFusion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using WpfApp1.UserMenuItems;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserHome1.xaml
    /// </summary>
    public partial class UserHome1 : Window
    {
        public UserHome1()
        {
            InitializeComponent(); // by defaults opens to homepage
            UserControl main = new UserControlHome();
            ListViewMenu.SelectedItem = ItemHome;
            GridMain.Children.Add(main);
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

        private void ListViewMenu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GridMain.Children.Clear(); // remove current selection from main grid
            switch (((ListViewItem)((ListView)sender).SelectedItem).Name)
            {
                case "ItemHome":
                    GridMain.Children.Add(new UserControlHome());
                    break;
                case "ItemPool":
                    GridMain.Children.Add(new UserControlPool());
                    break;
                case "ItemTrojanHorse":
                    GridMain.Children.Add(new UserControlTrojanHorse());
                    break;
                case "ItemRestaurant":
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
                TxtMessage = { Text = "In the left part of the screen there is a menu which includes all the utilities you have access to. \n\n" +
                "To enter the pool managment system click the pool icon from the menu. \n" +
                "To use the Trojan Horse RV you rented click the trojan horse icon from the menu. \n" +
                "If you want to enter the restaurant and order food or drinks click the restaurant icon from the menu. \n" +
                "If you want to return to the home screen click the home icon from the menu. \n\n" +
                "In the right top part of the screen there is a three dot button. If you click it you can logout and you can also see your account's details. \n", Foreground = Brushes.Black },
                TxtTitle = { Text = "Information", Foreground = Brushes.White },
                BtnOk = { Content = "Okay", Background = Brushes.DarkSlateGray },
                BtnCancel = { Content = "Cancel", Visibility = Visibility.Collapsed },
                MainContentControl = { Background = Brushes.Bisque },
                TitleBackgroundPanel = { Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546") },

                BorderBrush = (SolidColorBrush)new BrushConverter().ConvertFrom("#2e3546")
            };

            msg.Show();
            MessageBoxResult results = msg.Result;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow backToLogin = new MainWindow();
            backToLogin.Show();
            this.Close();
        }

    }
}
