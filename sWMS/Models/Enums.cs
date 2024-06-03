using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sWMS.Models
{
    public static class Enums
    {
       public enum DataOperationsEnum
       {
            Add,
            Delete,
            Update
       }
        public enum WMSObjectTypesEnum
        {
            Contractor = 98,
            Warehouse = 99,
        }
    }
}
