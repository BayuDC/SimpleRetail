using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionNewForm : Form {
        private readonly Database _db;
        public TransactionNewForm(Database db) {
            InitializeComponent();
            _db = db;
        }
    }
}
