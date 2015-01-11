using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebTorrent.Domain.Services.Torrent
{
    public class TorrentDto : ITorrentComonInfo
    {
        /// <summary>
        /// Ид торрента в базе
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название торрента
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Размер торрента
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Загружается ли на данный момент торрент
        /// </summary>
        private bool IsLoading
        {
            get { return State == TorrentState.Downloading; }
        }

        /// <summary>
        /// Процент загрузки
        /// </summary>
        public decimal DownloadingPercentage { get; set; }

        /// <summary>
        /// Данные торрент файла
        /// </summary>
        public byte[] Data { get; set; }

        /// <summary>
        /// Состояние загруженности торрента
        /// </summary>
        public TorrentState State { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime? Created { get; set; }

        /// <summary>
        /// Дата завершения загрузки
        /// </summary>
        public DateTime? Completed { get; set; }

        /// <summary>
        /// Дата изменения торрента
        /// </summary>
        public DateTime? Modified { get; set; }
    }
}
