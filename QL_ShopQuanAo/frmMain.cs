using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QL_ShopQuanAo
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            //Class.KetNoiCSDL.Connect(); //Mở kết nối
        }

        private void frmChatLieu_Click(object sender, EventArgs e)
        {
            frmChatLieu frm = new frmChatLieu(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmSanPham_Click(object sender, EventArgs e)
        {
            frmSanPham frm = new frmSanPham(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmKhachHang_Click(object sender, EventArgs e)
        {
            frmKhachHang frm = new frmKhachHang(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmNhanVien_Click(object sender, EventArgs e)
        {
            frmNhanVien frm = new frmNhanVien(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmHoaDon_Click(object sender, EventArgs e)
        {
            frmHoaDon frm = new frmHoaDon(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmChucVu_Click(object sender, EventArgs e)
        {
            frmChucVu frm= new frmChucVu(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void frmQuanLiTK_Click(object sender, EventArgs e)
        {
            frmTaiKhoan frm = new frmTaiKhoan(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

        private void đăngXuấtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            frmlogin frm = new frmlogin(); //Khởi tạo đối tượng
            frm.ShowDialog();
        }

    }
}
