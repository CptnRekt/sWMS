using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Article
    {
        public int ArtId { get; set; }
        public int ArtType { get; set; }
        public string ArtCode { get; set; } = null!;
        public string? ArtName { get; set; }
        public int? ArtDefaultPrimaryUnitId { get; set; }
        public int? ArtDefaultPrimaryUnitType { get; set; }
        public int? ArtDefaultSecondaryUnitId { get; set; }
        public int? ArtDefaultSecondaryUnitType { get; set; }
        public DateTime? ArtCreationDate { get; set; }
    }
}
