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
        public UserOrderPanel(List<MenuPage> selectedMenuPages, string[] images)
        {
            menuPages = selectedMenuPages;
            _imageFiles = images;
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

        // price
        private int total;
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Get all controls from the main window
            var parent = Window.GetWindow(this);
            var Total = (TextBlock)parent.FindName("Total");
            var dishesInExpander = (Expander)parent.FindName("maindishes_exp");
            var msg = (TextBlock)parent.FindName("msg");
            var dishes = (TextBlock)parent.FindName("maindishes");
            var paybtn = (Button)parent.FindName("paybtn");
            var DrawerHost = (MaterialDesignThemes.Wpf.DrawerHost)parent.FindName("DrawerHost");
            if (_selectedItems.Count > 0) // show expander
            {
                total = _selectedItems.Sum(selection => itemPrices.GetValueOrDefault(selection, 0));
                Total.Text = total.ToString() + " €";
                dishesInExpander.Visibility = Visibility.Visible;
                msg.Visibility = Visibility.Collapsed;
                dishes.Text = string.Join(", ", _selectedItems);
                paybtn.IsEnabled = true;
            }
            else // show textblock
            {
                Total.Text = "";
                dishesInExpander.Visibility = Visibility.Collapsed;
                msg.Visibility = Visibility.Visible;
                paybtn.IsEnabled = false;
            }
            DrawerHost.IsRightDrawerOpen = false;
        }

    }
}
