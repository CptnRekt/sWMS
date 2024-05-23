using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class AttrClass
    {
        public AttrClass()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int AtCId { get; set; }
        public int AtCType { get; set; }
        public string AtCName { get; set; } = null!;
        public string AtCDataType { get; set; } = null!;

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
