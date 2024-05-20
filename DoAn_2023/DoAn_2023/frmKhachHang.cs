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
    public partial class frmKhachHang : Form
    {
        public frmKhachHang()
        {
            InitializeComponent();
        }
        DB_Connect tt = new DB_Connect();
        SqlCommand cmdkh;
        SqlDataAdapter adapkh;
        DataTable tbkh;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        //thực hiện kiểm tra

        private static frmKhachHang khachhang = null;



        public static frmKhachHang KhachHang
        {
            get
            {
                if (khachhang == null || khachhang.IsDisposed)
                {
                    khachhang = new frmKhachHang();
                }
                return khachhang;
            }
        }


        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdkh = new SqlCommand();
                cmdkh.CommandText = "sp_ThemDSKHH";
                cmdkh.CommandType = CommandType.StoredProcedure;
                cmdkh.Connection = conn;

                SqlParameter ma = new SqlParameter("@MaKHH", txtMaKhachHang.Text);
                SqlParameter ten = new SqlParameter("@TenKHH", txtTenKhachHang.Text);
                SqlParameter sdt = new SqlParameter("@SDT", txtSoDienThoai.Text);
                SqlParameter diachi = new SqlParameter("@DiaChi", txtDiaChi.Text);

                cmdkh.Parameters.Add(ma);
                cmdkh.Parameters.Add(ten);
                cmdkh.Parameters.Add(sdt);
                cmdkh.Parameters.Add(diachi);
              



                if (cmdkh.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã thêm thành công");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = tt.ExcuteTable("sp_LayDSKH");
        }

        private void dgvKhachHang_SelectionChanged(object sender, EventArgs e)
        {
            if(dgvKhachHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectedRow = dgvKhachHang.SelectedRows[0];

                string data1 = selectedRow.Cells["MaKH"].Value.ToString();
                string data2 = selectedRow.Cells["TenKH"].Value.ToString();
                string data3 = selectedRow.Cells["SDT"].Value.ToString();
                string data4 = selectedRow.Cells["DiaChi"].Value.ToString();

                txtMaKhachHang.Text = data1;
                txtTenKhachHang.Text = data2;
                txtSoDienThoai.Text = data3;
                txtDiaChi.Text = data4;

            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvKhachHang.DataSource = tt.ExcuteTable("sp_LayDSKH");
            XoaHet();
        }

        ////hàm xóa hết trên textbox
        private void XoaHet()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtDiaChi.Text = "";
            txtSoDienThoai.Text = "";
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdkh = new SqlCommand();
                cmdkh.CommandText = "sp_XoaDSKHH";
                cmdkh.CommandType = CommandType.StoredProcedure;
                cmdkh.Connection = conn;

                SqlParameter ma = new SqlParameter("@MaKH", txtMaKhachHang.Text);
                

                cmdkh.Parameters.Add(ma);
               
                if (cmdkh.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã xóa thành công");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi" + ex.Message);
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
                cmdkh = new SqlCommand();
                cmdkh.CommandText = "sp_SuaDSKHH";
                cmdkh.CommandType = CommandType.StoredProcedure;
                cmdkh.Connection = conn;

                SqlParameter ma = new SqlParameter("@MaKHH", txtMaKhachHang.Text);
                SqlParameter ten = new SqlParameter("@TenKHH", txtTenKhachHang.Text);
                SqlParameter sdt = new SqlParameter("@SDT", txtSoDienThoai.Text);
                SqlParameter diachi = new SqlParameter("@DiaChi", txtDiaChi.Text);

                cmdkh.Parameters.Add(ma);
                cmdkh.Parameters.Add(ten);
                cmdkh.Parameters.Add(sdt);
                cmdkh.Parameters.Add(diachi);




                if (cmdkh.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã sửa thành công");
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show("Loi" + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void frmKhachHang_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult dia = MessageBox.Show("Bạn muốn thoát à", "Thông báo của bạn", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

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

            cl1.Value2 = "Mã khách hàng";

            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên khách hàng";

            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Số DT";
            cl3.ColumnWidth = 19.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Địa chỉ";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");



            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "D3");

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

        private void btnXuat_Click(object sender, EventArgs e)
        {
            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaKH");
            DataColumn col2 = new DataColumn("TenKH");
            DataColumn col3 = new DataColumn("SDT");
            DataColumn col4 = new DataColumn("DiaChi");

            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);

            dt1.Columns.Add(col4);


            foreach (DataGridViewRow data in dgvKhachHang.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;


                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "danhsach", "DANH SÁCH KHÁCH HÀNG");
        }

       
    }
}
