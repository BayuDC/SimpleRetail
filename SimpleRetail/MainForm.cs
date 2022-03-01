using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleRetail {
    public partial class MainForm : Form {
        private readonly Action _close;
        public MainForm(Action close) {
            InitializeComponent();
            _close = close;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e) => _close();
    }
}
