﻿using Jmerp.Commons.Database;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jmerp.Db.Model
{
    [Table("Location")]
    public class Location : BaseAggregate
    {
        public string Name { get; set; }
    }
}
