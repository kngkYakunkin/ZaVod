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

namespace ZaVod.Views.Add
{
    /// <summary>
    /// Interaction logic for BrandAdd.xaml
    /// </summary>
    public partial class BrandAdd : Window
    {
        private Worker worker;

        private BrandRepository brandRepository;
     
        public BrandAdd(Worker worker)
        {
            this.worker = worker;
            InitializeComponent();
           
            this.brandRepository = new BrandRepository();
           
        }

       
        // Back
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Brands brands = new Brands(this.worker);
            brands.Show();
            this.Close();
        }

        // Save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string enteredBrand = brandBox.Text.ToString();
               
                brandRepository.add(enteredBrand);
                System.Windows.MessageBox.Show("SUCCESS!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Validation Error!");
            }
        }
       
    }
}
