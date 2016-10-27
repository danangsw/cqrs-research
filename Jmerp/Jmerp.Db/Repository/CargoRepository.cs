using Jmerp.Db.Infrastructure;
using Jmerp.Db.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Repository
{
    public class CargoRepository : ReadModelRepositoryBase<Cargo>, IReadModelRepository<Cargo>, ICargoRepository
    {
        public CargoRepository(IDbFactory dbFactory) : base(dbFactory)
        {

        }

        public Cargo GetById(string keyBusiness)
        {
            return base.dbSet.Where(x => x.Id == keyBusiness).SingleOrDefault();
        }
    }


    public interface ICargoRepository
    {
        Cargo GetById(string keyBusiness);
    }
}
