using System;
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
                    _torrentApi.AddTorrent(torrentDto);
                    var userRecord = new TorrentRecord
                    {
                        Id = torrentDto.Id,
                        Name = torrentDto.Name,
                        Data = torrentDto.Data,
                        Completed = torrentDto.Completed,
                        Created = torrentDto.Created,
                        Modified = torrentDto.Modified,
                        State = torrentDto.State,
                    };
                    session.Save(userRecord);
                    transaction.Commit();
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

        public void Delete(int id)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.DeleteById<TorrentRecord>(id);
                    transaction.Commit();
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
                        

                        session.Update(record);
                    }
                    transaction.Commit();
                }
            }
        }
    }
}