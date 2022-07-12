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
        private readonly LocalStorage _storage = new LocalStorage();
        private readonly string _clientId;
        private readonly string _clientSecret;

        public SpotifyClient(string clientId, string clientSecret)
        {
            _clientId = clientId;
            _clientSecret = clientSecret;

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
                var loginInfo = OAuth2.Authorize(_clientId, _clientSecret);

                _storage.Store<string>("access_token", loginInfo.access_token);
                _storage.Store<string>("token_type", loginInfo.token_type);
                _storage.Store<string>("refresh_token", loginInfo.refresh_token);
                _storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(loginInfo.expires_in)));

                _storage.Persist();
                Console.WriteLine("You are now logged in!");
            }
            else
            {
                await RefreshTokenIfNeeded();
            }

            Client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _storage.Get<string>("access_token"));
        }

        public async Task Run()
        {
            bool isRunning = true;
            while (isRunning)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.Escape:
                        isRunning = false;
                        break;
                    case ConsoleKey.F3:
                        Player.AddItemToPlaybackQueue("spotify:track:4iV5W9uYEdYUVa79Axb7Rh");
                        break;
                    case ConsoleKey.F6:
                        User.FollowPlaylist("7wJufnewhs4Ue8MpeJ7hIe");
                        break;
                    case ConsoleKey.F7:
                        var currrentSong = await Player.GetCurrentlyPlayingTrack();
                        Console.WriteLine(currrentSong.item.name);
                        break;
                }
            }
        }

        private async Task RefreshToken()
        {
            Console.WriteLine("The token is expired. Refreshing...");

            var refrToken = _storage.Get<string>("refresh_token");
            var refreshInfo = await OAuth2.RefreshToken(refrToken);
            Console.WriteLine($"\nToken Refreshed!");

            StoreToken(refreshInfo.access_token);
        }

        private void StoreToken(string accessToken)
        {
            _storage.Store("access_token", accessToken);
            _storage.Store<DateTime>("expires_at", DateTime.Now.Add(TimeSpan.FromSeconds(3600)));
            _storage.Persist();
        }

        private async Task RefreshTokenIfNeeded()
        {
            var tokenExpirationTime = _storage.Get<DateTime>("expires_at");

            if (DateTime.Compare(DateTime.Now, tokenExpirationTime) > 0)
            {
                await RefreshToken();
            }
            else
            {
                Console.WriteLine($"The current token will expire on: {tokenExpirationTime}");
            }
        }

    }
}

