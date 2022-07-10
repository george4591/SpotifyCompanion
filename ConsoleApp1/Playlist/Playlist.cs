using SpotifyCompanion.Models;
using SpotifyCompanion.Utils.Http;
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
        public static async void GetPlaylist()
        {

        }

        public static async void ChangePlaylistDetails()
        {

        }

        public static async void GetPlaylistItems()
        {

        }

        public static async void AddItemsToPlaylist()
        {

        }

        public static async void UpdatePlaylistItems()
        {

        }

        public static async void RemovePlaylistItems()
        {

        }
        public static async void GetCurrentUserPlaylists()
        {
            var playlists = await HttpRequest.Get<PlaylistArrayModel>("https://api.spotify.com/v1/me/playlists");

            foreach(var playlist in playlists.items)
            {
                Console.WriteLine(playlist.name);
                
            }
        }
        public static async void GetUserPlaylist()
        {

        }
        public static async void CreatePlaylist()
        {

        }
        public static async void GetFeaturedPlaylist()
        {

        }
        public static async void GetCategoryPlaylist()
        {

        }
        public static async void GetPlaylistCoverImage()
        {

        }
        public static async void AddCustomPlaylistCoverImage()
        {

        }
    }
}
