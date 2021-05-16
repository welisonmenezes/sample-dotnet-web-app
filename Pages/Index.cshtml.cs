using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Trilha_Jr_1.Models;

namespace Trilha_Jr_1.Pages
{
    public class IndexModel : PageModel
    {
        public IList<PostViewModel> Posts;
        public IndexModel() 
        {
            Posts = new List<PostViewModel>();
        }

        public void OnGet() 
        {
            try
            {
                SqlConnection con = new("Server=DESKTOP-TP9GB75;Database=Test;Trusted_Connection=True");
                con.Open();
                SqlCommand cmd = new("SELECT * FROM Posts;", con);
                SqlDataReader results = cmd.ExecuteReader();
                while(results.Read())
                {
                    PostViewModel item = new PostViewModel {
                        Title = results["Title"].ToString(),
                        Content = results["Content"].ToString(),
                    };
                    Posts.Add(item);
                }
                con.Close();
            }
            catch (System.Exception e)
            {
                System.Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void OnPost()
        {}
    }
}
