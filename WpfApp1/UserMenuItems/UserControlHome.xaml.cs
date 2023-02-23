using MaterialDesignThemes.Wpf;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for UserControlHome.xaml
    /// </summary>
    public partial class UserControlHome : UserControl
    {
        public UserControlHome()
        {
            InitializeComponent();
        }

        bool LightBulbisChecked = false;
        bool TelevisionisChecked = false;
        bool ACisChecked = false;
        private void ToggleButtons_Click(object sender, RoutedEventArgs e) //This changes the icons next to the toggle button for lights.
        {
            string txtbx = ((ToggleButton)sender).Name;
            switch (txtbx)
            {
                case "LightsBtn": //this is for lightbulb
                    if (LightBulbisChecked)
                    {
                        Lightbulb.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "LightbulbOnOutline");
                        LightsRoom.Source = new BitmapImage(new Uri("/Assets/room.png", UriKind.Relative));
                        OpenDialog("Lights are on!");
                    }
                    else
                    {
                        Lightbulb.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "LightbulbOffOutline");
                        LightsRoom.Source = new BitmapImage(new Uri("/Assets/room_w_nolight.png", UriKind.Relative));
                        OpenDialog("Lights are off!");

                    }
                    LightBulbisChecked = !LightBulbisChecked;
                    break;

                case "TVBtn": //this is for tv
                    if (TelevisionisChecked)
                    {
                        Television.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "TelevisionClassicOff");
                        OpenDialog("Television is off!");
                    }
                    else
                    {
                        Television.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "TelevisionClassic");
                        OpenDialog("Television is on!");
                    }
                    TelevisionisChecked = !TelevisionisChecked;
                    break;

                case "ACBtn": //this is for tv
                    if (ACisChecked)
                    {
                        AC.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "FanOff");
                        OpenDialog("Air-Conditioner is off!");
                    }
                    else
                    {
                        AC.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Fan");
                        OpenDialog("Air-Conditioner is on!");
                    }
                    ACisChecked = !ACisChecked;
                    break;
            }
        }

        private void OpenDialog(string text) // dialog for door and ladder options - can be used for any dialog
        {

            var dialogContent = new StackPanel
            {
                Margin = new Thickness(20),
                Children =
                {
                    new TextBlock
                    {
                        Text = text,
                    },
                    new Button
                    {
                        Content = "OK",
                        Width = 100,
                        Margin = new Thickness(0, 20, 0, 0),
                        BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Command = DialogHost.CloseDialogCommand
                    }
                 }
            };
            DialogHost.Show(dialogContent, "RootDialog");
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem selectedItem = (ComboBoxItem)comboBox.SelectedItem;
            if (selectedItem.Content.ToString() == "Room service required")
            {
                BrushConverter converter = new BrushConverter();
                Brush brush = (Brush)converter.ConvertFromString("#E3A857");
                comboBox.Foreground = brush;
            }
            else if (selectedItem.Content.ToString() == "Occupied")
            {
                comboBox.Foreground = Brushes.IndianRed;
            }
        }
    }
}
