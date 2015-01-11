using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentInfoBlock<TTorentDataType>
    {
        public string Name { get; set; }
        public TTorentDataType Data { get; set; }
    }
}
