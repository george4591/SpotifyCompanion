using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Utils.Http
{
    public static class HttpRequest
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.GetAsync(new Uri(endpoint)))
            {
                EnsureSuccessfulRequest(Response);
                return await Response.Content.ReadAsAsync<T>();
            }
        }

        public static async void Post(string endpoint)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.PostAsync(endpoint, new StringContent(string.Empty)))
            {
                EnsureSuccessfulRequest(Response);
            }
        }

        public static async Task<T> Post<T>(string endpoint, FormUrlEncodedContent RequestBody)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.PostAsync(endpoint, RequestBody))
            {
                EnsureSuccessfulRequest(Response);
                return await Response.Content.ReadAsAsync<T>();
            }
        }

        public static async Task Put(string endpoint, StringContent RequestBody)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.PutAsync(endpoint, RequestBody))
            {
                EnsureSuccessfulRequest(Response);
            }
        }

        public static async Task Put(string endpoint)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.PutAsync(endpoint, new StringContent(string.Empty)))
            {
                EnsureSuccessfulRequest(Response);
            }
        }

        public static async Task Delete(string endpoint)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.DeleteAsync(endpoint))
            {
                EnsureSuccessfulRequest(Response);
            }
        }

        public static async Task Delete(string endpoint, StringContent RequestBody)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(endpoint),
                Content = RequestBody
            };

            using (HttpResponseMessage Response = await SpotifyClient.Client.SendAsync(request))
            {
                EnsureSuccessfulRequest(Response);
            }
        }
        private static void EnsureSuccessfulRequest(HttpResponseMessage Response)
        {
            if (!Response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error {(int)Response.StatusCode} - {Response.ReasonPhrase}");
            }
        }
    }
}
