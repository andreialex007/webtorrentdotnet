using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent.Peers
{
    public class PeerItem
    {
        public string Ip { get; set; }
        public string Client { get; set; }
        public string Flags { get; set; }
        public decimal Percentage { get; set; }

        public decimal DownloadingSpeed { get; set; }
        public decimal PeerDownloadingSpeed { get; set; }
        public decimal UploadingSpeed { get; set; }

        public string Requests { get; set; }
        public decimal Uploaded { get; set; }
        public decimal Downloaded { get; set; }
    }
}
