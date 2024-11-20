using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebStore.Models
{
    public partial class User
    {
        public User()
        {
            Cart = new HashSet<Cart>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int UserId { get; set; }
        [Required(ErrorMessage = "Username is required.")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email format.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Phone number is required.")]
        [Phone(ErrorMessage = "Invalid phone number format.")]
        public string PhoneNumber { get; set; }
        [Required(ErrorMessage = "Full name is required.")]
        public string FullName { get; set; }
        public string PasswordHash { get; set; }
        [Required(ErrorMessage = "Address is required.")]
        public string Address { get; set; }
        public string Role { get; set; }

        public virtual ICollection<Cart> Cart { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
