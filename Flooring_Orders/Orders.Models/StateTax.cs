using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.Models
{
    public class StateTax
    {
        public string StateAbrev { get; set; }
        public string StateName { get; set; }
        public decimal Tax { get; set; }
    }
}
