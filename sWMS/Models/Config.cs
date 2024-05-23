using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Config
    {
        public int ConfId { get; set; }
        public int ConfType { get; set; }
        public string ConfCodeName { get; set; } = null!;
        public string ConfName { get; set; } = null!;
        public string ConfValue { get; set; } = null!;
    }
}
