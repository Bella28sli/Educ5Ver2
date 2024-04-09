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
    /// Логика взаимодействия для PositionsPage.xaml
    /// </summary>
    public partial class PositionsPage : Page
    {
        private MusicalInstrumentStoreEntities context = new MusicalInstrumentStoreEntities();
        public PositionsPage()
        {
            InitializeComponent();
            PositionsGrid.ItemsSource = context.Positions.ToList();

        }
        private void PositionsGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                PositionNameBox.Text = (PositionsGrid.SelectedItem as Positions).Position.ToString();
                WorkHoursBox.Text = (PositionsGrid.SelectedItem as Positions).Work_Hours.ToString();

            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            Positions position = new Positions();
            if (PositionNameBox.Text != null && WorkHoursBox.Text != null)
            {
                if (Int32.TryParse(WorkHoursBox.Text, out int workhours) && PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)))
                {
                    if (workhours > 0 && workhours <= 12)
                    {
                        position.Position = PositionNameBox.Text;
                        position.Work_Hours = workhours;

                        context.Positions.Add(position);
                        context.SaveChanges();
                        PositionsGrid.ItemsSource = context.Positions.ToList();
                    }
                    else
                    {
                        MessageBox.Show("Часы работы не должны быть более 12 и менее 0");
                    }
                }
                else
                {
                    MessageBox.Show("Часы работы должны быть числом, а название не должно содержать специальных символов");
                }
            }
            else
            {
                MessageBox.Show("Заполните все поля");
            }
        }


        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                var position = PositionsGrid.SelectedItem as Positions;
                if (!context.Staff.ToList().Any(x => x.Position_ID == position.ID_Position))
                {
                    context.Positions.Remove(position);
                    context.SaveChanges();

                    PositionsGrid.ItemsSource = context.Positions.ToList();
                }
                else
                {
                    MessageBox.Show("Существует сотрудник с данной должностью, удаление невозможно");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для удаления");
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (PositionsGrid.SelectedItem != null)
            {
                if (PositionNameBox.Text != null && WorkHoursBox.Text != null && PositionNameBox.Text.All(ch => char.IsLetterOrDigit(ch)))
                {
                    if (Int32.TryParse(WorkHoursBox.Text, out int workhours))
                    {
                        if (workhours > 0 && workhours <= 12)
                        {
                            var position = PositionsGrid.SelectedItem as Positions;

                            position.Position = PositionNameBox.Text;
                            position.Work_Hours = workhours;

                            context.SaveChanges();
                            PositionsGrid.ItemsSource = context.Positions.ToList();
                        }
                        else
                        {
                            MessageBox.Show("Часы работы не должны быть более 12 и менее 0");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Часы работы должны быть числом, а название не должно содержать специальных символов");
                    }

                }
                else
                {
                    MessageBox.Show("Заполните все поля");
                }
            }
            else
            {
                MessageBox.Show("Выберете элемент для изменения");
            }
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                List<PositionsJSON> positions = SerDeser.DeserialiseObject<List<PositionsJSON>>();
                foreach (var position in positions)
                {
                    var newposition = new Positions();
                    newposition.Position = position.Position;
                    newposition.Work_Hours = position.Work_Hours;

                    context.Positions.Add(newposition);
                    context.SaveChanges();
                }
                PositionsGrid.ItemsSource = null;
                PositionsGrid.ItemsSource = context.Positions.ToList();
            }
            catch (Exception)
            {
                MessageBox.Show("Не тот формат");
            }
        }
    }
}
