using System;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentRecord : EntityBase, INamedEntity
    {
        public override int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual byte[] Data { get; set; }
        public virtual TorrentState State { get; set; }
        public virtual DateTime? Completed { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, State: {2}, Completed: {3}", Id, Name, State, Completed);
        }
    }
}
