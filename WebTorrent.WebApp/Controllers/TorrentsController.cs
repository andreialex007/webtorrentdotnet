using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebTorrent.Domain.Services.Torrent;

namespace WebTorrent.WebApp.Controllers
{
    public class TorrentsController : ApiController
    {
        // GET api/torrents
        public IEnumerable<string> Get()
        {
            var torrentsList = TorrentEngine.Current.AllTorrents().Select(x => x.Name).ToList();
            return torrentsList;
        }

        // GET api/torrents/5
        public string Get(int id)
        {
            return "value";
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