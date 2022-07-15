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
    public partial class frmChucVu : Form
    {
        ChucVu kn = new ChucVu();
        public frmChucVu()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmChucVu_Load(object sender, EventArgs e)
        {
            txtMaCV.Enabled = false;
            txtTenCV.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            //đỗ dữ liệu lên dataGV
            dgvChucVu.DataSource = kn.loadDLChucVu();
        }

        //  Lấy nội dung dòng dữ liệu người dùng chọn trong lưới DataGridView và hiển thị lên các điều khiển trên Form.
        private void dgvChucVu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dgvChucVu.Rows[e.RowIndex];
                txtMaCV.Text = dgvRow.Cells[0].Value.ToString();
                txtTenCV.Text = dgvRow.Cells[1].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //Nếu đồng ý
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Kiểm tra mã chât liệu định xóa có tồn tại trong bảng lớp hay không. Nếu có thì thông báo “Dữ liệu đang sử dụng không thể xóa ?”.
                if (kn.ktrKhoaNgoaiBangNV(txtMaCV.Text) == false)
                {
                    MessageBox.Show("Mã chức vụ đang được sử dụng", "Thông báo");
                    return;
                }
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaChucVu(txtMaCV.Text))
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
            txtTenCV.Enabled = true;
            txtMaCV.Enabled = true;
            btnLuu.Enabled = true;
            txtMaCV.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtTenCV.Enabled = true;
            txtMaCV.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaCV.Enabled)
            {
                //kiểm tra rỗng
                if (txtMaCV.Text.Length == 0 || txtTenCV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themChucVu(txtMaCV.Text, txtTenCV.Text))
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
                if (txtTenCV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaChucVu(txtMaCV.Text, txtTenCV.Text))
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
