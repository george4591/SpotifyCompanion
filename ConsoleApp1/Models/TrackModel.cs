using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Models
{
    public class TrackModel
    {
        public Album album { get; set; }
        public Artist[] artists { get; set; }
        public bool _explicit { get; set; }
        public string href { get; set; }
        public string id { get; set; }
        public bool is_local { get; set; }
        public string name { get; set; }
        public int popularity { get; set; }
        public string preview_url { get; set; }
        public string uri { get; set; }

        public class Album
        {
            public string album_type { get; set; }
            public Artist[] artists { get; set; }
            public string href { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string release_date { get; set; }
            public string release_date_precision { get; set; }
            public int total_tracks { get; set; }
            public string uri { get; set; }
        }

        public class Artist
        {
            public string href { get; set; }
            public string id { get; set; }
            public string name { get; set; }
            public string uri { get; set; }
        }
    }
}
