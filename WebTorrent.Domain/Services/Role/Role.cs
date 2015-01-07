using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.Role
{
    public class Role : EntityBase, INamedEntity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
    }
}
