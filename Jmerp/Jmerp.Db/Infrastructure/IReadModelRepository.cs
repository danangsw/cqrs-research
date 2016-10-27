using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Infrastructure
{
    public interface IReadModelRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        T GetByIndex(long idIndex);
        IEnumerable<T> GetList(Func<T,bool> where);
    }
}
