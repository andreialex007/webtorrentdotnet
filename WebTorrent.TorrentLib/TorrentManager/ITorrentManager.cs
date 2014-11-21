using System;
using System.Collections.Generic;
using MonoTorrent;
using MonoTorrent.Client;
using MonoTorrent.Client.Tracker;
using MonoTorrent.Common;

namespace WebTorrent.TorrentLib.TorrentManager
{
    public interface ITorrentManager
    {
        void ChangePicker(PiecePicker picker);
        void Dispose();
        bool Equals(MonoTorrent.Client.TorrentManager other);
        List<Piece> GetActiveRequests();
        List<PeerId> GetPeers();
        void HashCheck(bool autoStart);
        void MoveFile(TorrentFile file, string path);
        void MoveFiles(string newRoot, bool overWriteExisting);
        void Pause();
        void Start();
        void Stop();
        void AddPeers(Peer peer);
        void AddPeers(IEnumerable<Peer> peers);
        void LoadFastResume(FastResume data);
        FastResume SaveFastResume();
        BitField Bitfield { get; }
        bool CanUseDht { get; }
        bool Complete { get; }
        MonoTorrent.Client.ClientEngine Engine { get; }
        Error Error { get; }
        int PeerReviewRoundsComplete { get; }
        bool HashChecked { get; }
        int HashFails { get; }
        bool HasMetadata { get; }
        bool IsInEndGame { get; }
        ConnectionMonitor Monitor { get; }
        int OpenConnections { get; }
        PeerManager Peers { get; }
        PieceManager PieceManager { get; }
        double Progress { get; }
        string SavePath { get; }
        TorrentSettings Settings { get; }
        TorrentState State { get; }
        DateTime StartTime { get; }
        TrackerManager TrackerManager { get; }
        Torrent Torrent { get; }
        int UploadingTo { get; }
        bool IsInitialSeeding { get; }
        int InactivePeers { get; }
        InfoHash InfoHash { get; }
        List<Uri> InactivePeerList { get; }
        event EventHandler<PeerConnectionEventArgs> PeerConnected;
        event EventHandler<PeerConnectionEventArgs> PeerDisconnected;
        event EventHandler<PeersAddedEventArgs> PeersFound;
        event EventHandler<PieceHashedEventArgs> PieceHashed;
        event EventHandler<TorrentStateChangedEventArgs> TorrentStateChanged;
    }
}