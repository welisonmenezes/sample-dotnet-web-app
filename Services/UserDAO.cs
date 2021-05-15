using System.Data.SqlClient;
using Trilha_Jr_1.Models;
using Microsoft.Extensions.Configuration;

namespace Trilha_Jr_1.Services
{
    public class UserDAO
    {
        private IConfiguration configuration;
        private string connectionString { get; set; }
        
        public UserDAO(IConfiguration configuration)
        {
            this.configuration = configuration;
            this.connectionString = configuration.GetConnectionString("Default");
        }
        
        public UserDAO CreateTable()
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    string query = @"
                    IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
                        BEGIN
                            CREATE TABLE Users (
                                UserID INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
                                Name VARCHAR(255) NOT NULL,
                                Email VARCHAR(255) UNIQUE NOT NULL,
                                Password VARCHAR(255) NOT NULL,
                                Avatar VARCHAR(255),
                                CreatedAt DATETIME DEFAULT GETDATE() NOT NULL
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

        public UserDAO Add(User user)
        {
            try
            {
                using (SqlConnection con = new(connectionString))
                {
                    con.Open();
                    string query = @"INSERT INTO Users (Name, Email, Password)
                    VALUES ('"+ user.Name +"', '"+ user.Email +"', '"+ user.Password +"');";
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
                    string query = "SELECT TOP(1) UserID FROM Users";
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