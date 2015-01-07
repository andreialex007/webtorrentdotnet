using System;
using System.Collections.Generic;
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Client.Tracker;
using MonoTorrent.Common;
using WebTorrent.Domain.Wrappers;

namespace WebTorrent.TorrentLib
{
    public class TorrentManagerWrapper : ITorrentManager
    {
        public readonly TorrentManager TorrentManager;

        public TorrentManagerWrapper(TorrentManager torrentManager)
        {
            TorrentManager = torrentManager;
        }

        public void ChangePicker(PiecePicker picker)
        {
            TorrentManager.ChangePicker(picker);
        }

        public void Dispose()
        {
            TorrentManager.Dispose();
        }

        public bool Equals(TorrentManager other)
        {
            return TorrentManager.Equals(other);
        }

        public List<Piece> GetActiveRequests()
        {
            return TorrentManager.GetActiveRequests();
        }

        public List<PeerId> GetPeers()
        {
            return TorrentManager.GetPeers();
        }

        public void HashCheck(bool autoStart)
        {
            TorrentManager.HashCheck(autoStart);
        }

        public void MoveFile(TorrentFile file, string path)
        {
            TorrentManager.MoveFile(file, path);
        }

        public void MoveFiles(string newRoot, bool overWriteExisting)
        {
            TorrentManager.MoveFiles(newRoot, overWriteExisting);
        }

        public void Pause()
        {
            TorrentManager.Pause();
        }

        public void Start()
        {
            TorrentManager.Start();
        }

        public void Stop()
        {
            TorrentManager.Stop();
        }

        public void AddPeers(Peer peer)
        {
            TorrentManager.AddPeers(peer);
        }

        public void AddPeers(IEnumerable<Peer> peers)
        {
            TorrentManager.AddPeers(peers);
        }

        public void LoadFastResume(FastResume data)
        {
            TorrentManager.LoadFastResume(data);
        }

        public FastResume SaveFastResume()
        {
            return TorrentManager.SaveFastResume();
        }

        public BitField Bitfield
        {
            get { return TorrentManager.Bitfield; }
        }

        public bool CanUseDht
        {
            get { return TorrentManager.CanUseDht; }
        }

        public bool Complete
        {
            get { return TorrentManager.Complete; }
        }

        public ClientEngine Engine
        {
            get { return TorrentManager.Engine; }
        }

        public Error Error
        {
            get { return TorrentManager.Error; }
        }

        public int PeerReviewRoundsComplete
        {
            get { return TorrentManager.PeerReviewRoundsComplete; }
        }

        public bool HashChecked
        {
            get { return TorrentManager.HashChecked; }
        }

        public int HashFails
        {
            get { return TorrentManager.HashFails; }
        }

        public bool HasMetadata
        {
            get { return TorrentManager.HasMetadata; }
        }

        public bool IsInEndGame
        {
            get { return TorrentManager.IsInEndGame; }
        }

        public ConnectionMonitor Monitor
        {
            get { return TorrentManager.Monitor; }
        }

        public int OpenConnections
        {
            get { return TorrentManager.OpenConnections; }
        }

        public PeerManager Peers
        {
            get { return TorrentManager.Peers; }
        }

        public PieceManager PieceManager
        {
            get { return TorrentManager.PieceManager; }
        }

        public double Progress
        {
            get { return TorrentManager.Progress; }
        }

        public string SavePath
        {
            get { return TorrentManager.SavePath; }
        }

        public TorrentSettings Settings
        {
            get { return TorrentManager.Settings; }
        }

        public TorrentState State
        {
            get { return TorrentManager.State; }
        }

        public DateTime StartTime
        {
            get { return TorrentManager.StartTime; }
        }

        public TrackerManager TrackerManager
        {
            get { return TorrentManager.TrackerManager; }
        }

        public Torrent Torrent
        {
            get { return TorrentManager.Torrent; }
        }

        public int UploadingTo
        {
            get { return TorrentManager.UploadingTo; }
        }

        public bool IsInitialSeeding
        {
            get { return TorrentManager.IsInitialSeeding; }
        }

        public int InactivePeers
        {
            get { return TorrentManager.InactivePeers; }
        }

        public InfoHash InfoHash
        {
            get { return TorrentManager.InfoHash; }
        }

        public List<Uri> InactivePeerList
        {
            get { return TorrentManager.InactivePeerList; }
        }

        public event EventHandler<PeerConnectionEventArgs> PeerConnected
        {
            add { TorrentManager.PeerConnected += value; }
            remove { TorrentManager.PeerConnected -= value; }
        }

        public event EventHandler<PeerConnectionEventArgs> PeerDisconnected
        {
            add { TorrentManager.PeerDisconnected += value; }
            remove { TorrentManager.PeerDisconnected -= value; }
        }

        public event EventHandler<PeersAddedEventArgs> PeersFound
        {
            add { TorrentManager.PeersFound += value; }
            remove { TorrentManager.PeersFound -= value; }
        }

        public event EventHandler<PieceHashedEventArgs> PieceHashed
        {
            add { TorrentManager.PieceHashed += value; }
            remove { TorrentManager.PieceHashed -= value; }
        }

        public event EventHandler<TorrentStateChangedEventArgs> TorrentStateChanged
        {
            add { TorrentManager.TorrentStateChanged += value; }
            remove { TorrentManager.TorrentStateChanged -= value; }
        }
    }
}