using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonoTorrent.Client;
using NUnit.Framework;

namespace WebTorrent.TorrentLib.Tests
{
    [TestFixture]
    public class MainFunctionalityTests
    {
        private ClientEngine _clientEngine;
        private readonly string _savePath = Path.GetTempPath();

        [SetUp]
        public void SetUp()
        {
            var settings = new EngineSettings { SavePath = _savePath };
            _clientEngine = new ClientEngine(settings);
        }

        [TearDown]
        public void TearDown()
        {
            _clientEngine.Dispose();
        }

        [Test]
        public void DownloadTorrent()
        {
            var currentDirectory = Environment.CurrentDirectory;
        }
    }
}
