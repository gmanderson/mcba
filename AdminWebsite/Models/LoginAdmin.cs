using System;
using System.ComponentModel.DataAnnotations;

namespace AdminWebsite.Models
{
    public class LoginAdmin
    {
        [Display(Name = "Username")]
        public string username { get; set; }
        public string password { get; set; }
    }
}
