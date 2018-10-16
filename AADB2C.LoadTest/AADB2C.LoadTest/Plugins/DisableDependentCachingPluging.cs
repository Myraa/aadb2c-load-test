using Microsoft.VisualStudio.TestTools.WebTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AADB2C.LoadTest.Plugins
{
    public class DisableDependentCachingPluging : WebTestPlugin
    {
        public override void PostRequest(object sender, PostRequestEventArgs e)
        {
            if (AppSettingsPlugin.DisableDependentCaching)
            {
                foreach (WebTestRequest dependentRequest in e.Request.DependentRequests)
                {
                    dependentRequest.Cache = false;
                }
            }
            base.PostRequest(sender, e);
        }
    }
}
