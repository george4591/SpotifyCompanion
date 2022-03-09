using SpotifyCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public static class User
    {
        private static UserModel _userModel;

        public static async void GetCurrentUser()
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.GetAsync(new Uri("https://api.spotify.com/v1/me/")))
            {
                if (Response.IsSuccessStatusCode)
                {
                    _userModel = await Response.Content.ReadAsAsync<UserModel>();

                    Console.WriteLine(_userModel.display_name);
                }
                else
                {
                    var error = await Response.Content.ReadAsStringAsync();

                    Console.WriteLine(error);
                    throw new HttpRequestException(Response.ReasonPhrase);
                }
            }
        }
    }
}
