using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace AADB2C.LoadTest.Plugins
{
    public class AuthorizeRequestPlugin : WebTestRequestPlugin
    {
        public override void PreRequestDataBinding(object sender, PreRequestDataBindingEventArgs e)
        {
            // Remove empty state query string parameter
            QueryStringParameter state = null;
            foreach (var item in e.Request.QueryStringParameters)
            {
                if (item.Name.ToLower() =="state" && string.IsNullOrEmpty(item.Value))
                {
                    state = item;
                    break;
                }
            }

            if (state != null)
                e.Request.QueryStringParameters.Remove(state);

            base.PreRequestDataBinding(sender, e);
        }
    }
}
