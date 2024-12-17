using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using ZaVod.Models;

namespace ZaVod.Repositories
{
    internal class StatusRepository : Repository
    {
        public List<Status> getAll()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM statuses";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        List<Status> statuses = new List<Status>();

                        while (reader.Read())
                        {

                            Status status = new Status
                            {
                                Id = Convert.ToInt32(reader["id"].ToString()),
                                Value = reader["value"].ToString(),
                            };

                            statuses.Add(status);
                        }

                        return statuses;
                    }
                }
            }
        }
        public int getIdByName(string name)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT id FROM statuses WHERE value = @Name";
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
