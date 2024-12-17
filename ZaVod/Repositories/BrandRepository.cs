using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZaVod.Models;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Windows.Media.Media3D;
using System.Windows;

namespace ZaVod.Repositories
{
    internal class BrandRepository : Repository
    {
        public BrandRepository()
        {

        }

        public List<Brand> getAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM brands";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Brand> brands = new List<Brand>();

                        while (reader.Read())
                        {

                            Brand brand = new Brand
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Name = reader["name"].ToString(),
                            };

                            brands.Add(brand);
                        }

                        return brands;
                    }
                }
            }
        }

        public string getNameById(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT name FROM brands WHERE id= @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);

                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            return reader["name"].ToString();

                        }

                    }
                }
            }

            return "unknown";
        }

        public int getIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM Brands WHERE name = @Name";
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

        public void update(int id, string name)
        {
            string query = "UPDATE brands SET name= @Name WHERE id = @Id";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                command.Parameters.AddWithValue("@Name", name);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show("Error while updating the brand info!");
                    Console.Write(ex.ToString());
                }
            }
        }

        public void delete(int id)
        {
            string query = "DELETE FROM brands WHERE id=@id";

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
        public void add(string name)
        {
            string query = "INSERT INTO brands (name) VALUES (@Name)";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Name", name);

                try
                {
                    connection.Open();
                    int rowsAffected = command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error while inserting a new brand!");
                    Console.Write(ex.ToString());
                }
            }
        }
    }
}
