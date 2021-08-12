using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace IBCustomerSite.ViewModels
{
    public class ChartViewModel
    {
        public List<decimal> DailyTotalsA1 { get; set; }
        public List<decimal> DailyTotalsA2 { get; set; }
        public List<string> Dates { get; set; }

        public List<decimal> AccountBalances { get; set; }
        public List<string> AccountNames { get; set; }

    }
}
