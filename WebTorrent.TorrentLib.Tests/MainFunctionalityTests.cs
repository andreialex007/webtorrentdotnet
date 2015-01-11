using System.IO;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Linq;
using NUnit.Framework;
using WebTorrent.Data;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services._Common;

namespace WebTorrent.TorrentLib.Tests
{
    [TestFixture]
    public class MainFunctionalityTests
    {
        #region SetupTearDown

        [SetUp]
        public void SetUp()
        {
        }

        [TearDown]
        public void TearDown()
        {
        }

        #endregion

        #region TestMethods

        [Test]
        public void TestTorrent()
        {
            var torrentApi = new TorrentApi();
            var bytes = File.ReadAllBytes(@"C:\test.torrent");
            torrentApi.AddTorrent(new TorrentDto {Data = bytes, Id = 10});
        }

        [Test]
        public void TestNhibernateRead()
        {
            NHibertnateSession.FactoryCreator = () =>
                                                {

                                                    var dbConfig = MsSqlConfiguration.MsSql2012
                                                        .ConnectionString(@"Data Source=ANDREI-PC\MSSQLSERVER2012;Initial Catalog=TorrentDb;Integrated Security=True")
                                                        .ShowSql();

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

        #endregion
    }
}
