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
using VBSQLHelper;
namespace DoAn_2023
{
    public partial class frmMuaHang : Form
    {
        public frmMuaHang()
        {
            InitializeComponent();
            PhatSinhMa();
        }

        DB_Connect tt = new DB_Connect();
        SqlCommand cmd;
        SqlDataAdapter adap;
        DataTable tbnv;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");



        //thực hiện thủ tục mở form

        private static frmMuaHang muahang = null;



        public static frmMuaHang MuaHang
        {
            get
            {
                if (muahang == null || muahang.IsDisposed)
                {
                    muahang = new frmMuaHang();
                }
                return muahang;
            }
        }



        //load form

        private void frmDonHang_Load(object sender, EventArgs e)
        {
            dgvDatHang.DataSource = tt.ExcuteTable("sp_LayDatHang");
            PhatSinhMa();
        }

        private void dgvDatHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDatHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvDatHang.SelectedRows[0];

                string data1 = selectRow.Cells["MaDatHang"].Value.ToString();
                string data2 = selectRow.Cells["MaSP"].Value.ToString();
                string data3 = selectRow.Cells["MaKH"].Value.ToString();
                string data4 = selectRow.Cells["MaCH"].Value.ToString();
                string data5 = selectRow.Cells["TenKH"].Value.ToString();
                string data6 = selectRow.Cells["TenSanPham"].Value.ToString();
                string data7 = selectRow.Cells["NhaCungCap"].Value.ToString();
                string data8 = selectRow.Cells["DiaChi"].Value.ToString();
                string data9 = selectRow.Cells["HinhThucMua"].Value.ToString();
                string data10 = selectRow.Cells["Gia"].Value.ToString();


                txtMadat.Text = data1;
                txtMaSP.Text = data2;
                txtMaKH.Text = data3;
                txtMaCH.Text = data4;
                txtTenKH.Text = data5;
                txtTenSP.Text = data6;
                txtNhaCC.Text = data7;
                txtDiachi.Text = data8;
                cboHinhThucMua.Text = data9;
                txtGia.Text = data10;

            }
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_ThemDatHang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //them cac tham so



                //SqlParameter madathang = new SqlParameter("@MaDat",txtMadat.Text);
                SqlParameter masp = new SqlParameter("@MaSP", txtMaSP.Text);
                SqlParameter makh = new SqlParameter("@MaKH", txtMaKH.Text);
                SqlParameter mach = new SqlParameter("@MaCH", txtMaCH.Text);
                SqlParameter tenkh = new SqlParameter("@TenKH", txtTenKH.Text);
                SqlParameter tensp = new SqlParameter("@TenSanPham", txtTenSP.Text);
                SqlParameter nhacungcap = new SqlParameter("@NhaCungCap", txtNhaCC.Text);
                SqlParameter diachi = new SqlParameter("@DiaChi", txtDiachi.Text);
                SqlParameter htm = new SqlParameter("@HinhThucMua", cboHinhThucMua.Text);
                SqlParameter ngaydat = new SqlParameter("@NgayDatHang", dateDatHang.Value);
                SqlParameter gia = new SqlParameter("@Gia", txtGia.Text);



                //////cmd.Parameters.Add(madathang);
                cmd.Parameters.Add(masp);
                cmd.Parameters.Add(makh);
                cmd.Parameters.Add(mach);
                cmd.Parameters.Add(tenkh);
                cmd.Parameters.Add(tensp);
                cmd.Parameters.Add(nhacungcap);
                cmd.Parameters.Add(diachi);
                cmd.Parameters.Add(htm);
                cmd.Parameters.Add(ngaydat);
                cmd.Parameters.Add(gia);



                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Thêm thành công");
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

        //private void btnXoa_Click(object sender, EventArgs e)
        //{

        //    try
        //    {
        //        conn.Open();
        //        cmd = new SqlCommand();
        //        cmd.CommandText = "sp_Xoa";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = conn;

