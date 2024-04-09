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

namespace Educ5Ver2.Cashier
{
    /// <summary>
    /// Логика взаимодействия для CashierWindow.xaml
    /// </summary>
    public partial class CashierWindow : Window
    {
        static public Users account = new Users();
        public CashierWindow( Users user)
        {
            InitializeComponent();
            account = user;
        }
        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new OrderPage(account);
            OrderButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 26, 86)
            };
            ChecksButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(1, 169, 244)
            };

            OrderButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            ChecksButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };


        }

        private void ChecksButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ChecksPage();
            OrderButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(103, 218, 255)
            };
            ChecksButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 25, 86)
            };


            OrderButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            ChecksButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };

        }

        private void ReturnButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}
