namespace WebTorrent.Domain.Entities
{
    public class TorrentRecord
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual TorrentRecordState State { get; set; }
    }
}
