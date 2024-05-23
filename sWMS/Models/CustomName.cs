using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class CustomName
    {
        public int CunId { get; set; }
        public int CunType { get; set; }
        public string CunName { get; set; } = null!;
        public int? CunArtId { get; set; }
        public int? CunArtType { get; set; }
        public int? CunConId { get; set; }
        public int? CunConType { get; set; }
    }
}
