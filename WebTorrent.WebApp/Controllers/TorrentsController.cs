using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Common.Utils;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.WebApp.Code.Extensions;
using WebTorrent.WebApp.Controllers.Common;

namespace WebTorrent.WebApp.Controllers
{
    [System.Web.Http.RoutePrefix("api/torrents")]
    public class TorrentsController : ApiControllerBase
    {
        #region Read methods

        [System.Web.Http.Route(@"")]
        public IEnumerable<TorrentDto> Get()
        {
            var torrentsList = TorrentEngine.Current.AllTorrents().ToList();
            return torrentsList;
        }

        [System.Web.Http.Route(@"{state}")]
        public IEnumerable<TorrentDto> Get(string state)
        {
            var torrentState = (TorrentState)Enum.Parse(typeof(TorrentState), state.UppercaseFirst());
            var torrents = TorrentEngine.Current.TorrentsWithState(torrentState);
            return torrents;
        }

        //
        //        public void Post(string fileName)
        //        {
        //
        //            //            var torrentDto = new TorrentDto
        //            //                             {
        //            //                                 Data = file.ToByteArray()
        //            //                             };
        //            //            TorrentEngine.Current.Add(torrentDto);
        //        }

        #endregion

        #region Write methods

        [System.Web.Http.Route(@"upload")]
        public void Post()
        {
            var file = HttpContext.Current.Request.PostedFile();
            TorrentEngine.Current.Add(new TorrentDto
                                      {
                                          Name = string.Empty,
                                          Data = file.ToByteArray()
                                      });
        }

        public void Put(int id, [FromBody] string value)
        {
        }

        public void Delete(int id)
        {
            TorrentEngine.Current.Delete(id);
        }

        #endregion
    }
}