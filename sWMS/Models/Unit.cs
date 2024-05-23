using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Unit
    {
        public int UnitId { get; set; }
        public int UnitType { get; set; }
        public string UnitName { get; set; } = null!;
        public decimal UnitDividend { get; set; }
        public decimal UnitDivisor { get; set; }
    }
}
