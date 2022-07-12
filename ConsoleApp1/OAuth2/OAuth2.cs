using Hanssens.Net;
using Nito.AsyncEx;
using SpotifyCompanion.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using SpotifyCompanion.Utils.Http;

namespace SpotifyCompanion
{
    public static class OAuth2
    {
        private static readonly string _entrypoint = "https://accounts.spotify.com";
        private static AccessTokenModel _accessTokenModel;
        public static AccessTokenModel Authorize(string clientId, string clientSecret)
        {
            var scopes = new List<string>
            {
                Scopes.UserReadEmail, Scopes.UserReadPrivate, Scopes.PlaylistReadPrivate,
                Scopes.PlaylistReadCollaborative, Scopes.UserReadCurrentlyPlaying, Scopes.UserReadPlaybackState,
                Scopes.UserReadRecentlyPlayed, Scopes.UserFollowModify, Scopes.PlaylistModifyPrivate, Scopes.PlaylistModifyPublic,
                Scopes.UserModifyPlaybackState
            };

            var listener = new HttpListener();
            listener.Prefixes.Add(AppDetails.RedirectUri);
            listener.AuthenticationSchemes = AuthenticationSchemes.Anonymous;

            var requestData = new Dictionary<string, string>
            {
                { "response_type", "code" },
                { "client_id", clientId },
                { "scope", string.Join(" ", scopes) },
                { "redirect_uri", AppDetails.RedirectUri }
            };

            Func<KeyValuePair<string, string>, string> concatenatePair = pair => $"{pair.Key}={System.Web.HttpUtility.UrlEncode(pair.Value)}";
            var queryString = string.Join("&", requestData.Select(pair => concatenatePair(pair)));
            var url = $"{_entrypoint}/authorize?{queryString}";

            System.Diagnostics.Process.Start(url);

            listener.Start();
            AsyncContext.Run(() => ListenLoop(listener));
            listener.Stop();

            return _accessTokenModel;
        }

        private static async Task ListenLoop(HttpListener listener)
        {
            while (true)
            {
                HttpListenerContext context = await listener.GetContextAsync();
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
                        string errorResult = string.Format("{0}: {1}", query["error"], query["error_description"]);
                        Console.WriteLine(errorResult);
                        throw new HttpRequestException(errorResult);
                    }
                }
            }
        }

        private static async Task<AccessTokenModel> GetToken(string code)
        {
            var url = $"{_entrypoint}/api/token";
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "authorization_code" },
                { "code", code },
                { "redirect_uri", $"{AppDetails.RedirectUri}" }
            };

            var requestBody = new FormUrlEncodedContent(requestData);

            return await HttpRequest.Post<AccessTokenModel>(url, requestBody);
        }
        public static async Task<AccessTokenModel> RefreshToken(string refreshToken)
        {
            var url = $"{_entrypoint}/api/token";
            var requestData = new Dictionary<string, string>
            {
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

            var requestBody = new FormUrlEncodedContent(requestData);

            return await HttpRequest.Post<AccessTokenModel>(url, requestBody);
        }


    }
}
