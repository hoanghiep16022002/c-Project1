﻿using FrmMain.BussinessLayer;
using FrmMain.DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrmMain
{
    public partial class Frm_DoiMatKhau : Form
    {
        public Frm_DoiMatKhau()
        {
            InitializeComponent();
        }
        BLLUser bd;

        private void Frm_DoiMatKhau_Load(object sender, EventArgs e)
        {
            if(!ClsMain.taiKhoan.Equals("admin"))
            {
                gbAdmin.Enabled = false;

            }
            else
            {
                bd = new BLLUser(ClsMain.pathUser);
                ClsMain.users = bd.GetUsers();
                LoadCboUser();
            }
        }

        private void LoadCboUser()
        {
            cboUsers.DataSource = ClsMain.users;

            cboUsers.DisplayMember = "TaiKhoan";
            cboUsers.ValueMember = "ID";

            cboUsers.SelectedIndex = -1;
            cboUsers.Text = "---Chọn User---";
        }

        private void btnResetPassword_Click(object sender, EventArgs e)
        {
            if (cboUsers.SelectedIndex > -1)
            {
                CapNhatMatKhau(cboUsers.SelectedValue.ToString(), "cntt@123", true);
            }
        }

        private void CapNhatMatKhau(string iD, string matKhau, bool isID)
        {
            foreach (User item in ClsMain.users)
            {
                if (isID)
                {
                    if (item.ID.ToString().Equals(iD))
                    {
                        item.MatKhau = matKhau;
                        break;
                    }
                }
                else
                {
                    if (item.TaiKhoan.ToString().Equals(iD))
                    {
                        item.MatKhau = matKhau;
                        break;
                    }
                }

            }
            ClsMain.UpdateData(ClsMain.pathUser, ClsMain.users);
        }

        private void btnChangePassword_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtPassword1.Text) && !string.IsNullOrEmpty(txtPassword2.Text))
            {
                if (txtPassword1.Text.Equals(txtPassword2.Text))
                {

                    CapNhatMatKhau(ClsMain.taiKhoan, txtPassword1.Text, false);

                }
                else
                {
                    MessageBox.Show("Không trùng mật khẩu");
                }
            }
            else
            {
                MessageBox.Show("Không được để trống");
                txtPassword1.Focus();
            }
        }
    }
}
