using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Utils
{
    public static class HttpRequest
    {
        public static async Task<T> Get<T>(string endpoint)
        {
            using (HttpResponseMessage Response = await SpotifyClient.Client.GetAsync(new Uri(endpoint)))
            {
                if (Response.IsSuccessStatusCode)
                {
                    return await Response.Content.ReadAsAsync<T>();
                }
                else
                {
                    var error = await Response.Content.ReadAsStringAsync();

                    Console.WriteLine(error);
                    throw new HttpRequestException(Response.ReasonPhrase);
                }
            }
        }

        //public static async Task<T> Post<T>()
        //{ }
    }
}
