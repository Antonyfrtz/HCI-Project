using BespokeFusion;
using MaterialDesignThemes.Wpf;
using Microsoft.Web.WebView2.Core;
using System;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace WpfApp1.UserMenuItems
{
    /// <summary>
    /// Interaction logic for UserControlTrojanHorse.xaml
    /// </summary>
    public partial class UserControlTrojanHorse : UserControl
    {
        public UserControlTrojanHorse()
        {
            InitializeComponent();
            AddHandler(PreviewKeyDownEvent, new KeyEventHandler(MoveWithArrows), true);
        }

        // move trojan horse using arrow keys
        private void MoveWithArrows(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
                MoveUp_Click(sender, e);
            }
            else if (e.Key == Key.Down)
            {
                MoveDown_Click(sender, e);
            }
            else if (e.Key == Key.Left)
            {
                MoveLeft_Click(sender, e);
            }
            else if (e.Key == Key.Right)
            {
                MoveRight_Click(sender, e);
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            lat += 0.000080;
            lng += 0.000050;
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            webBrowser1.ExecuteScriptAsync("changePos(" + origin + ");"); // else just change the destination with calcroute
            if (webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html"))
            { // if we are navigating, refresh navigation
                webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + dest + ");");
            }
        }

        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            lat -= 0.000080;
            lng -= 0.000050;
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            webBrowser1.ExecuteScriptAsync("changePos(" + origin + ");"); // else just change the destination with calcroute
            if (webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html"))
            { // if we are navigating, refresh navigation
                webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + dest + ");");
            }
        }

        private void MoveLeft_Click(object sender, RoutedEventArgs e)
        {
            lat += 0.000030;
            lng -= 0.000150;
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            webBrowser1.ExecuteScriptAsync("changePos(" + origin + ");"); // else just change the destination with calcroute
            if (webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html"))
            { // if we are navigating, refresh navigation
                webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + dest + ");");
            }
        }

        private void MoveRight_Click(object sender, RoutedEventArgs e)
        {
            lat -= 0.000030;
            lng += 0.000150;
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            webBrowser1.ExecuteScriptAsync("changePos(" + origin + ");"); // else just change the destination with calcroute
            if (webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html"))
            { // if we are navigating, refresh navigation
                webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + dest + ");");
            } // if we arent navigating, update horse coordinates that build the mapview element
        }

        private void Park_Click(object sender, RoutedEventArgs e)
        {
            if (pbox.IsPopupOpen)
            {
                pbox.IsPopupOpen = false;
            }
            else
            {
                pbox.IsPopupOpen = true;
            }
        }

        // Coordinates - declared as double so they can be changed when the trojan horse is moved
        double lat = 35.667595;
        double lng = 139.776457;
        // String coordinates to be used for javascript calls
        private string origin = "";
        private string dest = "";
        // Navigation options
        private string gardens = "new google.maps.LatLng(35.666156, 139.776646)";
        private string palace = "new google.maps.LatLng(35.667262, 139.777619)";
        private void Navigate(string destination)
        {
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            dest = destination; // tells navigation buttons where we are navigating
            // enable cancel button and disable parking
            BtnCancelNav.Visibility = Visibility.Visible;
            BtnPark.IsEnabled = false;
            BtnPark.ToolTip = "You cannot park while navigating. Please end your navigation before parking by clicking cancel";
            if (webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\mapview.html"))
            {
                NavMapLoading(); // if no route has been loaded yet, wait for route map to load in
            }
            else                                    // format strings for js call
            {
                webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + destination + ");"); // else just change the destination with calcroute
            }
        }
        private void Gardens_Click(object sender, RoutedEventArgs e)//For Gardens
        {
            Navigate(gardens);
        }
        private void PalaceOfZeus_Click(object sender, RoutedEventArgs e)//For Palace of Zeus
        {
            Navigate(palace);
        }

        // we need to wait for map initialization (page load) before running our calcRoute script
        private async void NavMapLoading() //starts loading content asynchronously
        {
            await webBrowser1.EnsureCoreWebView2Async(null);
            webBrowser1.CoreWebView2.DOMContentLoaded += OnWebViewDOMContentLoaded;
            Uri uri = new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html");
            webBrowser1.Source = uri;
        }
        private async void OnWebViewDOMContentLoaded(object sender, CoreWebView2DOMContentLoadedEventArgs arg)
        {
            webBrowser1.CoreWebView2.DOMContentLoaded -= OnWebViewDOMContentLoaded;
            webBrowser1.Focus();
            // now we can load destination
            await webBrowser1.ExecuteScriptAsync("calcRoute(" + origin + "," + dest + ");");
        }

        String sURL = Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\html\\mapview.html";
        private void LoadMap()
        {
            // Before loading map, change the trojan horse coordinates to the latest ones by editing the html file
            string[] lines = File.ReadAllLines(sURL);
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].Contains("var lat"))
                {
                    lines[i] = "<script>var lat = " + lat + "; var lng = " + lng + ";</script>";
                    break;
                }
            }
            File.WriteAllLines(sURL, lines);
            // We can now load in the desired coordinates
            Uri uri = new Uri(sURL);
            webBrowser1.Source = uri;
        }
        private void DockPanel_Loaded(object sender, RoutedEventArgs e)
        {
            LoadMap();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            LoadMap();
            // re-enable parking and disable cancel button
            BtnPark.IsEnabled = true;
            BtnCancelNav.Visibility = Visibility.Hidden;
            BtnPark.ToolTip = "You can access all door controls by clicking this button";
        }

        private void OpenDoor_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('../../../../Assets/TrojanHorse_Open.png')");
            OpenDialog("Trojan Horse door is opening");
        }

        private void HalfOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('../../../../Assets/TrojanHorse_half_Open.png')");
            OpenDialog("Trojan Horse door is opening to intermediate position");
        }

        private void CloseDoor_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('../../../../Assets/TrojanHorse.png')");
            OpenDialog("Trojan Horse door is closing");
        }

        private void Ladder_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('../../../../Assets/TrojanHorse_w_Stairs.png')");
            OpenDialog("Ladder is being deployed");
        }

        private void OpenDialog(string text) // dialog for door and ladder options - can be used for any dialog
        {
            airspace.FixAirspace = true; // fixes overlay above WebView2 - p.s. microsoft u suck
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

        private void DialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            airspace.FixAirspace = false; // disable airspace fix when dialog is closed
        }
    }
}
