using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;

namespace SimpleRetail.Forms {
    public partial class ProductForm : Form {
        private readonly Database _db;
        public ProductForm(Database db) {
            InitializeComponent();
            _db = db;
        }

        private void ProductForm_Load(object sender, EventArgs e) {
            grpProduct.Enabled = false;
            dgvProduct.DataSource = (
                from product in _db.Products
                join supplier in _db.Suppliers
                on product.SupplierId equals supplier.Id
                select new {
                    product.Id, product.Name, product.Price, product.Stock,
                    Supplier = supplier.Name
                }
            ).ToList();
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            grpProduct.Enabled = true;

        }
    }
}
