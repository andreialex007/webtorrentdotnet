using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utils
{
    public static class StringUtils
    {
        public static string UppercaseFirst(this string input)
        {
            return string.IsNullOrEmpty(input)
                ? string.Empty
                : char.ToUpper(input[0]) + input.Substring(1);
        }
    }
}
