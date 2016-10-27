using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public interface IReadModelEntity
    {
        Nullable<int> MsSqlReadModelVersionColumn { get; set; }
    }
}
