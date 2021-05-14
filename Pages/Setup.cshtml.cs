using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Trilha_Jr_1.Models;
using Trilha_Jr_1.Services;

namespace Trilha_Jr_1.Pages
{
    public class SetupModel : PageModel
    {
        private UserDAO userDAO;
        private PostDAO postDAO;
        private CategoryDAO categoryDAO;
        private CategoryPostDAO categoryPostDAO;

        [BindProperty] public string email { get; set; }
        [BindProperty] public string name { get; set; }
        [BindProperty] public string password { get; set; }

        public SetupModel(UserDAO userDAO, PostDAO postDAO, CategoryDAO categoryDAO, CategoryPostDAO categoryPostDAO) 
        {
            this.userDAO = userDAO;
            this.postDAO = postDAO;
            this.categoryDAO = categoryDAO;
            this.categoryPostDAO = categoryPostDAO;
        }

        public void OnGet() 
        {}

        public void OnPost()
        {
            if (!userDAO.HasAny()) 
            {
                User user = new User
                {
                    Name = name,
                    Email = email,
                    Password = password
                };

                userDAO.CreateTable().Add(user);
                postDAO.CreateTable();
                categoryDAO.CreateTable();
                categoryPostDAO.CreateTable();
            }
        }
    }
}
