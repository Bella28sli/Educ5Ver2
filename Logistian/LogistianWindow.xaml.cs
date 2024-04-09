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

namespace Educ5Ver2.Logistian
{
    /// <summary>
    /// Логика взаимодействия для LogistianWindow.xaml
    /// </summary>
    public partial class LogistianWindow : Window
    {
        public LogistianWindow()
        {
            InitializeComponent();
        }
        private void ProvidersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProvidersPage();
            ProvidersButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 26, 86)
            };
            PickUpPointsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(1, 169, 244)
            };


            ProvidersButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            PickUpPointsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };


        }

        private void PickUpPointsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new PickUpPointsPage();
            ProvidersButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(103, 218, 255)
            };
            PickUpPointsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 25, 86)
            };


            ProvidersButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            PickUpPointsButton.Foreground = new SolidColorBrush
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
