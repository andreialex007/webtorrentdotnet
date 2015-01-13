namespace WebTorrent.Domain.Services.Torrent
{
    public enum TorrentState
    {
        Downloading = 1,
        Completed = 2,
        Checking = 3,
        Paused = 4,
//        Stopped = 5,
        Unknown = 6,
        Error = 7,
    }
}