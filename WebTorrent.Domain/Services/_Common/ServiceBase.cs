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

        public void Delete(int id)
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var userRecord = session.Query<UserRecord>()
                        .OrderBy(x => x.Id)
                        .Select(x => x)
                        .Single();
                    session.Delete(userRecord);
                    transaction.Commit();
                }
            }
        }
    }
}
