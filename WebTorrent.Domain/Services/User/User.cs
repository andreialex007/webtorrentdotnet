using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.User
{
    public class User : EntityBase, INamedEntity
    {
        public override int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
