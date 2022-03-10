using SpotifyCompanion.Models;
using SpotifyCompanion.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public static class Player
    {
        public static async void GetRecentlyPlayedTracks()
        {
            var recentTracks = await HttpRequest.Get<TrackArrayModel>("https://api.spotify.com/v1/me/player/recently-played");
            foreach (var item in recentTracks.items)
            {
                Console.WriteLine(item.track.name);
            }
        }
        public static async Task<CurrentTrackModel> GetCurrentlyPlayingTrack()
        {
            return await HttpRequest.Get<CurrentTrackModel>("https://api.spotify.com/v1/me/player/currently-playing");
        }
    }
}
