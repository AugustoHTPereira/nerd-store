using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NSE.Web.MVC.Helpers
{
    public static class RazorHelpers
    {
        public static string ToCurrency(this decimal value)
        {
            return value == 0 ? "Free" : value.ToString("C");
        }
    }
}
