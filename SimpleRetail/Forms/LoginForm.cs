using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SimpleRetail.Forms {
    public partial class LoginForm : Form {

        private readonly Database _db;
        public LoginForm() {
            InitializeComponent();
            _db = new Database();
        }

        private void BtnLogin_Click(object sender, EventArgs e) {
            var employee = _db.Employees.FirstOrDefault(e => e.Email == txtEmail.Text);

            if (employee == null) {
                MessageBox.Show("Email not found", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if(employee.Password != txtPassword.Text) {
                MessageBox.Show("Incorrect Password", "Login failed", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            txtEmail.Text = txtPassword.Text = string.Empty;

            (new MainForm(_db, this)).Show();
            this.Hide();
        }
    }
}
