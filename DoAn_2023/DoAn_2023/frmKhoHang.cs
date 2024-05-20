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
using System.IO;
using OfficeOpenXml;
using System.Security.Cryptography.X509Certificates;


namespace DoAn_2023
{
    public partial class frmKhoHang : Form
    {
        public frmKhoHang()
        {
            InitializeComponent();
        }

        //thủ tục kết nối database

        DB_Connect tt = new DB_Connect();
        SqlCommand cmdnv;
        SqlDataAdapter adapnv;
        DataTable tbnv;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        //Load dữ liệu từ database
        private void frmKhoHang_Load(object sender, EventArgs e)
        {
            dgvKhoHang.DataSource = tt.ExcuteTable("sp_LayDSCKH");
        }



        private static frmKhoHang khohang = null;



        public static frmKhoHang KHoHang
        {
            get
            {
                if(khohang == null || khohang.IsDisposed)
                {
                    khohang = new frmKhoHang();
                }
                return khohang;
            }
        }

        private void dgvKhoHang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //thêm kho hàng
        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_ThemDSKH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                //them cac tham so

                SqlParameter ma = new SqlParameter("@Makho", txtMaKho.Text);
                SqlParameter ten = new SqlParameter("@TenKho", txtTenKho.Text);
                SqlParameter lsxiat = new SqlParameter("@LSXuatHang",dateLSXuatHang.Text);
                SqlParameter slton = new SqlParameter("@Soluongton", txtSoLuongTon.Text);

                cmdnv.Parameters.Add(ma);
                cmdnv.Parameters.Add(ten);
                cmdnv.Parameters.Add(lsxiat);
                cmdnv.Parameters.Add(slton);




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

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            dgvKhoHang.DataSource = tt.ExcuteTable("sp_LayDSCKH");

            Xoahet();
        }

        //Xóa hết 

        private void Xoahet()
        {
            txtMaKho.Text = "";
            txtTenKho.Text = "";
            txtSoLuongTon.Text = "";
        }


        private void dgvKhoHang_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvKhoHang.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvKhoHang.SelectedRows[0];

                string data1 = selectRow.Cells["MaKho"].Value.ToString();
                string data2 = selectRow.Cells["TenKho"].Value.ToString();
                string data3 = selectRow.Cells["LSXuatHang"].Value.ToString();
                string data4 = selectRow.Cells["SoLuongTon"].Value.ToString();

                txtMaKho.Text = data1;
                txtTenKho.Text = data2;
                dateLSXuatHang.Text = data3;
                txtSoLuongTon.Text = data4;

            }
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {

                conn.Open();
                cmdnv = new SqlCommand();
                cmdnv.CommandText = "sp_XoaDSKH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;

                SqlParameter ma = new SqlParameter("@MaKho",txtMaKho.Text);
                cmdnv.Parameters.Add(ma);

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
                cmdnv.CommandText = "sp_SuaDSKH";
                cmdnv.CommandType = CommandType.StoredProcedure;
                cmdnv.Connection = conn;


                //thực hiện cập nhật

                cmdnv.Parameters.Add("@MaKho", SqlDbType.VarChar).Value = txtMaKho.Text;
                cmdnv.Parameters.Add("@TenKho", SqlDbType.NVarChar).Value = txtTenKho.Text;
                cmdnv.Parameters.Add("@LSXuatHang", SqlDbType.Date).Value = dateLSXuatHang.Text;
                cmdnv.Parameters.Add("@Soluongton", SqlDbType.VarChar).Value = txtSoLuongTon.Text;



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

        private void frmKhoHang_FormClosing(object sender, FormClosingEventArgs e)
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


        //hàm thực hiện xuất file excel


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

            cl1.Value2 = "Mã kho";

            cl1.ColumnWidth = 12;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên kho";

            cl2.ColumnWidth = 25.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Lịch sử xuất hàng";
            cl3.ColumnWidth = 19.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Số lượng hàng tồn";

            cl4.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("D3", "D3");





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





        private void btnTimKiem_Click(object sender, EventArgs e)
        {
     

            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaKho");
            DataColumn col2 = new DataColumn("TenKho");
            DataColumn col3 = new DataColumn("LSXuatHang");
            DataColumn col4 = new DataColumn("SoLuongTon");

            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);

            dt1.Columns.Add(col4);


            foreach (DataGridViewRow data in dgvKhoHang.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;


                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "danhsach", "Cửa Hàng");



        }

        
    }
}
 
    
