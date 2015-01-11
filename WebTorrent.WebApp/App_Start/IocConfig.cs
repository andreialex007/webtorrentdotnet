using System;
using System.IO;
using System.Web;
using Common.Utils;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
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
    // ReSharper disable CSharpWarnings::CS0618

    public class IocConfig
    {
        public static void Bootstrap()
        {
            var assembliesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "bin");

            TorrentEngine.CurrentUserTorrentEngineCreator = () =>
                                                            {
                                                                if (HttpContext.Current.Session["TorrentEngine"] == null)
                                                                    HttpContext.Current.Session["TorrentEngine"] = new TorrentEngine(new TorrentApi());

                                                                var torrentEngine = (TorrentEngine)HttpContext.Current.Session["TorrentEngine"];
                                                                return torrentEngine;
                                                            };

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