using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate.Linq;
using WebTorrent.Domain.Services._Common;
using WebTorrent.Domain.Wrappers;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentEngine : ServiceBase, ITorrentEngine
    {
        public static Func<TorrentEngine> CurrentUserTorrentEngineCreator;

        public static ITorrentEngine Current
        {
            get { return CurrentUserTorrentEngineCreator(); }
            private set { }
        }

        private IClientEngine _clientEngine;

        public TorrentEngine(IClientEngine clientEngine)
        {
            _clientEngine = clientEngine;
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
                var torrents = session.Query<TorrentRecord>()
                    .Where(x => x.State == torrentState)
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

        public TorrentDto Torrent(int id)
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
    }
}
