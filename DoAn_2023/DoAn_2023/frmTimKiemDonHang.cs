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
    public partial class frmTimKiemDonHang : Form
    {
        public frmTimKiemDonHang()
        {
            InitializeComponent();
        }
        SqlConnection connect = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void frmTimKiemDonHang_Load(object sender, EventArgs e)
        {

            dgvTimKiemDH.DataSource = LayDSDonHang();
        }
            private DataTable LayDSDonHang()
            {
                DataTable dtDonHang = null;
                try
                {
                    //mở kết nối
                    connect.Open();

                    //command
                    SqlCommand cmdDonHang = new SqlCommand();
                    cmdDonHang.CommandText = "sp_LayDSDHN";
                    cmdDonHang.CommandType = CommandType.StoredProcedure;
                    //Kết nối
                    cmdDonHang.Connection = connect;

                    SqlDataAdapter daDonHang = new SqlDataAdapter(cmdDonHang);
                    dtDonHang = new DataTable();
                    daDonHang.Fill(dtDonHang);
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
                return dtDonHang;
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
                if (String.IsNullOrEmpty(txtTimKiemDH.Text))
                {
                    MessageBox.Show("Chưa điền đủ thông tin!", "Thông báo");
                }
                else
                {
                    SqlCommand cmdTimKiemDH = new SqlCommand("sp_TK_DonHang", connect);
                    cmdTimKiemDH.CommandText = "sp_TK_DonHang";
                    cmdTimKiemDH.CommandType = CommandType.StoredProcedure;
                    SqlParameter paraMa = new SqlParameter("@MaDonHang", txtTimKiemDH.Text);
                    cmdTimKiemDH.Parameters.Add(paraMa);
                    SqlDataAdapter daTimKiemDH = new SqlDataAdapter(cmdTimKiemDH);
                    DataTable dtTimKiemDH = new DataTable();
                    daTimKiemDH.Fill(dtTimKiemDH);
                    dgvTimKiemDH.DataSource = dtTimKiemDH;
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
            txtTimKiemDH.Clear();
            txtTimKiemDH.Focus();
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
