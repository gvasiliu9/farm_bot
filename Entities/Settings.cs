using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Entites
{
    public class Settings: BaseEntity
    {
        public Guid FarmBotId { get; set; }

        [JsonIgnore]
        public FarmBot FarmBot { get; set; }

        public Guid PlantId { get; set; }

        [JsonIgnore]
        public Plant Plant { get; set; }

        public Guid UserId { get; set; }
    }
}
