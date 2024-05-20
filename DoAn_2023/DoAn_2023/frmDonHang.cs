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
    public partial class frmDonHang : Form
    {
        public frmDonHang()
        {
            InitializeComponent();
            PhatSinhMa();
        }


        DB_Connect tt = new DB_Connect();
        SqlCommand cmd;
        SqlDataAdapter adapkh;
        DataTable tbkh;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void frmDonHang_Load(object sender, EventArgs e)
        {
            dgvDonHang.DataSource = tt.ExcuteTable("sp_LayDSDHN");
            PhatSinhMa();
        }

        /// <summary>
        /// thực hiện thủ tục khóa form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private static frmDonHang donhang = null;



        public static frmDonHang DonHang
        {
            get
            {
                if (donhang == null || donhang.IsDisposed)
                {
                    donhang = new frmDonHang();
                }
                return donhang;
            }
        }


        private void dgvDonHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvDonHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvDonHang.SelectedRows[0];

                string data0 = selectRow.Cells["MaDonHang"].Value.ToString();

                string data1 = selectRow.Cells["MaDatHang"].Value.ToString();
                string data2 = selectRow.Cells["MaSP"].Value.ToString();
                string data3 = selectRow.Cells["MaKH"].Value.ToString();
                string data4 = selectRow.Cells["TrangThaiDH"].Value.ToString();
                string data5 = selectRow.Cells["PTTT"].Value.ToString();
                string data6 = selectRow.Cells["NgayGHDK"].Value.ToString();
                string data7 = selectRow.Cells["GhiChu"].Value.ToString();
               

                txtMaDonHang.Text = data0;
                txtMadat.Text = data1;
                txtMaSP.Text = data2;
                txtMaKH.Text = data3;
                txtTTDH.Text = data4;
                cboPTTT.Text = data5;
                dateDatHang.Text = data6;
                txtGhiChu.Text = data7;
                

            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_ThemDonHang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //them cac tham so



                //SqlParameter madathang = new SqlParameter("@MaDat",txtMadat.Text);

                SqlParameter madathang = new SqlParameter("@MaDatHang", txtMadat.Text);
                SqlParameter masp = new SqlParameter("@MaSP", txtMaSP.Text);
                SqlParameter makh = new SqlParameter("@MaKH", txtMaKH.Text);
                SqlParameter mach = new SqlParameter("@TrangThai", txtTTDH.Text);
                SqlParameter tenkh = new SqlParameter("@PTTT", cboPTTT.Text);
                SqlParameter tensp = new SqlParameter("@NgayGHDK", dateDatHang.Value);
                SqlParameter nhacungcap = new SqlParameter("@GhiChu", txtGhiChu.Text);

                //SqlParameter diachi = new SqlParameter("@DiaChi", txtDiachi.Text);
                //SqlParameter htm = new SqlParameter("@HinhThucMua", txtHTM.Text);
                //SqlParameter ngaydat = new SqlParameter("@NgayDatHang", dateDatHang.Value);
                //SqlParameter gia = new SqlParameter("@Gia", txtGhiChu.Text);



                //////cmd.Parameters.Add(madathang);
                ///
                cmd.Parameters.Add(madathang);
                cmd.Parameters.Add(masp);
                cmd.Parameters.Add(makh);
                cmd.Parameters.Add(mach);
                cmd.Parameters.Add(tenkh);
                cmd.Parameters.Add(tensp);
                cmd.Parameters.Add(nhacungcap);

                //cmd.Parameters.Add(diachi);
                //cmd.Parameters.Add(htm);
                //cmd.Parameters.Add(ngaydat);
                //cmd.Parameters.Add(gia);



                if (cmd.ExecuteNonQuery() > 0)
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

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_XoaDonHang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlParameter madonhang = new SqlParameter("@MaDonHang", txtMaDonHang.Text);
                SqlParameter madathang = new SqlParameter("@MaDatHang", txtMadat.Text);


                cmd.Parameters.Add(madonhang);
                cmd.Parameters.Add(madathang);




                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã xóa thông tin thành công");
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

        

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmd = new SqlCommand();
                cmd.CommandText = "sp_SuaDonHang";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                //them cac tham so



                //SqlParameter madathang = new SqlParameter("@MaDat",txtMadat.Text);

                SqlParameter madathang = new SqlParameter("@MaDatHang", txtMadat.Text);
                SqlParameter masp = new SqlParameter("@MaSP", txtMaSP.Text);
                SqlParameter makh = new SqlParameter("@MaKH", txtMaKH.Text);
                SqlParameter mach = new SqlParameter("@TrangThai", txtTTDH.Text);
                SqlParameter tenkh = new SqlParameter("@PTTT", cboPTTT.Text);
                SqlParameter tensp = new SqlParameter("@NgayGHDK", dateDatHang.Value);
                SqlParameter nhacungcap = new SqlParameter("@GhiChu", txtGhiChu.Text);


                //SqlParameter diachi = new SqlParameter("@DiaChi", txtDiachi.Text);
                //SqlParameter htm = new SqlParameter("@HinhThucMua", txtHTM.Text);
                //SqlParameter ngaydat = new SqlParameter("@NgayDatHang", dateDatHang.Value);
                //SqlParameter gia = new SqlParameter("@Gia", txtGhiChu.Text);



                //////cmd.Parameters.Add(madathang);
                ///
                cmd.Parameters.Add(madathang);
                cmd.Parameters.Add(masp);
                cmd.Parameters.Add(makh);
                cmd.Parameters.Add(mach);
                cmd.Parameters.Add(tenkh);
                cmd.Parameters.Add(tensp);
                cmd.Parameters.Add(nhacungcap);

                //cmd.Parameters.Add(diachi);
                //cmd.Parameters.Add(htm);
                //cmd.Parameters.Add(ngaydat);
                //cmd.Parameters.Add(gia);



                if (cmd.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã sửa thông tin thành công");
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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvDonHang.DataSource = tt.ExcuteTable("sp_LayDSDHN");
        }


        //hàm phát sinh mã đơn hàng tự động
        private void PhatSinhMa()
        {
            try
            {
                string query = "INSERT INTO DonHang DEFAULT VALUES;" +
                               " SELECT SCOPE_IDENTITY();";

                cmd = new SqlCommand(query, conn);

                object data = cmd.ExecuteScalar();

                if (data == null && cmd.ExecuteNonQuery() > 0)
                {
                    int ID = Convert.ToInt32(data);
                    string Ma = "DHA" + ID.ToString("D2"); // "D2" đảm bảo mã có 2 chữ số
                    txtMaDonHang.Text = Ma;
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

       

        private void frmDonHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dia = MessageBox.Show("Bạn muốn thoát hả", "Thông báo của bạn", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (dia == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void grDatHang_Enter(object sender, EventArgs e)
        {

        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            //{
            //    e.Handled = true; // Từ chối ký tự không phải số
            //}
        }


        /// <summary>
        /// hàm thực hiện in dữ liệu vào excel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        public void XuatFile(DataTable dataTable, string sheetName, string title)
        {
            //Tạo các đối tượng Excel

            Microsoft.Office.Interop.Excel.Application oExcel = new Microsoft.Office.Interop.Excel.Application();

            Microsoft.Office.Interop.Excel.Workbooks oBooks;

            Microsoft.Office.Interop.Excel.Sheets oSheets;

            Microsoft.Office.Interop.Excel.Workbook oBook;

            Microsoft.Office.Interop.Excel.Worksheet oSheet;

            //Tạo mới một Excel WorkBook 

            oExcel.Visible = true;

            oExcel.DisplayAlerts = false;

            oExcel.Application.SheetsInNewWorkbook = 1;

            oBooks = oExcel.Workbooks;

            oBook = (Microsoft.Office.Interop.Excel.Workbook)(oExcel.Workbooks.Add(Type.Missing));

            oSheets = oBook.Worksheets;

            oSheet = (Microsoft.Office.Interop.Excel.Worksheet)oSheets.get_Item(1);

            oSheet.Name = sheetName;

            // Tạo phần Tiêu đề
            Microsoft.Office.Interop.Excel.Range head = oSheet.get_Range("A1", "G1");

            head.MergeCells = true;

            head.Value2 = title;

            head.Font.Bold = true;

            head.Font.Name = "Times New Roman";

            head.Font.Size = "20";

            head.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo tiêu đề cột 

            Microsoft.Office.Interop.Excel.Range cl1 = oSheet.get_Range("A3", "A3");

            cl1.Value2 = "Mã Đơn Hàng";

            cl1.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Mã Đặt Hàng";

            cl2.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Mã Sản Phẩm";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Tên Sản Phẩm";

            cl4.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Mã Khách Hàng";

            cl5.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Tên Khách Hàng";

            cl6.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Trạng Thái";

            cl7.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("H3", "H3");

            cl8.Value2 = "PTTT";

            cl8.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl9 = oSheet.get_Range("I3", "I3");

            cl9.Value2 = "Ngày GHDK";

            cl9.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl10 = oSheet.get_Range("J3", "J3");

            cl10.Value2 = "Ghi Chú";

            cl10.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl11 = oSheet.get_Range("K3", "K3");

            cl11.Value2 = "Giá";

            cl11.ColumnWidth = 20.0;

            //tạo tiêu đề thông báo
            Microsoft.Office.Interop.Excel.Range cl12 = oSheet.get_Range("K3", "K3");





            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "K3");

            rowHead.Font.Bold = true;

            // Kẻ viền

            rowHead.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Thiết lập màu nền

            rowHead.Interior.ColorIndex = 6;

            rowHead.HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;

            // Tạo mảng theo datatable

            object[,] arr = new object[dataTable.Rows.Count, dataTable.Columns.Count];

            //Chuyển dữ liệu từ DataTable vào mảng đối tượng

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                DataRow dataRow = dataTable.Rows[row];

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    arr[row, col] = dataRow[col];
                }
            }

            //Thiết lập vùng điền dữ liệu

            int rowStart = 4;

            int columnStart = 1;

            int rowEnd = rowStart + dataTable.Rows.Count - 2;

            int columnEnd = dataTable.Columns.Count;

            // Ô bắt đầu điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c1 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowStart, columnStart];

            // Ô kết thúc điền dữ liệu

            Microsoft.Office.Interop.Excel.Range c2 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnEnd];

            // Lấy về vùng điền dữ liệu

            Microsoft.Office.Interop.Excel.Range range = oSheet.get_Range(c1, c2);

            //Điền dữ liệu vào vùng đã thiết lập

            range.Value2 = arr;

            // Kẻ viền

            range.Borders.LineStyle = Microsoft.Office.Interop.Excel.Constants.xlSolid;

            // Căn giữa cột mã nhân viên

            //Microsoft.Office.Interop.Excel.Range c3 = (Microsoft.Office.Interop.Excel.Range)oSheet.Cells[rowEnd, columnStart];

            //Microsoft.Office.Interop.Excel.Range c4 = oSheet.get_Range(c1, c3);

            //Căn giữa cả bảng 
            oSheet.get_Range(c1, c2).HorizontalAlignment = Microsoft.Office.Interop.Excel.XlHAlign.xlHAlignCenter;
        }


        private void btnInEXcel_Click(object sender, EventArgs e)
        {
           


            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaDonHang");
            DataColumn col2 = new DataColumn("MaDatHang");
            DataColumn col3 = new DataColumn("MaSP");
            DataColumn col4 = new DataColumn("TenSP");
            DataColumn col5 = new DataColumn("MaKH");
            DataColumn col6 = new DataColumn("TenKH");
            DataColumn col7 = new DataColumn("TrangThaiDH");
            DataColumn col8 = new DataColumn("PTTT");
            DataColumn col9 = new DataColumn("NgayGHDK");
            DataColumn col10 = new DataColumn("GhiChu");
            DataColumn col11 = new DataColumn("Gia");
           




            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);
            dt1.Columns.Add(col4);
            dt1.Columns.Add(col5);
            dt1.Columns.Add(col6);
            dt1.Columns.Add(col7);
            dt1.Columns.Add(col8);
            dt1.Columns.Add(col9);
            dt1.Columns.Add(col10);
            dt1.Columns.Add(col11);




            foreach (DataGridViewRow data in dgvDonHang.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;
                dtrow[4] = data.Cells[4].Value;
                dtrow[5] = data.Cells[5].Value;
                dtrow[6] = data.Cells[6].Value;
                dtrow[7] = data.Cells[7].Value;
                dtrow[8] = data.Cells[8].Value;
                dtrow[9] = data.Cells[9].Value;
                dtrow[10] = data.Cells[10].Value;





                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "ThuMucDH", "DANH SÁCH THÔNG TIN ĐƠN HÀNG");
        }
    }
}
