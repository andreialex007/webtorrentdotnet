using System;

namespace WebTorrent.Domain.Services.Torrent
{
    public interface ITorrentComonInfo
    {
        /// <summary>
        /// Ид торрента в базе
        /// </summary>
        int Id { get; set; }

        /// <summary>
        /// Название торрента
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Размер торрента
        /// </summary>
        long Size { get; set; }

        /// <summary>
        /// Процент загрузки
        /// </summary>
        decimal DownloadingPercentage { get; set; }

        /// <summary>
        /// Состояние загруженности торрента
        /// </summary>
        TorrentState State { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        DateTime? Created { get; set; }

        /// <summary>
        /// Дата завершения загрузки
        /// </summary>
        DateTime? Completed { get; set; }

        /// <summary>
        /// Дата изменения торрента
        /// </summary>
        DateTime? Modified { get; set; }
    }
}