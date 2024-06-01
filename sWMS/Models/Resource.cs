using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Resource
    {
        public int ResId { get; set; }
        public int ResType { get; set; }
        public int? ResWhId { get; set; }
        public string ResBatchCode { get; set; } = null!;
        public string ResBatchName { get; set; } = null!;
        public int? ResArtId { get; set; }
        public int? ResArtType { get; set; }
        public int? ResUnitId { get; set; }
        public int? ResUnitType { get; set; }
        public string? ResUnitName { get; set; }
        public int? ResSecondaryUnitId { get; set; }
        public int? ResSecondaryUnitType { get; set; }
        public decimal? ResQuantity { get; set; }
    }
}
