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
    /// Логика взаимодействия для StaffPage.xaml
    /// </summary>
    public partial class StaffPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public StaffPage()
        {
            InitializeComponent();
            StaffGrid.ItemsSource = context.Staff.ToList();

            PositionCB.ItemsSource = context.Positions.ToList();
            PositionCB.DisplayMemberPath = "Position";

        }

        private void StaffGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                SurnameBox.Text = (StaffGrid.SelectedItem as Staff).EmpSurname.ToString();
                NameBox.Text = (StaffGrid.SelectedItem as Staff).EmpName.ToString();
                PatronymicBox.Text = (StaffGrid.SelectedItem as Staff).EmpPatronymic.ToString();
                PositionCB.SelectedItem = (StaffGrid.SelectedItem as Staff).Positions;
                SalaryBox.Text = (StaffGrid.SelectedItem as Staff).Salary.ToString();

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Staff employee = new Staff();
            if (SurnameBox.Text != null && NameBox.Text != null
                && SalaryBox != null && PositionCB.SelectedItem != null)
            {
                if (Decimal.TryParse(SalaryBox.Text, out decimal salary) && SurnameBox.Text.All(ch => char.IsLetter(ch))
                    && NameBox.Text.All(ch => char.IsLetter(ch)))
                {
                    if (salary > 0 && salary < 10000000000)
                    {
                        employee.EmpSurname = SurnameBox.Text;
                        employee.EmpName = NameBox.Text;
                        if (PatronymicBox.Text != null && PatronymicBox.Text.All(ch => char.IsLetter(ch)))
                        {
                            employee.EmpPatronymic = PatronymicBox.Text;
                        }
                        employee.Salary = Math.Round(salary * 100) / 100;
                        employee.Position_ID = (PositionCB.SelectedItem as Positions).ID_Position;
                        context.Staff.Add(employee);
                        context.SaveChanges();
                        StaffGrid.ItemsSource = context.Staff.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Зарплата должна быть больше нуля \nP.S. (и меньше 10 лямов, имейте совесть)");
                    }
                }
                else
                {
                    MessageBox.Show("ФИО должно состоять только из букв, а зарплата только из цифр");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                var employee = StaffGrid.SelectedItem as Staff;
                if (!context.Users.ToList().Any(x => x.User_Employee_ID == employee.ID_Employee)
                    && !context.Orders.ToList().Any(x => x.Order_Employee_ID == employee.ID_Employee)
                    && !context.Staff_Choice.ToList().Any(x => x.Employee_ID == employee.ID_Employee))
                {
                    context.Staff.Remove(employee);
                    context.SaveChanges();

                    StaffGrid.ItemsSource = context.Staff.ToList();
                }
                else
                {
                    MessageBox.Show("Данный сотрудник связан с другими данныйми, удаление невозможно\n(см. Пользователи, Выбор сотрудника, Заказы)");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (StaffGrid.SelectedItem != null)
            {
                if (SurnameBox.Text != null && NameBox.Text != null
                    && SalaryBox != null && PositionCB.SelectedItem != null)
                {
                    if (Decimal.TryParse(SalaryBox.Text, out decimal salary) && SurnameBox.Text.All(ch => char.IsLetter(ch))
                        && NameBox.Text.All(ch => char.IsLetter(ch)))
                    {
                        if (salary > 0 && salary < 10000000000)
                        {
                            var employee = StaffGrid.SelectedItem as Staff;

                            employee.EmpSurname = SurnameBox.Text;
                            employee.EmpName = NameBox.Text;
                            if (PatronymicBox.Text != null && PatronymicBox.Text.All(ch => char.IsLetter(ch)))
                            {
                                employee.EmpPatronymic = PatronymicBox.Text;
                            }
                            employee.Salary = Math.Round(salary * 100) / 100;
                            employee.Position_ID = (PositionCB.SelectedItem as Positions).ID_Position;

                            context.SaveChanges();
                            StaffGrid.ItemsSource = context.Staff.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Зарплата должна быть больше нуля \nP.S. (и меньше 10 лямов, имейте совесть)");
                        }
                    }
                    else
                    {
                        MessageBox.Show("ФИО должно состоять только из букв, а зарплата только из цифр");
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
