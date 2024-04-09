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
    /// Логика взаимодействия для CategoriesPage.xaml
    /// </summary>
    public partial class CategoriesPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();

        public CategoriesPage()
        {
            InitializeComponent();
            CategoryGrid.ItemsSource = context.Categories.ToList();

        }
        private void CategoryGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CategoryGrid.SelectedItem != null)
            {
                CategoryBox.Text = (CategoryGrid.SelectedItem as Categories).Title.ToString();
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Categories category = new Categories();
            if (CategoryBox.Text != null)
            {
                if (CategoryBox.Text.All(ch => char.IsLetter(ch)))
                {
                    if (!context.Categories.ToList().Any(x => x.Title == CategoryBox.Text))
                    {
                        category.Title = CategoryBox.Text;

                        context.Categories.Add(category);
                        context.SaveChanges();
                        CategoryGrid.ItemsSource = context.Categories.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Такая категория уже существует");
                    }
                }
                else
                {
                    MessageBox.Show("Название должно состоять только из букв");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryGrid.SelectedItem != null)
            {
                var category = CategoryGrid.SelectedItem as Categories;

                if (!context.Products.ToList().Any(x => x.Category_ID == category.ID_Category))
                {
                    context.Categories.Remove(category);
                    context.SaveChanges();

                    CategoryGrid.ItemsSource = context.Categories.ToList();
                }
                else
                {
                    MessageBox.Show("Существует продукт с данной категорией, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }
        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (CategoryGrid.SelectedItem != null)
            {
                var category = CategoryGrid.SelectedItem as Categories;

                if (CategoryBox.Text != null)
                {
                    if (CategoryBox.Text.All(ch => char.IsLetter(ch)))
                    {
                        if (!context.Categories.ToList().Any(x => x.Title == CategoryBox.Text) || CategoryBox.Text == category.Title)
                        {

                            category.Title = CategoryBox.Text;

                            context.SaveChanges();
                            CategoryGrid.ItemsSource = context.Categories.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Такая категория уже существует");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Название должно состоять только из букв");
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
