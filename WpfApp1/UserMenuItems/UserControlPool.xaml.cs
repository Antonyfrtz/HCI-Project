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

namespace WpfApp1.UserMenuItems
{
    /// <summary>
    /// Interaction logic for UserControlPool.xaml
    /// </summary>
    public partial class UserControlPool : UserControl
    {
        public UserControlPool()
        {
            InitializeComponent();
        }

        bool LightBulbisChecked = false;
        bool AlarmisChecked = false;

        private void ToggleButtons_Click(object sender, RoutedEventArgs e) ///This changes the icons next to the toggle button for alarm and lights.
        {
            string txtbx = ((ToggleButton)sender).Name;
            switch (txtbx)
            {
                case "LightsBtn": ///this is for lightbulb
                    if (LightBulbisChecked)
                    {
                        Lightbulb.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "LightbulbOnOutline");
                        poolIcon.Source = new BitmapImage(new Uri("/Assets/outdoor_pool_w_light.jpg", UriKind.Relative));
                    }
                    else
                    {
                        Lightbulb.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "LightbulbOffOutline");
                        poolIcon.Source = new BitmapImage(new Uri("/Assets/outdoor_pool_nolight.jpg", UriKind.Relative));
                    }
                    LightBulbisChecked = !LightBulbisChecked;
                    break;

                case "AlarmBtn": ///this is for Alarm
                    if (AlarmisChecked)
                    {
                        Alarm.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "AlarmLight");
                    }
                    else
                    {
                        Alarm.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "AlarmLightOff");
                    }
                    AlarmisChecked = !AlarmisChecked;
                    break;
            }
        }

        private void Slider_Changed(object sender, RoutedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (slider != null)
            {
                if (slider.Value > 18)
                {
                    slider.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#E1341E");
                    if (tempLabel != null)
                    {
                        tempLabel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#E1341E");
                    }
                }
                else
                {
                    slider.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#1a78c2");
                    if (tempLabel != null)
                    {
                        tempLabel.Background = (SolidColorBrush)new BrushConverter().ConvertFrom("#1a78c2");
                    }
                }
            }
        }

        private void Slider_PoolLevel(object sender, RoutedEventArgs e)
        {
            Slider slider = sender as Slider;
            if (Level != null)
            {
                if (slider.Value == 1)
                {

                    Level.Text = "Half-Filled";
                    slider.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#7fbded");
                }
                else if (slider.Value == 2)
                {
                    Level.Text = "Filled";
                    slider.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("#1a78c2");
                }
                else
                {
                    Level.Text = "Empty";
                    slider.Foreground = (SolidColorBrush)new BrushConverter().ConvertFrom("Gray");
                }
            }
        }
    }
}
