using System;
using System.Collections.Generic;

namespace sWMS.Models
{
    public partial class Item
    {
        public int? ItDocId { get; set; }
        public int? ItDocType { get; set; }
        public int? ItNo { get; set; }
        public string? ItCode { get; set; }
        public string? ItName { get; set; }
        public decimal? ItQuantity { get; set; }
        public string? ItDescription { get; set; }
        public DateTime? ItCompletionDate { get; set; }
        public int? ItUnitId { get; set; }
        public int? ItUnitType { get; set; }
        public int? ItUnitNo { get; set; }
        public int? ItSecondaryUnitId { get; set; }
        public int? ItSecondaryUnitType { get; set; }
        public int? ItSecondaryUnitNo { get; set; }
        public int? ItCodeCunId { get; set; }
        public int? ItCodeCunType { get; set; }
        public int? ItCodeCunNo { get; set; }
        public int? ItNameCunId { get; set; }
        public int? ItNameCunType { get; set; }
        public int? ItNameCunNo { get; set; }
        public int? ItSrcWhId { get; set; }
        public int? ItCurrentWhId { get; set; }
        public int? ItDstWhId { get; set; }
        public int? ItArtId { get; set; }
        public int? ItArtType { get; set; }
        public int? ItArtNo { get; set; }
        public int? ItArBId { get; set; }
        public int? ItArbType { get; set; }
        public int? ItArbNo { get; set; }
        public int? ItConId { get; set; }
        public int? ItConType { get; set; }
        public int? ItConNo { get; set; }
    }
}
