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
using ZaVod.Repositories;
using ZaVod.Models;
using ZaVod.Middlewares;

namespace ZaVod.Views.Update
{
    /// <summary>
    /// Логика взаимодействия для CarUpdate.xaml
    /// </summary>
    public partial class CarUpdate : Window
    {
        private int id;
        private Worker worker;
        private Car car;

        private BrandRepository brandRepository;
        private ModelRepository modelRepository;
        private StatusRepository statusRepository;
        private ProductionShopRepository productionShopRepository;
        private CarRepository carRepository;

        private SetAdminAccess setAdminAccess = new SetAdminAccess();
        public CarUpdate(Worker worker, int id)
        {
            InitializeComponent();
            this.worker = worker;
            this.id = id;
            // Get car by id
            CarRepository carRepository = new CarRepository();
            Car car = carRepository.getCarById(id);
            this.car = car;

            this.carRepository = new CarRepository();
            this.brandRepository = new BrandRepository();
            this.modelRepository = new ModelRepository();
            this.statusRepository = new StatusRepository();
            this.productionShopRepository = new ProductionShopRepository();

            FillCombos();
            priceTextbox.Text = this.car.Price.ToString();

            // Middleware
            if (!setAdminAccess.isAdmin(this.worker))
            {
                brandCombo.IsEnabled = false;
                modelCombo.IsEnabled = false;
                productionShopCombo.IsEnabled = false;
                priceTextbox.IsEnabled = false;
            }
            
        }

        // Fill the comboBoxes
        private void FillCombos()
        {
            if(this.car == null)
            {
                return;   
            }

            List<Brand> brands = this.brandRepository.getAll();
            List<Model> models = this.modelRepository.getAllByBrandId(this.car.BrandId);
            List<Status> statuses = this.statusRepository.getAll();
            List<ProductionShop> productionShops = this.productionShopRepository.getAll();

            int maxCount = Math.Max(Math.Max(brands.Count, models.Count), Math.Max(statuses.Count, productionShops.Count));
            
            for (int i = 0; i < maxCount; i++)
            {
                if (i < brands.Count)
                {
                    brandCombo.Items.Add(brands[i].Name);
                    if (this.car.BrandId == brands[i].Id)
                    {
                        brandCombo.SelectedIndex = i;
                    }
                }
                if (i < models.Count)
                {
                    modelCombo.Items.Add(models[i].Name);
                    
                    if (this.car.ModelId == models[i].Id)
                    {
                        Console.WriteLine(models[i].Id);
                        modelCombo.SelectedIndex = i;
                    }
                }
                if (i < statuses.Count)
                {
                    statusCombo.Items.Add(statuses[i].Value);
                    if (this.car.StatusId == statuses[i].Id)
                    {
                        statusCombo.SelectedIndex = i;
                    }
                }
                if (i < productionShops.Count)
                {
                    productionShopCombo.Items.Add(productionShops[i].Name);
                    if (this.car.ProductionShopId == productionShops[i].Id)
                    {
                        productionShopCombo.SelectedIndex = i;
                    }
                }
            }
        }
        // Save
        private void Button_Click(object sender, RoutedEventArgs e)
        { 
            CarRepository carRepository = new CarRepository();
            string selectedBrand = brandCombo.SelectedValue.ToString();
            string selectedModel = modelCombo.SelectedValue.ToString();
            string selectedStatus = statusCombo.SelectedValue.ToString();
            string selectedProductionShop = productionShopCombo.SelectedValue.ToString();
            int enteredPrice = Convert.ToInt32(priceTextbox.Text);

            int brandId = this.brandRepository.getIdByName(selectedBrand);
            int modelId = this.modelRepository.getIdByName(selectedModel);
            int statusId = this.statusRepository.getIdByName(selectedStatus);
            int productionShopId = this.productionShopRepository.getIdByName(selectedProductionShop);

            carRepository.update(this.car.Id, brandId, modelId, statusId, productionShopId, enteredPrice);

            this.car = carRepository.getCarById(this.id);

            MessageBox.Show("Success!");
        }
        // Back
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Cars cars = new Cars(this.worker);
            cars.Show();
            this.Close();
        }

        // Delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.carRepository.delete(this.car.Id);
            MessageBox.Show("Success!");
        }
    }
}
