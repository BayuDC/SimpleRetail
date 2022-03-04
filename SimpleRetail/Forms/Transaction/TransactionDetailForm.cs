using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionDetailForm : Form {
        private readonly Database _db;
        private readonly string _transactionId;
        public TransactionDetailForm(Database db, string transactionId) {
            InitializeComponent();

            _db = db;
            _transactionId = transactionId;
        }

        private void TransactionDetailForm_Load(object sender, EventArgs e) {
            var id = lblId.Text = _transactionId;
            var transaction = _db.Transactions.Find(id);
            var employee = _db.Employees.Find(transaction.EmployeeId);
            var transactions = (
                from transactionProduct in _db.TransactionProducts
                join product in _db.Products
                on transactionProduct.ProductId equals product.Id
                where (transactionProduct.TransactionId == transaction.Id)
                select new {
                    product.Id,
                    product.Name,
                    product.Price,
                    transactionProduct.Quantity,
                    Subtotal = transactionProduct.Price
                }
            ).ToList();

            this.Text = "Transaction Detail for " + id;

            lblEmployee.Text = employee.Name;
            lblPrice.Text = transactions.Sum(t => t.Subtotal).ToString();
            lblDate.Text = transaction.Date.ToString("d");

            dgvTransaction.DataSource = transactions;
            dgvTransaction.Columns[0].HeaderText = "Product Id";
        }
    }
}
