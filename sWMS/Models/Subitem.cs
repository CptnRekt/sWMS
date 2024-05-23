using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Subitem
    {
        public Subitem()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int SitObjectId { get; set; }
        public int SitObjectType { get; set; }
        public int SitObjectItem { get; set; }
        public int SitObjectSubitem { get; set; }
        public decimal SitQuantity { get; set; }
        public decimal SitRealizedQuantity { get; set; }
        public int? SitUnitId { get; set; }
        public int? SitUnitType { get; set; }
        public int? SitSecondaryUnitId { get; set; }
        public int? SitSecondaryUnitType { get; set; }
        public int? SitArtId { get; set; }
        public int? SitArtType { get; set; }
        public int? SitResId { get; set; }
        public int? SitResType { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
