using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Document
    {
        public Document()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int DocObjectId { get; set; }
        public int DocObjectType { get; set; }
        public string DocNumberString { get; set; } = null!;
        public int DocNumber { get; set; }
        public int DocMonth { get; set; }
        public int DocYear { get; set; }
        public string DocSeries { get; set; } = null!;
        public DateTime DocCreationDate { get; set; }
        public DateTime? DocCompletionDate { get; set; }
        public int? DocSourceWhId { get; set; }
        public int? DocDestinationWhId { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
