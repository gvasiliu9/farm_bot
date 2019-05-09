using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entites
{
    public class Plant: BaseEntity
    {
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        [MaxLength(250)]
        public string Info { get; set; }

        public int RowDistance { get; set; }

        public short SeedDepth { get; set; }

        public byte WaterQuanity { get; set; }

        public byte IrigationsPerDay { get; set; }

        public short Duration { get; set; }
    }
}
