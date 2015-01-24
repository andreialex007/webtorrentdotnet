using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using WebTorrent.Domain.Services.Torrent;

namespace WebTorrent.Domain.Services
{
    /// <summary>
    /// Осуществляет периодический переопрос движка TorrentEngine 
    /// для получения данных о торренте
    /// </summary>
    public class TorrentInfoUpdater
    {
        private static Timer _timer;
        private static Action _event;
        private const int UpdateTimeInterval = 5000;

        public static Action Event
        {
            get { return _event; }
            set
            {
                _event = value;

                if (_timer != null)
                {
                    _timer.Dispose();
                    _timer = null;
                }

                _timer = new Timer();
                _timer.Elapsed += (sender, args) => _event();
                _timer.Interval = UpdateTimeInterval;
                _timer.Enabled = true;
            }
        }
    }
}
