using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ionic.Zip;
using MonoTorrent.Client;
using MonoTorrent.Common;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
using WebTorrent.Domain.Services._Common.Entities;
using WebTorrent.Domain.Wrappers;
using TorrentState = MonoTorrent.Common.TorrentState;

// ReSharper disable ConvertToLambdaExpression

namespace WebTorrent.TorrentLib
{
    public class TorrentApi : ITorrentApi
    {
        private readonly ClientEngine _clientEngine;

        public static string UserFolderPath { get; set; }

        public static string SavePath
        {
            get { return Path.GetTempPath(); }
        }

        private readonly Dictionary<int, TorrentManager> _idToManagersMapping = new Dictionary<int, TorrentManager>();
        public event Action<TorrentDto> TorrentDownloadingCompleted;

        protected virtual void OnTorrentDownloadingCompleted(TorrentDto obj)
        {
            Action<TorrentDto> handler = TorrentDownloadingCompleted;
            if (handler != null) handler(obj);
        }

        #region Public methods

        public TorrentApi()
        {
            var settings = new EngineSettings { SavePath = SavePath };
            _clientEngine = new ClientEngine(settings);
        }

        public string PrepareToDownload(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return string.Empty;

            var manager = _idToManagersMapping[torrentDto.Id];
            var fileName = string.Format("{0}.zip", Path.Combine(UserFolderPath, manager.Torrent.InfoHash.ToString()));
            if (File.Exists(fileName))
                return fileName;

            using (var zip = new ZipFile())
            {
                zip.AddDirectory(manager.Torrent.SavePath());
                zip.Save(fileName);
            }

            return fileName;
        }

        public void AddTorrent(TorrentDto torrentDto)
        {
            var torrent = Torrent.Load(torrentDto.Data);
            var manager = new TorrentManager(torrent, torrent.SavePath(), new TorrentSettings());
            _idToManagersMapping.Add(torrentDto.Id, manager);
            _clientEngine.Register(manager);
            LoadTorrentInfo(torrentDto, false);
            manager.TorrentStateChanged += (sender, args) =>
            {
                if (manager.State == TorrentState.Seeding)
                {
                    OnTorrentDownloadingCompleted(torrentDto);
                }
                Debug.WriteLine(string.Format("line={0}", manager.State.ToString()));
            };
        }

        public string GetName(TorrentDto torrentDto)
        {
            var torrent = Torrent.Load(torrentDto.Data);
            return torrent.Name;
        }

        public void LoadTorrentInfo(TorrentDto torrentDto, bool overrideState = true)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return;

            var manager = _idToManagersMapping[torrentDto.Id];
            torrentDto.Created = manager.Torrent.CreationDate;
            if (overrideState)
                torrentDto.State = manager.State.ToDomainTorrentState();
            torrentDto.DownloadingPercentage = (decimal)manager.Progress;
            torrentDto.Name = manager.Torrent.Name;
            torrentDto.Size = manager.Torrent.Size;
            torrentDto.CreatedOn = manager.Torrent.CreationDate;
            torrentDto.CreatedBy = manager.Torrent.CreatedBy;
        }

        public void Delete(TorrentDto torrentDto)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return;

            var manager = _idToManagersMapping[torrentDto.Id];
            manager.Stop();
            while (true)
            {
                if (manager.State == TorrentState.Stopped)
                {
                    _clientEngine.Unregister(manager);
                    _idToManagersMapping.Remove(torrentDto.Id);
                    return;
                }
                Debug.WriteLine("while iteration");
                Thread.Sleep(1000);
            }
        }

        public void Start(TorrentDto torrentDto)
        {
            ChangeState(torrentDto, torrentManager => torrentManager.Start());
        }

        public void Stop(TorrentDto torrentDto)
        {
            ChangeState(torrentDto, torrentManager => torrentManager.Stop());
        }

        public void Pause(TorrentDto torrentDto)
        {
            ChangeState(torrentDto, torrentManager => torrentManager.Pause());
        }

        private void ChangeState(TorrentDto torrentDto, Action<TorrentManager> command)
        {
            if (!_idToManagersMapping.ContainsKey(torrentDto.Id))
                return;
            var manager = _idToManagersMapping[torrentDto.Id];
            command(manager);
            LoadTorrentInfo(torrentDto);
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
                                                          Name = Path.GetFileName(x.FullPath),
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
