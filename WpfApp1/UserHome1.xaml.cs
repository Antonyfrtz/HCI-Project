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
            AddHandler(PreviewKeyDownEvent, new KeyEventHandler(MoveWithArrows), true);
        }

        // move trojan horse using WASD
        private void MoveWithArrows(object sender, KeyEventArgs e) // works when webView2 isnt in focus
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
            else if(e.Key == Key.Right)
            {
                MoveRight_Click(sender, e);
            }
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

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            lat +=0.000080;
            lng +=0.000050;
            origin = "new google.maps.LatLng(" + lat.ToString() + ", " + lng.ToString() + ")";
            webBrowser1.ExecuteScriptAsync("changePos(" + origin + ");"); // else just change the destination with calcroute
            if(webBrowser1.Source == new Uri(AppDomain.CurrentDomain.BaseDirectory + "html\\map_route.html")) { // if we are navigating, refresh navigation
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
            pbox.IsPopupOpen = true;
        }

        // Coordinates - declared as double so they can be changed when the trojan horse is moved
        double lat=35.667595;
        double lng=139.776457;
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
            await webBrowser1.ExecuteScriptAsync("calcRoute("+origin+","+dest+");");
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
            webBrowser1.ExecuteScriptAsync("parkPos('https://lh3.googleusercontent.com/H_iRj67z5zTIKKINEIDgWQ8-ZBYC2Bx3ls_9VbY0nI6MNevv75DfBxNaeHBX2l6sjwW03BwU8ONkb7PFeu3gvCzVocVuF6jAaVY0fcwy')");
            OpenDialog("Trojan Horse door is opening");
        }

        private void HalfOpenDoor_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('https://lh3.googleusercontent.com/enwRMlXNFnSYKvMZQsIjEBoSp9uHPXJ06duqN-3iSEBiRhszuYWCSE7emEjZM1F0vQQucC9b_PUYTfWfDcqIaNyyn2jgXQp8oXfF7IYU')");
            OpenDialog("Trojan Horse door is opening to intermediate position");
        }

        private void CloseDoor_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('https://lh3.googleusercontent.com/Dvym3JTcDxohGT_UeDCMoyWxObOguT5l1DVvHf1rvRfoDuzXn_1AteNK9ZFJiLbcCGDJChsR98UKUp94D7A0_oChkqdtkEu44TyUIpKb')");
            OpenDialog("Trojan Horse door is closing");
        }

        private void Ladder_Click(object sender, RoutedEventArgs e)
        {
            webBrowser1.ExecuteScriptAsync("parkPos('https://lh3.googleusercontent.com/n_s7CzvdlevFxOL7t7YfzvddL-uAHq2xf_Xu3Rapdw0hiyGRqP8rYFH10bVbcpiMI6QAYxo6TYz_xOtdPZZIbHIdQKwM1LtstEkEf_s')");
            OpenDialog("Ladder is being deployed");
        }

        private void OpenDialog(string text) // dialog for door and ladder options - can be used for any dialog
        {
            airspace.FixAirspace = true; // fixes overlay above WebView2 - p.s. microsoft u suck
            var dialogContent = new TextBlock // content to display
            {
                Text = text,
                Margin = new Thickness(20)
            };
            DialogHost.Show(dialogContent, "RootDialog");
        }

        private void DialogHost_DialogClosing(object sender, DialogClosingEventArgs eventArgs)
        {
            airspace.FixAirspace = false; // disable airspace fix when dialog is closed
        }
    }
}