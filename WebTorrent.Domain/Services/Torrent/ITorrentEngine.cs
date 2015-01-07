using System.Collections.Generic;

namespace WebTorrent.Domain.Services.Torrent
{
    public interface ITorrentEngine
    {
        List<TorrentDto> AllTorrents();
        List<TorrentDto> TorrentsWithState(TorrentState torrentState);
        TorrentDto Torrent(int id);
    }
}