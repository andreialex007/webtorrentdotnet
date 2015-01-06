using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MonoTorrent.Client;
using MonoTorrent.Common;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Proxy.DynamicProxy;
using NHibernate.Tool.hbm2ddl;
using NUnit.Core;
using NUnit.Framework;
using WebTorrent.Data;
using WebTorrent.Domain.Entities;
using Environment = System.Environment;

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
            //            var settings = new EngineSettings { SavePath = _savePath };
            //            _clientEngine = new ClientEngine(settings);

            //            Torrent torrent = Torrent.lo
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

        [Test]
        public void TestNhibernateRead()
        {
            var dbConfig = SQLiteConfiguration
                .Standard
                .UsingFile("C:\\nhibernate.db")
                .ShowSql();

            var sessionFactory = Fluently
                .Configure()
                .Database(dbConfig)
                .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TorrentRecordMapping>())
                .BuildSessionFactory();


            using (var session = sessionFactory.OpenSession())
            {
                var torrentsList = session.QueryOver<TorrentRecord>()
                 .Select(x => x.Name)
                 .OrderBy(x => x.Name)
                 .Asc
                 .List<string>();
            }
        }
    }
}
