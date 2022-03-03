using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class BrowseProductsFrom : Form {
        private readonly Database _db;
        public Action<string> SelectProduct { private get; set; }

        public BrowseProductsFrom(Database db) {
            InitializeComponent();

            _db = db;
        }

        private void BrowseProductsFrom_Load(object sender, EventArgs e) {
            dgvProduct.DataSource = (
                from product in _db.Products
                select new {
                    product.Id, product.Name, product.Price, product.Stock
                }
            ).ToList();
        }

        private void BtnSelect_Click(object sender, EventArgs e) {
            var id = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            SelectProduct(id);
            Close();
        }
    }
}
