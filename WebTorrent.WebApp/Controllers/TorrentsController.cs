using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using Common.Utils;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.WebApp.Code.Extensions;
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
            List<TorrentDto> torrentsList = TorrentEngine.Current.AllTorrents().ToList();
            return torrentsList;
        }

        [Route(@"{state}")]
        public IEnumerable<TorrentDto> Get(string state)
        {
            var torrentState = (TorrentState)Enum.Parse(typeof(TorrentState), state.UppercaseFirst());
            List<TorrentDto> torrents = TorrentEngine.Current.TorrentsWithState(torrentState);
            return torrents;
        }

        #endregion

        #region Write methods

        [Route(@"upload")]
        public void Post()
        {
            HttpPostedFile file = HttpContext.Current.Request.PostedFile();
            TorrentEngine.Current.Add(new TorrentDto
                                      {
                                          Name = string.Empty,
                                          Data = file.ToByteArray()
                                      });
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        [Route(@"{id:int}")]
        public void Delete(int id)
        {
            TorrentEngine.Current.Delete(id);
        }

        #endregion
    }
}