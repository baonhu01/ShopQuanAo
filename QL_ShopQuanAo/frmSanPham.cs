using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //Sử dụng thư viện để làm việc SQL server
using QL_ShopQuanAo.Class; //Sử dụng class KetnoiCSDL.cs

namespace QL_ShopQuanAo
{
    public partial class frmSanPham : Form
    {
        SanPham kn = new SanPham();
        ChatLieu kn1 = new ChatLieu();

        public frmSanPham()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmSanPham_Load(object sender, EventArgs e)
        {
            //load combobox ten chất liệu
            cbbMaCL.DataSource = kn1.loadDLChatLieu();
            cbbMaCL.DisplayMember = "TENCHATLIEU"; //thuộc tính được hiển thị
            cbbMaCL.ValueMember = "MACHATLIEU"; //lấy mã

            //đỗ dữ liệu lên dataGV
            dgvSanPham.DataSource = kn.loadDLSanPham();

            txtMaSP.Enabled = false;
            txtTenSP.Enabled = false;
            cbbMaCL.Enabled = false;
            txtSL.Enabled = false;
            txtDonGiaNhap.Enabled = false;
            txtDonGiaBan.Enabled = false;
            txtGhiChu.Enabled = false;
            btnMo.Enabled = false;
            btnXoa.Enabled = false;
            btnSua.Enabled = false;
            btnLuu.Enabled = false;
            txtAnh.Enabled = false;

            //add vao combobox
            List<string> dsMaCL = kn.loadMaChatLieu();

            foreach (string item in dsMaCL)
            {
                cbbTimKiem.Items.Add(item);
            }
            cbbTimKiem.SelectedIndex = 0;
        }

        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dgvSanPham.Rows[e.RowIndex];
                txtMaSP.Text = dgvRow.Cells[0].Value.ToString();
                txtTenSP.Text = dgvRow.Cells[1].Value.ToString();
                cbbMaCL.SelectedValue = dgvRow.Cells[2].Value.ToString();
                txtSL.Text = dgvRow.Cells[3].Value.ToString();
                txtDonGiaNhap.Text = dgvRow.Cells[4].Value.ToString();
                txtDonGiaBan.Text = dgvRow.Cells[5].Value.ToString();
                txtAnh.Text = dgvRow.Cells[6].Value.ToString();
                picAnh.Image = Image.FromFile(txtAnh.Text);
                txtGhiChu.Text = dgvRow.Cells[7].Value.ToString();
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnMo_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpen = new OpenFileDialog();
            dlgOpen.Filter = "Bitmap(*.bmp)|*.bmp|JPEG(*.jpg)|*.jpg|GIF(*.gif)|*.gif|All files(*.*)|*.*";
            dlgOpen.FilterIndex = 2;
            dlgOpen.Title = "Chọn ảnh minh hoạ cho sản phẩm";
            if (dlgOpen.ShowDialog() == DialogResult.OK)
            {
                picAnh.Image = Image.FromFile(dlgOpen.FileName);
                txtAnh.Text = dlgOpen.FileName;
            }
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaSP.Enabled)
            {
                //kiểm tra rỗng
                if (txtMaSP.Text.Length == 0 || txtTenSP.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themSanPham(txtMaSP.Text, txtTenSP.Text, cbbMaCL.SelectedValue.ToString(), txtSL.Text, txtDonGiaNhap.Text, txtDonGiaBan.Text, txtAnh.Text, txtGhiChu.Text))
                {
                    MessageBox.Show("Thêm thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Thông báo");
                }
            }
            else
            {
                //kiểm tra rỗng
                if (txtTenSP.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaSanPham(txtMaSP.Text, txtTenSP.Text, cbbMaCL.SelectedValue.ToString(), txtSL.Text, txtDonGiaNhap.Text, txtDonGiaBan.Text, txtAnh.Text, txtGhiChu.Text))
                {
                    MessageBox.Show("Sửa thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Sửa thất bại", "Thông báo");
                }
            }
            //button lưu bị vô hiệu hóa
            btnLuu.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaSP.Enabled = true;
            txtTenSP.Enabled = true;
            cbbMaCL.Enabled = true;
            txtSL.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtGhiChu.Enabled = true;
            btnMo.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = true;
            txtAnh.Enabled = true;
            txtMaSP.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtMaSP.Enabled = false;
            txtTenSP.Enabled = true;
            cbbMaCL.Enabled = true;
            txtSL.Enabled = true;
            txtDonGiaNhap.Enabled = true;
            txtDonGiaBan.Enabled = true;
            txtAnh.Enabled = true;
            txtGhiChu.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Nếu đồng ý
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaNhanVien(txtMaSP.Text))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo");
                }
            }
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            //đỗ dữ liệu lên dataGV
            dgvSanPham.DataSource = kn.timKiem(cbbTimKiem.SelectedItem.ToString());
        }

        private void btnHienThi_Click(object sender, EventArgs e)
        {
            //đỗ dữ liệu lên dataGV
            dgvSanPham.DataSource = kn.loadDLSanPham();
        }
    }
}
