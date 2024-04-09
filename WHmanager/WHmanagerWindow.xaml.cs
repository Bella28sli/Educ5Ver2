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

namespace Educ5Ver2.WHmanager
{
    /// <summary>
    /// Логика взаимодействия для WHmanagerWindow.xaml
    /// </summary>
    public partial class WHmanagerWindow : Window
    {
        public WHmanagerWindow()
        {
            InitializeComponent();
        }
        private void CategoriesButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new CategoriesPage();
            PositionsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 26, 86)
            };
            StaffButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(1, 169, 244)
            };
            UsersButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(0, 122, 193)
            };

            PositionsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            StaffButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            UsersButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };

        }

        private void MakersButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new MakersPage();
            PositionsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(103, 218, 255)
            };
            StaffButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 25, 86)
            };
            UsersButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(0, 122, 193)
            };

            PositionsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            StaffButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            UsersButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
        }

        private void ProductsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ProductsPage();
            PositionsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(103, 218, 255)
            };
            StaffButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(1, 169, 244)
            };
            UsersButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 26, 86)
            };

            UsersButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            PositionsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            StaffButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
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
