using System;
using System.Collections.Generic;

#nullable disable

namespace SimpleRetail.Models
{
    public partial class Transaction
    {
        public Transaction()
        {
            TransactionProducts = new HashSet<TransactionProduct>();
        }

        public string Id { get; set; }
        public DateTime Date { get; set; }
        public string EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual ICollection<TransactionProduct> TransactionProducts { get; set; }
    }
}
