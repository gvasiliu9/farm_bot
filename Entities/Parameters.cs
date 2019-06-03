using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Entities
{
    public class Parameters: BaseEntity
    {
        public Guid FarmBotId { get; set; }

        [JsonIgnore]
        public FarmBot FarmBot { get; set; }

        public byte Luminosity { get; set; }

        public byte AirTemperature { get; set; }

        public byte SoilHumidity { get; set; }

        public int SeededPlants { get; set; }
    }
}
