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
        private const string CommandStart = "start";
        private const string CommandStop = "stop";
        private const string CommandPause = "pause";
        private const int UpdatePause = 2500;

        static TorrentsController()
        {
            TorrentInfoUpdater.Event = () =>
                                       {
                                           GlobalHost.ConnectionManager.GetHubContext<AppHub>().Clients.All.updateTorrents();
                                       };
        }

        #region Read methods

        [Route(@"command/{id:int}/{command}")]
        [HttpGet]
        public void Command(int id, string command)
        {
            

            if (command == CommandStart)
            {
                TorrentEngine.Current.Start(id);
            }
            else if (command == CommandStop)
            {
                TorrentEngine.Current.Stop(id);
            }
            else if (command == CommandPause)
            {
                TorrentEngine.Current.Pause(id);
            }
        }

        [Route(@"{id:int}")]
        [HttpGet]
        public TorrentFullInfo Get(int id)
        {
            var info = TorrentEngine.Current.GetTorrentInfoBlocks(id);
            return info;
        }

        [Route(@"{state}")]
        public IEnumerable<TorrentDto> Get(string state)
        {
            if (state == "all")
                return TorrentEngine.Current.AllTorrents();

            var torrentState = (TorrentState)Enum.Parse(typeof(TorrentState), state.UppercaseFirst());
            var torrents = TorrentEngine.Current.TorrentsWithState(torrentState);


            Task.Run(() =>
            {
                Thread.Sleep(UpdatePause);
                GlobalHost.ConnectionManager.GetHubContext<AppHub>().Clients.All.myTestFunction();
            });

            return torrents;
        }

        #endregion

        #region Write methods

        [Route(@"upload")]
        [HttpPost]
        public void Post()
        {
            HttpPostedFile file = HttpContext.Current.Request.PostedFile();
            TorrentEngine.Current.Add(new TorrentDto
                                      {
                                          Name = string.Empty,
                                          Data = file.ToByteArray()
                                      });
        }

        [HttpPut]
        public void Put(int id, [FromBody] string value)
        {
        }

        [Route(@"{id:int}")]
        [HttpDelete]
        public void Delete(int id)
        {
            TorrentEngine.Current.Delete(id);
        }

        #endregion
    }
}