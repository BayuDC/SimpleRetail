using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleRetail.Models
{
    public partial class Product
    {
        public Product()
        {
            TransactionProducts = new HashSet<TransactionProduct>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int Stock { get; set; }
        public string SupplierId { get; set; }

        public virtual Supplier Supplier { get; set; }
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
    }
}
