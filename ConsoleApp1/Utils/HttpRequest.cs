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
            using (HttpResponseMessage response = await SpotifyClient.Client.GetAsync(new Uri(endpoint)))
            {
                EnsureSuccessfulRequest(response);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static async void Post(string endpoint)
        {
            using (HttpResponseMessage response = await SpotifyClient.Client.PostAsync(endpoint, new StringContent(string.Empty)))
            {
                EnsureSuccessfulRequest(response);
            }
        }

        public static async Task<T> Post<T>(string endpoint, FormUrlEncodedContent requestBody)
        {
            using (HttpResponseMessage response = await SpotifyClient.Client.PostAsync(endpoint, requestBody))
            {
                EnsureSuccessfulRequest(response);
                return await response.Content.ReadAsAsync<T>();
            }
        }

        public static async Task Put(string endpoint, StringContent requestBody)
        {
            using (HttpResponseMessage response = await SpotifyClient.Client.PutAsync(endpoint, requestBody))
            {
                EnsureSuccessfulRequest(response);
            }
        }

        public static async Task Put(string endpoint)
        {
            using (HttpResponseMessage response = await SpotifyClient.Client.PutAsync(endpoint, new StringContent(string.Empty)))
            {
                EnsureSuccessfulRequest(response);
            }
        }

        public static async Task Delete(string endpoint)
        {
            using (HttpResponseMessage response = await SpotifyClient.Client.DeleteAsync(endpoint))
            {
                EnsureSuccessfulRequest(response);
            }
        }

        public static async Task Delete(string endpoint, StringContent requestBody)
        {
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Delete,
                RequestUri = new Uri(endpoint),
                Content = requestBody
            };

            using (HttpResponseMessage response = await SpotifyClient.Client.SendAsync(request))
            {
                EnsureSuccessfulRequest(response);
            }
        }
        private static void EnsureSuccessfulRequest(HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                Console.WriteLine($"Error {(int)response.StatusCode} - {response.ReasonPhrase}");
            }
        }
    }
}
