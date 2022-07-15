using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QL_ShopQuanAo.Class
{
    public class TaiKhoan
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLTK = new DataSet();
        SqlDataAdapter da_TaiKhoan;

        public TaiKhoan()
        {
            loadTaiKhoan();
        }

        //tương tác với bản tài khoảng (ánh xạ bản khoa lên dataset)
        public void loadTaiKhoan()
        {
            //B1
            string cauLenh = "select * from TaiKhoan";
            //B2 tạo đối tượng
            da_TaiKhoan = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_TaiKhoan.Fill(ds_QLTK, "TaiKhoan"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLTK.Tables["TaiKhoan"].Columns[0];
            //set khóa chính
            ds_QLTK.Tables["TaiKhoan"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng tài khoản
        public DataTable loadDLTaiKhoan()
        {
            return ds_QLTK.Tables["TaiKhoan"];
        }

        //thêm vào bảng tài khoản
        public bool themTaiKhoan(string mail, string matKhau)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLTK.Tables["TaiKhoan"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MAIL"] = mail;
                dr_row["MATKHAU"] = matKhau;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLTK.Tables["TaiKhoan"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_TaiKhoan);
                //B5 updata vào csdl
                da_TaiKhoan.Update(ds_QLTK, "TaiKhoan");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng tài khoản
        public bool suaTaiKhoan(string mail, string matKhau)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLTK.Tables["TaiKhoan"].Rows.Find(mail);
                //B2 
                dr_row["MATKHAU"] = matKhau;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChucVu = new SqlCommandBuilder(da_TaiKhoan);
                //B5 updata vào csdl
                da_TaiKhoan.Update(ds_QLTK, "TaiKhoan");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng tài khoản
        public bool xoaTaiKhoan(string mail)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLTK.Tables["TaiKhoan"].Rows.Find(mail);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builTaiKhoan = new SqlCommandBuilder(da_TaiKhoan);
                //B5 updata vào csdl
                da_TaiKhoan.Update(ds_QLTK, "TaiKhoan");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //kiểm tra khóa ngoại trước khi xóa trong bảng tài khoản
        public bool ktrKhoaNgoaiBangNV(string mail)
        {
            try
            {
                //B1 kiem tra ket noi
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "select count(*) from NhanVien where Mail = '" + mail + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                int kq = (int)cmd.ExecuteScalar();
                //B5 thực hiện xong đóng kết nối
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                //B6 thông báo
                if (kq >= 1)
                    return false;
                else
                    return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
