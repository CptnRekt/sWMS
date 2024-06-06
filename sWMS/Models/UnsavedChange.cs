using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static sWMS.Models.Enums;

namespace sWMS.Models
{
    public class UnsavedChange
    {
        public int Index { get; set; }
        public WMSObjectTypesEnum Type { get; set; }
        public DataOperationsEnum DataOperation { get; set; }
        public int AffectedData { get; set; }
    }
}
