using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace WebTorrent.Domain.Services._Common
{
    public static class NHibertnateSession
    {
        public static Func<ISessionFactory> FactoryCreator;
        public static Func<ISession> SessionCreator = () => FactoryCreator().OpenSession();

        public static ISession OpenSession()
        {
            return SessionCreator();
        }
    }
}
