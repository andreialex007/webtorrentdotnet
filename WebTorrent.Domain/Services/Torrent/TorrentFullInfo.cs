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
        public TorrentFullInfo()
        {
            PeersInfo = new TorrentInfoBlock<List<PeerItem>>();
            TorrentInfo = new TorrentInfoBlock<List<NameValue>>();
            FilesInfo = new TorrentInfoBlock<List<TorrentFileItem>>();
            FilesInfo = new TorrentInfoBlock<List<TorrentFileItem>>();
        }

        public TorrentInfoBlock<List<PeerItem>> PeersInfo { get; set; }
        public TorrentInfoBlock<List<NameValue>> TorrentInfo { get; set; }
        public TorrentInfoBlock<List<TrackerItem>> TrackersInfo { get; set; }
        public TorrentInfoBlock<List<TorrentFileItem>> FilesInfo { get; set; }
    }
}
