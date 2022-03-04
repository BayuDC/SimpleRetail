using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Diagnostics;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Transaction {
    public partial class BrowseProductsFrom : Form {
        private readonly Database _db;
        private readonly Dictionary<string, Transaction> _transactions;

        public Action<string, int> SelectProduct { private get; set; }

        public BrowseProductsFrom(Database db, Dictionary<string, Transaction> transactions) {
            InitializeComponent();

            _db = db;
            _transactions = transactions;
        }

        private void BrowseProductsFrom_Load(object sender, EventArgs e) {
            dgvProduct.DataSource = (
                from product in _db.Products
                select new {
                    product.Id, product.Name, product.Price, 
                    Stock = product.Stock - (_transactions.ContainsKey(product.Id) ? _transactions[product.Id].Quantity : 0),
                }
            ).ToList();
        }

        private void BtnSelect_Click(object sender, EventArgs e) {
            var id = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            var stock = (int)dgvProduct.SelectedRows[0].Cells[3].Value;

            SelectProduct(id, stock);
            Close();
        }
    }
}
