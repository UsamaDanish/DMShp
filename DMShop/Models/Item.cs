using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DMShop.Models
{
    public partial class Item
    {
        public int Id { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
       
        public int Quantity { get; set; }
        [Required]
        
        public int Price { get; set; }
        [Required]
        [MaxLength(1000)]
        public string Image { get; set; }
        [Required]
        [MaxLength(20)]
        public string ModelNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Color { get; set; }
        public DateTime Date { get; set; }
    }
}
