using System;
using System.Windows.Forms;
using SimpleRetail.Forms.Data;
using SimpleRetail.Forms.Transaction;

namespace SimpleRetail.Forms {
    public partial class MainForm : Form {
        private readonly Database _db;
        private readonly LoginForm _loginForm;

        private ProductForm _produtctForm;
        private EmployeeForm _employeeForm;
        private SupplierForm _supplierForm;
        private TransactionNewForm _transactionNewForm;

        public MainForm(Database db, LoginForm loginForm) {
            InitializeComponent();
            _loginForm = loginForm;
            _db = db;
        }

        public static void ShowForm(Form form, Form freshForm) {
            if (form == null || form.IsDisposed) {
                form = freshForm;
                form.Show();
            }
            if (!form.Focused) {
                form.Focus();
            }
            if (form.WindowState == FormWindowState.Minimized) {
                form.WindowState = FormWindowState.Normal;
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) {
            if (_loginForm.Visible) return;

            _loginForm.Close();
        }

        private void ExitToolStripMenuItem_Click(object sender, EventArgs e) {
            _loginForm.Close();
        }

        private void LogoutToolStripMenuItem_Click(object sender, EventArgs e) {
            _loginForm.Show();
            this.Close();
        }

        private void ProductToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowForm(_produtctForm, new ProductForm(_db));
        }

        private void EmployeeToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowForm(_employeeForm, new EmployeeForm(_db));
        }

        private void SupplierToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowForm(_produtctForm, new SupplierForm(_db));
        }

        private void NewTransactionToolStripMenuItem_Click(object sender, EventArgs e) {
            ShowForm(_transactionNewForm, new TransactionNewForm(_db));
        }
    }
}
