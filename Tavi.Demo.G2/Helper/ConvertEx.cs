using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tavi.Demo.G2.Helper
{
   public static  class ConvertEx
    {
        public static DateTime ToDate(string inputValue)
        {
            DateTime dtmReturnValue = new DateTime(1900, 1, 1);
            if (!string.IsNullOrEmpty(inputValue))
            {
                DateTime.TryParse(ddmmyyyy_to_mmddyyyy(inputValue), out dtmReturnValue);
            }
            return dtmReturnValue;
        }
        public static string ddmmyyyy_to_mmddyyyy(string strValue)
        {
            if (strValue != "")
            {
                string[] strArray = strValue.Split(new char[] { '/' });
                string d = strArray[0];
                string m = strArray[1];
                string y = strArray[2];
                return (y + "/" + m + "/" + d);
            }
            return "";
        }
    }
}
