using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DoAn_2023
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();

            this.SizeChanged += frmMain_SizeChanged;

            UpSize();
        }

        private void frmMain_FormClosing(object sender, FormClosingEventArgs e)
        {
            DialogResult kq = MessageBox.Show("Bạn Có muốn thoát chương trình không?", "thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            //kiem tra kq chon:
            if (kq == DialogResult.Yes)
            {
                e.Cancel = false;
            }
        }



        
        private void khoHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmKhoHang.KHoHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmKhoHang.KHoHang.BringToFront();
            }
            else
            {
               //thực hiện show form

                frmKhoHang.KHoHang.MdiParent = this;
                frmKhoHang.KHoHang.Show();
            }
            
        }

        private void cửaHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmCuaHang.CuaHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmCuaHang.CuaHang.BringToFront();
            }
            else
            {


                frmCuaHang.CuaHang.MdiParent = this;
                frmCuaHang.CuaHang.Show();
            }

        }

        private void nhânViênToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frmKhoHang.KHoHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmKhoHang.KHoHang.BringToFront();
            }
            else
            {
                

                frmNhanVien.NhanVien.MdiParent = this;
                frmNhanVien.NhanVien.Show();
            }


            //frmNhanVien nhanvien = new frmNhanVien();
            //nhanvien.MdiParent = this;
            //nhanvien.Show();
        }

        private void sảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmSanPham.SanPham.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmSanPham.SanPham.BringToFront();
            }
            else
            {


                frmSanPham.SanPham.MdiParent = this;
                frmSanPham.SanPham.Show();
            }

        }

        private void kháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmKhachHang.KhachHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmKhachHang.KhachHang.BringToFront();
            }
            else
            {


                frmKhachHang.KhachHang.MdiParent = this;
                frmKhachHang.KhachHang.Show();
            }

        }

        private void đơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmMuaHang.MuaHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmMuaHang.MuaHang.BringToFront();
            }
            else
            {


                frmMuaHang.MuaHang.MdiParent = this;
                frmMuaHang.MuaHang.Show();
            }

        }
        /// <summary>
        /// thực hiện khóa form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void muaSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {

            if (frmDonHang.DonHang.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmDonHang.DonHang.BringToFront();
            }
            else
            {


                frmDonHang.DonHang.MdiParent = this;
                frmDonHang.DonHang.Show();
            }

        }

        private void nhậnXétToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmRptHoaDon nhanxet = new frmRptHoaDon();
            nhanxet.MdiParent = this;
            nhanxet.Show();
        }

       

       
        

        private void thoátToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Close();
        }

        

       

        private void hóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmHoaDon hoadon = new frmHoaDon();
            hoadon.MdiParent = this;
            hoadon.Show();
        }

        //sự kiện thực hiện thay đổi kích thước ảnh
        private void frmMain_SizeChanged(object sender, EventArgs e)
        {


            UpSize();
        }


        /// <summary>
        /// hàm thay đồi kích thước hình ảnh
        /// </summary>
        private void UpSize()
        {
            int newwith = this.Width;
            int newheigh = this.Height;

            pic1.Width = newwith / 10;
            pic1.Height = newheigh / 10;

            //pic2

            pic2.Width = newwith / 10;
            pic2.Height = newheigh /10;

            //pic3


            pic3.Width = newwith / 10;
            pic3.Height = newheigh / 10;

            //pic4
            pic4.Width = newwith / 10;
            pic4.Height = newheigh / 10;


            //pic5
            pic5.Width = newwith / 10;
            pic5.Height = newheigh / 10;

        }

        private void tìmKiếmTTĐặtHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimDatHang timDatHang = new frmTimDatHang();
            timDatHang.MdiParent = this;
            timDatHang.Show();
        }

        private void tìmKiếmTTĐơnHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemDonHang timDH = new frmTimKiemDonHang();
            timDH.MdiParent = this;
            timDH.Show();
        }

        private void tìmKiếmTTKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemKhachHang timKH = new frmTimKiemKhachHang();
            timKH.MdiParent = this;
            timKH.Show();
        }

        private void tìmKiếmTTKhoHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemKhoHang timKH = new frmTimKiemKhoHang();
            timKH.MdiParent = this;
            timKH.Show();
        }

        private void tìmKiếmTTSảnPhẩmToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmTimKiemSanPham timSP = new frmTimKiemSanPham();
            timSP.MdiParent = this;
            timSP.Show();
        }


        /// <summary>
        /// thực hiện in form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quảnLýHóaĐơnToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (frmHoaDon.HoaDon.Visible)
            {
                //nếu form đã mở thì không cho phép mở lại 

                frmHoaDon.HoaDon.BringToFront();
            }
            else
            {


                frmHoaDon.HoaDon.MdiParent = this;
                frmHoaDon.HoaDon.Show();
            }
        }
    }
}
