using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NHibernate;
using NHibernate.Linq;
using WebTorrent.Domain.Services.User;

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
