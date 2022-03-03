using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionNewForm : Form {
        private readonly Database _db;
        private BrowseProductsFrom _browseProductsFrom;

        public TransactionNewForm(Database db) {
            InitializeComponent();
            _db = db;
        }

        private void BtnBrowse_Click(object sender, EventArgs e) {
            MainForm.ShowForm(_browseProductsFrom, new BrowseProductsFrom(_db));
        }
    }
}
