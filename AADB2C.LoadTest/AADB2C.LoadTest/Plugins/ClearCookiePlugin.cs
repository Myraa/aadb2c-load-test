using Microsoft.VisualStudio.TestTools.WebTesting;

namespace AADB2C.LoadTest.Plugins
{
    /// <summary>
    /// Plugin for clearing the cookie
    /// </summary>
    public class ClearCookiePlugin : WebTestPlugin
    {
        /// <summary>
        /// Handle the event associated with the start of a Web performance test.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">A PreWebTestEventArgs that contains the event data.</param>
        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        {
            e.WebTest.Context.CookieContainer = new System.Net.CookieContainer();
        }
    }
}
