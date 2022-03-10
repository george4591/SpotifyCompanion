using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Models
{
    public class PlaylistModel
    {
        public string href { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public bool _public { get; set; }
        public Tracks tracks { get; set; }
        public string uri { get; set; }
    
        public class Tracks
        {
            public string href { get; set; }
            public CurrentTrackModel[] items { get; set; }
            public int limit { get; set; }
            public string next { get; set; }
            public int offset { get; set; }
            public string previous { get; set; }
            public int total { get; set; }
        }
    }
}
