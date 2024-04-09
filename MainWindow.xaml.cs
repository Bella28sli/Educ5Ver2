using Educ5Ver2.Admin;
using Educ5Ver2.Cashier;
using Educ5Ver2.Consultant;
using Educ5Ver2.Logistian;
using Educ5Ver2.WHmanager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
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

namespace Educ5Ver2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();

        public MainWindow()
        {
            InitializeComponent();
        }
        private void EnterButton_Click(object sender, RoutedEventArgs e)
        {
            bool show1 = true;
            var allogins = context.Users.ToList();
            foreach (var account in allogins)
            {
                if (account.User_Login == LoginBox.Text && account.User_Password == PasswordBX.Password)
                {
                    show1 = false;
                    switch (account.Staff.Position_ID)
                    {
                        case 1:
                            AdminWindow adminWindow = new AdminWindow();
                            adminWindow.Show();
                            this.Close();
                            break;
                        case 2:
                            ConsultantWindow consultant = new ConsultantWindow();
                            consultant.Show();
                            this.Close();
                            break;
                        case 3:
                            LogistianWindow logist = new LogistianWindow();
                            logist.Show();
                            this.Close();
                            break;
                        case 4:
                            WHmanagerWindow wh = new WHmanagerWindow();
                            wh.Show();
                            this.Close();
                            break;
                        case 5:
                            CashierWindow cashier = new CashierWindow(account);
                            cashier.Show();
                            this.Close();
                            break;
                        default:
                            MessageBox.Show("Такое окно ещё не разработано");
                            break;
                    }
                }
            }
            if (show1)
            {
                MessageBox.Show("Неверный логин или пароль");
            }

        }
    }
}
