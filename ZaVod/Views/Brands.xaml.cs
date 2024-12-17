using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using ZaVod.Models;
using ZaVod.Repositories;
using ZaVod.Views.Add;
using ZaVod.Views.Update;
using ZaVod.Middlewares;

namespace ZaVod.Views
{
    /// <summary>
    /// Interaction logic for Brands.xaml
    /// </summary>
    public partial class Brands : Window
    {
        private Worker worker;
        private SetAdminAccess setAdminAccess = new SetAdminAccess();
        public Brands(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
            CarRepository carRepository = new CarRepository();
            BrandRepository brandRepository = new BrandRepository();
            List<Brand> brands = brandRepository.getAll();

            int index = 0;
            foreach (Brand brand in brands)
            {
                Border border = new Border();
                border.CornerRadius = new CornerRadius(6);
                border.Width = 340;
                border.Background = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(1);
                cars_container.Children.Add(border);

                StackPanel brandContainer = new StackPanel();
                if (index % 2 == 0)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
                }
                else
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(176, 176, 176));
                }
                brandContainer.Width = 400;
                brandContainer.Height = 50;
                brandContainer.Orientation = Orientation.Horizontal;
                brandContainer.Cursor = Cursors.Hand;
                brandContainer.MouseDown += toUpdatePage;
                index++;

                TextBlock id = setNewCell("#" + brand.Id.ToString(), brandContainer);
                TextBlock name = setNewCell(brand.Name.ToString(), brandContainer);

                border.Child = brandContainer;
            }
        }

        private void toUpdatePage(object sender, MouseButtonEventArgs e)
        {
            if (!this.setAdminAccess.isAdmin(this.worker))
            {
                MessageBox.Show(this.setAdminAccess.error_msg);
                return;
            }

            StackPanel stackPanel = sender as StackPanel;
            var firstChild = stackPanel.Children[0];
            if (firstChild is TextBlock textBlock)
            {
                string rawId = textBlock.Text;
                string numberRawId = rawId.Replace("#", "");
                int id = int.Parse(numberRawId);

                BrandUpdate brandUpdate = new BrandUpdate(this.worker, id);
                brandUpdate.Show();
                this.Close();
            }
        }
        public TextBlock setNewCell(string text, StackPanel parent)
        {
            TextBlock cell = new TextBlock();
            cell.Width = 150;
            cell.Text = text;
            cell.Margin = new Thickness(5, 0, 5, 0);
            cell.FontSize = 16;
            cell.HorizontalAlignment = HorizontalAlignment.Center;

            parent.Children.Add(cell);

            return cell;
        }

        // Back
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.worker);
            menu.Show();
            this.Close();
        }

        // Add
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!this.setAdminAccess.isAdmin(this.worker))
            {
                MessageBox.Show(this.setAdminAccess.error_msg);
                return;
            }
            BrandAdd brandAdd = new BrandAdd(this.worker);
            brandAdd.Show();
            this.Close();
            
        }
    }
}
