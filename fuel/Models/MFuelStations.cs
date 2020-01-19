using System;
using System.Collections.Generic;

namespace fuelApi.Models
{
    public partial class MFuelStations
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public double? DieselLitres { get; set; }
        public double? PetrolLitres { get; set; }
        public double? LpgGasLitres { get; set; }
        public double? DieselPrice { get; set; }
        public double? PetrolPrice { get; set; }
        public double? LpgGasPrice { get; set; }
    }
}
