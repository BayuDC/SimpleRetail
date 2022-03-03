using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SimpleRetail.Models;

namespace SimpleRetail.Forms.Data {
    public partial class SupplierForm : Form {
        private readonly Database _db;

        private enum Mode { None, Add, Edit }
        private Mode _mode;

        public SupplierForm(Database db) {
            InitializeComponent();
            _db = db;
            _mode = Mode.None;
        }

        private void SupplierForm_Load(object sender, EventArgs e) {
            grpSupplier.Enabled = false;
            LoadSuppliers();
        }

        private void GrpSupplier_EnabledChanged(object sender, EventArgs e) {
            ClearInput();

            if (grpSupplier.Enabled) return;

            btnAdd.Enabled = btnEdit.Enabled = true;
            _mode = Mode.None;
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpSupplier.Enabled = true;

            var id = _db.Suppliers.OrderByDescending(e => e).FirstOrDefault()?.Id[1..];
            txtId.Text = id == null ? "S0001" : $"S{(int.Parse(id) + 1):D4}";

            _mode = Mode.Add;
        }

        private void BtnEdit_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpSupplier.Enabled = true;

            var id = dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();
            var supplier = _db.Suppliers.Find(id);

            txtId.Text = supplier.Id;
            txtName.Text = supplier.Name;

            _mode = Mode.Edit;
        }

        private void BtnDelete_Click(object sender, EventArgs e) {
            var result =
                MessageBox.Show("Are you sure?", "Confirmastion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            DeleteSupplier();
            LoadSuppliers();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            if (!ValidateSupplier()) return;

            if (_mode == Mode.Add) AddSupplier();
            if (_mode == Mode.Edit) EditSupplier();
            if (_mode == Mode.None) return;

            grpSupplier.Enabled = false;
            LoadSuppliers();
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            grpSupplier.Enabled = false;
        }
        private void ClearInput() {
            txtId.Text = txtName.Text = string.Empty;
        }

        private void LoadSuppliers() {
            dgvSupplier.DataSource = (
                from supplier in _db.Suppliers
                select new {
                    supplier.Id, supplier.Name, supplier.Products.Count
                }
            ).ToList();
            dgvSupplier.Columns[2].HeaderText = "Total Products";
        }
        private void AddSupplier() {
            var supplier = new Supplier {
                Id = txtId.Text,
                Name = txtName.Text
            };

            _db.Suppliers.Add(supplier);
            _db.SaveChanges();
        }
        private void EditSupplier() {
            var supplier = _db.Suppliers.Find(txtId.Text);

            supplier.Name = txtName.Text;

            _db.SaveChanges();
        }
        private void DeleteSupplier() {
            var id = dgvSupplier.SelectedRows[0].Cells[0].Value.ToString();

            _db.Suppliers.Remove(_db.Suppliers.Find(id));
            _db.SaveChanges();
        }
        private bool ValidateSupplier() {
            var errors = string.Empty;

            if (txtName.Text == string.Empty) errors += "Name field is required\n";

            if (errors != string.Empty) {
                MessageBox.Show(errors, "Invalid Data", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            return true;
        }
    }
}
