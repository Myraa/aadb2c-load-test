using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.WebTesting;

namespace AADB2C.LoadTest.Plugins
{
    public class AppSettingsPlugin : WebTestPlugin
    {
        public const bool DisableDependentCaching = true;

 
        const string ClientId = "e766b17d-50b7-433f-a0dd-308b55a57189";
        const string PolicyId = "B2C_1A_signup_signin";
        const string RedirectUri = "https%3A%2F%2Fjwt.ms%2F";
        const string TenantId = "your-tenant.onmicrosoft.com";
        const string Host = "login.microsoftonline.com";
        const string SignInOrSignUpPassword = "1234";
        const string StateParam = "";
        const string ResponseMode = "fragment"; //query|fragment|form_post

        //Signin web test settings
        const string SignInUser = "loadtest@contoso.com";


        // Signup web test settings
        public const string SignUpDomain = "contoso.com";
        public const string SignUpDisplayNamePrefix = "Test-";

        public override void PreWebTest(object sender, PreWebTestEventArgs e)
        {
            e.WebTest.Context["ClientId"] = ClientId;
            e.WebTest.Context["PolicyId"] = PolicyId;
            e.WebTest.Context["RedirectUri"] = RedirectUri;
            e.WebTest.Context["TenantId"] = TenantId;
            e.WebTest.Context["Host"] = Host;
            e.WebTest.Context["Password"] = SignInOrSignUpPassword;
            e.WebTest.Context["State"] = StateParam;
            e.WebTest.Context["ResponseMode"] = ResponseMode;

            // Signin web test only
            if (e.WebTest.Name.ToLower().Contains("signin"))
            {
                e.WebTest.Context["Username"] = SignInUser;
            }
            base.PreWebTest(sender, e);
        }
    }
}
