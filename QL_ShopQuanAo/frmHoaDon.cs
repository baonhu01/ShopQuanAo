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
using COMExcel = Microsoft.Office.Interop.Excel;

namespace QL_ShopQuanAo
{
    public partial class frmHoaDon : Form
    {
        HoaDon kn = new HoaDon();
        DataTable tblCTHDB; //Bảng chi tiết hoá đơn bán
        public frmHoaDon()
        {
            InitializeComponent();
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmHoaDon_Load(object sender, EventArgs e)
        {
            btnThem.Enabled = true;
            btnLuu.Enabled = false;
            btnXoa.Enabled = false;
            btnInHD.Enabled = false;
            txtMaHD.ReadOnly = true;
            txtTenNV.ReadOnly = true;
            txtTenKH.ReadOnly = true;
            txtDiaChi.ReadOnly = true;
            txtSDT.ReadOnly = true;
            txtTenSP.ReadOnly = true;
            txtDonGia.ReadOnly = true;
            txtThanhTien.ReadOnly = true;
            txtTongTien.ReadOnly = true;
            txtGiamGia.Text = "0";
            txtTongTien.Text = "0";

            kn.FillCombo("SELECT MAKH, TENKH FROM KhachHang", cbbMaKH, "MAKH", "MAKH");
            cbbMaKH.SelectedIndex = -1;
            kn.FillCombo("SELECT MANV, TENNV FROM NhanVien", cbbMaNV, "MANV", "MANV");
            cbbMaNV.SelectedIndex = -1;
            kn.FillCombo("SELECT MASP, TENSP FROM SanPham", cbbMaSP, "MASP", "MASP");
            cbbMaSP.SelectedIndex = -1;
            //Hiển thị thông tin của một hóa đơn được gọi từ form tìm kiếm
            if (txtMaHD.Text != "")
            {
                LoadInfoHoaDon();
                btnXoa.Enabled = true;
                btnInHD.Enabled = true;
            }
            LoadDataGridView();
        }

        //nạp chi tiết hóa đơn
        private void LoadInfoHoaDon()
        {
            string str;
            str = "SELECT NGAYBAN FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'";
            dtpNgayBan.Value = DateTime.Parse(kn.GetFieldValues(str));
            str = "SELECT MANV FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'";
            cbbMaNV.Text = kn.GetFieldValues(str);
            str = "SELECT MAKH FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'";
            cbbMaKH.Text = kn.GetFieldValues(str);
            str = "SELECT TONGTIEN FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'";
            txtTongTien.Text = kn.GetFieldValues(str);
            lbBangChu.Text = "Bằng chữ: " + kn.ChuyenSoSangChuoi(Double.Parse(txtTongTien.Text));
        }

        private void LoadDataGridView()
        {
            string sql;
            sql = "SELECT a.MASP, b.TENSP, a.SoLuong, b.DGBAN, a.GiamGia, a.ThanhTien FROM CT_HOADON AS a, SANPHAM AS b WHERE a.MAHD = '" + txtMaHD.Text + "' AND a.MASP=b.MASP";
            tblCTHDB = kn.GetDataToTable(sql);
            dgvHoaDon.DataSource = tblCTHDB;
            dgvHoaDon.Columns[0].HeaderText = "Mã hàng";
            dgvHoaDon.Columns[1].HeaderText = "Tên hàng";
            dgvHoaDon.Columns[2].HeaderText = "Số lượng";
            dgvHoaDon.Columns[3].HeaderText = "Đơn giá";
            dgvHoaDon.Columns[4].HeaderText = "Giảm giá %";
            dgvHoaDon.Columns[5].HeaderText = "Thành tiền";
            dgvHoaDon.AllowUserToAddRows = false;
            dgvHoaDon.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            txtSL.Enabled = true;
            txtGiamGia.Enabled = true;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnInHD.Enabled = false;
            btnThem.Enabled = false;
            ResetValues();
            txtMaHD.Text = kn.CreateKey("HD");
            LoadDataGridView();
        }
        private void ResetValues()
        {
            txtMaHD.Text = "";
            dtpNgayBan.Value = DateTime.Now;
            cbbMaNV.Text = "";
            cbbMaKH.Text = "";
            txtTongTien.Text = "0";
            lbBangChu.Text = "Bằng chữ: ";
            cbbMaSP.Text = "";
            txtSL.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string theDate = dtpNgayBan.Value.ToShortDateString();
            string sql;
            double sl, SLcon, tong, Tongmoi;

            // Kiểm tra xem số lượng hàng trong kho còn đủ để cung cấp không?
            sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM SanPham WHERE MASP = '" + cbbMaSP.SelectedValue + "'"));
            if (Convert.ToDouble(txtSL.Text) > sl)
            {
                MessageBox.Show("Số lượng mặt hàng này chỉ còn " + sl, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSL.Text = "";
                txtSL.Focus();
                return;
            }
            // Cập nhật lại số lượng của mặt hàng vào bảng san pham
            SLcon = sl - Convert.ToDouble(txtSL.Text);
            kn.capNhatSLSanPham(cbbMaSP.SelectedValue.ToString(), SLcon.ToString());

            sql = "SELECT MAHD FROM HoaDon WHERE MAHD='" + txtMaHD.Text + "'";
            if (!kn.CheckKey(sql))
            {
                // Mã hóa đơn chưa có, tiến hành lưu các thông tin chung
                // Mã HDBan được sinh tự động do đó không có trường hợp trùng khóa
                if (cbbMaNV.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập nhân viên", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbMaNV.Focus();
                    return;
                }
                if (cbbMaKH.Text.Length == 0)
                {
                    MessageBox.Show("Bạn phải nhập khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    cbbMaKH.Focus();
                    return;
                }
                if (kn.themHoaDon(txtMaHD.Text, cbbMaNV.SelectedValue.ToString(), theDate, cbbMaKH.SelectedValue.ToString(), txtTongTien.Text))
                {
                    MessageBox.Show("Thêm thành công vào hóa đơn", "Thông báo");
                }
                else
                {
                    MessageBox.Show("Thêm thất bại", "Thông báo");
                }
            }

             //Lưu thông tin của các mặt hàng
            if (cbbMaSP.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMaSP.Focus();
                return;
            }
            if ((txtSL.Text.Trim().Length == 0) || (txtSL.Text == "0"))
            {
                MessageBox.Show("Bạn phải nhập số lượng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSL.Text = "";
                txtSL.Focus();
                return;
            }
            if (txtGiamGia.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập giảm giá", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtGiamGia.Focus();
                return;
            }
            sql = "SELECT MASP FROM CT_HOADON WHERE MASP='" + cbbMaSP.SelectedValue + "' AND MAHD = '" + txtMaHD.Text.Trim() + "'";
            if (kn.CheckKey(sql))
            {
                MessageBox.Show("Mã hàng này đã có, bạn phải nhập mã khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ResetValuesHang();
                cbbMaSP.Focus();
                return;
            }
            if (kn.themHoaDon_CTHD(txtMaHD.Text, cbbMaSP.SelectedValue.ToString(), txtSL.Text, txtDonGia.Text, txtGiamGia.Text, txtThanhTien.Text))
            {
                MessageBox.Show("Thêm thành công vào chi tiết hóa đơn", "Thông báo");
            }
            else
            {
                MessageBox.Show("Thêm thất bại", "Thông báo");
            }

            LoadDataGridView();

            // Cập nhật lại tổng tiền cho hóa đơn bán
            tong = Convert.ToDouble(kn.GetFieldValues("SELECT TONGTIEN FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'"));
            Tongmoi = tong + Convert.ToDouble(txtThanhTien.Text);
            kn.capNhatTongTienHD(txtMaHD.Text, Tongmoi.ToString());
            txtTongTien.Text = Tongmoi.ToString();
            lbBangChu.Text = "Bằng chữ: " + kn.ChuyenSoSangChuoi(Double.Parse(Tongmoi.ToString()));
            ResetValuesHang();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnInHD.Enabled = true;
        }

        private void ResetValuesHang()
        {
            cbbMaSP.Text = "";
            txtSL.Text = "";
            txtGiamGia.Text = "0";
            txtThanhTien.Text = "0";
        }

        private void cbbMaSP_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbbMaSP.Text == "")
            {
                txtTenSP.Text = "";
                txtDonGia.Text = "";
            }
            // Khi chọn mã hàng thì các thông tin về hàng hiện ra
            str = "SELECT TENSP FROM SanPham WHERE MASP ='" + cbbMaSP.SelectedValue + "'";
            txtTenSP.Text = kn.GetFieldValues(str);
            str = "SELECT DGBAN FROM SanPham WHERE MASP ='" + cbbMaSP.SelectedValue + "'";
            txtDonGia.Text = kn.GetFieldValues(str);
        }

        private void cbbMaKH_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbbMaKH.Text == "")
            {
                txtTenKH.Text = "";
                txtDiaChi.Text = "";
                txtSDT.Text = "";
            }
            //Khi chọn Mã khách hàng thì các thông tin của khách hàng sẽ hiện ra
            str = "Select TENKH from KhachHang where MAKH = '" + cbbMaKH.SelectedValue + "'";
            txtTenKH.Text = kn.GetFieldValues(str);
            str = "Select DIACHI from KhachHang where MAKH = '" + cbbMaKH.SelectedValue + "'";
            txtDiaChi.Text = kn.GetFieldValues(str);
            str = "Select DT from KhachHang where MAKH= '" + cbbMaKH.SelectedValue + "'";
            txtSDT.Text = kn.GetFieldValues(str);
        }

        private void cbbMaNV_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str;
            if (cbbMaNV.Text == "")
                txtTenNV.Text = "";
            // Khi chọn Mã nhân viên thì tên nhân viên tự động hiện ra
            str = "Select TENNV from NhanVien where MANV ='" + cbbMaNV.SelectedValue + "'";
            txtTenNV.Text = kn.GetFieldValues(str);
        }

