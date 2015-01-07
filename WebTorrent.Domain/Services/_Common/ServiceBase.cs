using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;

namespace WebTorrent.Domain.Services._Common
{
    public class ServiceBase
    {
        protected virtual ISession OpenSession()
        {
            return NHibertnateSession.OpenSession();
        }
    }
}
