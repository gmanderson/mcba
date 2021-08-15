using System;

namespace IBCustomerSite.ViewModels
{
    public class PasswordViewModel
    {
        public int CustomerID { get; set; }
        public string RawPassword { get; set; }
        public string RawPasswordRepeat { get; set; }
        public string OldPassword { get; set; }


    }
}
