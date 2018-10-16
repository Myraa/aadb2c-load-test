using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.WebTesting;
using System.Web;

namespace AADB2C.LoadTest.Plugins
{
    public class UrlDecodePlugIn: WebTestRequestPlugin
    {
        public override void PreRequest(object sender, PreRequestEventArgs e)
        {
            e.Request.Url = HttpUtility.UrlDecode(e.Request.Url);
            base.PreRequest(sender, e);
        }
    }
}
