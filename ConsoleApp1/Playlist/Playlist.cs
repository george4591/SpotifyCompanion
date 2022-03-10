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
    public static class Playlist
    {
        public static async void GetCurrentUserPlaylists()
        {
            var playlists = await HttpRequest.Get<PlaylistArrayModel>("https://api.spotify.com/v1/me/playlists");
            //using (HttpResponseMessage Response = await SpotifyClient.Client.GetAsync(new Uri("https://api.spotify.com/v1/me/playlists")))
            //{
            //    if (Response.IsSuccessStatusCode)
            //    {
            //        var a = await Response.Content.ReadAsStringAsync();
            //        Console.WriteLine(a);
            //    }
            //    else
            //    {
            //        var error = await Response.Content.ReadAsStringAsync();

            //        Console.WriteLine(error);
            //        throw new HttpRequestException(Response.ReasonPhrase);
            //    }
            //}
            foreach(var playlist in playlists.items)
            {
                Console.WriteLine(playlist.name);
                
            }
        }
    }
}
