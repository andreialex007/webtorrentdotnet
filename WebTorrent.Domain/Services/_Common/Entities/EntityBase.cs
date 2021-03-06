﻿using System;
using System.ComponentModel.DataAnnotations;

namespace WebTorrent.Domain.Services._Common.Entities
{
    public abstract class EntityBase : IIdEntity
    {
        /// <summary>
        /// Идентификатор объекта в базе данных
        /// </summary>
        public abstract int Id { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public virtual DateTime? Created { get; set; }

        /// <summary>
        /// Дата изменения
        /// </summary>
        public virtual DateTime? Modified { get; set; }
    }
}
