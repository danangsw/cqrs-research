using Example.General.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Db.Model
{
    [Table("Cargo")]
    public class Cargo : BaseAggregate
    {
    }
}
