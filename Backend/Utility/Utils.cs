using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Backend.Utility
{
    public static class Utils
    {
        public static string IntPlace2(int number)
        {
            if (number < 10)
                return number.ToString() + " ";
            else
                return number.ToString();
        }
    }
}
