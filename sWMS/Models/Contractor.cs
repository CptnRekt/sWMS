using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Contractor
    {
        public int ConId { get; set; }
        public int? ConType { get; set; }
        public int? ConNo { get; set; }
        public string? ConCode { get; set; }
        public string? ConFullName { get; set; }
        public string? ConCountry { get; set; }
        public string? ConCity { get; set; }
        public string? ConStreet { get; set; }
        public string? ConPostal { get; set; }
        public int? ConCodeCunId { get; set; }
        public int? ConCodeCunType { get; set; }
        public int? ConCodeCunNo { get; set; }
        public int? ConNameCunId { get; set; }
        public int? ConNameCunType { get; set; }
        public int? ConNameCunNo { get; set; }
        public int? ConLogoBinDId { get; set; }
        public int? ConLogoBinDType { get; set; }
        public int? ConLogoBinDNo { get; set; }
    }
}
