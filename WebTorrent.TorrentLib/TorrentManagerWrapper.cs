using System;
using System.Collections.Generic;
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Client.Tracker;
using MonoTorrent.Common;

namespace WebTorrent.TorrentLib
{
    public class TorrentManagerWrapper : ITorrentManager
    {
        public readonly MonoTorrent.Client.TorrentManager _torrentManager;

        public TorrentManagerWrapper(MonoTorrent.Client.TorrentManager torrentManager)
        {
            _torrentManager = torrentManager;
        }

        public void ChangePicker(PiecePicker picker)
        {
            _torrentManager.ChangePicker(picker);
        }

        public void Dispose()
        {
            _torrentManager.Dispose();
        }

        public bool Equals(MonoTorrent.Client.TorrentManager other)
        {
            return _torrentManager.Equals(other);
        }

        public List<Piece> GetActiveRequests()
        {
            return _torrentManager.GetActiveRequests();
        }

        public List<PeerId> GetPeers()
        {
            return _torrentManager.GetPeers();
        }

        public void HashCheck(bool autoStart)
        {
            _torrentManager.HashCheck(autoStart);
        }

        public void MoveFile(TorrentFile file, string path)
        {
            _torrentManager.MoveFile(file, path);
        }

        public void MoveFiles(string newRoot, bool overWriteExisting)
        {
            _torrentManager.MoveFiles(newRoot, overWriteExisting);
        }

        public void Pause()
        {
            _torrentManager.Pause();
        }

        public void Start()
        {
            _torrentManager.Start();
        }

        public void Stop()
        {
            _torrentManager.Stop();
        }

        public void AddPeers(Peer peer)
        {
            _torrentManager.AddPeers(peer);
        }

        public void AddPeers(IEnumerable<Peer> peers)
        {
            _torrentManager.AddPeers(peers);
        }

        public void LoadFastResume(FastResume data)
        {
            _torrentManager.LoadFastResume(data);
        }

        public FastResume SaveFastResume()
        {
            return _torrentManager.SaveFastResume();
        }

        public BitField Bitfield
        {
            get { return _torrentManager.Bitfield; }
        }

        public bool CanUseDht
        {
            get { return _torrentManager.CanUseDht; }
        }

        public bool Complete
        {
            get { return _torrentManager.Complete; }
        }

        public MonoTorrent.Client.ClientEngine Engine
        {
            get { return _torrentManager.Engine; }
        }

        public Error Error
        {
            get { return _torrentManager.Error; }
        }

        public int PeerReviewRoundsComplete
        {
            get { return _torrentManager.PeerReviewRoundsComplete; }
        }

        public bool HashChecked
        {
            get { return _torrentManager.HashChecked; }
        }

        public int HashFails
        {
            get { return _torrentManager.HashFails; }
        }

        public bool HasMetadata
        {
            get { return _torrentManager.HasMetadata; }
        }

        public bool IsInEndGame
        {
            get { return _torrentManager.IsInEndGame; }
        }

        public ConnectionMonitor Monitor
        {
            get { return _torrentManager.Monitor; }
        }

        public int OpenConnections
        {
            get { return _torrentManager.OpenConnections; }
        }

        public PeerManager Peers
        {
            get { return _torrentManager.Peers; }
        }

        public PieceManager PieceManager
        {
            get { return _torrentManager.PieceManager; }
        }

        public double Progress
        {
            get { return _torrentManager.Progress; }
        }

        public string SavePath
        {
            get { return _torrentManager.SavePath; }
        }

        public TorrentSettings Settings
        {
            get { return _torrentManager.Settings; }
        }

        public TorrentState State
        {
            get { return _torrentManager.State; }
        }

        public DateTime StartTime
        {
            get { return _torrentManager.StartTime; }
        }

        public TrackerManager TrackerManager
        {
            get { return _torrentManager.TrackerManager; }
        }

        public Torrent Torrent
        {
            get { return _torrentManager.Torrent; }
        }

        public int UploadingTo
        {
            get { return _torrentManager.UploadingTo; }
        }

        public bool IsInitialSeeding
        {
            get { return _torrentManager.IsInitialSeeding; }
        }

        public int InactivePeers
        {
            get { return _torrentManager.InactivePeers; }
        }

        public InfoHash InfoHash
        {
            get { return _torrentManager.InfoHash; }
        }

        public List<Uri> InactivePeerList
        {
            get { return _torrentManager.InactivePeerList; }
        }

        public event EventHandler<PeerConnectionEventArgs> PeerConnected
        {
            add { _torrentManager.PeerConnected += value; }
            remove { _torrentManager.PeerConnected -= value; }
        }

        public event EventHandler<PeerConnectionEventArgs> PeerDisconnected
        {
            add { _torrentManager.PeerDisconnected += value; }
            remove { _torrentManager.PeerDisconnected -= value; }
        }

        public event EventHandler<PeersAddedEventArgs> PeersFound
        {
            add { _torrentManager.PeersFound += value; }
            remove { _torrentManager.PeersFound -= value; }
        }

        public event EventHandler<PieceHashedEventArgs> PieceHashed
        {
            add { _torrentManager.PieceHashed += value; }
            remove { _torrentManager.PieceHashed -= value; }
        }

        public event EventHandler<TorrentStateChangedEventArgs> TorrentStateChanged
        {
            add { _torrentManager.TorrentStateChanged += value; }
            remove { _torrentManager.TorrentStateChanged -= value; }
        }
    }
}
