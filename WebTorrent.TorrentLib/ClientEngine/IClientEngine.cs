using System;
using System.Net;
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Common;
using WebTorrent.TorrentLib.TorrentManager;

namespace WebTorrent.TorrentLib.ClientEngine
{
    public interface IClientEngine
    {
        void ChangeListenEndpoint(IPEndPoint endpoint);
        bool Contains(InfoHash infoHash);
        bool Contains(Torrent torrent);
        bool Contains(ITorrentManager manager);
        void Dispose();
        void PauseAll();
        void Register(ITorrentManager manager);
        void RegisterDht(IDhtEngine engine);
        void StartAll();
        void StopAll();
        void Unregister(ITorrentManager manager);
        ConnectionManager ConnectionManager { get; }
        IDhtEngine DhtEngine { get; }
        DiskManager DiskManager { get; }
        bool Disposed { get; }
        PeerListener Listener { get; }
        bool LocalPeerSearchEnabled { get; set; }
        bool IsRunning { get; }
        string PeerId { get; }
        EngineSettings Settings { get; }
        int TotalDownloadSpeed { get; }
        int TotalUploadSpeed { get; }
        event EventHandler<StatsUpdateEventArgs> StatsUpdate;
        event EventHandler<CriticalExceptionEventArgs> CriticalException;
        event EventHandler<TorrentEventArgs> TorrentRegistered;
        event EventHandler<TorrentEventArgs> TorrentUnregistered;
    }
}