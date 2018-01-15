using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DMShop.Models
{
    public partial class Vendor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Name { get; set; }
        [Required]
        [MaxLength(14)]
        public string PhoneNo { get; set; }
        [Required]
        [MaxLength(70)]
        public string Email { get; set; }
        [DisplayName(displayName: "Date")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-mm-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
    }
}
