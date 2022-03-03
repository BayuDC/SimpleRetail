﻿using System;
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
                MainForm.ShowForm(_browseProductsFrom, new BrowseProductsFrom(_db) {
                    SelectProduct = id => {
                        txtProductId.Text = id;
                        Focus();
                    }
                });
        }

        private void BtnAdd_Click(object sender, EventArgs e) {
            var product = _db.Products.Find(txtProductId.Text);

            if (!ValidateProduct(product)) return;

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
        private bool ValidateProduct(Product product) {
            if (txtProductId.Text == string.Empty) {
                MessageBox.Show(
                    "Product field is required", "Invalid Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return false;
            }

            if (product == null) {
                MessageBox.Show(
                    $"Product with Id {txtProductId.Text} is not found", "Invalid Data",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning
                );
                return false;
            }

            return true;
        }
        private void SaveTransactions() {
            // save
        }
        private bool ValidateTransactions() {
            // validate

            return true;
        }
    }
}
