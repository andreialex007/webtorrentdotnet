using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebTorrent.Domain.Services.Torrent;

namespace WebTorrent.WebApp.Controllers
{
    public class TorrentsController : ApiController
    {
        // GET api/torrents
        public IEnumerable<TorrentDto> Get()
        {
            var torrentsList = TorrentEngine.Current.AllTorrents().ToList();
            return torrentsList;
        }

        // GET api/torrents/5
        public TorrentDto Get(int id)
        {
            var torrent = TorrentEngine.Current.GetTorrentById(id);
            return torrent;
        }

        // POST api/torrents
        public void Post([FromBody] string value)
        {
        }

        // PUT api/torrents/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/torrents/5
        public void Delete(int id)
        {
        }
    }
}