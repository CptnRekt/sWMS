using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Contractor
    {
        public int ConId { get; set; }
        public int ConType { get; set; }
        public string ConCode { get; set; } = null!;
        public string? ConFullName { get; set; }
        public string? ConCountry { get; set; }
        public string? ConCity { get; set; }
        public string? ConStreet { get; set; }
        public string? ConPostal { get; set; }
        public int? ConLogoBinDId { get; set; }
        public int? ConLogoBinDType { get; set; }
    }
}
