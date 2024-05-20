using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace DoAn_2023
{
    public partial class frmTimKiemSanPham : Form
    {
        public frmTimKiemSanPham()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");
        private void frmTimKiemSanPham_Load(object sender, EventArgs e)
        {
            dgvTimKiemSP.DataSource = LayDSSanPham();
        }
        private DataTable LayDSSanPham()
        {
            DataTable dtSanPham = null;
            try
            {
                //mở kết nối
                connect.Open();

                //command
                SqlCommand cmdSanPham = new SqlCommand();
                cmdSanPham.CommandText = "sp_LayDSSP";
                cmdSanPham.CommandType = CommandType.StoredProcedure;
                //Kết nối
                cmdSanPham.Connection = connect;

                SqlDataAdapter daSanPham = new SqlDataAdapter(cmdSanPham);
                dtSanPham = new DataTable();
                daSanPham.Fill(dtSanPham);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu " + ex.Message);
            }
            finally
            {
                //đóng kết nối
                connect.Close();
            }
            return dtSanPham;
        }


        //Ham krta 
        private bool IsNumber(string s)
        {
            foreach (char item in s)
            {
                if (!Char.IsDigit(item))
                {
                    return false;
                }
            }
            return true;
        }
        private void btnTim_Click(object sender, EventArgs e)
        {
            try
            {
                //Xu ly cac control hợp lệ 
                if (String.IsNullOrEmpty(txtTimKiemSP.Text))
                {
                    MessageBox.Show("Chưa điền đủ thông tin!", "Thông báo");
                }
                else
                {
                    SqlCommand cmdTimKiemSP = new SqlCommand("sp_TK_SanPham", connect);
                    cmdTimKiemSP.CommandText = "sp_TK_SanPham";
                    cmdTimKiemSP.CommandType = CommandType.StoredProcedure;
                    SqlParameter paraMa = new SqlParameter("@MaSP", txtTimKiemSP.Text);
                    cmdTimKiemSP.Parameters.Add(paraMa);
                    SqlDataAdapter daTimKiemSP = new SqlDataAdapter(cmdTimKiemSP);
                    DataTable dtTimKiemSP = new DataTable();
                    daTimKiemSP.Fill(dtTimKiemSP);
                    dgvTimKiemSP.DataSource = dtTimKiemSP;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy cập " + ex.Message);
            }
            finally
            {
                connect.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTimKiemSP.Clear();
            txtTimKiemSP.Focus();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {

            DialogResult rs = MessageBox.Show("Bạn có muốn kết thúc không?", "Xác nhận!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }


    }
}
