using System.Data.SqlClient;
using Trilha_Jr_1.Models;

namespace Trilha_Jr_1.Services
{
    public class UserDAO
    {
        public UserDAO CreateTable()
        {
            try
            {
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
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
                    con.Open();
                    using (SqlCommand command = new SqlCommand(constr, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
                    string query = @"INSERT INTO Users (Name, Email, Password)
                    VALUES ('"+ user.Name +"', '"+ user.Email +"', '"+ user.Password +"');";
                    using SqlCommand cmd = new(query, con);
                    cmd.Connection = con;
                    con.Open();
                    using (SqlCommand command = new SqlCommand(constr, con))
                    {
                        cmd.ExecuteNonQuery();
                    }
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
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
                    bool hasOcurrency = false;
                    string query = "SELECT TOP(1) UserID FROM Users";
                    using SqlCommand cmd = new(query, con);
                    cmd.Connection = con;
                    con.Open();
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