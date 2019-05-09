using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entites
{
    public class BaseEntity
    {
        public Guid Id { get; set; }

        public DateTime Created { get; set; }

        public DateTime Updated { get; set; }
    }
}
