using System;
using System.Globalization;
using System.Net;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace AADB2C.LoadTest.Plugins
{
    public class ExtractCookieValue : ExtractionRule
    {
        /// <summary>
        /// Gets or sets the cookie name
        /// </summary>
        public string CookieName { get; set; }

        /// <summary>
        /// The Extract method. 
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The parameter e contains the web performance test context. </param>
        public override void Extract(object sender, ExtractionEventArgs e)
        {
            if (string.IsNullOrEmpty(CookieName))
            {
                throw new ArgumentException("Error --> CookieName cannot be null or empty.");
            }

            if (e.Response.Cookies != null)
            {
                Cookie cookie = e.Response.Cookies[CookieName];
                if (cookie != null)
                {
                    e.WebTest.Context[this.ContextParameterName] = cookie.Value;
                    e.Success = true;
                    return;
                }
            }

            // If the extraction fails, set the error text that the user sees  
            e.Success = false;
            e.Message = String.Format(CultureInfo.CurrentCulture, "Cookie Not Found: {0}", CookieName);
        }
    }
}
