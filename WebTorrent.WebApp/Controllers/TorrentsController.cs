using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Common.Utils;
using Microsoft.AspNet.SignalR;
using WebTorrent.Domain.Services;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.WebApp.Code.Extensions;
using WebTorrent.WebApp.Controllers.Common;
using WebTorrent.WebApp.Hubs;

namespace WebTorrent.WebApp.Controllers
{
    [RoutePrefix("api/torrents")]
    public class TorrentsController : ApiControllerBase
    {
        static TorrentsController()
        {
            TorrentInfoUpdater.Event = () =>
                                       {
                                           GlobalHost.ConnectionManager.GetHubContext<AppHub>().Clients.All.myTestFunction();
                                       };
        }

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


            Task.Run(() =>
            {
                Thread.Sleep(5000);
                GlobalHost.ConnectionManager.GetHubContext<AppHub>().Clients.All.myTestFunction();
            });


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