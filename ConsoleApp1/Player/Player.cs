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
        private static readonly string _entrypoint = "https://api.spotify.com/v1/me/player";
        private static readonly List<string> repeatModes = new List<string> { "off", "track", "context" };
        private static async Task<PlaybackStateModel> GetPlaybackState()
        {
            return await HttpRequest.Get<PlaybackStateModel>($"{_entrypoint}");
        }
        public static async void TransferPlayback()
        {

        }
        public static async void GetAvailableDevices()
        {

        }
        public static async void GetRecentlyPlayedTracks()
        {
            var recentTracks = await HttpRequest.Get<TrackArrayModel>($"{_entrypoint}/recently-played");
            foreach (var item in recentTracks.items)
            {
                Console.WriteLine(item.track.name);
            }
        }
        public static async Task<CurrentTrackModel> GetCurrentlyPlayingTrack()
        {
            return await HttpRequest.Get<CurrentTrackModel>($"{_entrypoint}/currently-playing");
        }

        private static async void PlayPlayback()
        {
            await HttpRequest.Put($"{_entrypoint}/play");
        }
        private static async void PausePlayback()
        {
            await HttpRequest.Put($"{_entrypoint}/pause");
        }

        public static async void PauseStartPlayback()
        {
            var state = await GetPlaybackState();
            if (state.is_playing) PausePlayback();
            else PlayPlayback();
        }
        public static async void SkipToNext()
        {
            HttpRequest.Post($"{_entrypoint}/next");
        }

        public static async void SkipToPrevious()
        {
            HttpRequest.Post($"{_entrypoint}/previous");
        }

        // FIXME Magic number.
        public static async void SeekToPosition()
        {
            await HttpRequest.Put($"{_entrypoint}/seek?position_ms={2500}");
        }

        public static async void SetRepeatMode()
        {
            var playbackState = await GetPlaybackState();
            var indexOfCurrentMode = repeatModes.IndexOf(playbackState.repeat_state);

            string RepeatMode = repeatModes[(indexOfCurrentMode + 1) % repeatModes.Count];

            await HttpRequest.Put($"{_entrypoint}/repeat?state={RepeatMode}");
        }

        // FIXME Magic number.
        public static async void SetPlaybackVolume()
        {
            await HttpRequest.Put($"{_entrypoint}/volume?volume_percent={50}");
        }

        public static async void TogglePlaybackShuffle()
        {
            var playbackState = await GetPlaybackState();

            // In playback state the shuffle is stored as a string but we need to pass a boolean here for some reason.
            var shuffleMode = playbackState.shuffle_state;
            bool newMode = !(shuffleMode == "true");

            await HttpRequest.Put($"{_entrypoint}/shuffle?state={newMode}");
        }

        public static void AddItemToPlaybackQueue(string itemUri)
        {
            HttpRequest.Post($"{_entrypoint}/queue?uri={itemUri}");
        }
    }
}
