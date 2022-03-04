using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace SimpleRetail.Forms.Report {
    public partial class DailyReportForm : Form {
        private readonly Database _db;

        public DailyReportForm(Database db) {
            InitializeComponent();
            _db = db;
        }

        private void DailyReportForm_Load(object sender, EventArgs e) {

        }
    }
}
