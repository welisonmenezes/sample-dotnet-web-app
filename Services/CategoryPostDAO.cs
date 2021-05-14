using System.Data.SqlClient;

namespace Trilha_Jr_1.Services
{
    public class CategoryPostDAO
    {
        public CategoryPostDAO CreateTable()
        {
            try
            {
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
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
                    string query = "SELECT TOP(1) CategoryID FROM CategoriesPosts";
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