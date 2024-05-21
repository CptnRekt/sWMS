using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Document
    {
        public int DocId { get; set; }
        public int? DocType { get; set; }
        public string? DocNumberString { get; set; }
        public int? DocNumber { get; set; }
        public int? DocMonth { get; set; }
        public int? DocYear { get; set; }
        public string? DocSeries { get; set; }
        public DateTime? DocCreationDate { get; set; }
        public DateTime? DocCompletionDate { get; set; }
    }
}
