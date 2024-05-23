using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Item
    {
        public Item()
        {
            Attributes = new HashSet<Attribute>();
        }

        public int ItObjectId { get; set; }
        public int ItObjectType { get; set; }
        public int ItObjectItem { get; set; }
        public string ItCode { get; set; } = null!;
        public string? ItName { get; set; }
        public decimal ItQuantity { get; set; }
        public decimal ItRealizedQuantity { get; set; }
        public string? ItDescription { get; set; }
        public DateTime? ItCompletionDate { get; set; }
        public int? ItUnitId { get; set; }
        public int? ItUnitType { get; set; }
        public int? ItArtId { get; set; }
        public int? ItArtType { get; set; }

        public virtual ICollection<Attribute> Attributes { get; set; }
    }
}
