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
        public PaymentWindow()
        {
            InitializeComponent();
        }

        //Messenger.Default.Register<string>(this, (text) =>
        //{
            //string CostTxt = text;
        //});

        private void CloseButton_Click(object sender, RoutedEventArgs e)
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
            }
            Close();
        }

        private void CancelPayment_Click(object sender, RoutedEventArgs e)
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
            }
            Close();
        }

        private void TextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text == "0000")
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }

            TextBox NameTxtBox = (TextBox)sender;
            if (NameTxtBox.Text == "FIRST LAST")
            {
                NameTxtBox.Text = string.Empty;
                NameTxtBox.Foreground = Brushes.Black;
            }

            TextBox DateTxtBox = (TextBox)sender;
            if (DateTxtBox.Text == "DAY/MONTH")
            {
                DateTxtBox.Text = string.Empty;
                DateTxtBox.Foreground = Brushes.Black;
            }

            TextBox CVVtxtBox = (TextBox)sender;
            if (CVVtxtBox.Text == "***")
            {
                CVVtxtBox.Text = string.Empty;
                CVVtxtBox.Foreground = Brushes.Black;
            }
        }

        private void TextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = "0000";
                textBox.Foreground = Brushes.Gray;
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
            if (string.IsNullOrEmpty(textBox.Text))
            {
                CVVtxtBox.Text = "***";
                CVVtxtBox.Foreground = Brushes.Gray;
            }
        }
    }
}
