using System;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace aad_client_console_net47
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("started");

            var authContext = new AuthenticationContext("https://login.microsoftonline.com/tsuyoshiwatanabehotmail.onmicrosoft.com");

            var authResult =
                authContext.AcquireTokenAsync(
                    "https://graph.microsoft.com/", // the resource we like to get to is graph api
                    "b0905193-7b35-4415-8029-2569d9d1637a", // a native application object in the aad tenant
                    new Uri("https://tsuyoshiwatanabehotmail.onmicrosoft.com/my-aad-client-console"), // one of the redirect URIs defined in the native application object
                    new PlatformParameters(PromptBehavior.Always)).Result;

            Console.Write(authResult.AccessToken);
        }
    }
}
