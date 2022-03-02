using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleRetail.Forms {
    public partial class MainForm : Form {
        private readonly Database _db;
        private readonly LoginForm _loginForm;
        public MainForm(Database db, LoginForm loginForm) {
            InitializeComponent();
            _loginForm = loginForm;
            _db = db;
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
    }
}