        //        SqlParameter madathang = new SqlParameter("@MaDatHang", txtMadat.Text);
        //        cmd.Parameters.Add(madathang);
        //        if (cmd.ExecuteNonQuery() > 0)
        //        {
        //            MessageBox.Show("Xóa thông tin thành công");
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvDatHang.DataSource = tt.ExcuteTable("sp_LayDatHang");
        }

        //private void btnSua_Click(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        conn.Open();
        //        cmd = new SqlCommand();
        //        cmd.CommandText = "sp_SuaDatHang";
        //        cmd.CommandType = CommandType.StoredProcedure;
        //        cmd.Connection = conn;

        //        //them cac tham so




        //        SqlParameter madathang = new SqlParameter("@MaDatHang", txtMadat.Text);
        //        SqlParameter masp = new SqlParameter("@MaSP", txtMaSP.Text);
        //        SqlParameter makh = new SqlParameter("@MaKH", txtMaKH.Text);
        //        SqlParameter mach = new SqlParameter("@MaCH", txtMaCH.Text);
        //        SqlParameter tenkh = new SqlParameter("@TenKH", txtTenKH.Text);
        //        SqlParameter tensp = new SqlParameter("@TenSanPham", txtTenSP.Text);
        //        SqlParameter nhacungcap = new SqlParameter("@NhaCungCap", txtNhaCC.Text);
        //        SqlParameter diachi = new SqlParameter("@DiaChi", txtDiachi.Text);
        //        SqlParameter htm = new SqlParameter("@HinhThucMua", txtHTM.Text);
        //        SqlParameter ngaydat = new SqlParameter("@NgayDatHang", dateDatHang.Value);
        //        SqlParameter gia = new SqlParameter("@Gia", txtGia.Text);



        //        cmd.Parameters.Add(madathang);
        //        cmd.Parameters.Add(masp);
        //        cmd.Parameters.Add(makh);
        //        cmd.Parameters.Add(mach);
        //        cmd.Parameters.Add(tenkh);
        //        cmd.Parameters.Add(tensp);
        //        cmd.Parameters.Add(nhacungcap);
        //        cmd.Parameters.Add(diachi);
        //        cmd.Parameters.Add(htm);
        //        cmd.Parameters.Add(ngaydat);
        //        cmd.Parameters.Add(gia);



        //        if (cmd.ExecuteNonQuery() > 0)
        //        {
        //            MessageBox.Show("Sửa thông tin thành công");
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        MessageBox.Show("Lỗi " + ex.Message);
        //    }
        //    finally
        //    {
        //        conn.Close();
        //    }
        //}

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }




        //thực hiện kiểm tra

        //private static frmDonHang donhang = null;



        //public static frmDonHang DonHang
        //{
        //    get
        //    {
        //        if (donhang == null || donhang.IsDisposed)
        //        {
        //            donhang = new frmDonHang();
        //        }
        //        return donhang;
        //    }
        //}



        //thực hiện hàm phát sinh mã tự động

        //public const string chuoiketnoi = "Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True";

        private void PhatSinhMa()
        {
            try
            {
                    string query = "INSERT INTO DatHang DEFAULT VALUES;" +
                                   " SELECT SCOPE_IDENTITY();";

                    cmd = new SqlCommand(query, conn);

                    object data = cmd.ExecuteScalar();
                    
                    if (data == null && cmd.ExecuteNonQuery() > 0)
                    {
                        int ID = Convert.ToInt32(data);
                        string Ma = "DH" + ID.ToString("D2"); // "D2" đảm bảo mã có 2 chữ số
                        txtMadat.Text = Ma;
                    }

                    else
                    {
                        MessageBox.Show("Đáng tiếc, không thành công rôi");
                    }
                }
            
            catch (Exception ex)
            {
                
            }
        }

        private void txtGia_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Từ chối ký tự không phải số
            }
        }

        private void cboHinhThucMua_SelectedIndexChanged(object sender, EventArgs e)
        {
             
        }
    }
}
