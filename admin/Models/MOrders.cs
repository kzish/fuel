using System;
using System.Collections.Generic;

namespace admin.Models
{
    public partial class MOrders
    {
        public int Id { get; set; }
        public string Mobile { get; set; }
        public double? Latitude { get; set; }
        public double? Longitude { get; set; }
        public int FuelType { get; set; }
        public double Litres { get; set; }
        public DateTime Date { get; set; }
        public int State { get; set; }
    }
}
