using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Extensions
{
    public static class DataExtensions
    {
        public static void DeleteById<T>(this ISession session, int id) where T : EntityBase
        {
            var record = session.Query<T>()
                       .OrderBy(x => x.Id)
                       .Select(x => x)
                       .Single();
            session.Delete(record);
        }
    }
}
