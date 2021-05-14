using System.Data.SqlClient;

namespace Trilha_Jr_1.Services
{
    public class PostDAO
    {
        public PostDAO CreateTable()
        {
            try
            {
                string constr = @"Server=DESKTOP-0KTTMSL;Database=Test;Trusted_Connection=True;";
                using (SqlConnection con = new(constr))
                {
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
                    string query = "SELECT TOP(1) PostID FROM Posts";
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