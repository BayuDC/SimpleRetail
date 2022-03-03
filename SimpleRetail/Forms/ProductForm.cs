using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using SimpleRetail.Models;

namespace SimpleRetail.Forms {
    public partial class ProductForm : Form {

        private readonly Database _db;
        private enum Mode { None, Add, Edit }
        private Mode _mode;

        public ProductForm(Database db) {
            InitializeComponent();

            _db = db;
            _mode = Mode.None;
        }

        private void ProductForm_Load(object sender, EventArgs e) {
            grpProduct.Enabled = false;

            LoadProducts();

            cmbSupplier.DataSource = (from supplier in _db.Suppliers select supplier.Name).ToList();
            cmbSupplier.SelectedItem = null;
        }
        private void GrpProduct_EnabledChanged(object sender, EventArgs e) {
            ClearInput();

            if (grpProduct.Enabled) return;

            btnAdd.Enabled = btnEdit.Enabled = true;
            _mode = Mode.None;
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpProduct.Enabled = true;

            var id = _db.Products.OrderByDescending(p => p).FirstOrDefault()?.Id[1..];
            txtId.Text = id == null ? "P0001" : $"P{(int.Parse(id) + 1):D4}";

            _mode = Mode.Add;
        }
        private void BtnEdit_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpProduct.Enabled = true;

            var id = txtId.Text = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();
            var product = _db.Products.Find(id);
            product.Supplier = _db.Suppliers.Find(product.SupplierId);

            txtName.Text = product.Name;
            txtPrice.Text = product.Price.ToString();
            txtStock.Text = product.Stock.ToString();
            cmbSupplier.SelectedItem = product.Supplier.Name;

            _mode = Mode.Edit;
        }
        private void BtnDelete_Click(object sender, EventArgs e) {
            var result =
                MessageBox.Show("Are you sure?", "Confirmastion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            DeleteProduct();
            LoadProducts();
        }
        private void BtnSave_Click(object sender, EventArgs e) {
            if (!ValidateProduct()) return;

            if (_mode == Mode.Add) AddProduct();
            if (_mode == Mode.Edit) EditProduct();
            if (_mode == Mode.None) return;

            grpProduct.Enabled = false;

            LoadProducts();
        }
        private void BtnCancel_Click(object sender, EventArgs e) {
            grpProduct.Enabled = false;
        }

        private void LoadProducts() {
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
        private void ClearInput() {
            txtId.Text = txtName.Text = txtPrice.Text = txtStock.Text = string.Empty;
            cmbSupplier.SelectedItem = null;
        }
        private void AddProduct() {
            var product = new Product {
                Id = txtId.Text,
                Name = txtName.Text,
                Price = int.Parse(txtPrice.Text),
                Stock = int.Parse(txtStock.Text),
                Supplier = _db.Suppliers.First(supplier => supplier.Name == cmbSupplier.Text)
            };

            _db.Products.Add(product);
            _db.SaveChanges();
        }
        private void EditProduct() {
            var product = _db.Products.Find(txtId.Text);

            product.Name = txtName.Text;
            product.Price = int.Parse(txtPrice.Text);
            product.Stock = int.Parse(txtStock.Text);
            product.SupplierId = _db.Suppliers.First(supplier => supplier.Name == cmbSupplier.Text).Id;

            _db.Products.Update(product);
            _db.SaveChanges();
        }
        private void DeleteProduct() {
            var id = dgvProduct.SelectedRows[0].Cells[0].Value.ToString();

            _db.Products.Remove(_db.Products.Find(id));
            _db.SaveChanges();
        }
        private bool ValidateProduct() {
            var errors = string.Empty;

            if (txtName.Text == string.Empty) errors += "Name field is required\n";

            if (txtPrice.Text == string.Empty) errors += "Price field is required\n";
            else if (!int.TryParse(txtPrice.Text, out int value) || value < 0)
                errors += "Price must be a positive number\n";

            if (txtStock.Text == string.Empty) errors += "Stock field is required\n";
            else if (!int.TryParse(txtStock.Text, out int value) || value < 0)
                errors += "Stock must be a positive number\n";

            if (cmbSupplier.SelectedItem == null) errors += "Name field is required\n";

            if (errors != string.Empty) {
                MessageBox.Show(errors, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
