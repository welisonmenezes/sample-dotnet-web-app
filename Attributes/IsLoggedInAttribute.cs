using System.Net.Cache;
using System.Net;
using System;
using Microsoft.AspNetCore.Http;

[AttributeUsage(AttributeTargets.Method)]
public sealed class IsLoggedInAttribute : Attribute
{
    public string IsLoggedIn { get; set; } = "true";

    public IsLoggedInAttribute(string theCookie = null)
    {
        //System.Console.WriteLine(Request.Cookie["cookie-key"]);

        if (string.IsNullOrEmpty(theCookie)) {
            System.Console.WriteLine("Faça o login");
        } else {
            System.Console.WriteLine("Logado!");
        }
    }
}