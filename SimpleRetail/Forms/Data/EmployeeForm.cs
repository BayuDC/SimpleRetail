﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using SimpleRetail.Models;

namespace SimpleRetail.Forms.Data {
    public partial class EmployeeForm : Form {

        private readonly Database _db;
        private enum Mode { None, Add, Edit }
        private Mode _mode;

        public EmployeeForm(Database db) {
            InitializeComponent();
            _db = db;
            _mode = Mode.None;
        }

        private void EmployeeForm_Load(object sender, EventArgs e) {
            grpEmployee.Enabled = false;
            LoadEmployees();
        }
        private void GrpEmployee_EnabledChanged(object sender, EventArgs e) {
            ClearInput();

            if (grpEmployee.Enabled) return;

            btnAdd.Enabled = btnEdit.Enabled = true;
            _mode = Mode.None;
        }
        private void BtnAdd_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpEmployee.Enabled = true;

            var id = _db.Employees.OrderByDescending(e => e).FirstOrDefault()?.Id[1..];
            txtId.Text = id == null ? "E0001" : $"E{(int.Parse(id) + 1):D4}";

            _mode = Mode.Add;
        }
        private void BtnEdit_Click(object sender, EventArgs e) {
            btnAdd.Enabled = btnEdit.Enabled = false;
            grpEmployee.Enabled = true;

            var id = txtId.Text = dgvEmployee.SelectedRows[0].Cells[0].Value.ToString();
            var employee = _db.Employees.Find(id);

            txtName.Text = employee.Name;
            txtEmail.Text = employee.Email;
            txtPhone.Text = employee.Phone;
            txtPassword.Text = employee.Password;

            _mode = Mode.Edit;
        }
        private void BtnDelete_Click(object sender, EventArgs e) {
            var result =
                MessageBox.Show("Are you sure?", "Confirmastion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.No) return;

            DeleteEmployee();
            LoadEmployees();
        }

        private void BtnSave_Click(object sender, EventArgs e) {
            if (_mode == Mode.Add) AddEMployee();
            if (_mode == Mode.Edit) EditEmployee();
            if (_mode == Mode.None) return;

            LoadEmployees();
            grpEmployee.Enabled = false;
        }

        private void BtnCancel_Click(object sender, EventArgs e) {
            grpEmployee.Enabled = false;
        }
        private void LoadEmployees() {
            dgvEmployee.DataSource = (
                from employee in _db.Employees
                select new { employee.Id, employee.Name, employee.Email, employee.Phone }
            ).ToList();
        }
        private void ClearInput() {
            txtId.Text = txtName.Text = txtEmail.Text = txtPhone.Text = txtPassword.Text = string.Empty;
        }
        private void AddEMployee() {
            var employee = new Employee {
                Id = txtId.Text,
                Name = txtName.Text,
                Email = txtEmail.Text,
                Phone = txtPhone.Text,
                Password = txtPassword.Text
            };

            _db.Employees.Add(employee);
            _db.SaveChanges();
        }
        private void EditEmployee() {
            var employee = _db.Employees.Find(txtId.Text);

            employee.Name = txtName.Text;
            employee.Email = txtEmail.Text;
            employee.Phone = txtPhone.Text;
            employee.Password = txtPassword.Text;

            _db.SaveChanges();
        }
        private void DeleteEmployee() {
            var id = dgvEmployee.SelectedRows[0].Cells[0].Value.ToString();

            _db.Employees.Remove(_db.Employees.Find(id));
            _db.SaveChanges();
        }
    }
}
