using System.Data.SqlClient;

namespace Trilha_Jr_1.Services
{
    public class CategoryDAO
    {
        public CategoryDAO CreateTable()
        {
            try
            {
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
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
                    string query = "SELECT TOP(1) CategoryID FROM Categories";
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