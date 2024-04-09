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

namespace Educ5Ver2.WHmanager
{
    /// <summary>
    /// Логика взаимодействия для MakersPage.xaml
    /// </summary>
    public partial class MakersPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();

        public MakersPage()
        {
            InitializeComponent();
            MakersGrid.ItemsSource = context.Makers.ToList();

        }
        private void MakersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (MakersGrid.SelectedItem != null)
            {
                TitleBox.Text = (MakersGrid.SelectedItem as Makers).Maker_Name.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Makers maker = new Makers();
            if (TitleBox.Text != null)
            {

                if (!context.Makers.ToList().Any(x => x.Maker_Name == TitleBox.Text))
                {
                    maker.Maker_Name = TitleBox.Text;

                    context.Makers.Add(maker);
                    context.SaveChanges();
                    MakersGrid.ItemsSource = context.Makers.ToList();
                }
                else
                {
                    MessageBox.Show("Такая категория уже существует");
                }

            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (MakersGrid.SelectedItem != null)
            {
                var maker = MakersGrid.SelectedItem as Makers;

                if (!context.Products.ToList().Any(x => x.Maker_ID == maker.ID_Maker)
                    && !context.Clients.ToList().Any(x => x.Maker_ID == maker.ID_Maker))
                {
                    context.Makers.Remove(maker);
                    context.SaveChanges();

                    MakersGrid.ItemsSource = context.Makers.ToList();
                }
                else
                {
                    MessageBox.Show("Существует продукт с данным производителем или производитель выбран любимым у клиента, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (MakersGrid.SelectedItem != null)
            {
                var maker = MakersGrid.SelectedItem as Makers;

                if (TitleBox.Text != null)
                {
                    if (!context.Makers.ToList().Any(x => x.Maker_Name == TitleBox.Text) || TitleBox.Text == maker.Maker_Name)
                    {

                        maker.Maker_Name = TitleBox.Text;

                        context.SaveChanges();
                        MakersGrid.ItemsSource = context.Makers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такой производитель уже существует");
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
