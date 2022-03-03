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

        private void TransactionNewForm_FormClosing(object sender, FormClosingEventArgs e) {
            _browseProductsFrom.Close();
        }
        private void BtnBrowse_Click(object sender, EventArgs e) {
            _browseProductsFrom = (BrowseProductsFrom)
                MainForm.ShowForm(_browseProductsFrom, new BrowseProductsFrom(_db) {
                    SelectProduct = id => {
                        txtProductId.Text = id;
                        Focus();
                    }
                });
        }

    }
}
