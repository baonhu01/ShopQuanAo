using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace QL_ShopQuanAo.Class
{
    public class NhanVien
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLNV = new DataSet();
        SqlDataAdapter da_NhanVien;

        public NhanVien()
        {
            loadNhanVien();
        }

        //tương tác với bản nhân viên (ánh xạ bản khoa lên dataset)
        public void loadNhanVien()
        {
            //B1
            string cauLenh = "select * from NhanVien";
            //B2 tạo đối tượng
            da_NhanVien = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_NhanVien.Fill(ds_QLNV, "NhanVien"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLNV.Tables["NhanVien"].Columns[0];
            //set khóa chính
            ds_QLNV.Tables["NhanVien"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng nhân viên
        public DataTable loadDLNhanVien()
        {
            return ds_QLNV.Tables["NhanVien"];
        }

        //thêm vào bảng nhân viên
        public bool themNhanVien(string ma, string ten, string gioitinh, string diachi, string sdt, string macv, string ngaysinh)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLNV.Tables["NhanVien"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MANV"] = ma;
                dr_row["TENNV"] = ten;
                dr_row["GIOITINH"] = gioitinh;
                dr_row["DIACHI"] = diachi;
                dr_row["DT"] = sdt;
                dr_row["MACHUCVU"] = macv;
                dr_row["NGAYSINH"] = ngaysinh;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLNV.Tables["NhanVien"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_NhanVien);
                //B5 updata vào csdl
                da_NhanVien.Update(ds_QLNV, "NhanVien");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng nhân viên
        public bool suaNhanVien(string ma, string ten, string gioitinh, string diachi, string sdt, string macv, string ngaysinh, string mail)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLNV.Tables["NhanVien"].Rows.Find(ma);
                //B2 
                dr_row["TENNV"] = ten;
                dr_row["GIOITINH"] = gioitinh;
                dr_row["DIACHI"] = diachi;
                dr_row["DT"] = sdt;
                dr_row["MACHUCVU"] = macv;
                dr_row["NGAYSINH"] = ngaysinh;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builKhachHang = new SqlCommandBuilder(da_NhanVien);
                //B5 updata vào csdl
                da_NhanVien.Update(ds_QLNV, "NhanVien");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng nhân viên
        public bool xoaNhanVien(string ma)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLNV.Tables["NhanVien"].Rows.Find(ma);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builKhachHang = new SqlCommandBuilder(da_NhanVien);
                //B5 updata vào csdl
                da_NhanVien.Update(ds_QLNV, "NhanVien");
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
