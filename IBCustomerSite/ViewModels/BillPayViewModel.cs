using System;
using System.Collections.Generic;
using IBCustomerSite.Models;

namespace IBCustomerSite.ViewModels
{
    public class BillPayViewModel
    {

        public List<BillPay> BillPays { get; set; }

        public string PeriodNames(char period)
        {
            switch (period)
            {
                case 'M':
                    return "Monthly";
                case 'Q':
                    return "Quarterly";
                case 'Y':
                    return "Yearly";
                case 'O':
                    return "One off";
                default:
                    return "";
            }
        }
    }
}
