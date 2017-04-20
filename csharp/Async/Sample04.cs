using System;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncSamples
{
    class Sample04
    {
        /// <summary>
        /// This is a bad "fire and foget" example.
        /// </summary>
        public static void Bad_FireAndNeverGetResult()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.GetAsync("http://www.microsoft.com");
        }

        /// <summary>
        /// 
        /// </summary>
        /// <remarks>
        /// "In particular, it’s usually a bad idea to block on async code by calling Task.Wait or Task.Result. "
        /// https://msdn.microsoft.com/en-us/magazine/jj991977.aspx
        /// </remarks>
        public static void Bad_()
        {
            HttpClient httpClient = new HttpClient();
            var httpResponseMessage = httpClient.GetAsync("http://www.microsoft.com").Result;
            Debug.Assert(httpResponseMessage.IsSuccessStatusCode);
        }
    }
}
