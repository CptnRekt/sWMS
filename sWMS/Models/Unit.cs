using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Unit
    {
        public int UnitId { get; set; }
        public int? UnitType { get; set; }
        public int? UnitNo { get; set; }
        public string? UnitName { get; set; }
        public decimal? UnitDividend { get; set; }
        public decimal? UnitDivisor { get; set; }
        public int? UnitAttachedToArtId { get; set; }
        public int? UnitAttachedToArtType { get; set; }
        public int? UnitAttachedToArtNo { get; set; }
    }
}
