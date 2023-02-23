using BespokeFusion;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// 
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameTextBox.Text != "admin")
            {
                UserHome1 userHome1 = new UserHome1();
                userHome1.Show();
            }
            else
            {
                UEmployeeHome1 employeeHome1 = new UEmployeeHome1();
                employeeHome1.Show();
            }
            this.Close();
        }

        bool LisChecked = false;
        bool SUisChecked = false;

        private void ShowUnShowPassword(object sender, RoutedEventArgs e) ///This shows or hides the password from the textbox taken from the passwordbox when the eye-button is pressed.
        {
            string txtbx = ((Button)sender).Name;
            switch (txtbx)
            {
                case "TogglePassword": ///this is for Login
                    if (LisChecked) ///shows password
                    {
                        PasswordTextBox.Password = passwordTxtBox.Text;
                        passwordTxtBox.Visibility = Visibility.Collapsed;
                        PasswordTextBox.Visibility = Visibility.Visible;
                        eyeIcon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Eye");
                    }
                    else ///hides password
                    {
                        passwordTxtBox.Text = PasswordTextBox.Password;
                        PasswordTextBox.Visibility = Visibility.Collapsed;
                        passwordTxtBox.Visibility = Visibility.Visible;
                        eyeIcon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "EyeOff");
                    }
                    LisChecked = !LisChecked;
                    break;

                case "SignUpTogglePassword": ///this is for SignUp
                    if (SUisChecked)
                    {
                        SignUpPasswordTextBox.Password = SignUppasswordTxtBox.Text;
                        SignUppasswordTxtBox.Visibility = Visibility.Collapsed;
                        SignUpPasswordTextBox.Visibility = Visibility.Visible;
                        SUeyeIcon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "Eye");
                    }
                    else
                    {
                        SignUppasswordTxtBox.Text = SignUpPasswordTextBox.Password;
                        SignUpPasswordTextBox.Visibility = Visibility.Collapsed;
                        SignUppasswordTxtBox.Visibility = Visibility.Visible;
                        SUeyeIcon.Kind = (MaterialDesignThemes.Wpf.PackIconKind)Enum.Parse(typeof(MaterialDesignThemes.Wpf.PackIconKind), "EyeOff");
                    }
                    SUisChecked = !SUisChecked;
                    break;
            }
        }

        private void ShowInfo(object sender, RoutedEventArgs e)
        {
            CustomMaterialMessageBox msg = new CustomMaterialMessageBox
            {
                TxtMessage = { Text = "You find yourself in the lobby of Zeus's palace. \n" +
                "You'll need to Login as an existing user (Employee or Tenant). \n" +
                "To connect as a user simply click the login button.\n" +
                "To connect as an employee write admin in username and click login.\n" +
                "If you haven't created your account you will need to fill your username and password. \n" +
                "Please do not forget to choose your role and agree to Terms of Use and Conditions. \n", Foreground = Brushes.Black },
                TxtTitle = { Text = "Information", Foreground = Brushes.White },
                BtnOk = { Content = "Okay", Background = Brushes.DarkSlateGray },
                BtnCancel = {Content = "Cancel" , Visibility=Visibility.Collapsed },
                MainContentControl = { Background = Brushes.Bisque },
                TitleBackgroundPanel = { Background = Brushes.Goldenrod },

                BorderBrush = Brushes.Goldenrod
            };

            msg.Show();
            MessageBoxResult results = msg.Result;
        }
    }
}
