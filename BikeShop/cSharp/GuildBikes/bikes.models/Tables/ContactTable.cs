using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace bikes.models.Tables
{
    public class ContactTable
    {
        public int ContactId { get; set; }
        public string CntctLastName { get; set; }
        public string CntctFirstName { get; set; }
        public string CntctPhone { get; set; }
        public string CntctEmail { get; set; }
        public string CntctMessage { get; set; }
    }
}
