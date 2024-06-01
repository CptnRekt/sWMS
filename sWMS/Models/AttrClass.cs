using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class AttrClass
    {
        public int AtCId { get; set; }
        public int AtCType { get; set; }
        public string AtCName { get; set; } = null!;
        public string? AtCDefaultValue { get; set; }
        public string AtCDataType { get; set; } = null!;
    }
}
