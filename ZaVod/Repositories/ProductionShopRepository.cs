using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ZaVod.Models;


namespace ZaVod.Repositories
{
    internal class ProductionShopRepository: Repository
    {
        public List<ProductionShop> getAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM production_shops";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<ProductionShop> productionShops = new List<ProductionShop>();

                        while (reader.Read())
                        {

                            ProductionShop productionShop = new ProductionShop
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                                Additional = reader["additional"].ToString(),
                            };

                            productionShops.Add(productionShop);
                        }

                        return productionShops;
                    }
                }
            }
        }
        public int getIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM production_shops WHERE name = @Name";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Name", name);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return Convert.ToInt32(reader["id"].ToString());
                        }

                    }
                }
            }

            return 0;
        }
    }
}
