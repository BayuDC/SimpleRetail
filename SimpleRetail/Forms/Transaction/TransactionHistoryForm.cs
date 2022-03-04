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
        private readonly List<TransactionDetailForm> _transactionDetailForms;
        public TransactionHistoryForm(Database db) {
            InitializeComponent();

            _db = db;
            _transactionDetailForms = new List<TransactionDetailForm>();
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

        private void TransactionHistoryForm_FormClosing(object sender, FormClosingEventArgs e) {
            foreach (var form in _transactionDetailForms) {
                form.Close();
            }
        }

        private void DgvTransaction_CellContentClick(object sender, DataGridViewCellEventArgs e) {
            if (e.ColumnIndex != 4) return;
            if (e.RowIndex < 0) return;

            var form = new TransactionDetailForm(_db, dgvTransaction[0, e.RowIndex].Value.ToString());
            form.Show();

            _transactionDetailForms.Add(form);
        }
    }
}
