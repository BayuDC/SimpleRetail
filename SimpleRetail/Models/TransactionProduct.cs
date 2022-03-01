using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleRetail.Models
{
    public partial class TransactionProduct
    {
        public int TransactionId { get; set; }
        public int ProdcutId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public virtual Product Prodcut { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
