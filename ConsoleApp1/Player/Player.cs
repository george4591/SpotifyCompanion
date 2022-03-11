using SpotifyCompanion.Models;
using SpotifyCompanion.Utils.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion
{
    public static class Player
    {
        public static async void GetPlaybackState()
        {

        }
        public static async void TransferPlayback()
        {

        }
        public static async void GetAvailableDevices()
        {

        }
        public static async void GetRecentlyPlayedTracks()
        {
            var recentTracks = await SpotifyHttpRequest.Get<TrackArrayModel>("https://api.spotify.com/v1/me/player/recently-played");
            foreach (var item in recentTracks.items)
            {
                Console.WriteLine(item.track.name);
            }
        }
        public static async Task<CurrentTrackModel> GetCurrentlyPlayingTrack()
        {
            return await SpotifyHttpRequest.Get<CurrentTrackModel>("https://api.spotify.com/v1/me/player/currently-playing");
        }

        public static async void StartResumePlayback()
        {

        }

        public static async void PausePlayback()
        {

        }

        public static async void SkipToNext()
        {

        }

        public static async void SkipToPrevious()
        {

        }

        public static async void SeekToPosition()
        {

        }

        public static async void SetRepeatMode()
        {

        }

        public static async void SetPlaybackVolume()
        {

        }

        public static async void TogglePlaybackShuffle()
        {

        }

        public static async void AddItemToPlaybackQueue()
        {

        }
    }
}
