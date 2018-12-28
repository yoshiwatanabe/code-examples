using System;
using System.Net.Http;
using System.Net.Http.Headers;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace aad_client_console
{
    class Program
    {
        static void Main(string[] args)
        {
            var authContext = new AuthenticationContext("https://login.microsoftonline.com/tsuyoshiwatanabehotmail.onmicrosoft.com");

            // var authResult =
            //     authContext.AcquireTokenAsync(
            //         "https://graph.microsoft.com/", // the resource we like to get to is graph api
            //         "b0905193-7b35-4415-8029-2569d9d1637a", // a native application object in the aad tenant
            //         new Uri("https://tsuyoshiwatanabehotmail.onmicrosoft.com/my-aad-client-console"), // one of the redirect URIs defined in the native application object
            //         new PlatformParameters(PromptBehavior.Always)).Result;

            var codeResult = authContext.AcquireDeviceCodeAsync("https://graph.microsoft.com/", "b0905193-7b35-4415-8029-2569d9d1637a").Result;
                Console.ResetColor();
                Console.WriteLine("You need to sign in.");
                Console.WriteLine("Message: " + codeResult.Message + "\n");

            var authResult = authContext.AcquireTokenByDeviceCodeAsync(codeResult).Result;

            Console.WriteLine("ID token: " + authResult.IdToken);
            Console.WriteLine("Access token: " + authResult.AccessToken);
                       
            var request = new HttpRequestMessage(HttpMethod.Get, $"https://graph.microsoft.com/v1.0/users?$filter=givenName eq 'Tsuyoshi'");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", authResult.AccessToken);
            
            var client = new HttpClient();
            var response = client.SendAsync(request).Result;

            Console.WriteLine("User: " + response.Content.ReadAsStringAsync().Result);
        }
    }
}
