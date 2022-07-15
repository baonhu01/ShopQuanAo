using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QL_ShopQuanAo.Class
{
    public class SanPham
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLSP = new DataSet();
        SqlDataAdapter da_SanPham;

        public SanPham()
        {
            loadSanPham();
        }

        //tương tác với bản sản phẩm (ánh xạ bản khoa lên dataset)
        public void loadSanPham()
        {
            //B1
            string cauLenh = "select * from SanPham";
            //B2 tạo đối tượng
            da_SanPham = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_SanPham.Fill(ds_QLSP, "SanPham"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLSP.Tables["SanPham"].Columns[0];
            //set khóa chính
            ds_QLSP.Tables["SanPham"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng sản phẩm
        public DataTable loadDLSanPham()
        {
            return ds_QLSP.Tables["SanPham"];
        }

        //thêm vào bảng san pham
        public bool themSanPham(string maSP, string tenSP, string maCL, string sl, string dongianhap, string dongiaban, string anhSP, string ghichu)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["SanPham"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MASP"] = maSP;
                dr_row["TENSP"] = tenSP;
                dr_row["MACHATLIEU"] = maCL;
                dr_row["SOLUONG"] = sl;
                dr_row["DGNHAP"] = dongianhap;
                dr_row["DGBAN"] = dongiaban;
                dr_row["ANHSP"] = anhSP;
                dr_row["GHICHU"] = ghichu;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLSP.Tables["SanPham"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builSanPham = new SqlCommandBuilder(da_SanPham);
                //B5 updata vào csdl
                da_SanPham.Update(ds_QLSP, "SanPham");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng san pham
        public bool suaSanPham(string maSP, string tenSP, string maCL, string sl, string dongianhap, string dongiaban, string anhSP, string ghichu)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["SanPham"].Rows.Find(maSP);
                //B2 
                dr_row["TENSP"] = tenSP;
                dr_row["MACHATLIEU"] = maCL;
                dr_row["SOLUONG"] = sl;
                dr_row["DGNHAP"] = dongianhap;
                dr_row["DGBAN"] = dongiaban;
                dr_row["ANHSP"] = anhSP;
                dr_row["GHICHU"] = ghichu;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builSanPham = new SqlCommandBuilder(da_SanPham);
                //B5 updata vào csdl
                da_SanPham.Update(ds_QLSP, "SanPham");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng san pham
        public bool xoaNhanVien(string ma)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["SanPham"].Rows.Find(ma);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builSanPham = new SqlCommandBuilder(da_SanPham);
                //B5 updata vào csdl
                da_SanPham.Update(ds_QLSP, "SanPham");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //tim kiem theo ten
        public DataTable timKiem(string maCL)
        {
            string CauLenh = "select * from SanPham where MACHATLIEU like '%" + maCL + "%'";
            SqlCommand cmd = new SqlCommand(CauLenh, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable tb = new DataTable();
            da.Fill(tb);
            return tb;
        }

        //load mã chất liệu
        public List<string> loadMaChatLieu()
        {
            List<string> dsMa = new List<string>();
            try
            {
                //B1 kiem tra ket noi
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "select MaChatLieu from ChatLieu";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, conn);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                SqlDataReader rd = cmd.ExecuteReader();
                //B4.1 duyệt dr đọc du liêu ra
                while (rd.Read())
                {
                    string ketqua = rd["MaChatLieu"].ToString();
                    dsMa.Add(ketqua);
                }
                rd.Close();
                //B5 thực hiện xong đóng kết nối
                if (conn.State == ConnectionState.Open)
                    conn.Close();
                //B6 thông báo
                return dsMa;
            }
            catch
            {
                return null;
            }
        }

    }
}