        private void txtSL_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi số lượng thì thực hiện tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSL.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSL.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void txtGiamGia_TextChanged(object sender, EventArgs e)
        {
            //Khi thay đổi giảm giá thì tính lại thành tiền
            double tt, sl, dg, gg;
            if (txtSL.Text == "")
                sl = 0;
            else
                sl = Convert.ToDouble(txtSL.Text);
            if (txtGiamGia.Text == "")
                gg = 0;
            else
                gg = Convert.ToDouble(txtGiamGia.Text);
            if (txtDonGia.Text == "")
                dg = 0;
            else
                dg = Convert.ToDouble(txtDonGia.Text);
            tt = sl * dg - sl * dg * gg / 100;
            txtThanhTien.Text = tt.ToString();
        }

        private void cbbMaHD_DropDown(object sender, EventArgs e)
        {
            kn.FillCombo("SELECT MAHD FROM HoaDon", cbbMaHD, "MAHD", "MAHD");
            cbbMaHD.SelectedIndex = -1;
        }

        private void btnTimkiem_Click(object sender, EventArgs e)
        {
            if (cbbMaHD.Text == "")
            {
                MessageBox.Show("Bạn phải chọn một mã hóa đơn để tìm", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cbbMaHD.Focus();
                return;
            }
            txtMaHD.Text = cbbMaHD.Text;
            LoadInfoHoaDon();
            LoadDataGridView();
            btnXoa.Enabled = true;
            btnLuu.Enabled = true;
            btnInHD.Enabled = true;
            cbbMaHD.SelectedIndex = -1;
        }

        private void btnInHD_Click(object sender, EventArgs e)
        {
            // Khởi động chương trình Excel
            COMExcel.Application exApp = new COMExcel.Application();
            COMExcel.Workbook exBook; //Trong 1 chương trình Excel có nhiều Workbook
            COMExcel.Worksheet exSheet; //Trong 1 Workbook có nhiều Worksheet
            COMExcel.Range exRange;
            string sql;
            int hang = 0, cot = 0;
            DataTable tblThongtinHD, tblThongtinHang;
            exBook = exApp.Workbooks.Add(COMExcel.XlWBATemplate.xlWBATWorksheet);
            exSheet = exBook.Worksheets[1];
            // Định dạng chung
            exRange = exSheet.Cells[1, 1];
            exRange.Range["A1:Z300"].Font.Name = "Times new roman"; //Font chữ
            exRange.Range["A1:B3"].Font.Size = 10;
            exRange.Range["A1:B3"].Font.Bold = true;
            exRange.Range["A1:B3"].Font.ColorIndex = 5; //Màu xanh da trời
            exRange.Range["A1:A1"].ColumnWidth = 7;
            exRange.Range["B1:B1"].ColumnWidth = 15;
            exRange.Range["A1:B1"].MergeCells = true;
            exRange.Range["A1:B1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A1:B1"].Value = "Shop B-NY";
            exRange.Range["A2:B2"].MergeCells = true;
            exRange.Range["A2:B2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:B2"].Value = "Tân Thành Bình - Bến Tre";
            exRange.Range["A3:B3"].MergeCells = true;
            exRange.Range["A3:B3"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A3:B3"].Value = "Điện thoại: (098)123456789";
            exRange.Range["C2:E2"].Font.Size = 16;
            exRange.Range["C2:E2"].Font.Bold = true;
            exRange.Range["C2:E2"].Font.ColorIndex = 3; //Màu đỏ
            exRange.Range["C2:E2"].MergeCells = true;
            exRange.Range["C2:E2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C2:E2"].Value = "HÓA ĐƠN BÁN";
            // Biểu diễn thông tin chung của hóa đơn bán
            sql = "SELECT a.MAHD, a.NgayBan, a.TongTien, b.TENKH, b.DiaChi, b.DT, c.TENNV FROM HOADON AS a, KHACHHANG AS b, NHANVIEN AS c WHERE a.MAHD = N'" + txtMaHD.Text + "' AND a.MAKH = b.MAKH AND a.MANV = c.MANV";
            tblThongtinHD = kn.GetDataToTable(sql);
            exRange.Range["B6:C9"].Font.Size = 12;
            exRange.Range["B6:B6"].Value = "Mã hóa đơn:";
            exRange.Range["C6:E6"].MergeCells = true;
            exRange.Range["C6:E6"].Value = tblThongtinHD.Rows[0][0].ToString();
            exRange.Range["B7:B7"].Value = "Khách hàng:";
            exRange.Range["C7:E7"].MergeCells = true;
            exRange.Range["C7:E7"].Value = tblThongtinHD.Rows[0][3].ToString();
            exRange.Range["B8:B8"].Value = "Địa chỉ:";
            exRange.Range["C8:E8"].MergeCells = true;
            exRange.Range["C8:E8"].Value = tblThongtinHD.Rows[0][4].ToString();
            exRange.Range["B9:B9"].Value = "Điện thoại:";
            exRange.Range["C9:E9"].MergeCells = true;
            exRange.Range["C9:E9"].Value = tblThongtinHD.Rows[0][5].ToString();
            //Lấy thông tin các mặt hàng
            sql = "SELECT b.TENSP, a.SoLuong, b.DGBAN, a.GiamGia, a.ThanhTien " + "FROM CT_HOADON AS a , SANPHAM AS b WHERE a.MAHD = N'" +
                  txtMaHD.Text + "' AND a.MASP = b.MASP";
            tblThongtinHang = kn.GetDataToTable(sql);
            //Tạo dòng tiêu đề bảng
            exRange.Range["A11:F11"].Font.Bold = true;
            exRange.Range["A11:F11"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["C11:F11"].ColumnWidth = 12;
            exRange.Range["A11:A11"].Value = "STT";
            exRange.Range["B11:B11"].Value = "Tên hàng";
            exRange.Range["C11:C11"].Value = "Số lượng";
            exRange.Range["D11:D11"].Value = "Đơn giá";
            exRange.Range["E11:E11"].Value = "Giảm giá";
            exRange.Range["F11:F11"].Value = "Thành tiền";
            for (hang = 0; hang < tblThongtinHang.Rows.Count; hang++)
            {
                //Điền số thứ tự vào cột 1 từ dòng 12
                exSheet.Cells[1][hang + 12] = hang + 1;
                for (cot = 0; cot < tblThongtinHang.Columns.Count; cot++)
                //Điền thông tin hàng từ cột thứ 2, dòng 12
                {
                    exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString();
                    if (cot == 3) exSheet.Cells[cot + 2][hang + 12] = tblThongtinHang.Rows[hang][cot].ToString() + "%";
                }
            }
            exRange = exSheet.Cells[cot][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = "Tổng tiền:";
            exRange = exSheet.Cells[cot + 1][hang + 14];
            exRange.Font.Bold = true;
            exRange.Value2 = tblThongtinHD.Rows[0][2].ToString();
            exRange = exSheet.Cells[1][hang + 15]; //Ô A1 
            exRange.Range["A1:F1"].MergeCells = true;
            exRange.Range["A1:F1"].Font.Bold = true;
            exRange.Range["A1:F1"].Font.Italic = true;
            exRange.Range["A1:F1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignRight;
            exRange.Range["A1:F1"].Value = "Bằng chữ: " + kn.ChuyenSoSangChuoi(Double.Parse(tblThongtinHD.Rows[0][2].ToString()));
            exRange = exSheet.Cells[4][hang + 17]; //Ô A1 
            exRange.Range["A1:C1"].MergeCells = true;
            exRange.Range["A1:C1"].Font.Italic = true;
            exRange.Range["A1:C1"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            DateTime d = Convert.ToDateTime(tblThongtinHD.Rows[0][1]);
            exRange.Range["A1:C1"].Value = "Hà Nội, ngày " + d.Day + " tháng " + d.Month + " năm " + d.Year;
            exRange.Range["A2:C2"].MergeCells = true;
            exRange.Range["A2:C2"].Font.Italic = true;
            exRange.Range["A2:C2"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A2:C2"].Value = "Nhân viên bán hàng";
            exRange.Range["A6:C6"].MergeCells = true;
            exRange.Range["A6:C6"].Font.Italic = true;
            exRange.Range["A6:C6"].HorizontalAlignment = COMExcel.XlHAlign.xlHAlignCenter;
            exRange.Range["A6:C6"].Value = tblThongtinHD.Rows[0][6];
            exSheet.Name = "Hóa đơn nhập";
            exApp.Visible = true;
        }

        private void txtSL_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (((e.KeyChar >= '0') && (e.KeyChar <= '9')) || (Convert.ToInt32(e.KeyChar) == 8))
                e.Handled = false;
            else e.Handled = true;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            double sl, slcon, slxoa;
            if (MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                string sql = "SELECT MASP, SOLUONG FROM CT_HOADON WHERE MAHD = '" + txtMaHD.Text + "'";
                DataTable tblHang = kn.GetDataToTable(sql);
                for (int hang = 0; hang <= tblHang.Rows.Count - 1; hang++)
                {
                    // Cập nhật lại số lượng cho các mặt hàng
                    sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM SanPham WHERE MASP = '" + tblHang.Rows[hang][0].ToString() + "'"));
                    slxoa = Convert.ToDouble(tblHang.Rows[hang][1].ToString());
                    slcon = sl + slxoa;
                    kn.capNhatSLSanPham(tblHang.Rows[hang][0].ToString(), slcon.ToString());
                }

                //Xóa chi tiết hóa đơn
                kn.xoaChiTietHD(txtMaHD.Text);

                //Xóa hóa đơn
                kn.xoaHoaDon(txtMaHD.Text);
                ResetValues();
                LoadDataGridView();
                btnXoa.Enabled = false;
                btnInHD.Enabled = false;
            }
        }

        private void dgvHoaDon_DoubleClick(object sender, EventArgs e)
        {
            string MaHangxoa, sql;
            Double ThanhTienxoa, SoLuongxoa, sl, slcon, tong, tongmoi;
            if (tblCTHDB.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if ((MessageBox.Show("Bạn có chắc chắn muốn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes))
            {
                //Xóa hàng và cập nhật lại số lượng hàng 
                MaHangxoa = dgvHoaDon.CurrentRow.Cells["MASP"].Value.ToString();
                SoLuongxoa = Convert.ToDouble(dgvHoaDon.CurrentRow.Cells["SOLUONG"].Value.ToString());
                ThanhTienxoa = Convert.ToDouble(dgvHoaDon.CurrentRow.Cells["THANHTIEN"].Value.ToString());
                kn.xoaChiTietHDClick(txtMaHD.Text, MaHangxoa);
                // Cập nhật lại số lượng cho các mặt hàng
                sl = Convert.ToDouble(kn.GetFieldValues("SELECT SOLUONG FROM SanPham WHERE MASP = '" + MaHangxoa + "'"));
                slcon = sl + SoLuongxoa;
                kn.capNhatSLSanPham(MaHangxoa, slcon.ToString());
                // Cập nhật lại tổng tiền cho hóa đơn bán
                tong = Convert.ToDouble(kn.GetFieldValues("SELECT TONGTIEN FROM HoaDon WHERE MAHD = '" + txtMaHD.Text + "'"));
                tongmoi = tong - ThanhTienxoa;
                kn.capNhatTongTienHD(txtMaHD.Text, tongmoi.ToString());
                txtTongTien.Text = tongmoi.ToString();
                lbBangChu.Text = "Bằng chữ: " + kn.ChuyenSoSangChuoi(Double.Parse(tongmoi.ToString()));
                LoadDataGridView();
            }
        }

    }
}
