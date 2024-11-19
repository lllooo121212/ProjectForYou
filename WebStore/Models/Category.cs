using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebStore.Models
{
    public partial class Category
    {
        public Category()
        {
            Product = new HashSet<Product>();
        }

        [Required(ErrorMessage = "Category ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number.")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(100)]
        public string CategoryName { get; set; }

        public virtual ICollection<Product> Product { get; set; }
    }
}
