using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Entities.Enums;

namespace Entities
{
    public class Event : BaseEntity
    {
        public EventType Type {get; set;}

        [MaxLength(250)]
        public string Info { get; set; }
    }
}
