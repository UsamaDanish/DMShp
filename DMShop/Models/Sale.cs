using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMShop.Models
{
    public partial class Sale
    {
        public int Id { get; set; }
        [Required]
        public int ItemId { get; set; }
        [Required]
        public int CustomerId { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public int PricePerUnit { get; set; }
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-mm-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        [Required]
        public int TotalPrice { get; set; }
    }
}
