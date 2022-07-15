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
    public partial class frmChatLieu : Form
    {
        ChatLieu kn = new ChatLieu();
        public frmChatLieu()
        {
            InitializeComponent();
        }

        private void frmChatLieu_Load(object sender, EventArgs e)
        {
            txtMaCL.Enabled = false;
            txtTenCL.Enabled = false;
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = false;
            //đỗ dữ liệu lên dataGV
            gvDMChatLieu.DataSource = kn.loadDLChatLieu();
        }

        //  Lấy nội dung dòng dữ liệu người dùng chọn trong lưới DataGridView và hiển thị lên các điều khiển trên Form.
        private void gvDMChatLieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow dgvRow = gvDMChatLieu.Rows[e.RowIndex];
                txtMaCL.Text = dgvRow.Cells[0].Value.ToString();
                txtTenCL.Text = dgvRow.Cells[1].Value.ToString();
            }
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            //kiểm tra rỗng
            if (txtMaCL.Text.Length == 0)
            {
                MessageBox.Show("Bạn chưa nhập mã", "Thông báo");
                return;
            }
            if (kn.xoaChatLieu(txtMaCL.Text))
            {
                MessageBox.Show("Xóa thành công", "Thông báo");
            }
            else
            {
                MessageBox.Show("Xóa thất bại", "Thông báo");
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            txtTenCL.Enabled = true;
            txtMaCL.Enabled = true;
            btnLuu.Enabled = true;
            txtMaCL.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = true;
            txtTenCL.Enabled = true;
            txtMaCL.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (txtMaCL.Enabled)
            {
                //kiểm tra rỗng
                if (txtMaCL.Text.Length == 0 || txtTenCL.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //Lưu
                if (kn.themChatLieu(txtMaCL.Text, txtTenCL.Text))
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
                if (txtTenCL.Text.Length == 0)
                {
                    MessageBox.Show("Bạn đã điền thiếu thông tin!");
                }
                //sửa
                if (kn.suaChatLieu(txtMaCL.Text, txtTenCL.Text))
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

        private void btnXoa_Click_1(object sender, EventArgs e)
        {
            //Nếu đồng ý
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                //Kiểm tra mã chât liệu định xóa có tồn tại trong bảng lớp hay không. Nếu có thì thông báo “Dữ liệu đang sử dụng không thể xóa ?”.
                if (kn.ktrKhoaNgoaiBangSP(txtMaCL.Text) == false)
                {
                    MessageBox.Show("Mã chất liệu đang được sử dụng", "Thông báo");
                    return;
                }
                //Nếu mã chất liệu không tồn tại bên bảng lớp thì xóa bên bảng sản phẩm.
                if (kn.xoaChatLieu(txtMaCL.Text))
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
