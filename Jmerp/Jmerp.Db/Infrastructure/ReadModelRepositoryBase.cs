using Jmerp.Db.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Infrastructure
{
    public abstract class ReadModelRepositoryBase<T> where T : class
    {

        private JmrepContext dbContext;
        public readonly IDbSet<T> dbSet;

        protected IDbFactory DbFactory
        {
            get;
            private set;
        }

        protected JmrepContext DbContext
        {
            get { return dbContext ?? (dbContext = DbFactory.Init()); }
        }

        protected ReadModelRepositoryBase(IDbFactory dbFactory)
        {
            DbFactory = dbFactory;
            dbSet = DbContext.Set<T>();
        }

        public virtual void Add(T entity)
        {
            dbSet.Add(entity);
        }

        public virtual void Update(T entity)
        {
            dbSet.Attach(entity);
            DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual void Delete(T entity)
        {
            dbSet.Remove(entity);
        }

        public virtual T GetByIndex(long id)
        {
            return dbSet.Find(id);
        }

        public virtual IEnumerable<T> GetList(Func<T,bool> where)
        {
            return dbSet.Where(where);
        }

    }
}
