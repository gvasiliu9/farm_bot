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

        [MaxLength(50)]
        public string IpCameraAddress { get; set; }

        public int LastX{ get; set; }

        public int LastY{ get; set; }

        public int Width{ get; set; }

        public int Length{ get; set; }
    }
}
