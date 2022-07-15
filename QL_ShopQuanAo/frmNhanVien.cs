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
    public partial class frmNhanVien : Form
    {
        NhanVien kn = new NhanVien();
        public frmNhanVien()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmNhanVien_Load(object sender, EventArgs e)
        {
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = false;
            txtChucVu.Enabled = false;
            txtDiaChi.Enabled = false;
            txtSDT.Enabled = false;
            dtNgaySinh.Enabled = false;
            txtMail.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            //đỗ dữ liệu lên dataGV
            dgvNhanVien.DataSource = kn.loadDLNhanVien();
        }

        //  Lấy nội dung dòng dữ liệu người dùng chọn trong lưới DataGridView và hiển thị lên các điều khiển trên Form.
        private void dgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dgvNhanVien.Rows[e.RowIndex];
                txtMaNV.Text = dgvRow.Cells[0].Value.ToString();
                txtTenNV.Text = dgvRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dgvRow.Cells[3].Value.ToString();
                txtSDT.Text = dgvRow.Cells[4].Value.ToString();
                txtChucVu.Text = dgvRow.Cells[5].Value.ToString();
                dtNgaySinh.Text = dgvRow.Cells[6].Value.ToString();
                txtMail.Text = dgvRow.Cells[7].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtMaNV.Enabled = true;
            txtTenNV.Enabled = true;
            txtChucVu.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtMaNV.Focus();
            btnLuu.Enabled = true;
            txtMail.Text = "";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string theDate = dtNgaySinh.Value.ToShortDateString();

            string gtinh = "Nữ";
            if (cbNam.Checked)
                gtinh = "Nam";

            if (txtMaNV.Enabled)
            {
                //kiểm tra rỗng
                if (txtMaNV.Text.Length == 0 || txtTenNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themNhanVien(txtMaNV.Text, txtTenNV.Text, gtinh, txtDiaChi.Text, txtSDT.Text, txtChucVu.Text, theDate))
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
                if (txtTenNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaNhanVien(txtMaNV.Text, txtTenNV.Text, gtinh, txtDiaChi.Text, txtSDT.Text, txtChucVu.Text, theDate, txtMail.Text))
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

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtMaNV.Enabled = false;
            txtTenNV.Enabled = true;
            txtChucVu.Enabled = true;
            txtDiaChi.Enabled = true;
            txtSDT.Enabled = true;
            dtNgaySinh.Enabled = true;
            txtMail.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Nếu đồng ý
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaNhanVien(txtMaNV.Text))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo");
                }
            }
        }

    }
}
