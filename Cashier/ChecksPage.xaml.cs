using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Educ5Ver2.Cashier
{
    /// <summary>
    /// Логика взаимодействия для ChecksPage.xaml
    /// </summary>
    public partial class ChecksPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public ChecksPage()
        {
            InitializeComponent();
            DateCB.ItemsSource = context.Orders.ToList();
            DateCB.DisplayMemberPath = "Order_Date";
        }

        private void DateCB_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DateCB.SelectedItem != null)
            {
                var order = (DateCB.SelectedItem as Orders);
                List<Products> products = new List<Products>();
                context.Checks.ToList().Where(item => item.Order_ID == order.ID_Order).ToList().ForEach(item => products.Add(item.Products));
                ProductsGrid.ItemsSource = products;

                StaffBox.Content = order.Staff.EmpSurname.ToString() + " " + order.Staff.EmpName.ToString() + " " + order.Staff.EmpPatronymic.ToString();
                DateBox.Content = order.Order_Date.ToString();
                SummBox.Content = order.Total_Amount.ToString();
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (DateCB.SelectedItem != null)
            {
                var order = (DateCB.SelectedItem as Orders);
                List<Products> products = new List<Products>();

                context.Checks.ToList().Where(item => item.Order_ID == order.ID_Order).ToList().ForEach(item => products.Add(item.Products));

                string goods = "";
                foreach (Products product in products)
                {
                    goods += "\n\t" + product.Product_Name + "\t-\t" + product.Price;

                }

                var dialog = new CommonOpenFileDialog();
                dialog.IsFolderPicker = true;
                var result = dialog.ShowDialog();
                if (result == CommonFileDialogResult.Ok)
                {
                    var path = dialog.FileName + "\\Check" + order.ID_Order.ToString() + ".txt";
                    string checkdesc = "\t   Струны души\r\n\t  Кассовый чек №" + order.ID_Order + "\r\n\r" + goods + "\n\r\nИтого к оплате: " + order.Total_Amount + "\r\nВнесено: " + order.Deposited + "\r\nСдача: " + order.Change_Amount + "";
                    File.WriteAllText(path, checkdesc);
                }
            }
        }
    }
}
