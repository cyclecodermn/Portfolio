using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Orders.UI
{
    public class DateTimeCustom
    {
        static string getCrentDateMMDDYY()
        {
            DateTime crntDate = DateTime.Now;
            string dateMMddYYY = crntDate.ToString("MMddyyyy");
            return dateMMddYYY;
        }
        static DateTime strToMMddyyyy(string strMMddyyyy)
        {

            int intMM = int.Parse(strMMddyyyy.Substring(0, 2));
            int intDD = int.Parse(strMMddyyyy.Substring(2, 2));
            int intYYYY = int.Parse(strMMddyyyy.Substring(4, 4));

            DateTime newDate = new DateTime(intYYYY, intMM, intDD);
            newDate = newDate.Date;

            return newDate;
        }
    }
}
