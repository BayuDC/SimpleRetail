using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionHistoryForm : Form {
        private readonly Database _db;
        public TransactionHistoryForm(Database db) {
            InitializeComponent();

            _db = db;
        }

        private void TransactionHistoryForm_Load(object sender, EventArgs e) {

        }
    }
}
