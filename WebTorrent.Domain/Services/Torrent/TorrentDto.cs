using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentDto
    {
        public int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual TorrentState State { get; set; }
    }
}
