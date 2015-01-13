using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using Common.Utils;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [RoutePrefix("api/torrents")]
    public class TorrentsController : ApiControllerBase
    {
        #region Read methods

        [Route(@"")]
        public IEnumerable<TorrentDto> Get()
        {
            var torrentsList = TorrentEngine.Current.AllTorrents().ToList();
            return torrentsList;
        }

        [Route(@"{state}")]
        public IEnumerable<TorrentDto> Get(string state)
        {
            var torrentState = (TorrentState)Enum.Parse(typeof(TorrentState), state.UppercaseFirst());
            var torrents = TorrentEngine.Current.TorrentsWithState(torrentState);
            return torrents;
        }

        #endregion

        #region Write methods

        public void Post([FromBody] string value)
        {
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
        }

        #endregion
    }
}