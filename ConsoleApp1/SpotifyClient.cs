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
        public static HttpClient Client { get; set; } = new HttpClient();
        private readonly LocalStorage storage = new LocalStorage();
        private readonly string ClientId;
        private readonly string ClientSecret;

        public SpotifyClient(string clientId, string clientSecret)
        {
            ClientId = clientId;
            ClientSecret = clientSecret;

            Client.BaseAddress = new Uri(AppDetails.BaseAdress);
            Client.DefaultRequestHeaders.Accept.Clear();
            Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            string credentials = Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}"));
            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", credentials);
        }
        
        public async Task Initialize()
        {
            if (!File.Exists(".localstorage"))
            {
                var LoginInfo = OAuth2.Authorize(ClientId, ClientSecret);
                Console.WriteLine(LoginInfo.access_token);

                storage.Store<string>("access_token", LoginInfo.access_token);
                storage.Store<string>("token_type", LoginInfo.token_type);
                storage.Store<string>("refresh_token", LoginInfo.refresh_token);
                storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(LoginInfo.expires_in)));

                storage.Persist();
                Console.WriteLine("Succesfully wrote to .localstorage");
            }
            else
            {
                await RefreshTokenIfNeeded();
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", storage.Get<string>("access_token"));
        }

        public void Run()
        {
            bool appIsRunning = true;
            while (appIsRunning) 
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        appIsRunning = false; 
                        break;
                    case ConsoleKey.F6:
                        User.FollowPlaylist("7wJufnewhs4Ue8MpeJ7hIe");
                        break;
                    case ConsoleKey.F7:
                        Player.GetRecentlyPlayedTracks();
                        break;
                }
            }
        }

        private void RefreshToken()
        {
            Console.WriteLine("The token is expired. Requesting new token...");

            var refrToken = storage.Get<string>("refresh_token");
            var refreshInfo = await OAuth2.RefreshToken(refrToken);
            Console.WriteLine($"\nThe new token is: {refreshInfo.access_token}");

            WriteTokenToFile(refreshInfo.access_token);
        }

        private void WriteTokenToFile(string accessToken)
        {
            storage.Store("access_token", accessToken);
            storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(3600)));
            storage.Persist();
        }

        private async Task RefreshTokenIfNeeded()
        {
            var timeWhenTokenExpires = storage.Get<DateTime>("expires_at");
            Console.WriteLine($"Time when current token expires: {timeWhenTokenExpires}");

            if (DateTime.Compare(DateTime.Now, timeWhenTokenExpires) > 0)
            {
                RefreshToken();
            }
            else
            {
                Console.WriteLine("The token is not expired yet");
            }
        }
        
    }
}

