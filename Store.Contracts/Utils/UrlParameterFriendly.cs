using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace System
{
    public class UrlParameterFriendly
    {
        public static string FriendlyName(string param)
        {
            if (param != null)
            {
                param = param.Replace(' ', '-');
                param = param.Replace('.', '-');
                param = param.Replace('Ą', 'A');
                param = param.Replace('Ć', 'C');
                param = param.Replace('Ę', 'E');
                param = param.Replace('Ó', 'O');
                param = param.Replace('Ń', 'N');
                param = param.Replace('Ł', 'L');
                param = param.Replace('Ź', 'Z');
                param = param.Replace('Ż', 'Z');
                param = param.Replace('Ś', 'S');
                param = param.Replace('ą', 'a');
                param = param.Replace('ć', 'c');
                param = param.Replace('ę', 'e');
                param = param.Replace('ó', 'o');
                param = param.Replace('ń', 'n');
                param = param.Replace('ł', 'l');
                param = param.Replace('ź', 'z');
                param = param.Replace('ż', 'z');
                param = param.Replace('ś', 's');
            }
            else {
                param = string.Empty;
            }
            return param;
        }

        public static bool StringEquals(string first, string second)
        {
            var result = false;

            if (first != null && second != null)
            {
                result = FriendlyName(first).ToLower() == FriendlyName(second).ToLower();
            }

            return result;
        }

    }
}
