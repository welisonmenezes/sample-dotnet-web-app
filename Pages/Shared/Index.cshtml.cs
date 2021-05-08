using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Security;
using Microsoft.AspNetCore.Mvc;

public class IndexModel : PageModel
{
    [BindProperty] public string RequestVerb { get; set; }
    public string Logins { get; set; }

    public IndexModel()
    {
        
    }

    [IsLoggedIn()]
    public void OnGet(string action, int value)
    {
        string sexo = Request.Cookies["cookie-key"];
        System.Console.WriteLine("sexo");
    }

}