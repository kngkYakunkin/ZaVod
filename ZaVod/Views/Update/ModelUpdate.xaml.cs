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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using ZaVod.Models;
using ZaVod.Repositories;

namespace ZaVod.Views.Update
{
    /// <summary>
    /// Interaction logic for ModelUpdate.xaml
    /// </summary>
    public partial class ModelUpdate : Window
    {
        private int id;
        private Worker worker;
        private Model model;

        private BrandRepository brandRepository;
        private ModelRepository modelRepository;

        public ModelUpdate(Worker worker, int id)
        {
            InitializeComponent();
            this.worker = worker;
            this.id = id;
            this.brandRepository = new BrandRepository();
            this.modelRepository = new ModelRepository();
   
            CarRepository carRepository = new CarRepository();
            Model model = modelRepository.getModelById(id);
            this.model = model;

            FillCombos();
            modelBox.Text = this.model.Name.ToString();
        }

        // Fill the comboBoxes
        private void FillCombos()
        {
            if (this.model == null)
            {
                return;
            }

            List<Brand> brands = this.brandRepository.getAll();

            for (int i = 0; i < brands.Count; i++)
            {
                if (i < brands.Count)
                {
                    brandCombo.Items.Add(brands[i].Name);
                    if (this.model.BrandId == brands[i].Id)
                    {
                        brandCombo.SelectedIndex = i;
                    }
                }
               
            }
        }
        // Save
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                CarRepository carRepository = new CarRepository();
                string selectedBrand = brandCombo.SelectedValue.ToString();
                int selectedBrandId = brandRepository.getIdByName(selectedBrand);
                string enteredName = modelBox.Text.ToString();

                this.modelRepository.update(this.id, enteredName, selectedBrandId);

                System.Windows.MessageBox.Show("Success!");
            }catch(Exception ex)
            {
                System.Windows.MessageBox.Show("Validation Error!");
            }
        }
        // Back
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Models models = new Models(this.worker);
            models.Show();
            this.Close();
        }

        // Delete
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            this.modelRepository.delete(this.id);
            System.Windows.MessageBox.Show("Success!");
        }

    }
}
