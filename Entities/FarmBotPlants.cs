using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class FarmBotPlant : BaseEntity
    {
        public Guid FarmBotId { get; set; }

        public FarmBot FarmBot { get; set; } 

        public Guid PlantId { get; set; }

        public Plant Plant { get; set; }
    }
}
