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
        static readonly string MainEndpoint = "https://api.spotify.com/v1/me/player";
        static readonly List<string> RepeatModes = new List<string> { "off", "track", "context" };
        private static async Task<PlaybackStateModel> GetPlaybackState()
        {
            return await HttpRequest.Get<PlaybackStateModel>($"{MainEndpoint}");
        }
        public static async void TransferPlayback()
        {

        }
        public static async void GetAvailableDevices()
        {

        }
        public static async void GetRecentlyPlayedTracks()
        {
            var recentTracks = await HttpRequest.Get<TrackArrayModel>($"{MainEndpoint}/recently-played");
            foreach (var item in recentTracks.items)
            {
                Console.WriteLine(item.track.name);
            }
        }
        public static async Task<CurrentTrackModel> GetCurrentlyPlayingTrack()
        {
            return await HttpRequest.Get<CurrentTrackModel>($"{MainEndpoint}/currently-playing");
        }

        private static async void PlayPlayback()
        {
            await HttpRequest.Put($"{MainEndpoint}/play");
        }
        private static async void PausePlayback()
        {
            await HttpRequest.Put($"{MainEndpoint}/pause");
        }

        public static async void PauseStartPlayback()
        {
            var state = await GetPlaybackState();
            if (state.is_playing) PausePlayback();
            else PlayPlayback();
        }
        public static async void SkipToNext()
        {
            HttpRequest.Post($"{MainEndpoint}/next");
        }

        public static async void SkipToPrevious()
        {
            HttpRequest.Post($"{MainEndpoint}/previous");
        }

        //FIXME magic number
        public static async void SeekToPosition()
        {
            await HttpRequest.Put($"{MainEndpoint}/seek?position_ms={2500}");
        }

        public static async void SetRepeatMode()
        {
            var PlaybackState = await GetPlaybackState();
            var IndexOfCurrentMode = RepeatModes.IndexOf(PlaybackState.repeat_state);

            string RepeatMode = RepeatModes[(IndexOfCurrentMode + 1) % RepeatModes.Count];

            await HttpRequest.Put($"{MainEndpoint}/repeat?state={RepeatMode}");
        }

        //FIXME magic number
        public static async void SetPlaybackVolume()
        {
            await HttpRequest.Put($"{MainEndpoint}/volume?volume_percent={50}");
        }

        public static async void TogglePlaybackShuffle()
        {
            var PlaybackState = await GetPlaybackState();

            //in playback state the shuffle is stored as a string but we need to pass a boolean here for some reason
            var ShuffleMode = PlaybackState.shuffle_state;
            bool NewMode = !(ShuffleMode == "true");

            await HttpRequest.Put($"{MainEndpoint}/shuffle?state={NewMode}");
        }

        public static void AddItemToPlaybackQueue(string ItemUri)
        {
            HttpRequest.Post($"{MainEndpoint}/queue?uri={ItemUri}");
        }
    }
}
