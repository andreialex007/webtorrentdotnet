using System.IO;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MonoTorrent.Client;
using MonoTorrent.Common;
using NHibernate.Linq;
using NUnit.Framework;
using WebTorrent.Data;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services._Common;
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
            SetupTorrent();
        }

        private void SetupTorrent()
        {
            var settings = new EngineSettings { SavePath = _savePath };
            _clientEngine = new ClientEngine(settings);


            //            var torrent = Torrent.Load(null);
            //            new Torrent();
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
            NHibertnateSession.FactoryCreator = () =>
                                                {

                                                    var dbConfig = MsSqlConfiguration.MsSql2012
                                                        .ConnectionString(@"Data Source=ANDREI-PC\MSSQLSERVER2012;Initial Catalog=TorrentDb;Integrated Security=True")
                                                        .ShowSql();

                                                    //                                                    var dbConfig = SQLiteConfiguration
                                                    //                                                        .Standard
                                                    //                                                        .UsingFile("C:\\nhibernate.db")
                                                    //                                                        .ShowSql();

                                                    var sessionFactory = Fluently
                                                        .Configure()
                                                        .Database(dbConfig)
                                                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TorrentRecordMapping>())
                                                        .BuildSessionFactory();

                                                    return sessionFactory;
                                                };




            using (var session = NHibertnateSession.OpenSession())
            {
                var records = session.Query<TorrentRecord>().Select(x => new TorrentDto
                                                                       {
                                                                           Id = x.Id,
                                                                           Name = x.Name,
                                                                           Created = x.Created,
                                                                           State = x.State
                                                                       }).ToList();
            }
        }
    }
}
