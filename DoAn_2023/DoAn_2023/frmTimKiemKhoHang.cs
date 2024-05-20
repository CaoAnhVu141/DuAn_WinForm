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
    public partial class frmTimKiemKhoHang : Form
    {
        public frmTimKiemKhoHang()
        {
            InitializeComponent();
        }

        DB_Connect tt = new DB_Connect();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-99VV1J2\HUYNHPHUOC;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void frmTimKiemKhoHang_Load(object sender, EventArgs e)
        {
            dgvTimKiemKH.DataSource = LayDSKhoHang();
            //dgvTimKiemKH.DataSource = tt.ExcuteTable("sp_LayDSCKH");
        }
        private DataTable LayDSKhoHang()
        {
            DataTable dtKhoHang = null;
            try
            {
                //mở kết nối
                connect.Open();

                //command
                SqlCommand cmdKhoHang = new SqlCommand();
                cmdKhoHang.CommandText = "sp_LayDSCKH";
                cmdKhoHang.CommandType = CommandType.StoredProcedure;
                //Kết nối
                cmdKhoHang.Connection = connect;

                SqlDataAdapter daKhoHang = new SqlDataAdapter(cmdKhoHang);
                dtKhoHang = new DataTable();
                daKhoHang.Fill(dtKhoHang);
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
            return dtKhoHang;
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
                if (String.IsNullOrEmpty(txtTimKiemKH.Text))
                {
                    MessageBox.Show("Chưa điền đủ thông tin!", "Thông báo");
                }
                else
                {
                    SqlCommand cmdTimKiemKH = new SqlCommand("sp_TK_KhoHang", connect);
                    cmdTimKiemKH.CommandText = "sp_TK_KhoHang";
                    cmdTimKiemKH.CommandType = CommandType.StoredProcedure;
                    SqlParameter paraMa = new SqlParameter("@MaKho", txtTimKiemKH.Text);
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
                connect.Close();
            }
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            txtTimKiemKH.Clear();
            txtTimKiemKH.Focus();
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
