using SpotifyCompanion.Models;
using SpotifyCompanion.Utils;
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
            _userModel = await HttpRequest.Get<UserModel>("https://api.spotify.com/v1/me/");
            Console.WriteLine(_userModel.display_name);
        }
    }
}
