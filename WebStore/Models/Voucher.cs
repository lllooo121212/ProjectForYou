using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace WebStore.Models
{
    public partial class Voucher
    {
        public Voucher()
        {
            OrderDetail = new HashSet<OrderDetail>();
            OrderItem = new HashSet<OrderItem>();
        }

        public int VoucherId { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal? MaxDiscountValue { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public virtual ICollection<OrderDetail> OrderDetail { get; set; }
        public virtual ICollection<OrderItem> OrderItem { get; set; }
    }
}
