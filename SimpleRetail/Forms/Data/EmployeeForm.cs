using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SimpleRetail.Models;

namespace SimpleRetail.Forms.Data {
    public partial class EmployeeForm : Form {

        private readonly Database _db;

        public EmployeeForm(Database db) {
            InitializeComponent();
            _db = db;
        }
    }
}
