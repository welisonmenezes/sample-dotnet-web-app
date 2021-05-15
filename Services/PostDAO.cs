using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Trilha_Jr_1.Services
{
    public class PostDAO
    {
        private IConfiguration configuration;
        private string connectionString { get; set; }

        public PostDAO(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("Default");
        }

        public PostDAO CreateTable()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    string query = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Posts' AND xtype='U')
                        BEGIN
                            CREATE TABLE Posts (
                                PostID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
                                Title VARCHAR(255) NOT NULL,
                                Content TEXT NOT NULL,
                                IsActive BIT DEFAULT 0,
                                Image VARCHAR(255),
                                CreatedAt DATETIME DEFAULT GETDATE() NOT NULL,
                                UserID INT FOREIGN KEY REFERENCES Users(UserID)
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
                    string query = "SELECT TOP(1) PostID FROM Posts";
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