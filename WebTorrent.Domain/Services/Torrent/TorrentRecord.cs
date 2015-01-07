using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentRecord : EntityBase, INamedEntity
    {
        public override int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual TorrentState State { get; set; }
    }
}
