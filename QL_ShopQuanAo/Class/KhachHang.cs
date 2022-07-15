using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QL_ShopQuanAo.Class
{
    public class KhachHang
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLKH = new DataSet();
        SqlDataAdapter da_KhachHang;

        public KhachHang()
        {
            loadKhachHang();
        }

        //tương tác với bản khách hàng (ánh xạ bản khoa lên dataset)
        public void loadKhachHang()
        {
            //B1
            string cauLenh = "select * from KhachHang";
            //B2 tạo đối tượng
            da_KhachHang = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_KhachHang.Fill(ds_QLKH, "KhachHang"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLKH.Tables["KhachHang"].Columns[0];
            //set khóa chính
            ds_QLKH.Tables["KhachHang"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng khách hàng
        public DataTable loadDLKhachHang()
        {
            return ds_QLKH.Tables["KhachHang"];
        }

        //thêm vào bảng khách hàng
        public bool themKhachHang(string ma, string ten, string diaChi, string sđt)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLKH.Tables["KhachHang"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MAKH"] = ma;
                dr_row["TENKH"] = ten;
                dr_row["DIACHI"] = diaChi;
                dr_row["DT"] = sđt;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLKH.Tables["KhachHang"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_KhachHang);
                //B5 updata vào csdl
                da_KhachHang.Update(ds_QLKH, "KhachHang");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng khách hàng
        public bool suaKhachHang(string ma, string ten, string diaChi, string sđt)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLKH.Tables["KhachHang"].Rows.Find(ma);
                //B2 
                dr_row["TENKH"] = ten;
                dr_row["DIACHI"] = diaChi;
                dr_row["DT"] = sđt;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builKhachHang = new SqlCommandBuilder(da_KhachHang);
                //B5 updata vào csdl
                da_KhachHang.Update(ds_QLKH, "KhachHang");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng khách hàng
        public bool xoaKhachHang(string ma)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLKH.Tables["KhachHang"].Rows.Find(ma);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builKhachHang = new SqlCommandBuilder(da_KhachHang);
                //B5 updata vào csdl
                da_KhachHang.Update(ds_QLKH, "KhachHang");
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
