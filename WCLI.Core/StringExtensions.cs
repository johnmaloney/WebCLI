using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace WebCLI.Core
{
    public static class StringExtensions
    {
        /// <summary>
        /// Takes an input with the signature of p:password and returns the password only.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitCharacter"></param>
        /// <returns></returns>
        public static string RightSideOf(this string value, string splitCharacter)
        {
            var values = Regex.Split(value, splitCharacter);
            return values.Skip(1).First();
        }

        /// <summary>
        /// Takes an input with the signature of p:password and returns the p value only.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="splitCharacter"></param>
        /// <returns></returns>
        public static string LeftSideOf(this string value, string splitCharacter)
        {
            var values = Regex.Split(value, splitCharacter);
            return values.First();
        }
    }
}
