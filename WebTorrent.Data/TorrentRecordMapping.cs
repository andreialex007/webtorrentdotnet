using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WebTorrent.Domain;
using WebTorrent.Domain.Entities;

namespace WebTorrent.Data
{
    public class TorrentRecordMapping : ClassMap<TorrentRecord>
    {
        public TorrentRecordMapping()
        {
            Table("TorrentRecords");

            Id(x => x.Id).Column("Id");
            Map(x => x.Name).Column("Name");
            Map(x => x.Data).Column("Data");
            Map(x => x.State).Column("State");
        }
    }
}
