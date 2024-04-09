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

namespace Educ5Ver2.Admin
{
    /// <summary>
    /// Логика взаимодействия для UsersPage.xaml
    /// </summary>
    public partial class UsersPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public UsersPage()
        {
            InitializeComponent();
            UsersGrid.ItemsSource = context.Users.ToList();
            StaffCB.ItemsSource = context.Staff.ToList();
            StaffCB.DisplayMemberPath = "EmpSurname";

        }

        private void UsersGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                LoginBox.Text = (UsersGrid.SelectedItem as Users).User_Login.ToString();
                PasswordBox.Password = (UsersGrid.SelectedItem as Users).User_Password.ToString();
                StaffCB.SelectedItem = (UsersGrid.SelectedItem as Users).Staff;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Users user = new Users();
            if (LoginBox.Text != null && PasswordBox.Password != null
                && StaffCB.SelectedItem != null)
            {
                if (PasswordBox.Password.Length >= 5)
                {
                    if (!context.Users.ToList().Any(x => x.User_Login == LoginBox.Text))
                    {
                        user.User_Login = LoginBox.Text;
                        user.User_Password = PasswordBox.Password;
                        user.User_Employee_ID = (StaffCB.SelectedItem as Staff).ID_Employee;
                        context.Users.Add(user);
                        context.SaveChanges();
                        UsersGrid.ItemsSource = context.Users.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такой логин уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Пароль должен быть длинее 5");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                var user = UsersGrid.SelectedItem as Users;
                if (!(user.Staff.Positions.ID_Position == 1))
                {
                    context.Users.Remove(user);
                    context.SaveChanges();

                    UsersGrid.ItemsSource = context.Users.ToList();
                }
                else
                {
                    MessageBox.Show("Удаление пользователей с ролью Админ запрещено");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsersGrid.SelectedItem != null)
            {
                var user = UsersGrid.SelectedItem as Users;

                if (LoginBox.Text != null && PasswordBox.Password != null
                && StaffCB.SelectedItem != null)
                {
                    if (PasswordBox.Password.Length >= 5)
                    {
                        if (!context.Users.ToList().Any(x => x.User_Login == LoginBox.Text) || LoginBox.Text == user.User_Login)
                        {

                            user.User_Login = LoginBox.Text;
                            user.User_Password = PasswordBox.Password;
                            user.User_Employee_ID = (StaffCB.SelectedItem as Staff).ID_Employee;

                            context.SaveChanges();
                            UsersGrid.ItemsSource = context.Users.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такой логин уже существует");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Пароль должен быть длинее 5");
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
