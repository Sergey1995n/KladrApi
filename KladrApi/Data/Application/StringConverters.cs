using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace KladrApi
{
    public static class StringConverters
    {

        public static Int32 ToInt32(this string str)
        {

            int i;

            if (String.IsNullOrEmpty(str))
                return 0;

            if (Int32.TryParse(str, out i))
                return i;
            else
                return 0;

        }

        public static bool ToBool(this string str)
        {
            string val = (str ?? "").ToLower().Trim();
            return val == "true" || val == "1";
        }
                

        public static string ReplaceText(this string str, string find, string val)
        {

            if (val!="") {
                string result = Regex.Replace(str, find, val, RegexOptions.IgnoreCase);
                return result;
            }

            return str;
        }



    }



}
