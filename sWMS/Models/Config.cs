using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Config
    {
        public int ConfId { get; set; }
        public string? ConfCodeName { get; set; }
        public string? ConfName { get; set; }
        public string? ConfValue { get; set; }
    }
}
