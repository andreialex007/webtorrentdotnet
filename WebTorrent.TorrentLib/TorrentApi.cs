using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using MonoTorrent.Client;
using MonoTorrent.Common;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
using WebTorrent.Domain.Services._Common.Entities;
using WebTorrent.Domain.Wrappers;

namespace WebTorrent.TorrentLib
{
    public class TorrentApi : ITorrentApi
    {
        private readonly ClientEngine _clientEngine;
        private readonly string _savePath = Path.GetTempPath();
        private readonly Dictionary<int, TorrentManager> _idToManagersMapping = new Dictionary<int, TorrentManager>();

        #region Public methods

        public TorrentApi()
        {
            var settings = new EngineSettings {SavePath = _savePath};
            _clientEngine = new ClientEngine(settings);
        }

        public void AddTorrent(TorrentDto torrentDto)
        {
            var torrent = Torrent.Load(torrentDto.Data);
            var manager = new TorrentManager(torrent, _savePath, new TorrentSettings());
            _idToManagersMapping.Add(torrentDto.Id, manager);
            _clientEngine.Register(manager);
            LoadTorrentInfo(torrentDto);
        }

        public void LoadTorrentInfo(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return;

            var manager = _idToManagersMapping[torrentDto.Id];
            torrentDto.Created = manager.Torrent.CreationDate;
            torrentDto.State = manager.State.ToDomainTorrentState();
            torrentDto.DownloadingPercentage = (decimal) manager.Progress;
            torrentDto.Name = manager.Torrent.Name;
            torrentDto.Size = manager.Torrent.Size;
        }

        #endregion

        #region Torrent info

        public List<NameValue> GetTorrentInfo(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return null;

            var nameValues = new List<NameValue>();

            var manager = _idToManagersMapping[torrentDto.Id];
            var torrent = manager.Torrent;
            nameValues.Add(new NameValue("Publisher", torrent.Publisher));
            nameValues.Add(new NameValue("SavePath", manager.SavePath));
            nameValues.Add(new NameValue("TotalSize", torrent.Size.ToString(CultureInfo.InvariantCulture)));
            nameValues.Add(new NameValue("Pieces", string.Format("{0} x {1} kb", torrent.Pieces.Count, torrent.PieceLength)));
            nameValues.Add(new NameValue("Created on", torrent.CreationDate.ToShortDateString()));
            nameValues.Add(new NameValue("Created by", torrent.CreatedBy));
            nameValues.Add(new NameValue("Comment", torrent.Comment));
            return nameValues;
        }

        public List<TorrentFileItem> GetFileItems(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return null;

            var manager = _idToManagersMapping[torrentDto.Id];
            var torrent = manager.Torrent;
            var fileItems = torrent.Files.Select(x => new TorrentFileItem
                                                      {
                                                          Name = x.FullPath,
                                                          Priority = x.Priority.ToString(),
                                                          Downloaded = x.BytesDownloaded,
                                                          Size = x.Length
                                                      }).ToList();

            return fileItems;
        }

        public List<PeerItem> GetPeerItems(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return null;

            var manager = _idToManagersMapping[torrentDto.Id];

            var peerItems = manager.GetPeers().Select(x => new PeerItem
                                                           {
                                                               Client = x.ClientApp.ToString(),
                                                               Ip = x.Uri.Host,
                                                               Downloaded = x.Monitor.DataBytesDownloaded,
                                                               Uploaded = x.Monitor.DataBytesUploaded,
                                                               DownloadingSpeed = x.Monitor.DownloadSpeed,
                                                               UploadingSpeed = x.Monitor.UploadSpeed
                                                           }).ToList();

            return peerItems;
        }

        public List<TrackerItem> GetTrackerItems(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return null;

            var manager = _idToManagersMapping[torrentDto.Id];

            var trackers = manager.TrackerManager.SelectMany(x => x.GetTrackers()).ToList();
            var trackerItems = trackers.Select(x => new TrackerItem
                                                    {
                                                        Name = x.Uri.ToString(),
                                                        State = x.Status.ToString(),
                                                        Downloaded = x.Downloaded,
                                                        UpdateAfter = x.UpdateInterval.ToString()
                                                    }).ToList();

            return trackerItems;
        }

        #endregion
    }
}
