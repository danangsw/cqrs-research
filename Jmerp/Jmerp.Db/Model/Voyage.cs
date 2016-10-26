using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    public class Voyage : BaseEntity
    {
        
        public string Id { get; set; }
        public Schedule Schedule { get; set; }

    }
}
