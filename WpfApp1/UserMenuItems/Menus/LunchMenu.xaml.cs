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

namespace WpfApp1.UserMenuItems.Menus
{
    /// <summary>
    /// Interaction logic for LunchMenu.xaml
    /// </summary>
    public partial class LunchMenu : UserControl
    {
        public LunchMenu()
        {
            InitializeComponent();
        }

        private void LoadImage()
        {
            Menus.Source = new System.Windows.Media.Imaging.BitmapImage(new System.Uri(_imageFiles[_currentImageIndex], System.UriKind.Relative));
        }

        private int _currentImageIndex = 0;
        private readonly string[] _imageFiles = { "Assets/menus/menu_main_1.png", "Assets/menus/menu_main_1.png", "Assets/menus/menu_main_1.png" };

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
                Previousbtn.IsEnabled = false;
                Nextbtn.IsEnabled = true;
            }
            else if (_currentImageIndex == _imageFiles.Length - 1) // last page
            {
                Previousbtn.IsEnabled = true;
                Nextbtn.IsEnabled = false;
            }
            else // intermediate
            {
                Previousbtn.IsEnabled = true;
                Nextbtn.IsEnabled = true;
            }
            LoadImage();
        }
    }
}
