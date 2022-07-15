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
    public partial class frmKhachHang : Form
    {
        KhachHang kn = new KhachHang();
        public frmKhachHang()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmKhachHang_Load(object sender, EventArgs e)
        {
            txtMaKH.Enabled = false;
            txtTenKH.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            //đỗ dữ liệu lên dataGV
            dgvKhachHang.DataSource = kn.loadDLKhachHang();
        }

        //  Lấy nội dung dòng dữ liệu người dùng chọn trong lưới DataGridView và hiển thị lên các điều khiển trên Form.
        private void dgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dgvKhachHang.Rows[e.RowIndex];
                txtMaKH.Text = dgvRow.Cells[0].Value.ToString();
                txtTenKH.Text = dgvRow.Cells[1].Value.ToString();
                txtDiaChi.Text = dgvRow.Cells[2].Value.ToString();
                txtSDT.Text = dgvRow.Cells[3].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Nếu đồng ý
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaKhachHang(txtMaKH.Text))
                {
                    MessageBox.Show("Xóa thành công", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Xóa thất bại", "Thông báo");
                }
            }
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtTenKH.Enabled = true;
            txtMaKH.Enabled = true;
            btnLuu.Enabled = true;
            txtMaKH.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtTenKH.Enabled = true;
            txtMaKH.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaKH.Enabled)
            {
                //kiểm tra rỗng
                if (txtMaKH.Text.Length == 0 || txtTenKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themKhachHang (txtMaKH.Text, txtTenKH.Text, txtDiaChi.Text, txtSDT.Text))
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
                if (txtTenKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaKhachHang(txtMaKH.Text, txtTenKH.Text, txtDiaChi.Text, txtSDT.Text))
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


    }
}
