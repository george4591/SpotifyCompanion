using Hanssens.Net;
using Nito.AsyncEx;
using SpotifyCompanion.Models;
using SpotifyCompanion.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;

namespace SpotifyCompanion
{
    public static class OAuth2
    {
        private static AccessTokenModel _accessTokenModel;
        public static AccessTokenModel Authorize(string ClientId, string ClientSecret)
        {
            var Scope = new List<string>
            {
                Scopes.UserReadEmail, Scopes.UserReadPrivate, Scopes.PlaylistReadPrivate,
                Scopes.PlaylistReadCollaborative, Scopes.UserReadCurrentlyPlaying, Scopes.UserReadPlaybackState,
                Scopes.UserReadRecentlyPlayed, Scopes.UserFollowModify, Scopes.PlaylistModifyPrivate, Scopes.PlaylistModifyPublic

            };


            HttpListener Listener = new HttpListener();
            Listener.Prefixes.Add(AppDetails.RedirectUri);
            Listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            Dictionary<string, string> RequestData = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", ClientId },
                { "scope", string.Join(" ", Scope) },
                { "redirect_uri", AppDetails.RedirectUri }
            };

            var queryString = string.Join("&", RequestData.Select(KeyValPair => $"{KeyValPair.Key}={HttpUtility.UrlEncode(KeyValPair.Value)}"));
            string Url = $"https://accounts.spotify.com/authorize?{queryString}";

            System.Diagnostics.Process.Start(Url);

            Listener.Start();
            AsyncContext.Run(() => ListenLoop(Listener));
            Listener.Stop();

            return _accessTokenModel;
        }

        private static async Task ListenLoop(HttpListener Listener)
        {
            while (true)
            {
                var context = await Listener.GetContextAsync();
                var query = context.Request.QueryString;

                if (query != null && query.Count > 0)
                {
                    if (!string.IsNullOrEmpty(query["code"]))
                    {

                        _accessTokenModel = await GetToken(query["code"]);

                        break;
                    }
                    else if (!string.IsNullOrEmpty(query["error"]))
                    {
                        var _errorResult = string.Format("{0}: {1}", query["error"], query["error_description"]);
                        Console.WriteLine(_errorResult);
                        throw new HttpRequestException(_errorResult);
                    }
                }
            }
        }

        private static async Task<AccessTokenModel> GetToken(string code)
        {
            string url = "https://accounts.spotify.com/api/token";
            Dictionary<string, string> RequestData = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", $"{AppDetails.RedirectUri}" }
            };

            FormUrlEncodedContent RequestBody = new FormUrlEncodedContent(RequestData);

            return await SpotifyHttpRequest.Post<AccessTokenModel>(url, RequestBody);
        }
        public static async Task<AccessTokenModel> RefreshToken(string refreshToken)
        {
            string url = "https://accounts.spotify.com/api/token";
            Dictionary<string, string> RequestData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            FormUrlEncodedContent RequestBody = new FormUrlEncodedContent(RequestData);

            return await SpotifyHttpRequest.Post<AccessTokenModel>(url, RequestBody);
        }

       
    }
}
