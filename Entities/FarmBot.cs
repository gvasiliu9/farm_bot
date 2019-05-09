using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entites
{
    public class FarmBot: BaseEntity
    {
        [MaxLength(75)]
        public string Name { get; set; }

        [MaxLength(15)]
        public string IpAddress { get; set; }
    }
}
