using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentFullInfo
    {
        public TorrentInfoBlock<List<PeerItem>> PeersInfo { get; set; }
        public TorrentInfoBlock<List<NameValue>> TorrentInfo { get; set; }
        public TorrentInfoBlock<List<TrackerItem>> TrackersInfo { get; set; }
        public TorrentInfoBlock<List<TorrentFileItem>> FilesInfo { get; set; }
    }
}
