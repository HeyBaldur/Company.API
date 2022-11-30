using Company.Models.v1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Models.Helpers
{
    public class DfcParams
    {
        public string[] ids { get; set; }
        public string uid { get; set; }
        public double? N { get; set; }
        public DiscountedCashFlow? cashFlow { get; set; }
    }
}
