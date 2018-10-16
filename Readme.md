# Azure AD B2C load testing

This Azure AD B2C sample load testing solution simulates sign-in and sign-up with local account, process, and measures its response. Use the load test sample solution to perform and determine your web application and B2C policy behavior under anticipated peak load conditions. Using this load test helps you identify the maximum operating capacity of your tenant as well as any bottlenecks on your web app, and determine which element is causing degradation. 

## Azure AD B2C Throttling 
Azure AD B2C throttling aims to prevent or limit the amount of resources a single tenant can have on the overall service, so that other tenant’s services and experiences will not be negatively impacted. There is no hard limit around throttling, as the service is dynamic and different circumstances may affect the overall performance of the service. If you reach the throttling Azure AD B2C returns HTTP Status Code 429 error code.



## Load test flow
The visual studio solution contains following Web tests
* (POST)LocalAccount_SignIn.webtest - Simulates local account sign-in with single user
* (POST)LocalAccount_SignUp.webtest - Simulates local account sign-up with random generated users

The sign-in web test runs with single user (user name and password). You can change the load test to sign-in with predefined list of users from external source. Sign-up web test generates users (email address, first name, and last name) with fixed predefined password.

### Sending authorization code back to your web application
After successful sign-up or sign-in, Azure AD B2C sends the resulting authorization code or id_token back to your web app. B2C allows you to configure how the return values send back to your web application, via: Query string, URI fragment (# tag) or via HTTP POST. Set `response_mode` context parameter to define the best value fits your web application.

> [!NOTE]
>The **form_post** response mode is recommended for best security.

* When setting `response_mode` to `query` or `fragment` Azure AD B2C redirects (HTTP 302)  back to your web application. In this scenario, you should run LocalAccount_SignIn.webtest or LocalAccount_SignUp.webtest web tests. Those web tests follow the redirection and generate HTTP request to your web app.
* When setting `response_mode` to `form_post` Azure AD B2C returns HTTP 200 response, without redirection (HTTP 302). The redirect is done on the client side (JavaScript). In load test process, the load test extracts the HTML form values, and calls your web app directly. Use POST_LocalAccount_SignIn and POST_LocalAccount_SignUp web test. 

## Step 1: Prepare your load test environment
* [Download and install Visual Studio Enterprise](https://www.visualstudio.com/downloads/download-visual-studio-vs), if you don't already have it.
* [Create a VSTS account](https://www.visualstudio.com/products/visual-studio-team-services-vs), if you don't have one already.

    >[!NOTE]
    > For more information, see [Load test your app in the cloud using Visual Studio and VSTS](https://docs.microsoft.com/en-us/vsts/load-test/getting-started-with-performance-testing)

## Step 2: Get  and configure the sample load test project
1. Download or clone the solution to your local machine.  
2. Open the **AADB2C.LoadTest** solution
3. Under the **Plugins** folder, open the **AppSettingsPlugin.cs** file
4. Change following setting according you your environment:

    ```C
    public const bool DisableDependentCaching = true;
    const string Host = "login.microsoftonline.com";
    const string TenantId = "<Your tenant name e.g. contoso.onmicrosoft.com>";
    const string ClientId = "<Your client ID>";
    const string PolicyId = "<Policy name you want to test>";
    const string RedirectUri = "<Redirect URL e.g. https%3A%2F%2Fjwt.ms%2F>";
    const string SignInOrSignUpPassword = "<Password for sign-in and sign-up>";
    const string StateParam = "<State parameter, can be empty>";
    const string ResponseMode = "<query|fragment|form_post>";
        
    // Signin web test settings
    const string SignInUser = "<Sign-in user name e.g. loadtest@contoso.com>";

    // Signup web test settings
    public const string SignUpDomain = "<Sign-up domain name e.g. contoso.com>";
    public const string SignUpDisplayNamePrefix = "Test-";
    ```

For more information regarding sending authentication requests and query string parameters, see: [Web sign-in with OpenID Connect](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-reference-oidc)


## Step 3: Custom UI settings
If you customized your Azure AD B2C user interface (UI), you should point the web test to your HTML templates, and add the dependency requests (for sign-in and sign-up web tests). Following screenshots illustrates how to change the URL and add your custom UI elements.

![UI elements](media/UI_Settings.png)

For more information, see:
* [Customize the Azure AD B2C user interface (UI)](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-reference-ui-customization)
* [Configure UI customization in a custom policy](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-ui-customization-custom)

## Step 4: Load test Settings
Configure **LoadTest1.loadtest** with your load test targets, including:
* Text mix between scenarios (sign-in and sign-up)
* Step load pattern 
* Test location
* Browser mix
* Network mix
* And more...

To simulate that the load is being generated through more than one type of network connection and run your test closer to where your users are.  Consider setting the load test **Network mix** and **Location**.

To simulate load more realistically in a load test scenario. 
    ![Network mix](media/network.png)

To run your test closer to where your users are, select a location closer to your users.
    ![Test location](media/location.png)

## Step 5: Disable email verification during sign-up load test
When enabled, Azure Active Directory (Azure AD) B2C gives a consumer the ability to **sign up** for applications by providing an email address and creating a local account. Azure AD B2C ensures valid email addresses by requiring consumers to verify them during the sign-up process. It also prevents a malicious automated process from generating fake accounts for the applications. 

To automate the sign-up process, you need to disable email verification during the sign-up load test. 
* For built-in policy, see [Disable email verification during consumer sign-up](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-reference-disable-ev)
* For custom policies, see [Modify sign up to add new claims and configure user input](https://docs.microsoft.com/en-us/azure/active-directory-b2c/active-directory-b2c-configure-signup-self-asserted-custom#optional-remove-email-verification-from-signup-journey)

## Step 6: Run the load test
* Connect to your VSTS account - Before you can run load tests in the cloud, connect Visual Studio to your VSTS account
* Run and analyze your load test - In Solution Explorer, open the **LoadTest1** load test

    >[!NOTE]
    > * For more information how to run the load test, see [Load test your app in the cloud using Visual Studio and VSTS](https://docs.microsoft.com/en-us/vsts/load-test/getting-started-with-performance-testing)
    > * You can also run your load test locally. In Solution Explorer right click on **Local.testsettings** file and select **Active load and Web Test settings**

## Disclaimer
The sample is developed and managed by the open-source community in GitHub. The solution is not part of Azure AD B2C product and it's not supported under any Microsoft standard support program or service. The sample (Azure AD B2C policy and any companion code) is provided AS IS without warranty of any kind.
