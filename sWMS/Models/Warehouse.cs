using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Warehouse
    {
        public int WhId { get; set; }
        public int WhType { get; set; }
        public string WhCode { get; set; } = null!;
        public string WhName { get; set; } = null!;
        public string? WhCountry { get; set; }
        public string? WhCity { get; set; }
        public string? WhStreet { get; set; }
        public string? WhPostal { get; set; }
        public int? WhIssuesNumber { get; set; }
        public int? WhAcceptancesNumber { get; set; }
    }
}
