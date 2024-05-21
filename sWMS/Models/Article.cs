using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Article
    {
        public int ArtId { get; set; }
        public int? ArtType { get; set; }
        public int? ArtNo { get; set; }
        public string? ArtCode { get; set; }
        public string? ArtName { get; set; }
        public int? ArtDefaultUnitId { get; set; }
        public DateTime? ArtCreationDate { get; set; }
    }
}
