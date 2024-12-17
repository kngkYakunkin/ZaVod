using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ZaVod.Repositories;
using ZaVod.Models;
using System.Windows;

namespace ZaVod.Repositories
{
    internal class ModelRepository : Repository
    {
        public ModelRepository()
        {

        }

        public List<Model> getAll()
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM models";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    //command.Parameters.AddWithValue("@Login", login);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Model> models = new List<Model>();

                        while (reader.Read())
                        {

                            Model model = new Model
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                            };

                            // Add the row to the list
                            models.Add(model);
                        }

                        return models;
                    }
                }
            }
        }

        public Model getModelById(int id)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM models WHERE id = @id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {

                        if (reader.Read())
                        {

                            Model model = new Model
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                            };

                            // Add the row to the list
                            return model;
                        }

                        return new Model();
                    }
                }
            }
        }

        public List<Model> getAllByBrandId(int brandId)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM models WHERE brand_id=@BrandId";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@BrandId", brandId);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Model> models = new List<Model>();

                        while (reader.Read())
                        {

                            Model model = new Model
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                                BrandId = Convert.ToInt32(reader["brand_id"].ToString()),
                            };

                            // Add the row to the list
                            models.Add(model);
                        }

                        return models;
                    }
                }
            }
        }

        public int getIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM models WHERE name = @Name";
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


        public void update(int id, string name, int brandId)
        {
            string query = "UPDATE models SET name= @Name, brand_id = @BrandId WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@BrandId", brandId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error while updating the model info!");
                    Console.Write(ex.ToString());
                }
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM models WHERE id=@id";

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

        public void add(string name, int brandId)
        {
            string query = "INSERT INTO models (name, brand_id) VALUES (@Name, @BrandId)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);
                command.Parameters.AddWithValue("@BrandId", brandId);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while inserting a new model!");
                    Console.Write(ex.ToString());
                }
            }
        }

    }
}
