using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class BinaryData
    {
        public int BinDId { get; set; }
        public int BinDType { get; set; }
        public byte[] BinDContent { get; set; } = null!;
    }
}
