using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    [ComplexType]
    public class Schedule
    {
        public virtual ICollection<CarrierMovement> CarrierMovements { get; set; }
    }
}
