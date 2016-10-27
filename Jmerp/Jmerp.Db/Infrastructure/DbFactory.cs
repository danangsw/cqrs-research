using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Infrastructure
{
    public class DbFactory : Disposable, IDbFactory
    {
        JmrepContext dbContext;

        public JmrepContext Init()
        {
            return dbContext ?? (dbContext = new JmrepContext());
        }

        protected override void DisposeCore()
        {
            if (dbContext != null)
            {
                dbContext.Dispose();
            }
        }
    }
}
