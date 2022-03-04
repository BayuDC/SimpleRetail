using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Report {
    public partial class DailyReportForm : Form {
        private readonly Database _db;

        public DailyReportForm(Database db) {
            InitializeComponent();
            _db = db;
        }

        private void DailyReportForm_Load(object sender, EventArgs e) {
            var date = DateTime.Now.Date;
            var transactionCount = _db.Transactions.Where(t => t.Date == date).Count();
            var transactionProducts = (
                from transactionProduct in _db.TransactionProducts
                join transaction in _db.Transactions
                on transactionProduct.TransactionId equals transaction.Id
                where (transaction.Date == date)
                select new {
                    transactionProduct.Quantity,
                    transactionProduct.Price
                }
            ).ToList();

            lblDate.Text = date.ToString("d");
            lblTransactions.Text = transactionCount.ToString();
            lblProducts.Text = transactionProducts.Sum(tp => tp.Quantity).ToString();
            lblIncome.Text = transactionProducts.Sum(tp => tp.Price).ToString();
        }
    }
}
