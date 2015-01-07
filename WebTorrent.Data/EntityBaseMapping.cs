using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Data
{
    public class EntityBaseMapping<T> : ClassMap<T> where T : EntityBase
    {
        public void MapEntityBase()
        {
            Map(x => x.Created).Column("Created");
            Map(x => x.Modified).Column("Modified");
        }
    }
}
