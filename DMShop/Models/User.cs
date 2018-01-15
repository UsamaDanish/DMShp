using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace DMShop.Models
{
    public partial class User
    {
        public int Id { get; set; }
        [Required]
        [DisplayName("User Name")]
        public string Name { get; set; }
        [Required]
        [DisplayName("Password")]
        public string Password { get; set; }
        [Required]
        [DisplayName("Role")]
        public string Role { get; set; }
        [Required]
        [DisplayName("Email")]
        public string Email { get; set; }
        public DateTime Date { get; set; }
    }
}
