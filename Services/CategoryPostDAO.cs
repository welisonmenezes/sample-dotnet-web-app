using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Trilha_Jr_1.Services
{
    public class CategoryPostDAO
    {
        private IConfiguration configuration;
        private string connectionString { get; set; }

        public CategoryPostDAO(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public CategoryPostDAO CreateTable()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    string query = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='CategoriesPosts' AND xtype='U')
                        BEGIN
                            CREATE TABLE CategoriesPosts (
                                PostID INT FOREIGN KEY REFERENCES Posts(PostID) ON DELETE CASCADE,
                                CategoryID INT FOREIGN KEY REFERENCES Categories(CategoryID)
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
                    string query = "SELECT TOP(1) CategoryID FROM CategoriesPosts";
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