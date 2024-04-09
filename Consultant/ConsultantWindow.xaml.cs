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

namespace Educ5Ver2.Consultant
{
    /// <summary>
    /// Логика взаимодействия для ConsultantWindow.xaml
    /// </summary>
    public partial class ConsultantWindow : Window
    {
        public ConsultantWindow()
        {
            InitializeComponent();
        }
        private void ClientsButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new ClientsPage();
            ClientsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 26, 86)
            };
            StaffChoiceButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(1, 169, 244)
            };


            ClientsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(255, 255, 255)
            };
            StaffChoiceButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };


        }

        private void StaffChoiceButton_Click(object sender, RoutedEventArgs e)
        {
            Frame.Content = new Staff_ChoicePage();
            ClientsButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(103, 218, 255)
            };
            StaffChoiceButton.Background = new SolidColorBrush
            {
                Color = Color.FromRgb(4, 25, 86)
            };


            ClientsButton.Foreground = new SolidColorBrush
            {
                Color = Color.FromRgb(25, 4, 83)
            };
            StaffChoiceButton.Foreground = new SolidColorBrush
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
