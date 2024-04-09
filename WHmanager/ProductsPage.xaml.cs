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
    /// Логика взаимодействия для ProductsPage.xaml
    /// </summary>
    public partial class ProductsPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public ProductsPage()
        {
            InitializeComponent();
            ProductsGrid.ItemsSource = context.Products.ToList();

            CategoryCB.ItemsSource = context.Categories.ToList();
            CategoryCB.DisplayMemberPath = "Title";
            MakerCB.ItemsSource = context.Makers.ToList();
            MakerCB.DisplayMemberPath = "Maker_Name";
            ProviderCB.ItemsSource = context.Providers.ToList();
            ProviderCB.DisplayMemberPath = "PrName";
        }

        private void ProductsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                TitleBox.Text = (ProductsGrid.SelectedItem as Products).Product_Name.ToString();
                PriceBox.Text = (ProductsGrid.SelectedItem as Products).Price.ToString();
                AvailableBox.IsChecked = (ProductsGrid.SelectedItem as Products).Availible;
                CategoryCB.SelectedItem = (ProductsGrid.SelectedItem as Products).Categories;
                MakerCB.SelectedItem = (ProductsGrid.SelectedItem as Products).Makers;
                ProviderCB.SelectedItem = (ProductsGrid.SelectedItem as Products).Providers;

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Products product = new Products();
            if (TitleBox.Text != null && PriceBox.Text != null
                && AvailableBox.IsChecked != null && CategoryCB.SelectedItem != null
                && MakerCB.SelectedItem != null && ProviderCB.SelectedItem != null)
            {
                if (Decimal.TryParse(PriceBox.Text, out decimal price))
                {
                    if (price > 0 && price < 10000000000)
                    {
                        if (!context.Products.ToList().Any(x => x.Product_Name == TitleBox.Text))
                        {

                            product.Product_Name = TitleBox.Text;
                            product.Price = Math.Round(price * 100) / 100;
                            product.Availible = (bool)AvailableBox.IsChecked;
                            product.Category_ID = (CategoryCB.SelectedItem as Categories).ID_Category;
                            product.Maker_ID = (MakerCB.SelectedItem as Makers).ID_Maker;
                            product.Provider_ID = (ProviderCB.SelectedItem as Providers).ID_Provider;

                            context.Products.Add(product);
                            context.SaveChanges();
                            ProductsGrid.ItemsSource = context.Products.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такой продукт уже существует");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Цена должна быть больше нуля \nP.S. (и меньше 10 лямов, имейте совесть)");
                    }
                }
                else
                {
                    MessageBox.Show("Цена введена в неверном формате");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                var product = ProductsGrid.SelectedItem as Products;
                if (!context.Checks.ToList().Any(x => x.Product_ID == product.ID_Product))
                {
                    context.Products.Remove(product);
                    context.SaveChanges();

                    ProductsGrid.ItemsSource = context.Products.ToList();
                }
                else
                {
                    MessageBox.Show("Данный продукт связан с таблицей чеков, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (ProductsGrid.SelectedItem != null)
            {
                if (TitleBox.Text != null && PriceBox.Text != null
                    && AvailableBox.IsChecked != null && CategoryCB.SelectedItem != null
                    && MakerCB.SelectedItem != null && ProviderCB.SelectedItem != null)
                {
                    if (Decimal.TryParse(PriceBox.Text, out decimal price))
                    {
                        if (price > 0 && price < 10000000000)
                        {
                            var product = ProductsGrid.SelectedItem as Products;
                            if (!context.Products.ToList().Any(x => x.Product_Name == TitleBox.Text) || TitleBox.Text == product.Product_Name)
                            {

                                product.Product_Name = TitleBox.Text;
                                product.Price = Math.Round(price * 100) / 100;
                                product.Availible = (bool)AvailableBox.IsChecked;
                                product.Category_ID = (CategoryCB.SelectedItem as Categories).ID_Category;
                                product.Maker_ID = (MakerCB.SelectedItem as Makers).ID_Maker;
                                product.Provider_ID = (ProviderCB.SelectedItem as Providers).ID_Provider;

                                context.SaveChanges();
                                ProductsGrid.ItemsSource = context.Products.ToList();
                            }
                            else
                            {
                                MessageBox.Show("Такой продукт уже существует");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Цена должна быть больше нуля \nP.S. (и меньше 10 лямов, имейте совесть)");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Цена введена в неверном формате");
                    }
                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            try
            {
                List<ProductsJSON> products = SerDeser.DeserialiseObject<List<ProductsJSON>>();
                foreach (var product in products)
                {
                    var newproduct = new Products();
                    newproduct.Product_Name = product.Product_Name;
                    newproduct.Price = product.Price;
                    newproduct.Availible = product.Availible;
                    newproduct.Category_ID = product.Category_ID;
                    newproduct.Maker_ID = product.Maker_ID;
                    newproduct.Provider_ID = product.Provider_ID;

                    context.Products.Add(newproduct);
                    context.SaveChanges();
                }
                ProductsGrid.ItemsSource = null;
                ProductsGrid.ItemsSource = context.Products.ToList();

            }
            catch (Exception)
            {
                MessageBox.Show("Не тот формат");
            }
        }
    }
}
