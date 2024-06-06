using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sWMS.Models
{
    public class SQLParameter
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public SQLParameter(string Name, object Value) 
        {
            this.Name = Name;
            this.Value = Value;
        }
    }
}
