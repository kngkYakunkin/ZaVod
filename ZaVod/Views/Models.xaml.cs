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
using System.Windows.Shapes;
using ZaVod.Models;
using ZaVod.Repositories;
using ZaVod.Views.Add;
using ZaVod.Views.Update;
using ZaVod.Middlewares;

namespace ZaVod.Views
{
    /// <summary>
    /// Interaction logic for Models.xaml
    /// </summary>
    public partial class Models : Window
    {
        private Worker worker;
        private SetAdminAccess setAdminAccess = new SetAdminAccess();
       
        public Models(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
            CarRepository carRepository = new CarRepository();
            ModelRepository modelRepository= new ModelRepository();
            BrandRepository brandRepository = new BrandRepository();
            List<Model> models = modelRepository.getAll();

            int index = 0;
            foreach (Model model in models)
            {
                Border border = new Border();
                border.CornerRadius = new CornerRadius(6);
                border.Width = 400;
                border.Background = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(1);
                cars_container.Children.Add(border);

                StackPanel modelContainer = new StackPanel();
                if (index % 2 == 0)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
                }
                else
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(176, 176, 176));
                }
                modelContainer.Width = 400;
                modelContainer.Height = 50;
                modelContainer.Orientation = Orientation.Horizontal;
                modelContainer.Cursor = Cursors.Hand;
                modelContainer.MouseDown += toUpdatePage;
                index++;

                TextBlock id = setNewCell("#" + model.Id.ToString(), modelContainer);
                TextBlock name = setNewCell(model.Name.ToString(), modelContainer);
                string brandName = brandRepository.getNameById(model.BrandId);
                TextBlock brand= setNewCell(brandName, modelContainer);

                border.Child = modelContainer;
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

                ModelUpdate modelUpdate= new ModelUpdate(this.worker, id);
                modelUpdate.Show();
                this.Close();
            }
        }
        public TextBlock setNewCell(string text, StackPanel parent)
        {
            TextBlock cell = new TextBlock();
            cell.Width = 100;
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

            ModelAdd modelAdd = new ModelAdd(this.worker);
            modelAdd.Show();
            this.Close();

        }
    }
}
