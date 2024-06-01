using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class ArticlesUnit
    {
        public int ArUId { get; set; }
        public int ArUType { get; set; }
        public int? ArUArtId { get; set; }
        public int? ArUArtType { get; set; }
        public int? ArUUnitId { get; set; }
        public int? ArUUnitType { get; set; }
        public string ArUName { get; set; } = null!;
        public decimal ArUDividend { get; set; }
        public decimal ArUDivisor { get; set; }
    }
}
