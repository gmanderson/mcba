using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using IBCustomerSite.Models;

namespace IBCustomerSite.Filters
{
    public class AuthorizeCustomerAttribute : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var customerID = context.HttpContext.Session.GetInt32(nameof(Customer.CustomerID));
            if (!customerID.HasValue)
                context.Result = new RedirectToActionResult("Login", "Login", null);
        }
    }
}
