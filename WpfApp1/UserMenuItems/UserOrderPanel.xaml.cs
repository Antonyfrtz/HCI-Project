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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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

        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            badge.Badge = 0;
            menuListBox.SelectedItems.Clear();
        }

        // price of all items
        static private int total;
        // number of different menus selected
        static private int menusSelected;
        // price of items of specific menu
        private int menutotal;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get all controls from the main window
            var parent = Window.GetWindow(this);
            var Total = (TextBlock)parent.FindName("Total");
            var itemsInExpander = (Expander)parent.FindName(_cart + "_exp");
            var msg = (TextBlock)parent.FindName("msg");
            var dishes = (TextBlock)parent.FindName(_cart);
            var paybtn = (Button)parent.FindName("paybtn");
            var DrawerHost = (MaterialDesignThemes.Wpf.DrawerHost)parent.FindName("DrawerHost");
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

    }
}
