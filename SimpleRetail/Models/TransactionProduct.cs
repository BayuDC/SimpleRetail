using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleRetail.Models
{
    public partial class TransactionProduct
    {
        public string TransactionId { get; set; }
        public string ProductId { get; set; }
        public int Quantity { get; set; }
        public int Price { get; set; }

        public virtual Product Product { get; set; }
        public virtual Transaction Transaction { get; set; }
    }
}
