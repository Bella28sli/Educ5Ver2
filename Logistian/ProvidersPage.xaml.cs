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
    /// Логика взаимодействия для ProvidersPage.xaml
    /// </summary>
    public partial class ProvidersPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();

        public ProvidersPage()
        {
            InitializeComponent();
            ProvidersGrid.ItemsSource = context.Providers.ToList();

        }
        private void ProvidersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProvidersGrid.SelectedItem != null)
            {
                TitleBox.Text = (ProvidersGrid.SelectedItem as Providers).PrName.ToString();
                PhoneBox.Text = (ProvidersGrid.SelectedItem as Providers).PrPhone_Number.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Providers provider = new Providers();
            if (TitleBox.Text != null && PhoneBox.Text != null)
            {
                if (PhoneBox.Text.All(ch => char.IsDigit(ch)) && PhoneBox.Text.Length == 11)
                {
                    if (!context.Providers.ToList().Any(x => x.PrName == TitleBox.Text))
                    {
                        provider.PrName = TitleBox.Text;
                        provider.PrPhone_Number = PhoneBox.Text;

                        context.Providers.Add(provider);
                        context.SaveChanges();
                        ProvidersGrid.ItemsSource = context.Providers.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такой поставщик уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Номер телефона должен состоять из 11 цифр");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedItem != null)
            {
                var provider = ProvidersGrid.SelectedItem as Providers;

                if (!context.Products.ToList().Any(x => x.Provider_ID == provider.ID_Provider))
                {
                    context.Providers.Remove(provider);
                    context.SaveChanges();

                    ProvidersGrid.ItemsSource = context.Providers.ToList();
                }
                else
                {
                    MessageBox.Show("Существует продукт с данным поставщиком, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProvidersGrid.SelectedItem != null)
            {
                var provider = ProvidersGrid.SelectedItem as Providers;

                if (TitleBox.Text != null && PhoneBox.Text != null)
                {
                    if (PhoneBox.Text.All(ch => char.IsDigit(ch)) && PhoneBox.Text.Length == 11)
                    {
                        if (!context.Providers.ToList().Any(x => x.PrName == TitleBox.Text) || provider.PrName == TitleBox.Text)
                        {
                            provider.PrName = TitleBox.Text;
                            provider.PrPhone_Number = PhoneBox.Text;

                            context.SaveChanges();
                            ProvidersGrid.ItemsSource = context.Providers.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такой поставщик уже существует");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Номер телефона должен состоять из 11 цифр");
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
