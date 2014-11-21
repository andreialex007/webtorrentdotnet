using System;
using System.Net;
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Common;
using WebTorrent.TorrentLib.TorrentManager;

namespace WebTorrent.TorrentLib.ClientEngine
{
    public class ClientEngineWrapper : IClientEngine
    {
        private readonly MonoTorrent.Client.ClientEngine _clientEngine;

        public void ChangeListenEndpoint(IPEndPoint endpoint)
        {
            _clientEngine.ChangeListenEndpoint(endpoint);
        }

        public bool Contains(InfoHash infoHash)
        {
            return _clientEngine.Contains(infoHash);
        }

        public bool Contains(Torrent torrent)
        {
            return _clientEngine.Contains(torrent);
        }

        public bool Contains(ITorrentManager manager)
        {
            return _clientEngine.Contains(((TorrentManagerWrapper)manager)._torrentManager);
        }

        public void Dispose()
        {
            _clientEngine.Dispose();
        }

        public void PauseAll()
        {
            _clientEngine.PauseAll();
        }

        public void Register(ITorrentManager manager)
        {
            _clientEngine.Register(((TorrentManagerWrapper)manager)._torrentManager);
        }

        public void RegisterDht(IDhtEngine engine)
        {
            _clientEngine.RegisterDht(engine);
        }

        public void StartAll()
        {
            _clientEngine.StartAll();
        }

        public void StopAll()
        {
            _clientEngine.StopAll();
        }

        public void Unregister(ITorrentManager manager)
        {
            _clientEngine.Unregister(((TorrentManagerWrapper)manager)._torrentManager);
        }

        public ConnectionManager ConnectionManager
        {
            get { return _clientEngine.ConnectionManager; }
        }

        public IDhtEngine DhtEngine
        {
            get { return _clientEngine.DhtEngine; }
        }

        public DiskManager DiskManager
        {
            get { return _clientEngine.DiskManager; }
        }

        public bool Disposed
        {
            get { return _clientEngine.Disposed; }
        }

        public PeerListener Listener
        {
            get { return _clientEngine.Listener; }
        }

        public bool LocalPeerSearchEnabled
        {
            get { return _clientEngine.LocalPeerSearchEnabled; }
            set { _clientEngine.LocalPeerSearchEnabled = value; }
        }

        public bool IsRunning
        {
            get { return _clientEngine.IsRunning; }
        }

        public string PeerId
        {
            get { return _clientEngine.PeerId; }
        }

        public EngineSettings Settings
        {
            get { return _clientEngine.Settings; }
        }

        public int TotalDownloadSpeed
        {
            get { return _clientEngine.TotalDownloadSpeed; }
        }

        public int TotalUploadSpeed
        {
            get { return _clientEngine.TotalUploadSpeed; }
        }

        public event EventHandler<StatsUpdateEventArgs> StatsUpdate
        {
            add { _clientEngine.StatsUpdate += value; }
            remove { _clientEngine.StatsUpdate -= value; }
        }

        public event EventHandler<CriticalExceptionEventArgs> CriticalException
        {
            add { _clientEngine.CriticalException += value; }
            remove { _clientEngine.CriticalException -= value; }
        }

        public event EventHandler<TorrentEventArgs> TorrentRegistered
        {
            add { _clientEngine.TorrentRegistered += value; }
            remove { _clientEngine.TorrentRegistered -= value; }
        }

        public event EventHandler<TorrentEventArgs> TorrentUnregistered
        {
            add { _clientEngine.TorrentUnregistered += value; }
            remove { _clientEngine.TorrentUnregistered -= value; }
        }

        public ClientEngineWrapper(MonoTorrent.Client.ClientEngine clientEngine)
        {
            _clientEngine = clientEngine;
        }
    }
}
