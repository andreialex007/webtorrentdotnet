using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Mapping;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services.User;

namespace WebTorrent.Data
{
    public class UserRecordMapping : ClassMap<UserRecord>
    {
        public UserRecordMapping()
        {
            Table("Users");

            Id(x => x.Id)
                .Column("Id")
                .GeneratedBy
                .Identity();

            Map(x => x.Name)
                .Column("Name");
            Map(x => x.Email)
                .Column("Email");
            Map(x => x.Password)
                .Column("Password");
            Map(x => x.Role)
                .Column("Role");
        }
    }
}
