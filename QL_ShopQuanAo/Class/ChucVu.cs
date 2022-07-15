using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QL_ShopQuanAo.Class
{
    public class ChucVu
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLCV = new DataSet();
        SqlDataAdapter da_ChucVu;

        public ChucVu()
        {
            loadChucVu();
        }

        //tương tác với bản chức vụ (ánh xạ bản khoa lên dataset)
        public void loadChucVu()
        {
            //B1
            string cauLenh = "select * from ChucVu";
            //B2 tạo đối tượng
            da_ChucVu = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_ChucVu.Fill(ds_QLCV, "ChucVu"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLCV.Tables["ChucVu"].Columns[0];
            //set khóa chính
            ds_QLCV.Tables["ChucVu"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng chức vụ
        public DataTable loadDLChucVu()
        {
            return ds_QLCV.Tables["ChucVu"];
        }

        //thêm vào bảng chức vụ
        public bool themChucVu(string ma, string ten)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLCV.Tables["ChucVu"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MACHUCVU"] = ma;
                dr_row["TENCHUCVU"] = ten;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLCV.Tables["ChucVu"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_ChucVu);
                //B5 updata vào csdl
                da_ChucVu.Update(ds_QLCV, "ChucVu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng chức vụ
        public bool suaChucVu(string ma, string ten)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLCV.Tables["ChucVu"].Rows.Find(ma);
                //B2 
                dr_row["TENCHUCVU"] = ten;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChucVu = new SqlCommandBuilder(da_ChucVu);
                //B5 updata vào csdl
                da_ChucVu.Update(ds_QLCV, "ChucVu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng chức vụ
        public bool xoaChucVu(string ma)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLCV.Tables["ChucVu"].Rows.Find(ma);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChucVu = new SqlCommandBuilder(da_ChucVu);
                //B5 updata vào csdl
                da_ChucVu.Update(ds_QLCV, "ChucVu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //kiểm tra khóa ngoại trước khi xóa trong bảng chức vụ
        public bool ktrKhoaNgoaiBangNV(string ma)
        {
            try
            {
                //B1 kiem tra ket noi
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "select count(*) from NhanVien where MACHUCVU = '" + ma + "'";
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
