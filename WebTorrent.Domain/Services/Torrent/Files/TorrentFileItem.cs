using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent.Files
{
    public class TorrentFileItem
    {
        public string Name { get; set; }
        public double Size { get; set; }
        public double Downloaded { get; set; }
        public double Percentage { get; set; }
        public string Priority { get; set; }
    }
}
