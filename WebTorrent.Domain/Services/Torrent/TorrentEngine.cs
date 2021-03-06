﻿using System;
using System.Collections.Generic;
using System.Linq;
using NHibernate.Linq;
using WebTorrent.Domain.Extensions;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
using WebTorrent.Domain.Services.User;
using WebTorrent.Domain.Services._Common;
using WebTorrent.Domain.Services._Common.Entities;
using WebTorrent.Domain.Wrappers;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentEngine : ServiceBase, ITorrentEngine
    {
        #region Static members

        public static Func<TorrentEngine> CurrentUserTorrentEngineCreator;

        public static ITorrentEngine Current
        {
            get { return CurrentUserTorrentEngineCreator(); }
            private set { }
        }

        #endregion

        private readonly ITorrentApi _torrentApi;

        #region Public members

        public TorrentEngine(ITorrentApi torrentApi)
        {
            _torrentApi = torrentApi;
            InitTorrents();
        }

        public void Add(TorrentDto torrentDto)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var torrentRecord = new TorrentRecord
                    {
                        Name = _torrentApi.GetName(torrentDto),
                        Data = torrentDto.Data,
                        Completed = null,
                        Created = DateTime.Now,
                        Modified = DateTime.Now,
                        State = TorrentState.Paused,
                    };
                    session.Save(torrentRecord);
                    transaction.Commit();
                    torrentDto.Id = torrentRecord.Id;
                    _torrentApi.AddTorrent(torrentDto);
                }
            }
        }

        public List<TorrentDto> AllTorrents()
        {
            using (var session = OpenSession())
            {
                var torrents = session.Query<TorrentRecord>()
                    .OrderBy(x => x.Id)
                    .Select(x => new TorrentDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     State = x.State
                                 })
                    .ToList();

                torrents.ForEach(x => _torrentApi.LoadTorrentInfo(x));

                return torrents;
            }
        }

        public List<TorrentDto> TorrentsWithState(TorrentState torrentState)
        {
            using (var session = OpenSession())
            {
                var orderedQueryable = session.Query<TorrentRecord>()
                    .Where(x => x.State == torrentState)
                    .OrderBy(x => x.Id);

                List<TorrentDto> torrents = orderedQueryable
                    .Select(x => new TorrentDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     State = x.State
                                 })
                    .ToList();

                torrents.ForEach(x => _torrentApi.LoadTorrentInfo(x));

                return torrents;
            }
        }

        public TorrentDto GetTorrentById(int id)
        {
            using (var session = OpenSession())
            {
                var torrent = session.Query<TorrentRecord>()
                    .Where(x => x.Id == id)
                    .Select(x => new TorrentDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     State = x.State
                                 })
                    .SingleOrDefault();
                return torrent;
            }
        }

        public string GetZippedFile(int id)
        {
            var torrentDto = GetTorrentById(id);
            var zippedFilePath = _torrentApi.PrepareToDownload(torrentDto);
            return zippedFilePath;
        }

        public void Start(int id)
        {
            ChangeState(id, dto => _torrentApi.Start(dto));
        }

        public void Stop(int id)
        {
            ChangeState(id, dto => _torrentApi.Stop(dto));
        }

        public void Pause(int id)
        {
            ChangeState(id, dto => _torrentApi.Pause(dto));
        }

        private void ChangeState(int id, Action<TorrentDto> command)
        {
            using (var session = OpenSession())
            {
                var torrent = session.Query<TorrentRecord>()
                    .Where(x => x.Id == id)
                    .Select(x => new TorrentDto
                    {
                        Id = x.Id,
                        Name = x.Name,
                        State = x.State
                    })
                    .SingleOrDefault();

                command(torrent);
                var torrentRecord = session.Query<TorrentRecord>().SingleOrDefault(x => x.Id == id);
                torrentRecord.State = torrent.State;
                using (var transaction = session.BeginTransaction())
                {
                    session.Update(torrentRecord);
                    transaction.Commit();
                }
            }
        }

        public void Delete(int id)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var record = session.Query<TorrentRecord>()
                       .Where(x => x.Id == id)
                       .Select(x => x)
                       .Single();
                    session.DeleteById<TorrentRecord>(id);
                    transaction.Commit();
                    _torrentApi.Delete(new TorrentDto
                                       {
                                           Id = record.Id,
                                           Name = record.Name,
                                           State = record.State
                                       });
                }
            }
        }

        public TorrentFullInfo GetTorrentInfoBlocks(int id)
        {
            using (var session = OpenSession())
            {
                var torrent = session.Query<TorrentRecord>()
                    .Where(x => x.Id == id)
                    .Select(x => new TorrentDto
                                 {
                                     Id = x.Id,
                                     Name = x.Name,
                                     State = x.State
                                 })
                    .SingleOrDefault();


                if (torrent == null)
                    return new TorrentFullInfo();

                var torrentFullInfo = new TorrentFullInfo
                                      {
                                          FilesInfo = new TorrentInfoBlock<List<TorrentFileItem>>
                                                      {
                                                          Name = "Files",
                                                          Data = _torrentApi.GetFileItems(torrent)
                                                      },
                                          TorrentInfo = new TorrentInfoBlock<List<NameValue>>
                                                        {
                                                            Name = "Torrent info",
                                                            Data = _torrentApi.GetTorrentInfo(torrent)
                                                        },
                                          PeersInfo = new TorrentInfoBlock<List<PeerItem>>
                                                      {
                                                          Name = "Peers",
                                                          Data = _torrentApi.GetPeerItems(torrent)
                                                      },
                                          TrackersInfo = new TorrentInfoBlock<List<TrackerItem>>
                                                         {
                                                             Name = "Trackers",
                                                             Data = _torrentApi.GetTrackerItems(torrent)
                                                         }
                                      };
                return torrentFullInfo;
            }
        }

        #endregion

        private void InitTorrents()
        {
            using (var session = OpenSession())
            {
                var torrents = session.Query<TorrentRecord>()
                    .OrderBy(x => x.Id)
                    .ToList();

                using (var transaction = session.BeginTransaction())
                {

                    foreach (var record in torrents)
                    {
                        var torrentDto = new TorrentDto
                                         {
                                             Id = record.Id,
                                             Data = record.Data
                                         };
                        _torrentApi.AddTorrent(torrentDto);
                        _torrentApi.TorrentDownloadingCompleted += DownloadingCompleted;
                        session.Update(record);
                    }
                    transaction.Commit();
                }

                torrents
                    .Where(x => x.State == TorrentState.Completed
                        || x.State == TorrentState.Completed
                        || x.State == TorrentState.Downloading
                        || x.State == TorrentState.Checking
                        )
                    .ToList()
                    .ForEach(record =>
                             {
                                 _torrentApi.Start(new TorrentDto
                                                   {
                                                       Id = record.Id,
                                                       Data = record.Data
                                                   });
                             });

            }
        }

        private void DownloadingCompleted(TorrentDto dto)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var torrent = session.Query<TorrentRecord>().SingleOrDefault(x => x.Id == dto.Id);
                    torrent.State = TorrentState.Completed;
                    session.Update(torrent);
                    transaction.Commit();
                }
            }
        }
    }
}