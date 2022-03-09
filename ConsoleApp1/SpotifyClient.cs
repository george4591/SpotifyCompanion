using Hanssens.Net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public class SpotifyClient
    {
        public static HttpClient Client { get; set; }
        private LocalStorage storage = new LocalStorage();
        public async Task Initialize()
        {
            Client = new HttpClient();
            Client.BaseAddress = new Uri(AppDetails.BaseAdress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{AppDetails.ClientId}:{AppDetails.ClientSecret}"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            if (!File.Exists(".localstorage"))
            {
                var LoginInfo = OAuth2.Authorize();
                Console.WriteLine(LoginInfo.access_token);

                storage.Store<string>("access_token", LoginInfo.access_token);
                storage.Store<string>("token_type", LoginInfo.token_type);
                storage.Store<string>("refresh_token", LoginInfo.refresh_token);
                storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(LoginInfo.expires_in)));

                storage.Persist();
                Console.WriteLine("Succesfully wrote to .localstorage");

                storage.Clear();
            }
            else
            {
               await RefreshTokenIfNeeded();
            }
        }

        public void Run()
        {
            bool running = true;
            while(running)
            {
                switch(Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        running = false; break;
                }
            }
        }

        private async Task RefreshTokenIfNeeded()
        {
            var timeWhenTokenExpires = storage.Get<DateTime>("expires_at");
            Console.WriteLine($"Time when current token expires: {timeWhenTokenExpires}");

            if (DateTime.Compare(DateTime.Now, timeWhenTokenExpires) > 0)
            {
                Console.WriteLine("The token is expired. Requesting new token...");

                var oldToken = storage.Get<string>("access_token");
                Console.WriteLine($"The old token is: {oldToken}");

                var refrToken = storage.Get<string>("refresh_token");
                var refreshInfo = await OAuth2.RefreshToken(refrToken);
                Console.WriteLine($"\nThe new token is: {refreshInfo.access_token}");

                storage.Store("access_token", refreshInfo.access_token);
                storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(refreshInfo.expires_in)));

                storage.Persist();
                storage.Clear();
            }
            else
            {
                Console.WriteLine("The token is not expired yet");
            }
        }
    }
}
