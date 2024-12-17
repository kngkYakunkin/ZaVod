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

namespace ZaVod.Views
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Window
    {
        public Worker worker;
        public Menu(Worker worker)
        {
            this.worker = worker;
            InitializeComponent();

            CarRepository carRepository = new CarRepository();

            all.Text = carRepository.getAll().Count().ToString();
            sold.Text = carRepository.getAllByStatusId(5).Count().ToString();
            inProcess.Text = carRepository.getAllByStatusId(2).Count().ToString();
            notSold.Text = carRepository.getAllByStatusId(4).Count().ToString();
        }

        private void toCars(object sender, RoutedEventArgs e)
        {
            Cars cars = new Cars(this.worker);
            cars.Show();
            this.Close();
        }

        private void a2_Click(object sender, RoutedEventArgs e)
        {
            Brands brands = new Brands(this.worker);
            brands.Show();
            this.Hide();
        }

        private void a3_Click(object sender, RoutedEventArgs e)
        {
            Models models = new Models(this.worker);
            models.Show();
            this.Close();
        }

        private void a4_Click(object sender, RoutedEventArgs e)
        {
            Report report = new Report(this.worker);
            report.Show();
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Auth auth = new Auth();
            auth.Show();
            this.Close();
        }
    }
}
