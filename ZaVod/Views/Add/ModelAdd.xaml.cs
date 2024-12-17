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

namespace ZaVod.Views.Add
{
    /// <summary>
    /// Interaction logic for ModelAdd.xaml
    /// </summary>
    public partial class ModelAdd : Window
    {
        private Worker worker;

        private BrandRepository brandRepository;
        private ModelRepository modelRepository;

        public ModelAdd(Worker worker)
        {
            InitializeComponent();
            this.worker = worker;
 
            this.brandRepository = new BrandRepository();
            this.modelRepository = new ModelRepository();

            CarRepository carRepository = new CarRepository();
            
            FillCombos();
        }

        // Fill the comboBoxes
        private void FillCombos()
        {
            List<Brand> brands = this.brandRepository.getAll();
            Console.WriteLine(brands[0].Name);
            
            for (int i = 0; i < brands.Count; i++)
            {
                if (i < brands.Count)
                {
                    brandCombo.Items.Add(brands[i].Name);
                    brandCombo.SelectedIndex = 0;
                }
            }
        }
     
        // back
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Models models = new Models(this.worker);
            models.Show();
            this.Close();
        }
        // Add
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarRepository carRepository = new CarRepository();
                string selectedBrand = brandCombo.SelectedValue.ToString();
                int selectedBrandId = brandRepository.getIdByName(selectedBrand);
                string enteredName = modelBox.Text.ToString();

                this.modelRepository.add(enteredName, selectedBrandId);

                System.Windows.MessageBox.Show("Success!");
            }
            catch (Exception ex)
            {
                System.Windows.MessageBox.Show("Validation Error!");
            }
        }

    }
}
