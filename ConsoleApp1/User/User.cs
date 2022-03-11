using Newtonsoft.Json;
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
    public static class User
    {
        public static async void GetCurrentUser()
        {
            var currentUser = await SpotifyHttpRequest.Get<UserModel>("https://api.spotify.com/v1/me/");
            Console.WriteLine(currentUser.id);
        }
        public static async void GetUserTopItems()
        {

        }
        public static async void GetUser(string UserId)
        {
            var user = await SpotifyHttpRequest.Get<UserModel>($"https://api.spotify.com/v1/users/{UserId}");
            Console.WriteLine(user.display_name);
        }
        public static async void FollowPlaylist(string PlaylistId)
        {
            var endpoint = $"https://api.spotify.com/v1/playlists/{PlaylistId}/followers";
            await SpotifyHttpRequest.Put(endpoint, new StringContent("public: true"));
        }
        public static async void UnfollowPlaylist(string PlaylistId)
        {
            var endpoint = $"https://api.spotify.com/v1/playlists/{PlaylistId}/followers";
            await SpotifyHttpRequest.Delete(endpoint);
        }
        public static async void GetFollowedArtists()
        {

        }
        public static async void Follow(List<string> ids, string type = "user")
        {
            var endpoint = $"https://api.spotify.com/v1/me/following?type={type}";
            var serializedIds = JsonConvert.SerializeObject(ids);
            var RequestBody = new StringContent(serializedIds);

            await SpotifyHttpRequest.Put(endpoint, RequestBody);
        }
        public static async void Unfollow(List<string> ids, string type = "user")
        {
            var endpoint = $"https://api.spotify.com/v1/me/following?type={type}";
            var serializedIds = JsonConvert.SerializeObject(ids);
            var RequestBody = new StringContent(serializedIds);

            await SpotifyHttpRequest.Delete(endpoint, RequestBody);
        }
        //public static async Task<List<bool>> IsFollowingUser(List<string> ids, string type = "user")
        //{
        //    var endpoint = $"https://api.spotify.com/v1/me/following?type={type}";
        //    var serializedIds = JsonConvert.SerializeObject(ids);
        //    var RequestBody = new StringContent(serializedIds);

        //    await SpotifyHttpRequest.Get(endpoint, RequestBody);
        //}
        public static async void IsFollwingPlaylist()
        {

        }
    }
}
