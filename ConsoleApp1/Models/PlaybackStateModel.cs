using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Models
{
    public class PlaybackStateModel
    {
        public Device device { get; set; }
        public string repeat_state { get; set; }
        public string shuffle_state { get; set; }
        public Context context { get; set; }
        public Int64 timestamp { get; set; }
        public int progress_ms { get; set; }
        public bool is_playing { get; set; }
        public struct Device
        {
            public string id { get; set; }
            public bool is_active { get; set; }
            public bool is_private_session { get; set; }
            public bool is_restricted { get; set; }
            public string name { get; set; }
            public string type { get; set; }
            public int volume_percent { get; set; }
        }

        public class Context
        {
            public string type { get; set; }
            public string href { get; set; }
            public string uri { get; set; }
        }
    }
}