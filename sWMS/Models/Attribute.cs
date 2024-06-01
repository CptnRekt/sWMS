using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Attribute
    {
        public int AttrId { get; set; }
        public int AttrType { get; set; }
        public int AttrObjectId { get; set; }
        public int AttrObjectType { get; set; }
        public int AttrObjectItem { get; set; }
        public int AttrObjectSubitem { get; set; }
        public int AttrAtCId { get; set; }
        public int AttrAtCType { get; set; }
        public string AttrValue { get; set; } = null!;
    }
}
