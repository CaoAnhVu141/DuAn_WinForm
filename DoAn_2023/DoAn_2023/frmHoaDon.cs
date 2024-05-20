using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_2023
{
    public partial class frmHoaDon : Form
    {
        public frmHoaDon()
        {
            InitializeComponent();
        }

        DB_Connect tt = new DB_Connect();
        SqlCommand cmdnv;
        SqlDataAdapter adapnv;
        DataTable tbnv;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");


        //thực hiện kiểm tra thủ tục mở form

        private static frmHoaDon hoadon = null;



        public static frmHoaDon HoaDon
        {
            get
            {
                if (hoadon == null || hoadon.IsDisposed)
                {
                    hoadon = new frmHoaDon();
                }
                return hoadon;
            }
        }


        //

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            
        }

       
        private void dgvHoaDon_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvHoaDon.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvHoaDon.SelectedRows[0];

                string data0 = selectRow.Cells["MaHoaDon"].Value.ToString();
                string data1 = selectRow.Cells["MaDatHang"].Value.ToString();
                string data2 = selectRow.Cells["MaDonHang"].Value.ToString();
                string data3 = selectRow.Cells["MaSP"].Value.ToString();
                string data4 = selectRow.Cells["MaKH"].Value.ToString();
               


                txtMaHoaDon.Text = data0;
                txtMadat.Text = data1;
                txtMaDonHang.Text = data2;
                txtMaSP.Text = data3;
                txtMaKH.Text = data4;


            }
        }

        

        private void frmHoaDon_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnHD_Click(object sender, EventArgs e)
        {
            dgvHoaDon.DataSource = tt.ExcuteTable("sp_layHoaDon");
        }

        
        //xuất import chi tiết cho đơn hàng
        
        private void btnHDCT_Click(object sender, EventArgs e)
        {

        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_TaoHoaDon";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                //them cac tham so



                //SqlParameter madathang = new SqlParameter("@MaDat",txtMadat.Text);

                SqlParameter mahd = new SqlParameter("@MaHoaDon", txtMaHoaDon.Text);
                SqlParameter madat = new SqlParameter("@MaDatHang", txtMadat.Text);
                SqlParameter madh = new SqlParameter("@MaDonHang", txtMaDonHang.Text);
                SqlParameter masp = new SqlParameter("@MaSP", txtMaSP.Text);
                SqlParameter makhkh = new SqlParameter("@MaKH", txtMaKH.Text);


                cmdnv.Parameters.Add(mahd);
                cmdnv.Parameters.Add(madat);
                cmdnv.Parameters.Add(madh);
                cmdnv.Parameters.Add(masp);
                cmdnv.Parameters.Add(makhkh);

               



                if (cmdnv.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã thêm thông tin thành công");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }
    }
}
