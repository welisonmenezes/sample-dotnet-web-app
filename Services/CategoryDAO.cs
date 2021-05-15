using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Trilha_Jr_1.Services
{
    public class CategoryDAO
    {
        private IConfiguration configuration;
        private string connectionString { get; set; }

        public CategoryDAO(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public CategoryDAO CreateTable()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    string query = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Categories' AND xtype='U')
                        BEGIN
                            CREATE TABLE Categories (
                                CategoryID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
                                Name VARCHAR(255) NOT NULL
                            );
                        END";
                    using SqlCommand cmd = new(query, con);
                    cmd.Connection = con;
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                return this;
            }
            catch (System.Exception)
            {
                throw;
            }
        }

        public bool HasAny()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    bool hasOcurrency = false;
                    string query = "SELECT TOP(1) CategoryID FROM Categories";
                    using SqlCommand cmd = new(query, con);
                    cmd.Connection = con;
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        hasOcurrency = sdr.Read();
                    }
                    con.Close();
                    return hasOcurrency;
                }
            }
            catch (System.Exception)
            {
                return false;
            }
        }

    }
}