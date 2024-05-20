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
    public partial class frmCuaHang : Form
    {


        public frmCuaHang()
        {
            InitializeComponent();

        }

        DB_Connect tt = new DB_Connect();
        SqlCommand cmdnv;
        SqlDataAdapter adapnv;
        DataTable tbnv;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");


        //thực hiện lấy dữ liệu



        // Constructor để nhận instance của lớp lưu trữ


        //thực hiện kiểm tra form

        private static frmCuaHang cuahang = null;



        public static frmCuaHang CuaHang
        {
            get
            {
                if (cuahang == null || cuahang.IsDisposed)
                {
                    cuahang = new frmCuaHang();
                }
                return cuahang;
            }
        }



        private void dgvKhoHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void frmCuaHang_Load(object sender, EventArgs e)
        {
            dgvCuaHang.DataSource = tt.ExcuteTable("sp_LayDSCH");
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_ThemDSCH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                //them cac tham so

                cmdnv.Parameters.Add("@MaCH", SqlDbType.VarChar).Value = txtMaCuaHang.Text;
                cmdnv.Parameters.Add("@TenCH", SqlDbType.NVarChar).Value = txtTenCuaHang.Text;
                cmdnv.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                cmdnv.Parameters.Add("@MaKho", SqlDbType.VarChar).Value = txtMaKho.Text;
                cmdnv.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = txtMaNhanVien.Text;




                //Thực thi câu lệnh

                if (cmdnv.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã thêm thành công");
                }



            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi gì đó rồi nè " + ex.Message);
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
                cmdnv.CommandText = "sp_XoaDSCH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                cmdnv.Parameters.Add("@MaCH", SqlDbType.VarChar).Value = txtMaCuaHang.Text;

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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvCuaHang.DataSource = tt.ExcuteTable("sp_LayDSCH");

            XoaHet();
        }

        private void XoaHet()
        {
            txtMaCuaHang.Text = "";
            txtTenCuaHang.Text = "";
            txtDiaChi.Text = "";
            txtMaKho.Text = "";
            txtMaNhanVien.Text = "";
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_SuaDSCH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;


                //thực hiện cập nhật

                //cmdnv.Parameters.Add("@MaCH", SqlDbType.VarChar).Value = txtMaCuaHang.Text;
                //cmdnv.Parameters.Add("@TenCH", SqlDbType.NVarChar).Value = txtTenCuaHang.Text;
                //cmdnv.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                //cmdnv.Parameters.Add("@MaKho", SqlDbType.VarChar).Value = txtMaKho.Text;
                //cmdnv.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = txtMaNhanVien.Text;

                cmdnv.Parameters.Add("@MaCH", SqlDbType.VarChar).Value = txtMaCuaHang.Text;
                cmdnv.Parameters.Add("@TenCH", SqlDbType.NVarChar).Value = txtTenCuaHang.Text;
                cmdnv.Parameters.Add("@Diachi", SqlDbType.NVarChar).Value = txtDiaChi.Text;
                cmdnv.Parameters.Add("@MaKho", SqlDbType.VarChar).Value = txtMaKho.Text;
                cmdnv.Parameters.Add("@MaNV", SqlDbType.VarChar).Value = txtMaNhanVien.Text;


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

        private void dgvCuaHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvCuaHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvCuaHang.SelectedRows[0];

                string data1 = selectRow.Cells["MaCH"].Value.ToString();
                string data2 = selectRow.Cells["TenCH"].Value.ToString();
                string data3 = selectRow.Cells["DiaChi"].Value.ToString();
                string data4 = selectRow.Cells["MaKho"].Value.ToString();
                string data5 = selectRow.Cells["MaNV"].Value.ToString();


                txtMaCuaHang.Text = data1;
                txtTenCuaHang.Text = data2;
                txtDiaChi.Text = data3;
                txtMaKho.Text = data4;
                txtMaNhanVien.Text = data5;

            }
        }

        private void frmCuaHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dia = MessageBox.Show("Bạn muốn thoát à", "Thông báo của bạn", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
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

        private void btnTim_Click(object sender, EventArgs e)
        {

        }

        public void XuatFile(DataTable dataTable, string sheetName, string title)
        {
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

            cl1.Value2 = "Mã Cửa Hàng";

            cl1.ColumnWidth = 20;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên Cửa Hàng";

            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Địa Chỉ";
            cl3.ColumnWidth = 19.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Mã Kho";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Mã Nhân Viên";

            cl5.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("E3", "E3");




            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "E3");

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

        private void btnIndata_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaCH");
            DataColumn col2 = new DataColumn("TenCH");
            DataColumn col3 = new DataColumn("DiaChi");
            DataColumn col4 = new DataColumn("MaKho");
            DataColumn col5 = new DataColumn("MaNV");


            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);
            dt1.Columns.Add(col4);
            dt1.Columns.Add(col5);


            foreach (DataGridViewRow data in dgvCuaHang.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;
                dtrow[4] = data.Cells[4].Value;
              



                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "cuahang", "DỮ LIỆU THÔNG TIN CỬA HÀNG");

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }
    }
}

    
    
