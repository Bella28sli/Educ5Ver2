using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
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
    /// Логика взаимодействия для OrderPage.xaml
    /// </summary>
    public partial class OrderPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        static public List<Products> products = new List<Products>();
        static decimal totalsumm;
        static Users account = new Users();
        public OrderPage(Users user)
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = context.Products.ToList().Where(item => item.Availible);
            ClientCB.ItemsSource = context.Clients.ToList();
            ClientCB.DisplayMemberPath = "Surname";
            PickUpPointCB.ItemsSource = context.Pickup_Points.ToList();
            PickUpPointCB.DisplayMemberPath = "Point_Address";
            account = user;
        }

        private void PlusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                products.Add(ProductsGrid.SelectedItem as Products);
                totalsumm += (ProductsGrid.SelectedItem as Products).Price;

                TotalBox.Content = " Товары в чеке      Сумма: " + totalsumm;
                ChoiceGrid.ItemsSource = null;
                ChoiceGrid.ItemsSource = products;
            }
        }

        private void MinusButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                products.Remove(ProductsGrid.SelectedItem as Products);
                totalsumm -= (ProductsGrid.SelectedItem as Products).Price;

                TotalBox.Content = " Товары в чеке      Сумма: " + totalsumm; 
                ChoiceGrid.ItemsSource = null;
                ChoiceGrid.ItemsSource = products;
            }
        }

        private void SaveButton_Click(object sender, RoutedEventArgs e)
        {
            if (products.Count != 0 && PickUpPointCB.SelectedItem != null
                && ClientCB.SelectedItem != null && EnterBox.Text != null)
            {
                if (Decimal.TryParse(EnterBox.Text, out decimal entersumm))
                {
                    if (entersumm >= totalsumm)
                    {
                        Orders order = new Orders();

                        order.Change_Amount = entersumm - totalsumm;
                        order.Order_Date = DateTime.Now;
                        order.Total_Amount = totalsumm;
                        order.Deposited = entersumm;
                        order.Point_ID = (PickUpPointCB.SelectedItem as Pickup_Points).ID_Point;
                        order.Client_ID = (ClientCB.SelectedItem as Clients).ID_Client;
                        order.Order_Employee_ID = account.Staff.ID_Employee;

                        context.Orders.Add(order);
                        context.SaveChanges();

                        string goods = "";
                        foreach(Products product in products)
                        {
                            Checks check = new Checks();
                            check.Product_ID = product.ID_Product;
                            check.Order_ID = order.ID_Order;
                            context.Checks.Add(check);
                            context.SaveChanges();

                            goods += "\n\t" + product.Product_Name + "\t-\t" + product.Price;

                        }

                        var dialog = new CommonOpenFileDialog();
                        dialog.IsFolderPicker = true;
                        var result = dialog.ShowDialog();
                        if (result == CommonFileDialogResult.Ok)
                        {
                            var path = dialog.FileName + "\\Check" + order.ID_Order.ToString()+".txt";
                            string checkdesc = "\t   Струны души\r\n\t  Кассовый чек №"+order.ID_Order+"\r\n\r"+goods+"\n\r\nИтого к оплате: "+order.Total_Amount+"\r\nВнесено: "+order.Deposited+"\r\nСдача: "+order.Change_Amount+"";
                            File.WriteAllText(path, checkdesc);
                        }
                    }
                    else
                    {
                        MessageBox.Show("Не хватает денег");
                    }
                }
                else
                {
                    MessageBox.Show("Внесённая сумма в неверном формате");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
    }
}
