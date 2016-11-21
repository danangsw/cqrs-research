using Example.General.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Example.Db.Model
{
    [Table("Location")]
    public class Location : BaseAggregate
    {
        public string Name { get; set; }
    }
}
