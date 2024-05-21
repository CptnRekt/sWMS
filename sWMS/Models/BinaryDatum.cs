using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class BinaryDatum
    {
        public int BinDId { get; set; }
        public int? BinDType { get; set; }
        public int? BinDNo { get; set; }
        public byte[]? BinDContent { get; set; }
    }
}
