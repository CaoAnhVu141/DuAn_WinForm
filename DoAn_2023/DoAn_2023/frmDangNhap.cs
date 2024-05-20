using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_2023
{
    public partial class frmDangNhap : Form
    {
        public frmDangNhap()
        {
            InitializeComponent();

        }

        private void cbHienMatKhau_CheckedChanged(object sender, EventArgs e)
        { }

        private void dong_Click(object sender, EventArgs e)
        {
            if(txtPassword.PasswordChar== '*')
            {
                mo.BringToFront();

                txtPassword.PasswordChar = '\0';
            }    
        }

        private void mo_Click(object sender, EventArgs e)
        {
            if (txtPassword.PasswordChar == '\0')
            {
                dong.BringToFront();

                txtPassword.PasswordChar = '*';
            }
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            if(txtUsername.Text == "admin" && txtPassword.Text == "demo")
            {
                frmMain dangnhap = new frmMain();
                MessageBox.Show("Đăng nhập thành công");
                dangnhap.ShowDialog();
               
                this.Show();
                this.Close();
            }
            else if(txtUsername.Text != "admin" || txtPassword.Text != "demo")
            {
                MessageBox.Show("Đăng nhập không thành công");
            }
            
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void frmDangNhap_Load(object sender, EventArgs e)
        {

        }
    }
}
