using BespokeFusion;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media;
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
            //UserControl main = new UserControlHome();
            //ListViewMenu.SelectedItem = ItemHome;
            //GridMain.Children.Add(main);
            LoadImage();
            DataContext = this;
            menuListBox.ItemsSource = menuPages[0].Items.Select(item => item.Name);
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

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Resources/UserManual.pdf"))
            {
                try
                {
                    Process.Start("microsoft-edge:", "Resources/UserManual.pdf");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred while trying to open the PDF file.\n" + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("The PDF file could not be found.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadImage()
        {
            Menus.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(_imageFiles[_currentImageIndex], System.UriKind.Relative));
        }

        private int _currentImageIndex=0;
        private List<string> _selectedItems = new List<string>();
        private readonly string[] _imageFiles = { "Assets/menus/menu_main_1.png", "Assets/menus/menu_main_2.png", "Assets/menus/menu_main_3.png", "Assets/menus/menu_main_4.png" };
        private void ChangeMenuItem(object sender, RoutedEventArgs e)
        {
            // Get button that made the call
            string senderName = ((FrameworkElement)sender).Name;
            if (senderName== "Nextbtn")
            {
                _currentImageIndex++;
            }
            else
            {
                _currentImageIndex--;
            }
            if (_currentImageIndex == 0) // first page
            {
                Previousbtn.Visibility = Visibility.Hidden;
                Nextbtn.Visibility = Visibility.Visible;
            }
            else if (_currentImageIndex == _imageFiles.Length - 1) // last page
            {
                Previousbtn.Visibility = Visibility.Visible;
                Nextbtn.Visibility = Visibility.Hidden;
            }
            else // intermediate
            {
                Previousbtn.Visibility = Visibility.Visible;
                Previousbtn.Visibility = Visibility.Visible;
            }
            LoadImage();
            // temporarily remove event handler to avoid refreshing selected plate list and badge values
            menuListBox.SelectionChanged -= new SelectionChangedEventHandler(menuListBox_SelectionChanged);
            menuListBox.ItemsSource = menuPages[_currentImageIndex].Items.Select(item => item.Name);
            foreach (var item in menuPages[_currentImageIndex].Items)
            {
                if (_selectedItems.Contains(item.Name))
                {
                    menuListBox.SelectedItems.Add(item.Name);
                }
            }
            // add event handler back
            menuListBox.SelectionChanged += new SelectionChangedEventHandler(menuListBox_SelectionChanged);
        }

        public class MenuPage
        {
            public List<MenuItem> Items { get; set; }
        }

        public class MenuItem
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }

        private void menuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                // This item was selected
                _selectedItems.Add(item.ToString());
                badge.Badge = _selectedItems.Count;
            }

            foreach (var item in e.RemovedItems)
            {
                // This item was unselected
                _selectedItems.Remove(item.ToString());
                badge.Badge = _selectedItems.Count;
            }
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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            // TO BE IMPLEMENTED

        }

        // lookup map
        Dictionary<string, int> itemPrices = menuPages.SelectMany(page => page.Items).ToDictionary(item => item.Name, item => item.Price);
        // price
        private int total;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (_selectedItems.Count > 0) // show expander
            {
                total = _selectedItems.Sum(selection => itemPrices.GetValueOrDefault(selection, 0));
                Total.Text = total.ToString()+ " €";
                maindishes_exp.Visibility = Visibility.Visible;
                msg.Visibility= Visibility.Collapsed;
                maindishes.Text = string.Join(", ", _selectedItems);
                paybtn.IsEnabled = true;
            }
            else // show textblock
            {
                Total.Text = "";
                maindishes_exp.Visibility = Visibility.Collapsed;
                msg.Visibility = Visibility.Visible;
                paybtn.IsEnabled = false;
            }
            DrawerHost.IsRightDrawerOpen = false;
        }
    }
}