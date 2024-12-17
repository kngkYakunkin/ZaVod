using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
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

namespace ZaVod.Views
{
    /// <summary>
    /// Interaction logic for Report.xaml
    /// </summary>
    public partial class Report : Window
    {
  
        private Worker worker;
        private int currentCarPage = 0;
        private StatusRepository statusRepository;
        public Report(Worker worker)
        {
            this.worker = worker;
            this.statusRepository = new StatusRepository();
            InitializeComponent();
            // Get cars
            fillTable(0);

            fillCombos();

        }

        public void fillTable(int statusId)
        {
            CarRepository carRepository = new CarRepository();
            List<ZaVod.Models.Car> cars = carRepository.getAllByStatusId(statusId);

            int index = 0;
            cars_container.Children.Clear();
            foreach (ZaVod.Models.Car car in cars)
            {
                Border border = new Border();
                border.CornerRadius = new CornerRadius(6);
                border.Width = 800;
                border.Background = new SolidColorBrush(Colors.White);
                border.BorderThickness = new Thickness(1);
                
                cars_container.Children.Add(border);
                

                StackPanel carContainer = new StackPanel();
                if (index % 2 == 0)
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
                carContainer.Cursor = Cursors.Hand;
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

        public void fillCombos()
        {

            List<Status> statusesList = this.statusRepository.getAll();
            foreach(Status status in statusesList)
            {
                statusCombo.Items.Add(status.Value);
            }

            statusCombo.SelectedIndex = 0;
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

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Menu menu = new Menu(this.worker);
            menu.Show();
            this.Close();
        }

        private void statusCombo_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string comboValue = statusCombo.SelectedValue.ToString();
      
            int statusId = this.statusRepository.getIdByName(comboValue);
            Console.WriteLine(statusId);

            fillTable(statusId);
        }
    }

}
