using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class BrowseProductsFrom : Form {
        private readonly Database _db;

        public BrowseProductsFrom(Database db) {
            InitializeComponent();
            _db = db;
        }
    }
}
