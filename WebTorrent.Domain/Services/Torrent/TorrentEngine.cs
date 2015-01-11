using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using NHibernate.Linq;
using WebTorrent.Domain.Services.Torrent.Files;
using WebTorrent.Domain.Services.Torrent.Peers;
using WebTorrent.Domain.Services.Torrent.Trackers;
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

        public TorrentEngine(ITorrentApi torrentApi)
        {
            _torrentApi = torrentApi;
            InitTorrents();
        }

        private void InitTorrents()
        {
            using (var session = OpenSession())
            {
                var torrents = session.Query<TorrentRecord>()
                    .OrderBy(x => x.Id)
                    .ToList();

                foreach (var torrentRecord in torrents)
                {
                    _torrentApi.AddTorrent(new TorrentDto
                                           {
                                               Id = torrentRecord.Id,
                                               Data = torrentRecord.Data
                                           });
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
    }
}