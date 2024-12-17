using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZaVod.Models;
using ZaVod.Repositories;

namespace ZaVod.Views.Update
{
    /// <summary>
    /// Interaction logic for BrandUpdate.xaml
    /// </summary>
    public partial class BrandUpdate : Window
    {
        public BrandUpdate()
        {
            InitializeComponent();
        }
        private int id;
        private Worker worker;
        private string brand;

        private BrandRepository brandRepository;

        public BrandUpdate(Worker worker, int id)
        {
            InitializeComponent();
            this.worker = worker;
            this.id = id;
            // Get car by id
            CarRepository carRepository = new CarRepository();
            this.brand = carRepository.getBrandById(this.id);

            this.brandRepository = new BrandRepository();

            brandCombo.Text = this.brand;
        }
     
        //Save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string brandValue = brandCombo.Text.ToString();

            this.brandRepository.update(this.id, brandValue);

            System.Windows.MessageBox.Show("Success!");
        }
        // Delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.brandRepository.delete(this.id);
            System.Windows.MessageBox.Show("Success!");
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Brands brands = new Brands(this.worker);
            brands.Show();
            this.Close();
        }
    }
}
