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
    public partial class frmTimDatHang : Form
    {
        public frmTimDatHang()
        {
            InitializeComponent();
        }
        DB_Connect tt = new DB_Connect();
        SqlConnection connect = new SqlConnection(@"Data Source=DESKTOP-99VV1J2\HUYNHPHUOC;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void frmTimDatHang_Load(object sender, EventArgs e)
        {
            dgvTimKiemDH.DataSource = LayDSDatHang();
        }
        private DataTable LayDSDatHang()
        {
            DataTable dtDatHang = null;
            try
            {
                //mở kết nối
                connect.Open();

                //command
                SqlCommand cmd = new SqlCommand();
                SqlCommand cmdDatHang = new SqlCommand();
                cmdDatHang.CommandText = "sp_LayDatHang";
                cmdDatHang.CommandType = CommandType.StoredProcedure;
                //Kết nối
                cmdDatHang.Connection = connect;

                SqlDataAdapter daDatHang = new SqlDataAdapter(cmdDatHang);
                dtDatHang = new DataTable();
                daDatHang.Fill(dtDatHang);
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
            return dtDatHang;
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
                    SqlCommand cmdTimKiemDH = new SqlCommand("sp_TK_DatHang", connect);
                    cmdTimKiemDH.CommandText = "sp_TK_DatHang";
                    cmdTimKiemDH.CommandType = CommandType.StoredProcedure;
                    SqlParameter paraMa = new SqlParameter("@MaDatHang", txtTimKiemDH.Text);
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

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}
