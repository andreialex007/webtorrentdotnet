using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTorrent.Domain.Services.Torrent;
using MonoTorrentState = MonoTorrent.Common.TorrentState;

namespace WebTorrent.TorrentLib
{
    public static class Extensions
    {
        private static readonly Dictionary<MonoTorrentState, TorrentState> TorrentStatesDictionary =
            new Dictionary<MonoTorrentState, TorrentState>
            {
                {MonoTorrentState.Downloading, TorrentState.Downloading},
                {MonoTorrentState.Paused, TorrentState.Paused},
                {MonoTorrentState.Seeding, TorrentState.Completed},
                {MonoTorrentState.Error, TorrentState.Error},
                {MonoTorrentState.Stopping, TorrentState.Paused},
                {MonoTorrentState.Stopped, TorrentState.Paused}
            };

        public static TorrentState ToDomainTorrentState(this MonoTorrentState torrentState)
        {
            return TorrentStatesDictionary.ContainsKey(torrentState)
                ? TorrentStatesDictionary[torrentState]
                : TorrentState.Unknown;
        }
    }
}
