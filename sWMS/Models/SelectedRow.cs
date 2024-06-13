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
    public class SelectedRow
    {
        public WMSObjectTypes Type { get; set; }
        public DataRow AffectedRow { get; set; }
    }
}
