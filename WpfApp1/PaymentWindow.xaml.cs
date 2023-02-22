using MaterialDesignThemes.Wpf;
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
using System.Windows.Shapes;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for PaymentWindow.xaml
    /// </summary>
    public partial class PaymentWindow : Window
    {
        public PaymentWindow(string total)
        {
            InitializeComponent();
            CostTxt.Text = total;
        }

        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Are you sure you want to cancel your payment? Your order will be deleted!", (result) =>
            {
                if (result)
                {
                    if (Owner is Window ownerWindow)
                    {
                        // if the current window is a modal dialog
                        DialogResult = false;
                        ownerWindow.Activate();
                    }
                    else
                    {
                        // if the current window is a regular window
                        var previousWindow = new UserHome1();
                        previousWindow.Show();
                        Close();
                    }
                }
            });
        }

        private void CancelPayment_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Are you sure you want to cancel your payment? Your order will be deleted!", (result) =>
            {
                if (result)
                {
                    if (Owner is Window ownerWindow)
                    {
                        // if the current window is a modal dialog
                        DialogResult = false;
                        ownerWindow.Activate();
                    }
                    else
                    {
                        // if the current window is a regular window
                        var previousWindow = new UserHome1();
                        previousWindow.Show();
                        Close();
                    }
                }
            });
        }

        private void PayButton_Click(object sender, RoutedEventArgs e)
        {
            OpenDialog("Are you sure you want to proceed with the payment?", (result) =>
            {
                if (result)
                {
                    if (Owner is Window ownerWindow)
                    {
                        // if the current window is a modal dialog
                        DialogResult = false;
                        ownerWindow.Activate();
                    }
                    else
                    {
                        // if the current window is a regular window
                        var previousWindow = new UserHome1("Order Placed! \n" + "Your order was placed successfully. Coming right Up!");
                        previousWindow.Show();
                        Close();
                    }
                }
            });
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (textbox.Text == "0000")
            {
                textbox.Text = string.Empty;
                textbox.Foreground = Brushes.White;
            }

            TextBox NameTxtBox = (TextBox)sender;
            if (NameTxtBox.Text == "FIRST LAST")
            {
                NameTxtBox.Text = string.Empty;
                NameTxtBox.Foreground = Brushes.White;
            }

            TextBox DateTxtBox = (TextBox)sender;
            if (DateTxtBox.Text == "DAY/MONTH")
            {
                DateTxtBox.Text = string.Empty;
                DateTxtBox.Foreground = Brushes.White;
            }

            TextBox CVVtxtBox = (TextBox)sender;
            if (CVVtxtBox.Text == "***")
            {
                CVVtxtBox.Text = string.Empty;
                CVVtxtBox.Foreground = Brushes.White;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "0000";
                textbox.Foreground = Brushes.Gray;
            }

            TextBox NameTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(NameTxtBox.Text))
            {
                NameTxtBox.Text = "FIRST LAST";
                NameTxtBox.Foreground = Brushes.Gray;
            }

            TextBox DateTxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(DateTxtBox.Text))
            {
                DateTxtBox.Text = "DAY/MONTH";
                DateTxtBox.Foreground = Brushes.Gray;
            }

            TextBox CVVtxtBox = (TextBox)sender;
            if (string.IsNullOrEmpty(CVVtxtBox.Text))
            {
                CVVtxtBox.Text = "***";
                CVVtxtBox.Foreground = Brushes.Gray;
            }
        }

        private void OpenDialog(string text, Action<bool> callback) // dialog for door and ladder options - can be used for any dialog
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
                        Content = "Yes",
                        Width = 100,
                        Margin = new Thickness(0, 10, 0, 0),
                        BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Command = DialogHost.CloseDialogCommand,
                        Tag = true // set a tag to identify the button
                    },
                    new Button
                    {
                        Content = "No",
                        Width = 100,
                        Margin = new Thickness(0, 10, 0, 0),
                        BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                        Command = DialogHost.CloseDialogCommand,
                        Tag = false // set a tag to identify the button
                    }
                 }
            };
            ((Button)dialogContent.Children[1]).Click += (sender, args) =>
            {
                callback?.Invoke(true);
            };

            ((Button)dialogContent.Children[2]).Click += (sender, args) =>
            {
                callback?.Invoke(false);
            };
            DialogHost.Show(dialogContent, "RootDialog");
        }
    }
}
