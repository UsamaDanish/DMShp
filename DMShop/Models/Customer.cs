using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DMShop.Models
{
    public partial class Customer
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [MaxLength(14)]
        public string PhoneNo { get; set; }
        [Required]
        [MaxLength(50)]
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
