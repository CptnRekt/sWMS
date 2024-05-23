using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class ArticlesBatch
    {
        public int ArBId { get; set; }
        public int? ArBType { get; set; }
        public int? ArBNo { get; set; }
        public string? ArBCode { get; set; }
        public string? ArBName { get; set; }
        public decimal? ArBQuantity { get; set; }
        public int? ArBUnitId { get; set; }
        public int? ArBUnitType { get; set; }
        public int? ArBUnitNo { get; set; }
        public int? ArBSecondaryUnitId { get; set; }
        public int? ArBSecondaryUnitType { get; set; }
        public int? ArBSecondaryUnitNo { get; set; }
        public int? ArBArtId { get; set; }
        public int? ArBArtType { get; set; }
        public int? ArBArtNo { get; set; }
    }
}
