using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent.Trackers
{
    public class TrackerItem
    {
        public string Name { get; set; }
        public string State { get; set; }
        public string UpdateAfter { get; set; }
        public int Seeds { get; set; }
        public int Peers { get; set; }
        public int Downloaded { get; set; }
    }
}
