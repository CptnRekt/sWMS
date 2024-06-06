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
        public int? SQL_Id { get; set; }
        public WMSObjectTypesEnum SQL_Type { get; set; }
        public DataOperationsEnum DataOperation { get; set; }
        //public DataTable? AffectedData { get; set; }
    }
}
