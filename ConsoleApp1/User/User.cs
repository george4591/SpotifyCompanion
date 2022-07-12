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
        private static readonly string _entrypoint = "https://api.spotify.com/v1";
        public static async void GetCurrentUser()
        {
            var currentUser = await HttpRequest.Get<UserModel>($"{_entrypoint}/me/");
            Console.WriteLine(currentUser.id);
        }
        public static async void GetUserTopItems()
        {

        }
        public static async void GetUser(string userId)
        {
            var user = await HttpRequest.Get<UserModel>($"{_entrypoint}/users/{userId}");
            Console.WriteLine(user.display_name);
        }
        public static async void FollowPlaylist(string playlistId)
        {
            var endpoint = $"{_entrypoint}/playlists/{playlistId}/followers";
            await HttpRequest.Put(endpoint, new StringContent("public: true"));
        }
        public static async void UnfollowPlaylist(string PlaylistId)
        {
            var endpoint = $"{_entrypoint}/playlists/{PlaylistId}/followers";
            await HttpRequest.Delete(endpoint);
        }
        public static async void GetFollowedArtists()
        {

        }
        public static async void Follow(List<string> ids, string type = "user")
        {
            var endpoint = $"{_entrypoint}/me/following?type={type}";
            var serializedIds = JsonConvert.SerializeObject(ids);
            var requestBody = new StringContent(serializedIds);

            await HttpRequest.Put(endpoint, requestBody);
        }
        public static async void Unfollow(List<string> ids, string type = "user")
        {
            var endpoint = $"{_entrypoint}/me/following?type={type}";
            var serializedIds = JsonConvert.SerializeObject(ids);
            var requestBody = new StringContent(serializedIds);

            await HttpRequest.Delete(endpoint, requestBody);
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
