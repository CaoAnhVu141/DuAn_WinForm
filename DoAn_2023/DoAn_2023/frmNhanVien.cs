using OfficeOpenXml;
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
using OfficeOpenXml;




namespace DoAn_2023
{
    public partial class frmNhanVien : Form
    {
        public frmNhanVien()
        {
            InitializeComponent();

        }
        DB_Connect tt = new DB_Connect();
        SqlCommand cmdnv;
        SqlDataAdapter adapnv;
        DataTable tbnv;
        SqlConnection conn = new SqlConnection("Data Source=DESKTOP-88SK3C6;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        private void Form1_Load(object sender, EventArgs e)
        {
            txtChucVu.Focus();

            dgvNhanVien.DataSource = tt.ExcuteTable("sp_LaydanhsachNV");
           


        }


        private static frmNhanVien nhanvien = null;



        public static frmNhanVien NhanVien
        {
            get
            {
                if (nhanvien == null || nhanvien.IsDisposed)
                {
                    nhanvien = new frmNhanVien();
                }
                return nhanvien;
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "_spThemDSNV";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                //them cac tham so

                SqlParameter ma = new SqlParameter("@MaNV", txtMaNhanVien.Text);
                SqlParameter mach = new SqlParameter("@MaCH", txtMaCH.Text);
                SqlParameter ten = new SqlParameter("@TenNV", txtTenNhanVien.Text);
                SqlParameter diachi = new SqlParameter("@Diachi", txtDiaChi.Text);
                SqlParameter sdt = new SqlParameter("@SDT", txtSoDienThoai.Text);
                SqlParameter luong = new SqlParameter("@Luong", txtLuong.Text);
                SqlParameter chucvu = new SqlParameter("@Chucvu", txtChucVu.Text);

                cmdnv.Parameters.Add(ma);
                cmdnv.Parameters.Add(mach);
                cmdnv.Parameters.Add(ten);
                cmdnv.Parameters.Add(diachi);
                cmdnv.Parameters.Add(sdt);
                cmdnv.Parameters.Add(luong);
                cmdnv.Parameters.Add(chucvu);


                //Thực thi câu lệnh

                if (cmdnv.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã thêm thành công");
                    //GenerateStudentID();
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi " + ex.Message);
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
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_XóaDSNV";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                cmdnv.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = txtMaNhanVien.Text;

                if (cmdnv.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Xóa thành công");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gì đó á " + ex);
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
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "_spSuaDSNV";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;


                //thực hiện cập nhật

                SqlParameter ma = new SqlParameter("@MaNV", txtMaNhanVien.Text);
                SqlParameter mach = new SqlParameter("@MaCH", txtMaCH.Text);
                SqlParameter ten = new SqlParameter("@TenNV", txtTenNhanVien.Text);
                SqlParameter diachi = new SqlParameter("@Diachi", txtDiaChi.Text);
                SqlParameter sdt = new SqlParameter("@SDT", txtSoDienThoai.Text);
                SqlParameter luong = new SqlParameter("@Luong", txtLuong.Text);
                SqlParameter chucvu = new SqlParameter("@Chucvu", txtChucVu.Text);

                cmdnv.Parameters.Add(ma);
                cmdnv.Parameters.Add(mach);
                cmdnv.Parameters.Add(ten);
                cmdnv.Parameters.Add(diachi);
                cmdnv.Parameters.Add(sdt);
                cmdnv.Parameters.Add(luong);
                cmdnv.Parameters.Add(chucvu);


                //thực thi câu lệnh
                if (cmdnv.ExecuteNonQuery() > 0)
                {

                    MessageBox.Show("Sửa thành công rồi nha");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi đâu đó rồi nè " + ex.Message);
            }
            finally
            {
                conn.Close();
            }

        }

        private void dgvNhanVien_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvNhanVien.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvNhanVien.SelectedRows[0];

                string data1 = selectedRow.Cells["MaNV"].Value.ToString();
                string data0 = selectedRow.Cells["MaCH"].Value.ToString();
                string data2 = selectedRow.Cells["TenNV"].Value.ToString();
                string data3 = selectedRow.Cells["Diachi"].Value.ToString();
                string data4 = selectedRow.Cells["SDT"].Value.ToString();
                string data5 = selectedRow.Cells["Luong"].Value.ToString();
                string data6 = selectedRow.Cells["ChucVu"].Value.ToString();


                txtMaNhanVien.Text = data1;
                txtMaCH.Text = data0;
                txtTenNhanVien.Text = data2;
                txtDiaChi.Text = data3;
                txtSoDienThoai.Text = data4;
                txtLuong.Text = data5;
                txtChucVu.Text = data6;
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvNhanVien.DataSource = tt.ExcuteTable("sp_LaydanhsachNV");
        }

        private void frmNhanVien_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dia = MessageBox.Show("Bạn muốn thoát hả", "Thông báo của bạn", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
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

            cl1.Value2 = "Mã Nhân Viên";

            cl1.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Mã Cửa Hàng";

            cl2.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Tên Nhân Viên";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Địa chỉ";

            cl4.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Số Điện Thoại";

            cl5.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Lương";

            cl6.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl7 = oSheet.get_Range("G3", "G3");

            cl7.Value2 = "Chức Vụ";

            cl7.ColumnWidth = 20.0;



            //tạo tiêu đề thông báo
            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("G3", "G3");





            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "G3");

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



        private void btnInExcel_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaNV");
            DataColumn col2 = new DataColumn("MaCH");
            DataColumn col3 = new DataColumn("TenNV");
            DataColumn col4 = new DataColumn("DiaChi");
            DataColumn col5 = new DataColumn("SDT");
            DataColumn col6 = new DataColumn("Luong");
            DataColumn col7 = new DataColumn("ChucVu");


            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);
            dt1.Columns.Add(col4);
            dt1.Columns.Add(col5);
            dt1.Columns.Add(col6);
            dt1.Columns.Add(col7);



            foreach (DataGridViewRow data in dgvNhanVien.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;
                dtrow[4] = data.Cells[4].Value;
                dtrow[5] = data.Cells[5].Value;
                dtrow[6] = data.Cells[6].Value;



                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "ThuMucNV", "DANH SÁCH NHÂN VIÊN");
        }

        private void txtLuong_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Từ chối ký tự không phải số
            }
        }

        private void txtLuong_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

