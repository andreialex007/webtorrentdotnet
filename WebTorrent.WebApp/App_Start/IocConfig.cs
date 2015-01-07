using System;
using System.IO;
using System.Web;
using Common.Utils;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using MonoTorrent.Client;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;
using WebTorrent.Data;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services._Common;
using WebTorrent.TorrentLib;

namespace WebTorrent.WebApp
{
    public class IocConfig
    {
        public static void Bootstrap()
        {
            var assembliesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            TorrentEngine.CurrentUserTorrentEngineCreator = () =>
                                                            {
                                                                if (HttpContext.Current.Session["TorrentEngine"] == null)
                                                                {
                                                                    var clientEngine = new ClientEngine(new EngineSettings());
                                                                    var clientEngineWrapper = new ClientEngineWrapper(clientEngine);
                                                                    HttpContext.Current.Session["TorrentEngine"] = new TorrentEngine(clientEngineWrapper);
                                                                }
                                                                var torrentEngine = (TorrentEngine)HttpContext.Current.Session["TorrentEngine"];
                                                                return torrentEngine;
                                                            };

            NHibertnateSession.FactoryCreator = () =>
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

                return sessionFactory;
            };

            ObjectFactory.Configure(config =>
            {
                config.Scan(
                    x =>
                    {
                        x.AssembliesFromPath(assembliesPath);
                        x.WithDefaultConventions()
                            .OnAddedPluginTypes(t => t.LifecycleIs(Lifecycles.Get<HttpContextLifecycle>()));

                    });
            });

            IoC.ResolvingExpression = ObjectFactory.GetInstance;
        }
    }
}