using System.Collections.Generic;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Wrappers
{
    public interface ITorrentApi
    {
        void AddTorrent(TorrentDto torrentDto);
        void LoadTorrentInfo(TorrentDto torrentDto);
        List<NameValue> GetTorrentInfo(TorrentDto torrentDto);
        List<TorrentFileItem> GetFileItems(TorrentDto torrentDto);
        List<PeerItem> GetPeerItems(TorrentDto torrentDto);
        List<TrackerItem> GetTrackerItems(TorrentDto torrentDto);
        void Start(TorrentDto torrentDto);
        void Stop(TorrentDto torrentDto);
        void Pause(TorrentDto torrentDto);
        void Delete(TorrentDto torrentDto);
    }
}