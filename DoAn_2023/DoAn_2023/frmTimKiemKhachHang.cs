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
    public partial class frmTimKiemKhachHang : Form
    {
        public frmTimKiemKhachHang()
        {
            InitializeComponent();
        }
        DB_Connect tt = new DB_Connect();
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void frmTraCuuKhachHang_Load(object sender, EventArgs e)
        {
            dgvTimKiemKH.DataSource = LayDSKhachHang();
        }

        /// <summary>
        /// lay danh sách khách hàng hiển thị thông tin trên dgv
        /// </summary>
        /// <returns></returns>
        private DataTable LayDSKhachHang()
        {
            DataTable dtKhachHang = null;
            try
            {
                //mở kết nối
                conn.Open();

                //command
                SqlCommand cmdKhachHang = new SqlCommand();
                cmdKhachHang.CommandText = "sp_LayDSKH";
                cmdKhachHang.CommandType = CommandType.StoredProcedure;
                //Kết nối
                cmdKhachHang.Connection = conn;

                SqlDataAdapter daKhachHang = new SqlDataAdapter(cmdKhachHang);
                dtKhachHang = new DataTable();
                daKhachHang.Fill(dtKhachHang);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Không đọc được dữ liệu " + ex.Message);
            }
            finally
            {
                //đóng kết nối
                conn.Close();
            }
            return dtKhachHang;
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
        private void btmTim_Click_1(object sender, EventArgs e)
        {
            try
            {
                //Xu ly cac control hợp lệ 
                if (String.IsNullOrEmpty(txtTimKiemKH.Text))
                {
                    MessageBox.Show("Chưa điền đủ thông tin!", "Thông báo");
                }
                else
                {
                    SqlCommand cmdTimKiemKH = new SqlCommand(" sp_TK_KhachHang", conn);
                    cmdTimKiemKH.CommandText = "sp_TK_KhachHang";
                    cmdTimKiemKH.CommandType = CommandType.StoredProcedure;
                    SqlParameter paraMa = new SqlParameter("@MaKH", txtTimKiemKH.Text);
                    cmdTimKiemKH.Parameters.Add(paraMa);
                    SqlDataAdapter daTimKiemKH = new SqlDataAdapter(cmdTimKiemKH);
                    DataTable dtTimKiemKH = new DataTable();
                    daTimKiemKH.Fill(dtTimKiemKH);
                    dgvTimKiemKH.DataSource = dtTimKiemKH;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi truy cập " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void btnReset_Click_1(object sender, EventArgs e)
        {
            txtTimKiemKH.Clear();
            txtTimKiemKH.Focus();
        }

        private void btnThoat_Click_1(object sender, EventArgs e)
        {
            DialogResult rs = MessageBox.Show("Bạn có muốn kết thúc không?", "Xác nhận!!!", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (rs == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void frmTimKiemKhachHang_Load(object sender, EventArgs e)
        {
            dgvTimKiemKH.DataSource = tt.ExcuteTable("sp_LayDSKH");
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
