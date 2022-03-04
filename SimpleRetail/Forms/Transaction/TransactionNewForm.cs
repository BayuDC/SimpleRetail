using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Diagnostics;
using SimpleRetail.Models;

namespace SimpleRetail.Forms.Transaction {
    public partial class TransactionNewForm : Form {
        private readonly Database _db;
        private BrowseProductsFrom _browseProductsFrom;
        private readonly Dictionary<string, Transaction> _transactions;

        public TransactionNewForm(Database db) {
            InitializeComponent();
            _db = db;
            _transactions = new Dictionary<string, Transaction>();
        }
        private void TransactionNewForm_Load(object sender, EventArgs e) {
            LoadTransactions();
            dgvTransaction.Columns[0].HeaderText = "Product Id";
            dgvTransaction.Columns[1].HeaderText = "Name";
            dgvTransaction.Columns[2].HeaderText = "Price";
        }

        private void TransactionNewForm_FormClosing(object sender, FormClosingEventArgs e) {
            _browseProductsFrom?.Close();
        }
        private void BtnBrowse_Click(object sender, EventArgs e) {
            _browseProductsFrom = (BrowseProductsFrom)
                MainForm.ShowForm(_browseProductsFrom,
                    new BrowseProductsFrom(_db, _transactions) {
                        SelectProduct = (id, stock) => {
                            txtProductId.Text = id;
                            numQuantity.Maximum = stock;
                            numQuantity.Enabled = true;
                            Focus();
                        }
                    }
                );
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            var product = _db.Products.Find(txtProductId.Text);

            AddTransaction(product);

            LoadTransactions();
            ClearInput();
        }
        private void BtnCheckout_Click(object sender, EventArgs e) {
            if (!ValidateTransactions()) return;

            SaveTransactions();
            Close();
        }

        private void LoadTransactions() {
            dgvTransaction.DataSource = (
                from transaction in _transactions
                select new {
                    transaction.Key,
                    transaction.Value.ProductName,
                    transaction.Value.ProductPrice,
                    transaction.Value.Quantity,
                    Subtotal = transaction.Value.ProductPrice * transaction.Value.Quantity
                }
            ).ToList();
        }
        private void ClearInput() {
            txtProductId.Text = string.Empty;
            numQuantity.Value = 0;
            numQuantity.Maximum = int.MaxValue;
            numQuantity.Enabled = false;
        }
        private void AddTransaction(Product product) {
            var quantity = (int)numQuantity.Value;


            if (quantity == 0) return;

            if (_transactions.ContainsKey(product.Id)) {
                var addedProduct = _transactions[product.Id];
                var currentStock = product.Stock - addedProduct.Quantity;

                if (quantity > currentStock) quantity = currentStock;

                addedProduct.Quantity += quantity;
                AddTotalPrice(quantity * product.Price);
                return;
            }

            if (quantity > product.Stock) quantity = product.Stock;

            _transactions.Add(product.Id, new Transaction() {
                ProductName = product.Name,
                ProductPrice = product.Price,
                Quantity = quantity
            });
            AddTotalPrice(quantity * product.Price);
        }
        private void AddTotalPrice(int price) {
            var currentPrice = int.Parse(lblPrice.Text);
            lblPrice.Text = (currentPrice + price).ToString();
        }
        private void SaveTransactions() {
            var id = _db.Transactions.OrderByDescending(p => p).FirstOrDefault()?.Id[1..];
            id = id == null ? "T0001" : $"T{(int.Parse(id) + 1):D4}";

            _db.Transactions.Add(new Models.Transaction {
                Id = id,
                Date = DateTime.Now,
                EmployeeId = LoginForm.EmployeeId
            });

            foreach(var transaction in _transactions) {
                _db.TransactionProducts.Add(new TransactionProduct {
                    TransactionId = id,
                    ProductId = transaction.Key,
                    Quantity = transaction.Value.Quantity,
                    Price = transaction.Value.ProductPrice * transaction.Value.Quantity
                });

                _db.Products.Find(transaction.Key).Stock -= transaction.Value.Quantity;
            }

            _db.SaveChanges();
        }
        private bool ValidateTransactions() {
            if(_transactions.Count == 0) {
                MessageBox.Show("Checkout at least one product");
                return false;
            }

            return true;
        }
    } 
}
