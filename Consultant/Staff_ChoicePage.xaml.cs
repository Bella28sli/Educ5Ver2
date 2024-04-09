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
    /// Логика взаимодействия для Staff_ChoicePage.xaml
    /// </summary>
    public partial class Staff_ChoicePage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public Staff_ChoicePage()
        {
            InitializeComponent();
            StaffChoiceGrid.ItemsSource = context.Staff_Choice.ToList();
            ClientCB.ItemsSource = context.Clients.ToList();
            ClientCB.DisplayMemberPath = "Surname";
            StaffCB.ItemsSource = context.Staff.ToList().Where(item => item.Positions.ID_Position == 2);
            StaffCB.DisplayMemberPath = "EmpSurname";

        }

        private void StaffChoiceGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffChoiceGrid.SelectedItem != null)
            {
                StaffCB.SelectedItem = (StaffChoiceGrid.SelectedItem as Staff_Choice).Staff;
                ClientCB.SelectedItem = (StaffChoiceGrid.SelectedItem as Staff_Choice).Clients;

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Staff_Choice staffchoice = new Staff_Choice();
            if (StaffCB.SelectedItem != null && ClientCB.SelectedItem != null)
            {
                staffchoice.Clients_ID = (StaffCB.SelectedItem as Clients).ID_Client;
                staffchoice.Employee_ID = (StaffCB.SelectedItem as Staff).ID_Employee;

                context.Staff_Choice.Add(staffchoice);
                context.SaveChanges();
                StaffChoiceGrid.ItemsSource = context.Staff_Choice.ToList();
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffChoiceGrid.SelectedItem != null)
            {
                var staffchoice = StaffChoiceGrid.SelectedItem as Staff_Choice;

                context.Staff_Choice.Remove(staffchoice);
                context.SaveChanges();

                StaffChoiceGrid.ItemsSource = context.Staff_Choice.ToList();
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffChoiceGrid.SelectedItem != null)
            {
                var staffchoice = StaffChoiceGrid.SelectedItem as Staff_Choice;

                if (StaffCB.SelectedItem != null && ClientCB.SelectedItem != null)
                {
                    staffchoice.Clients_ID = (StaffCB.SelectedItem as Clients).ID_Client;
                    staffchoice.Employee_ID = (StaffCB.SelectedItem as Staff).ID_Employee;

                    context.Staff_Choice.Add(staffchoice);
                    context.SaveChanges();
                    StaffChoiceGrid.ItemsSource = context.Staff_Choice.ToList();
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }
    }
}
