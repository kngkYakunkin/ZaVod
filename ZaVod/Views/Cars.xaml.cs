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
using ZaVod.Views.Update;
using ZaVod.Views.Add;
using ZaVod.Middlewares;

namespace ZaVod.Views
{
    /// <summary>
    /// Логика взаимодействия для Cars.xaml
    /// </summary>
    public partial class Cars : Window
    {
        public Worker worker;
        public int currentCarPage = 0;
        private SetAdminAccess setAdminAccess = new SetAdminAccess();
        public Cars(Worker worker)
        {
            this.worker = worker;
            InitializeComponent();
            // Get cars
            CarRepository carRepository = new CarRepository();
            List<ZaVod.Models.Car> cars = carRepository.getAll();

            int index = 0;
            foreach(ZaVod.Models.Car car in cars){
                Border border = new Border();
                border.CornerRadius = new CornerRadius(6);
                border.Width = 800;
                border.Background = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(1);
                cars_container.Children.Add(border);

                StackPanel carContainer = new StackPanel();
                if(index % 2 == 0)
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(224, 224, 224));
                }
                else
                {
                    border.Background = new SolidColorBrush(Color.FromRgb(176, 176, 176));
                }
                carContainer.Width = 800;
                carContainer.Height = 50;
                carContainer.Orientation = Orientation.Horizontal;
                //carContainer.Margin = new Thickness(0, 5, 0, 0);
                carContainer.Cursor = Cursors.Hand;
                carContainer.MouseDown += toUpdatePage;
                index++;

                TextBlock id = setNewCell("#" + car.Id.ToString(), carContainer);
                TextBlock brand = setNewCell(carRepository.getBrandById(car.BrandId), carContainer);
                TextBlock model = setNewCell(carRepository.getModelById(car.ModelId), carContainer);
                TextBlock productionShop = setNewCell(carRepository.getProductionShopById(car.ProductionShopId), carContainer);
                TextBlock price = setNewCell(car.Price.ToString() + " руб.", carContainer);
                TextBlock status = setNewCell(carRepository.getStatusById(car.StatusId), carContainer);

                
                border.Child = carContainer;

            }

        }

        private void toUpdatePage(object sender, MouseButtonEventArgs e)
        {
            StackPanel stackPanel = sender as StackPanel;
            var firstChild = stackPanel.Children[0];
            if(firstChild is TextBlock textBlock)
            {
                string rawId = textBlock.Text;
                string numberRawId = rawId.Replace("#", "");
                int id = int.Parse(numberRawId);

                CarUpdate carUpdate = new CarUpdate(this.worker, id);
                carUpdate.Show();
                this.Close();
            }            
        }

        public TextBlock setNewCell(string text, StackPanel parent)
        {
            TextBlock cell = new TextBlock();
            cell.Width = 120;
            cell.Text = text;
            cell.Margin = new Thickness(5, 0, 5, 0);
            cell.FontSize = 16;
            cell.HorizontalAlignment = HorizontalAlignment.Center;

            parent.Children.Add(cell);

            return cell;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.worker);
            menu.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!this.setAdminAccess.isAdmin(this.worker))
            {
                MessageBox.Show(this.setAdminAccess.error_msg);
                return;
            }
            Add.Car car = new Add.Car(this.worker);
            car.Show();
            this.Close();
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.worker);
            menu.Show();
            this.Close();
        }
    }
}
