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
    /// Interaction logic for Car.xaml
    /// </summary>
    public partial class Car : Window
    {
        private Worker worker;

        private BrandRepository brandRepository;
        private ModelRepository modelRepository;
        private StatusRepository statusRepository;
        private ProductionShopRepository productionShopRepository;
        private CarRepository carRepository;
        public Car(Worker worker)
        {
            this.worker = worker;
            InitializeComponent();
            this.carRepository = new CarRepository();
            this.brandRepository = new BrandRepository();
            this.modelRepository = new ModelRepository();
            this.statusRepository = new StatusRepository();
            this.productionShopRepository = new ProductionShopRepository();
            fillCombos();
        }

        private void fillCombos()
        {
            List<Brand> brands = this.brandRepository.getAll();
            List<Model> models = this.modelRepository.getAll();
            List<Status> statuses = this.statusRepository.getAll();
            List<ProductionShop> productionShops = this.productionShopRepository.getAll();

            int maxCount = Math.Max(Math.Max(brands.Count, models.Count), Math.Max(statuses.Count, productionShops.Count));

            for (int i = 0; i < maxCount; i++)
            {
                if (i < brands.Count)
                {
                    brandCombo.Items.Add(brands[i].Name);
                }
                if (i < models.Count)
                {
                    modelCombo.Items.Add(models[i].Name);

                }
                if (i < statuses.Count)
                {
                    statusCombo.Items.Add(statuses[i].Value);
                }
                if (i < productionShops.Count)
                {
                    productionShopCombo.Items.Add(productionShops[i].Name);
                }
            }

            brandCombo.SelectedIndex = 0;
            modelCombo.SelectedIndex = 0;
            statusCombo.SelectedIndex = 0;
            productionShopCombo.SelectedIndex = 0;

        }
        // Back
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cars cars = new Cars(this.worker);
            cars.Show();
            this.Close();
        }

        // Save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string selectedBrand = brandCombo.SelectedValue.ToString();
                string selectedModel = modelCombo.SelectedValue.ToString();
                string selectedStatus = statusCombo.SelectedValue.ToString();
                string selectedProductionShop = productionShopCombo.SelectedValue.ToString();
                int enteredPrice = Convert.ToInt32(priceTextbox.Text);

                int brandId = this.brandRepository.getIdByName(selectedBrand);
                int modelId = this.modelRepository.getIdByName(selectedModel);
                int statusId = this.statusRepository.getIdByName(selectedStatus);
                int productionShopId = this.productionShopRepository.getIdByName(selectedProductionShop);

                carRepository.add(brandId, modelId, statusId, productionShopId, enteredPrice);
                MessageBox.Show("SUCCESS!");
            }
            catch(Exception ex)
            {
                MessageBox.Show("Validation Error!");
            }

        }
    }
}
