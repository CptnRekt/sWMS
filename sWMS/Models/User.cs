﻿using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class User
    {
        public int UsrId { get; set; }
        public int UsrType { get; set; }
        public string UsrLogin { get; set; } = null!;
        public string? UsrPassword { get; set; }
        public bool? UsrAutologin { get; set; }
    }
}
