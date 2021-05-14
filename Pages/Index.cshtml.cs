using Microsoft.AspNetCore.Mvc.RazorPages;
using Trilha_Jr_1.Services;

namespace Trilha_Jr_1.Pages
{
    public class IndexModel : PageModel
    {
        private UserDAO userDAO;
        public IndexModel(UserDAO userDAO) 
        {
            this.userDAO = userDAO;
        }

        public void OnGet() 
        {
            if (!userDAO.HasAny()) Response.Redirect("Setup");
        }

        public void OnPost()
        {}
    }
}
