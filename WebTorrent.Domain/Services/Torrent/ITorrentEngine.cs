using System.Collections.Generic;

namespace WebTorrent.Domain.Services.Torrent
{
    public interface ITorrentEngine
    {
        List<TorrentDto> AllTorrents();
        List<TorrentDto> TorrentsWithState(TorrentState torrentState);
        TorrentDto GetTorrentById(int id);
        TorrentFullInfo GetTorrentInfoBlocks(int id);
        void Delete(int id);
        void Add(TorrentDto torrentDto);
        void Start(int id);
        void Stop(int id);
        void Pause(int id);
    }
}