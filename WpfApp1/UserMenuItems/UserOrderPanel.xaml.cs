using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp1.UserMenuItems
{
    /// <summary>
    /// Interaction logic for UserOrderPanel.xaml
    /// </summary>
    public partial class UserOrderPanel : UserControl
    {
        List<MenuPage> menuPages;
        Dictionary<string, int> itemPrices;
        private string[] _imageFiles;
        private string _cart;
        public UserOrderPanel(List<MenuPage> selectedMenuPages, string[] images, string cart)
        {
            menuPages = selectedMenuPages;
            _imageFiles = images;
            _cart = cart;
            InitializeComponent();
            // first image
            LoadImage();
            // first page
            menuListBox.ItemsSource = menuPages[0].Items.Select(item => item.Name);
            // lookup map
            itemPrices = menuPages.SelectMany(page => page.Items).ToDictionary(item => item.Name, item => item.Price);
        }

        private void LoadImage()
        {
            Menus.Source = new BitmapImage(new Uri(_imageFiles[_currentImageIndex], UriKind.Relative));
        }

        private int _currentImageIndex = 0;
        private List<string> _selectedItems = new List<string>();
        private void ChangeMenuItem(object sender, RoutedEventArgs e)
        {
            // Get button that made the call
            string senderName = ((FrameworkElement)sender).Name;
            if (senderName == "Nextbtn")
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

        private void menuListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            foreach (var item in e.AddedItems)
            {
                // This item was selected
                OpenDialog("Add quantity", (result) =>
                {
                    for (int i = 0; i < result; i++)
                    {
                        _selectedItems.Add(item.ToString());
                    }
                    badge.Badge = _selectedItems.Count;
                });
            }

            foreach (var item in e.RemovedItems)
            {
                // This item was unselected
                while (_selectedItems.Contains(item.ToString()))
                {
                    _selectedItems.Remove(item.ToString());
                }
                badge.Badge = _selectedItems.Count;
            }
        }

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            _selectedItems.Clear(); // clear the collection of selected items
            badge.Badge = 0; // set the badge count to 0
            ((IEnumerable<object>)menuListBox.SelectedItems).ToList().ForEach(menuListBox.SelectedItems.Remove); // clear the selected items in the ListBox control
        }


        // price of all items
        static public int total;
        // price of items of specific menu
        private int menutotal;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get all controls from the main window
            UserHome1 parent = (UserHome1)Window.GetWindow(this);
            UserControlRestaurant restaurant = (UserControlRestaurant)parent.passControlDown();
            var Total = (TextBlock)restaurant.FindName("Total");
            var itemsInExpander = (Expander)restaurant.FindName(_cart + "_exp");
            var msg = (TextBlock)restaurant.FindName("msg");
            var dishes = (TextBlock)restaurant.FindName(_cart);
            var paybtn = (Button)restaurant.FindName("paybtn");
            var DrawerHost = (DrawerHost)restaurant.FindName("DrawerHost");
            // Update cart
            total -= menutotal; // remove previously saved value of items from the cart
            menutotal = _selectedItems.Sum(selection => itemPrices.GetValueOrDefault(selection, 0));
            total += menutotal; // add latest value to the cart
            Total.Text = total.ToString() + " €";
            // Logic for no items in specific menu
            if (_selectedItems.Count == 0)
            {
                itemsInExpander.Visibility = Visibility.Collapsed;
            }
            else
            {
                itemsInExpander.Visibility = Visibility.Visible;
                dishes.Text = string.Join(", ", _selectedItems);
            }
            // Logic for no items in cart
            if (total == 0)
            {
                Total.Text = "";
                msg.Visibility = Visibility.Visible;
                paybtn.IsEnabled = false;
            }
            else
            {
                msg.Visibility = Visibility.Collapsed;
                paybtn.IsEnabled = true;
            }
            DrawerHost.IsRightDrawerOpen = false;
        }


        private void OpenDialog(string text, Action<int> callback) // dialog for door and ladder options - can be used for any dialog
        {
            var label = new Label(); // create a new Label control
            label.Visibility = Visibility.Visible;
            var dialogContent = new StackPanel
            {
                Margin = new Thickness(20),
                Children =
        {
            new TextBlock
            {
                Text = text,
            },
            label, // add the Label control to the StackPanel
            new Button
            {
                Content = "+",
                Width = 100,
                Margin = new Thickness(0, 10, 0, 0),
                BorderBrush= new SolidColorBrush(Color.FromRgb(35,168,73)),
                Background = new SolidColorBrush(Color.FromRgb(35,168,73)),
                Tag = true // set a tag to identify the button
            },
            new Button
            {
                Content = "-",
                Width = 100,
                Margin = new Thickness(0, 10, 0, 0),
                BorderBrush= new SolidColorBrush(Color.FromRgb(205,92,92)),
                Background = new SolidColorBrush(Color.FromRgb(205,92,92)),
                Tag = false // set a tag to identify the button
            },
            new Button
            {
                Content = "Okay",
                Width = 100,
                Margin = new Thickness(0, 10, 0, 0),
                BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                Command = DialogHost.CloseDialogCommand
            }
         }
            };
            int count = 1; // initialize a counter variable
            label.Content = count.ToString();
            ((Button)dialogContent.Children[2]).Click += (sender, args) =>
            {
                count++; // increment the counter
                label.Content = count.ToString(); // update the content of the Label control
            };

            ((Button)dialogContent.Children[3]).Click += (sender, args) =>
            {
                if (count > 1) // check if the counter is greater than 0
                {
                    count--; // decrement the counter
                    label.Content = count.ToString(); // update the content of the Label control
                }
            };

            ((Button)dialogContent.Children[4]).Click += (sender, args) =>
            {
                callback?.Invoke(count);
            };
            
            DialogHost.Show(dialogContent, "RootDialog");
        }

        private void DialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {

        }

    }
}
