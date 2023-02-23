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
                        var message2 = "Your order wasn't successful.\nIf you need any help do not hesitate to ask.";
                        var previousWindow = new UserHome1(message2, new Uri("../../../Assets/voki/OrderFail.mp4", UriKind.RelativeOrAbsolute));
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
                        var message2  = "Your order wasn't successful.\nIf you need any help do not hesitate to ask.";
                        var previousWindow = new UserHome1(message2, new Uri("../../../Assets/voki/OrderFail.mp4", UriKind.RelativeOrAbsolute));
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
                        var message1 = "Payment was succesful and your order has been placed!\n" + "Coming right up!";
                        var previousWindow = new UserHome1(message1, new Uri("../../../Assets/voki/Success.mp4", UriKind.RelativeOrAbsolute));
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
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textbox = (TextBox)sender;
            if (string.IsNullOrEmpty(textbox.Text))
            {
                textbox.Text = "0000";
                textbox.Foreground = Brushes.Gray;
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
                    new StackPanel
                    {
                        HorizontalAlignment = HorizontalAlignment.Center,
                        Orientation= Orientation.Horizontal,
                        Children =
                        {
                            new Button
                            {
                                Content = "Yes",
                                Width = 100,
                                Margin = new Thickness(0, 10, 5, 0),
                                BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                                Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                                Command = DialogHost.CloseDialogCommand,
                                Tag = true // set a tag to identify the button
                            },
                            new Button
                            {
                                Content = "No",
                                Width = 100,
                                Margin = new Thickness(5, 10, 0, 0),
                                BorderBrush= new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                                Background = new SolidColorBrush(Color.FromRgb(18, 105, 199)),
                                Command = DialogHost.CloseDialogCommand,
                                Tag = false // set a tag to identify the button
                            }
                        }
                    }
                 }
            };
            StackPanel stackpanel = (StackPanel)dialogContent.Children[1];
            ((Button)stackpanel.Children[0]).Click += (sender, args) =>
            {
                callback?.Invoke(true);
            };

            ((Button)stackpanel.Children[1]).Click += (sender, args) =>
            {
                callback?.Invoke(false);
            };
            DialogHost.Show(dialogContent, "RootDialog");
        }
    }
}
