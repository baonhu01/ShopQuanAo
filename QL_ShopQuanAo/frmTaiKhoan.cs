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
using QL_ShopQuanAo.Class;//Sử dụng class KetnoiCSDL.cs
using System.Security.Cryptography; //MD5

namespace QL_ShopQuanAo
{
    public partial class frmTaiKhoan : Form
    {
        TaiKhoan kn = new TaiKhoan();

        public frmTaiKhoan()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmTaiKhoan_Load(object sender, EventArgs e)
        {
            txtMail.Enabled = false;
            txtMatKhau.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            //đỗ dữ liệu lên dataGV
            dgvTaiKhoan.DataSource = kn.loadDLTaiKhoan();
        }

        //  Lấy nội dung dòng dữ liệu người dùng chọn trong lưới DataGridView và hiển thị lên các điều khiển trên Form.
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = dgvTaiKhoan.Rows[e.RowIndex];
                txtMail.Text = dgvRow.Cells[0].Value.ToString();
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
                if (kn.ktrKhoaNgoaiBangNV(txtMail.Text) == false)
                {
                    MessageBox.Show("Mail này đang được sử dụng", "Thông báo");
                    return;
                }
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaTaiKhoan(txtMail.Text))
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
            txtMatKhau.Enabled = true;
            txtMail.Enabled = true;
            btnLuu.Enabled = true;
            txtMail.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtMatKhau.Enabled = true;
            txtMail.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtMatKhau.Text);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            if (txtMail.Enabled)
            {
                //kiểm tra rỗng
                if (txtMail.Text.Length == 0 || txtMatKhau.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themTaiKhoan(txtMail.Text, hasPass))
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
                if (txtMatKhau.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaTaiKhoan(txtMail.Text, hasPass))
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
