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

namespace Educ5Ver2.Consultant
{
    /// <summary>
    /// Логика взаимодействия для ClientsPage.xaml
    /// </summary>
    public partial class ClientsPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public ClientsPage()
        {
            InitializeComponent();
            ClientsGrid.ItemsSource = context.Clients.ToList();

            MakerCB.ItemsSource = context.Makers.ToList();
            MakerCB.DisplayMemberPath = "Maker_Name";

        }

        private void ClientsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                SurnameBox.Text = (ClientsGrid.SelectedItem as Clients).Surname.ToString();
                NameBox.Text = (ClientsGrid.SelectedItem as Clients).Firstname.ToString();
                PatronymicBox.Text = (ClientsGrid.SelectedItem as Clients).Patronymic.ToString();
                MakerCB.SelectedItem = (ClientsGrid.SelectedItem as Clients).Makers;
                PhoneBox.Text = (ClientsGrid.SelectedItem as Clients).ClientPhone_Number.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Clients client = new Clients();
            if (SurnameBox.Text != null && NameBox.Text != null
                && PhoneBox != null)
            {
                if (PhoneBox.Text.All(ch => char.IsDigit(ch)) && PhoneBox.Text.Length == 11
                    && SurnameBox.Text.All(ch => char.IsLetter(ch))
                    && NameBox.Text.All(ch => char.IsLetter(ch)))
                {
                    client.Surname = SurnameBox.Text;
                    client.Firstname = NameBox.Text;
                    if (PatronymicBox.Text != null && PatronymicBox.Text.All(ch => char.IsLetter(ch)))
                    {
                        client.Patronymic = PatronymicBox.Text;
                    }
                    client.ClientPhone_Number = PhoneBox.Text;
                    if (MakerCB.SelectedItem != null)
                    {
                        client.Maker_ID = (MakerCB.SelectedItem as Makers).ID_Maker;
                    }
                    context.Clients.Add(client);
                    context.SaveChanges();
                    ClientsGrid.ItemsSource = context.Clients.ToList();
                }
                else
                {
                    MessageBox.Show("ФИО должно состоять только из букв, а номер телефона из 11 цифр");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                var client = ClientsGrid.SelectedItem as Clients;
                if (!context.Orders.ToList().Any(x => x.Client_ID == client.ID_Client)
                    && !context.Staff_Choice.ToList().Any(x => x.Clients_ID == client.ID_Client))
                {
                    context.Clients.Remove(client);
                    context.SaveChanges();

                    ClientsGrid.ItemsSource = context.Clients.ToList();
                }
                else
                {
                    MessageBox.Show("Данный клиент связан с другими данныйми, удаление невозможно\n(см. Выбор сотрудника, Заказы)");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ClientsGrid.SelectedItem != null)
            {
                var client = ClientsGrid.SelectedItem as Clients;

                if (SurnameBox.Text != null && NameBox.Text != null
                     && PhoneBox != null)
                {
                    if (PhoneBox.Text.All(ch => char.IsDigit(ch)) && PhoneBox.Text.Length == 11
                        && SurnameBox.Text.All(ch => char.IsLetter(ch))
                        && NameBox.Text.All(ch => char.IsLetter(ch)))
                    {
                        client.Surname = SurnameBox.Text;
                        client.Firstname = NameBox.Text;
                        if (PatronymicBox.Text != null && PatronymicBox.Text.All(ch => char.IsLetter(ch)))
                        {
                            client.Patronymic = PatronymicBox.Text;
                        }
                        client.ClientPhone_Number = PhoneBox.Text;
                        if (MakerCB.SelectedItem != null)
                        {
                            client.Maker_ID = (MakerCB.SelectedItem as Makers).ID_Maker;
                        }
                        context.SaveChanges();
                        ClientsGrid.ItemsSource = context.Clients.ToList();
                    }
                    else
                    {
                        MessageBox.Show("ФИО должно состоять только из букв, а номер телефона из 11 цифр");
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
