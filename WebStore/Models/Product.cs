using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebStore.Models
{
    public partial class Product
    {
        public Product()
        {
            CartDetail = new HashSet<CartDetail>();
            OrderDetail = new HashSet<OrderDetail>();
        }

        [Required(ErrorMessage = "Product ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Product ID must be a positive number.")]
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(100, ErrorMessage = "Product Name cannot be longer than 100 characters.")]
        public string ProductName { get; set; }

        public bool? IsFavorite { get; set; }

        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }

        public int? VoucherListId { get; set; }

        [Range(0, 5, ErrorMessage = "Rating must be between 0 and 5.")]
        public decimal? Rating { get; set; }

        public string Version { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        public string ImageList { get; set; }

        public int? CommentListId { get; set; }

        [Required(ErrorMessage = "Category ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Category ID must be a positive number.")]
        public int? CategoryId { get; set; }

        public DateTime? CreatedDate { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<CartDetail> CartDetail { get; set; }
        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
    }
}