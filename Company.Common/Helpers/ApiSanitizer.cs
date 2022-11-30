using Ganss.Xss;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Company.Common.Helpers
{
    public static class ApiSanitizer
    {
        /// <summary>
        /// HtmlSanitizer is a .NET library for cleaning HTML fragments and documents from constructs that can 
        /// lead to XSS attacks. It uses AngleSharp to parse, manipulate, and render HTML and CSS.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public static string Sanitizer(string request)
        {
            var sanitizer = new HtmlSanitizer();

            return sanitizer.Sanitize(request);
        }
    }
}
