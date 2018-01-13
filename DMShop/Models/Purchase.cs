using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DMShop.Models
{
    public partial class Purchase
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public int ItemId { get; set; }
        public int Quantity { get; set; }
        public int PricePerUnit { get; set; }
        public int TotalPrice { get; set; }
        [DisplayName(displayName:  "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
