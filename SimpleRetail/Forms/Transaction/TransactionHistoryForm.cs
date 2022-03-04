using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionHistoryForm : Form {
        private readonly Database _db;
        public TransactionHistoryForm(Database db) {
            InitializeComponent();

            _db = db;
        }

        private void TransactionHistoryForm_Load(object sender, EventArgs e) {
            dgvTransaction.DataSource = (
                from transation in _db.Transactions
                join employee in _db.Employees
                on transation.EmployeeId equals employee.Id
                select new {
                    transation.Id,
                    transation.Date,
                    Employee = employee.Name,
                    Total = transation.TransactionProducts.Where(t => t.TransactionId == transation.Id).Sum(t => t.Price),
                }
            ).ToList();

            dgvTransaction.Columns.Add(new DataGridViewButtonColumn() {
                Text = "Detail",
                HeaderText = "Detail",
                UseColumnTextForButtonValue = true
            });
        }

        private void DgvTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 4) return;
            if (e.RowIndex < 0) return;

            new TransactionDetailForm(_db, dgvTransaction[0, e.RowIndex].Value.ToString()).Show();
        }
    }
}
