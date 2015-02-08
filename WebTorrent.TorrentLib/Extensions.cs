using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTorrent.Common;
using MonoTorrentState = MonoTorrent.Common.TorrentState;
using TorrentState = WebTorrent.Domain.Services.Torrent.TorrentState;

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

        public static string SavePath(this Torrent torrent)
        {
            return Directory.CreateDirectory(Path.Combine(TorrentApi.SavePath, torrent.InfoHash.ToString())).FullName;
        }
    }
}
