using SpotifyCompanion.Models;
using SpotifyCompanion.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public class Track
    {
        private static TrackModel _trackModel;
        public static async void GetCurrentTrack()
        {
            _trackModel = await HttpRequest.Get<TrackModel>("https://api.spotify.com/v1/me/player/currently-playing");
            Console.WriteLine(_trackModel.item.name);
        }
    }
}
