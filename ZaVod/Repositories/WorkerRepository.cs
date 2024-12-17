using System;
using System.Data.SqlClient;
using ZaVod.Models;

namespace ZaVod.Repositories
{
    public class WorkerRepository : Repository
    {
        public WorkerRepository()
        {

        }

        public Worker GetWorker(string login)
        {
            Worker worker = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                connection.Open();
                string query = "SELECT * FROM Workers WHERE login = @Login";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Login", login);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            worker = new Worker
                            {
                                Login = reader["login"].ToString(),
                                Password = reader["password"].ToString(),
                                Firstname = reader["firstname"].ToString(),
                                Fathername = reader["fathername"].ToString(),
                                Lastname = reader["lastname"].ToString(),
                                Phone = reader["phone"].ToString(),
                                RoleId = Convert.ToInt32(reader["role_id"].ToString()),

                            };
                        }
                    }
                }
            }

            return worker;
        }
    }
}