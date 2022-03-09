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
            var Spotify = new SpotifyClient();
            await Spotify.Initialize();
            Spotify.Run();
        }
    }
}
