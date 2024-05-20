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
    public partial class frmSanPham : Form
    {
        public frmSanPham()
        {
            InitializeComponent();
        }

        DB_Connect tt = new DB_Connect();
        SqlCommand cmdsp;
        SqlDataAdapter adapsp;
        DataTable tbsp;
        SqlConnection conn = new SqlConnection("Data Source=CAOVU;Initial Catalog=DuAn2023_NK04;Integrated Security=True");

        //thực hiện kiểm tra


        private static frmSanPham sanpham = null;



        public static frmSanPham SanPham
        {
            get
            {
                if (sanpham == null || sanpham.IsDisposed)
                {
                    sanpham = new frmSanPham();
                }
                return sanpham;
            }
        }



        private void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdsp = new SqlCommand();
                cmdsp.CommandText = "spThemDSSP";
                cmdsp.CommandType = CommandType.StoredProcedure;
                cmdsp.Connection = conn;

                //them cac tham so

                SqlParameter ma = new SqlParameter("@MaSP", txtMaSanPham.Text);
                SqlParameter ten = new SqlParameter("@TenSP", txtTenSanPham.Text);
                SqlParameter nhacc = new SqlParameter("@NhaCungCap", txtNhaCungCap.Text);
                SqlParameter gia = new SqlParameter("@Gia", txtGia.Text);
                SqlParameter mach = new SqlParameter("@MaCH", txtMaCuaHang.Text);
                SqlParameter mota = new SqlParameter("@MoTa", txtMoTa.Text);

                cmdsp.Parameters.Add(ma);
                cmdsp.Parameters.Add(ten);
                cmdsp.Parameters.Add(nhacc);
                cmdsp.Parameters.Add(gia);
                cmdsp.Parameters.Add(mach);

                cmdsp.Parameters.Add(mota);



                //Thực thi câu lệnh

                if (cmdsp.ExecuteNonQuery() > 0)
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

        private void btnLuu_Click(object sender, EventArgs e)
        {
            SqlCommandBuilder buiding = new SqlCommandBuilder();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            dgvSanPham.DataSource = tt.ExcuteTable("sp_LayDSSP");
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdsp = new SqlCommand();
                cmdsp.CommandText = "sp_XoaDSSP";
                cmdsp.CommandType = CommandType.StoredProcedure;
                cmdsp.Connection = conn;

                //them cac tham so

                SqlParameter ma = new SqlParameter("@MaSP", txtMaSanPham.Text);

                cmdsp.Parameters.Add(ma);



                //Thực thi câu lệnh

                if (cmdsp.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã xóa thành công");
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                cmdsp = new SqlCommand();
                cmdsp.CommandText = "sp_SuaDSSP";
                cmdsp.CommandType = CommandType.StoredProcedure;
                cmdsp.Connection = conn;

                //them cac tham so

                SqlParameter ma = new SqlParameter("@MaSP", txtMaSanPham.Text);
                SqlParameter ten = new SqlParameter("@TenSP", txtTenSanPham.Text);
                SqlParameter nhacc = new SqlParameter("@NhaCungCap", txtNhaCungCap.Text);
                SqlParameter gia = new SqlParameter("@Gia", txtGia.Text);
                SqlParameter mach = new SqlParameter("@MaCH", txtMaCuaHang.Text);

                SqlParameter mota = new SqlParameter("@MoTa", txtMoTa.Text);

                cmdsp.Parameters.Add(ma);
                cmdsp.Parameters.Add(ten);
                cmdsp.Parameters.Add(nhacc);
                cmdsp.Parameters.Add(gia);
                cmdsp.Parameters.Add(mach);

                cmdsp.Parameters.Add(mota);



                //Thực thi câu lệnh

                if (cmdsp.ExecuteNonQuery() > 0)
                {
                    MessageBox.Show("Bạn đã sửa thành công");
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
            dgvSanPham.DataSource = tt.ExcuteTable("sp_LayDSSP");
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void dgvSanPham_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvSanPham.SelectedRows.Count > 0)
            {
                DataGridViewRow selectRow = dgvSanPham.SelectedRows[0];


                string data1 = selectRow.Cells["MaSP"].Value.ToString();
                string data2 = selectRow.Cells["TenSP"].Value.ToString();
                string data3 = selectRow.Cells["NhaCungCap"].Value.ToString();
                string data4 = selectRow.Cells["Gia"].Value.ToString();
                string data5 = selectRow.Cells["MaCH"].Value.ToString();

                string data7 = selectRow.Cells["MoTa"].Value.ToString();



                txtMaSanPham.Text = data1;
                txtTenSanPham.Text = data2;
                txtNhaCungCap.Text = data3;
                txtGia.Text = data4;
                txtMaCuaHang.Text = data5;

                txtMoTa.Text = data7;
            }
        }

        //


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

            cl1.Value2 = "Mã Sản Phẩm";

            cl1.ColumnWidth = 15;

            Microsoft.Office.Interop.Excel.Range cl2 = oSheet.get_Range("B3", "B3");

            cl2.Value2 = "Tên Sản Phẩm";

            cl2.ColumnWidth = 15.0;

            Microsoft.Office.Interop.Excel.Range cl3 = oSheet.get_Range("C3", "C3");

            cl3.Value2 = "Nhà Cung Cấp";
            cl3.ColumnWidth = 20.0;

            Microsoft.Office.Interop.Excel.Range cl4 = oSheet.get_Range("D3", "D3");

            cl4.Value2 = "Giá";

            cl4.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl5 = oSheet.get_Range("E3", "E3");

            cl5.Value2 = "Mã Cửa Hàng";

            cl5.ColumnWidth = 20.0;


            Microsoft.Office.Interop.Excel.Range cl6 = oSheet.get_Range("F3", "F3");

            cl6.Value2 = "Mô Tả";

            cl6.ColumnWidth = 20.0;




            //tạo tiêu đề thông báo
            Microsoft.Office.Interop.Excel.Range cl8 = oSheet.get_Range("F3", "f3");





            Microsoft.Office.Interop.Excel.Range rowHead = oSheet.get_Range("A3", "F3");

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
            //string data1 = selectRow.Cells["MaSP"].Value.ToString();
            //string data2 = selectRow.Cells["TenSP"].Value.ToString();
            //string data3 = selectRow.Cells["NhaCungCap"].Value.ToString();
            //string data4 = selectRow.Cells["Gia"].Value.ToString();
            //string data5 = selectRow.Cells["MaCH"].Value.ToString();

            //string data7 = selectRow.Cells["MoTa"].Value.ToString();




            DataTable dt1 = new DataTable();
            DataColumn col1 = new DataColumn("MaSP");
            DataColumn col2 = new DataColumn("TenSP");
            DataColumn col3 = new DataColumn("NhaCungCap");
            DataColumn col4 = new DataColumn("Gia");
            DataColumn col5 = new DataColumn("MaCH");
            DataColumn col6 = new DataColumn("MoTa");
            


            dt1.Columns.Add(col1);
            dt1.Columns.Add(col2);
            dt1.Columns.Add(col3);
            dt1.Columns.Add(col4);
            dt1.Columns.Add(col5);
            dt1.Columns.Add(col6);
           



            foreach (DataGridViewRow data in dgvSanPham.Rows)
            {
                DataRow dtrow = dt1.NewRow();

                dtrow[0] = data.Cells[0].Value;
                dtrow[1] = data.Cells[1].Value;
                dtrow[2] = data.Cells[2].Value;
                dtrow[3] = data.Cells[3].Value;
                dtrow[4] = data.Cells[4].Value;
                dtrow[5] = data.Cells[5].Value;
              



                dt1.Rows.Add(dtrow);
            }

            XuatFile(dt1, "ThuMucSP", "DANH SÁCH SẢN PHẨM");
        }

        private void txtGia_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true; // Từ chối ký tự không phải số
            }
        }
    }
}
