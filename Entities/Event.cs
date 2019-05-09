using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using static Entites.Enums;

namespace Entites
{
    public class Event : BaseEntity
    {
        public EventType Type {get; set;}

        [MaxLength(250)]
        public string Info { get; set; }
    }
}
