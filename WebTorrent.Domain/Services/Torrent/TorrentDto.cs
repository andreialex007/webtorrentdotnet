using System;
using Common.Utils;

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


        public string SizeString
        {
            get { return Size.BytesToString(); }
        }

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

        public string StateName
        {
            get { return State.ToString(); }
        }

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


        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, Size: {2}, IsLoading: {3}, DownloadingPercentage: {4}, State: {5}, StateName: {6}", Id, Name, Size, IsLoading, DownloadingPercentage, State, StateName);
        }
    }
}
