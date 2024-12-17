using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaVod.Models;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace ZaVod.Repositories
{
    class CarRepository : Repository
    {
        public CarRepository()
        {

        }

        public List<Car> getAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Cars";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@Login", login);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Car> cars = new List<Car>();

                        while (reader.Read())
                        {

                            Car car = new Car
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                                ModelId = Convert.ToInt32(reader["model_id"].ToString()),
                                StatusId = Convert.ToInt32(reader["status_id"].ToString()),
                                ProductionShopId = Convert.ToInt32(reader["production_shop_id"].ToString()),
                                Price = Convert.ToInt32(reader["price"].ToString()),
                            };
                            
                            

                            // Add the row to the list
                            cars.Add(car);
                        }

                        return cars;
                    }
                }
            }
        }
        public List<Car> getAllByStatusId(int statusId)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Cars WHERE status_id=@StatusId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@StatusId", statusId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Car> cars = new List<Car>();

                        while (reader.Read())
                        {
                            Car car = new Car
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                                ModelId = Convert.ToInt32(reader["model_id"].ToString()),
                                StatusId = Convert.ToInt32(reader["status_id"].ToString()),
                                ProductionShopId = Convert.ToInt32(reader["production_shop_id"].ToString()),
                                Price = Convert.ToInt32(reader["price"].ToString()),
                            };

                            // Add the row to the list
                            cars.Add(car);
                        }

                        return cars;
                    }
                }
            }
        }
        public string getBrandById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM brands WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["name"].ToString();
                        }
                    }
                }
            }

            return "Default";

        }
        public Car getCarById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM cars WHERE id = @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            Car car = new Car
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                                ModelId = Convert.ToInt32(reader["model_id"].ToString()),
                                StatusId = Convert.ToInt32(reader["status_id"].ToString()),
                                ProductionShopId = Convert.ToInt32(reader["production_shop_id"].ToString()),
                                Price = Convert.ToInt32(reader["price"].ToString()),
                            };

                            return car;
                        }
                    }
                }
            }
            return new Car();

        }

        public string getModelById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM models WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["name"].ToString();
                        }
                    }
                }
            }

            return "Default";

        }

        public string getProductionShopById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM production_shops WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["name"].ToString();
                        }
                    }
                }
            }

            return "Default";

        }

        public string getStatusById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT value FROM statuses WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            return reader["value"].ToString();
                        }
                    }
                }
            }

            return "Unknown";

        }
        public void update(int id, int brandId, int modelId, int statusId, int productionShopId, int price)
        {
            string query = "UPDATE cars SET brand_id = @BrandId, model_id= @ModelId, status_id= @StatusId, production_shop_id = @ProductionShop, price = @Price WHERE Id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@BrandId", brandId);
                command.Parameters.AddWithValue("@ModelId", modelId);
                command.Parameters.AddWithValue("@StatusId", statusId);
                command.Parameters.AddWithValue("@ProductionShop", productionShopId);
                command.Parameters.AddWithValue("@Price", price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while updating the car info!");
                    Console.Write(ex.ToString());
                }
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM cars WHERE id=@id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while deleting the car info!");
                    Console.Write(ex.ToString());
                }
            }
        }

        public void add(int brandId, int modelId, int statusId, int productionShopId, int price)
        {
            string query = "INSERT INTO cars (brand_id, model_id, status_id, production_shop_id, price) VALUES (@BrandId, @ModelId, @StatusId, @ProductionShop, @Price)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@BrandId", brandId);
                command.Parameters.AddWithValue("@ModelId", modelId);
                command.Parameters.AddWithValue("@StatusId", statusId);
                command.Parameters.AddWithValue("@ProductionShop", productionShopId);
                command.Parameters.AddWithValue("@Price", price);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while inserting a new car!");
                    Console.Write(ex.ToString());
                }
            }
        }
    }
}
