﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebTorrent.Domain.Services._Common.Entities;

namespace WebTorrent.Domain.Services.User
{
    /// <summary>
    /// Пользователь осуществляющий доступ к системе
    /// </summary>
    public class UserRecord : EntityBase, INamedEntity
    {
        public override int Id { get; set; }
        [Required]
        public virtual string Name { get; set; }
        [Required]
        public virtual string Email { get; set; }
        [Required]
        public virtual string Password { get; set; }
        [Required]
        public virtual string Role { get; set; }
    }
}