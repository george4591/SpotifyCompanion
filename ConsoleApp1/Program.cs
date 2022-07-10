using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var Spotify = new SpotifyClient("1d6a111ca15a48a5a5b1f0364147756f", "4c72324c6ce8489ba4f61605b0b0f240");
            await Spotify.Initialize();
            await Spotify.Run();
        }
    }
}
