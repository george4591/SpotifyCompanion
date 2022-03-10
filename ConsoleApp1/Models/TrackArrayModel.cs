using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpotifyCompanion.Models
{
    public class TrackArrayModel
    {
        public Item[] items { get; set; }
        public class Item
        {
            public TrackModel track;
        }
    }
}
