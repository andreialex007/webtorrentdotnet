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
        #region Private members

        private Timer _timer;
        private TorrentFullInfo _torrentFullInfo;
        private const int UpdateTimeInterval = 1000;

        private void OnTimerEvent()
        {
            var handler = TimerEvent;
            if (handler != null)
                handler(_torrentFullInfo);
        }

        #endregion

        #region Public members

        public void StartUpdate(int torrentId)
        {
            _timer = new Timer();
            _timer.Elapsed += (sender, args) =>
                              {
                                  _torrentFullInfo = TorrentEngine.Current.GetTorrentInfoBlocks(torrentId);
                              };
            _timer.Interval = UpdateTimeInterval;
            _timer.Enabled = true;

        }

        public void StopUpdate()
        {
            _timer.Dispose();
            _timer = null;
        }

        public event Action<TorrentFullInfo> TimerEvent = null;

        #endregion
    }
}
