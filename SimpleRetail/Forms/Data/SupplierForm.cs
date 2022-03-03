using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SimpleRetail.Models;

namespace SimpleRetail.Forms.Data {
    public partial class SupplierForm : Form {
        private readonly Database _db;
        public SupplierForm(Database db) {
            InitializeComponent();
            _db = db;
        }
    }
}
