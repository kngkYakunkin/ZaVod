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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;

namespace ZaVod
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            DatabaseHelper dbHelper = new DatabaseHelper();
            var data = dbHelper.GetData();
            MyListView.ItemsSource = data; 
           
        }

    }

    public class DatabaseHelper
    {
   
        private string connectionString = "Data Source=DESKTOP-A504I5S\\MSSQLSERVER1;Initial Catalog=ZaVod;Integrated Security=True";

        public List<MyDataModel> GetData()
        {
            var dataList = new List<MyDataModel>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM workers"; // Ваш SQL-запрос

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var item = new MyDataModel
                            {
                                Id = (int)reader["id"],
                                Name = reader["firstname"].ToString()  
                            };
                            dataList.Add(item);
                        }
                    }
                }
            }

            return dataList;
        }
    }

    public class MyDataModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }


}
