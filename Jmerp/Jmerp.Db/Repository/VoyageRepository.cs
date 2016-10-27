using Jmerp.Db.Infrastructure;
using Jmerp.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Repository
{
    public class VoyageRepository : ReadModelRepositoryBase<Voyage>, IReadModelRepository<Voyage>, IVoyageRepository
    {
        public VoyageRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public Voyage GetById(string keyBusiness)
        {
            return base.dbSet.Where(x => x.Id == keyBusiness).SingleOrDefault();
        }
    }

    public interface IVoyageRepository
    {
        Voyage GetById(string keyBusiness);
    }
}
