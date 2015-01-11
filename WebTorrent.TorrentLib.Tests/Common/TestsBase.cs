using System;
using NUnit.Framework;
using StructureMap;
using StructureMap.Graph;
using StructureMap.Pipeline;
//using WebTorrent.Domain.Wrappers;

namespace WebTorrent.TorrentLib.Tests.Common
{
    [TestFixture]
    public class TestsBase
    {
        [SetUp]
        public virtual void Setup()
        {
            Bootstrap();
        }

        public virtual void TearDown()
        {
        }

        public void Bootstrap()
        {
            ObjectFactory.Configure(config =>
                                    {
//                                        config.For<IClientEngine>().LifecycleIs<SingletonLifecycle>().Use<ClientEngineWrapper>();
//                                        config.For<ITorrentManager>().LifecycleIs<SingletonLifecycle>().Use<TorrentManagerWrapper>();
                                        config.Scan(
                                            x =>
                                            {
                                                x.AssembliesFromPath(Environment.CurrentDirectory);
                                                x.WithDefaultConventions()
                                                    .OnAddedPluginTypes(t => t.LifecycleIs(Lifecycles.Get<SingletonLifecycle>()));
                                            });
                                    });
        }
    }
}