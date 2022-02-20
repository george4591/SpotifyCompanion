using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public static class SpotifyClient
    {
        public static HttpClient Client { get; set; }
        
        public static void Initialize()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(AppDetails.BaseAdress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{AppDetails.ClientId}:{AppDetails.ClientSecret}"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var LoginInfo = OAuth2.Authorize();
            Console.WriteLine(LoginInfo.access_token);
        }
    }
}
