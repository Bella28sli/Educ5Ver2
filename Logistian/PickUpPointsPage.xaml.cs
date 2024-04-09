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

namespace Educ5Ver2.Logistian
{
    /// <summary>
    /// Логика взаимодействия для PickUpPointsPage.xaml
    /// </summary>
    public partial class PickUpPointsPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();

        public PickUpPointsPage()
        {
            InitializeComponent();
            PickUpPointsGrid.ItemsSource = context.Pickup_Points.ToList();

        }
        private void PickUpPointsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PickUpPointsGrid.SelectedItem != null)
            {
                SubwayBox.Text = (PickUpPointsGrid.SelectedItem as Pickup_Points).Subway.ToString();
                AddressBox.Text = (PickUpPointsGrid.SelectedItem as Pickup_Points).Point_Address.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Pickup_Points point = new Pickup_Points();
            if (SubwayBox.Text != null && AddressBox.Text != null)
            {

                if (!context.Pickup_Points.ToList().Any(x => x.Point_Address == AddressBox.Text))
                {
                    point.Subway = SubwayBox.Text;
                    point.Point_Address = AddressBox.Text;

                    context.Pickup_Points.Add(point);
                    context.SaveChanges();
                    PickUpPointsGrid.ItemsSource = context.Pickup_Points.ToList();
                }
                else
                {
                    MessageBox.Show("Такой адрес уже занят");
                }

            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PickUpPointsGrid.SelectedItem != null)
            {
                var point = PickUpPointsGrid.SelectedItem as Pickup_Points;

                if (!context.Orders.ToList().Any(x => x.Point_ID == point.ID_Point))
                {
                    context.Pickup_Points.Remove(point);
                    context.SaveChanges();

                    PickUpPointsGrid.ItemsSource = context.Pickup_Points.ToList();
                }
                else
                {
                    MessageBox.Show("Существует заказ с данным пунктом выдачи, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PickUpPointsGrid.SelectedItem != null)
            {
                var point = PickUpPointsGrid.SelectedItem as Pickup_Points;

                if (SubwayBox.Text != null && AddressBox.Text != null)
                {
                    if (!context.Pickup_Points.ToList().Any(x => x.Point_Address == AddressBox.Text) || point.Point_Address == AddressBox.Text)
                    {
                        point.Subway = SubwayBox.Text;
                        point.Point_Address = AddressBox.Text;

                        context.SaveChanges();
                        PickUpPointsGrid.ItemsSource = context.Pickup_Points.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такой адрес уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }
    }
}
