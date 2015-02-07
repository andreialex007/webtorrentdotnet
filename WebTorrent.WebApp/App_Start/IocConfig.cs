using System;
using System.IO;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using Common.Utils;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
using StructureMap.Web.Pipeline;
using WebTorrent.Data;
using WebTorrent.Domain.Services.Torrent;
using WebTorrent.Domain.Services.User;
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
                                                        .ConnectionString(@"Data Source=NOTEBOOK-PC;Initial Catalog=TorrentDb;Integrated Security=True")
                                                        .ShowSql();

                                                    var sessionFactory = Fluently
                                                        .Configure()
                                                        .Database(dbConfig)
                                                        .Mappings(x => x.FluentMappings.AddFromAssemblyOf<TorrentRecordMapping>())
                                                        .BuildSessionFactory();

                                                    return sessionFactory;
                                                };

            // GlobalConfiguration.Configuration.DependencyResolver = new WebApiDependcyResolver();

            ObjectFactory.Configure(config =>
                                    {
                                        config.For<IUserService>().Use<UserService>();
                                        config.Scan(
                                            x =>
                                            {
                                                x.AssembliesFromPath(assembliesPath);
                                                x.WithDefaultConventions()
                                                    .OnAddedPluginTypes(t => t.LifecycleIs(Lifecycles.Get<HttpContextLifecycle>()));

                                            });
                                    });

            ControllerBuilder.Current.SetControllerFactory(new StructureMapControllerFactory());
            IoC.ResolvingExpression = ObjectFactory.GetInstance;
        }
    }
}