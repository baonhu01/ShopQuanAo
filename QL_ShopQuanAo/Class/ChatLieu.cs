using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QL_ShopQuanAo.Class
{
    public class ChatLieu
    {
        //khai báo đối tượng kết nối
        SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");
        
        //B1 Tạo dataset
        DataSet ds_QLSP = new DataSet();
        SqlDataAdapter da_ChatLieu;

        public ChatLieu()
        {
            loadChatLieu();
        }

        //tương tác với bản chất liệu (ánh xạ bản khoa lên dataset)
        public void loadChatLieu()
        {
            //B1
            string cauLenh = "select * from ChatLieu";
            //B2 tạo đối tượng
            da_ChatLieu = new SqlDataAdapter(cauLenh, conn);
            //B3 ánh xạ dữ liệu lên dataset
            da_ChatLieu.Fill(ds_QLSP, "ChatLieu"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLSP.Tables["ChatLieu"].Columns[0];
            //set khóa chính
            ds_QLSP.Tables["ChatLieu"].PrimaryKey = key;
        }

        //load dữ liệu lên giao diện bảng chất liệu
        public DataTable loadDLChatLieu()
        {
            return ds_QLSP.Tables["ChatLieu"];
        }

        //thêm vào bảng chất liệu
        public bool themChatLieu(string ma, string ten)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["ChatLieu"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MACHATLIEU"] = ma;
                dr_row["TENCHATLIEU"] = ten;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLSP.Tables["ChatLieu"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_ChatLieu);
                //B5 updata vào csdl
                da_ChatLieu.Update(ds_QLSP, "ChatLieu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //sửa bảng chất liệu
        public bool suaChatLieu(string ma, string ten)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["ChatLieu"].Rows.Find(ma);
                //B2 
                dr_row["TenChatLieu"] = ten;
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_ChatLieu);
                //B5 updata vào csdl
                da_ChatLieu.Update(ds_QLSP, "ChatLieu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa bảng chất liệu
        public bool xoaChatLieu(string ma)
        {
            try
            {
                //B1 tìm ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLSP.Tables["ChatLieu"].Rows.Find(ma);
                //B2 
                dr_row.Delete();
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builChatLieu = new SqlCommandBuilder(da_ChatLieu);
                //B5 updata vào csdl
                da_ChatLieu.Update(ds_QLSP, "ChatLieu");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //kiểm tra khóa ngoại trước khi xóa trong bảng chất liệu
        public bool ktrKhoaNgoaiBangSP(string ma)
        {
            try
            {
                //B1 kiem tra ket noi
                if (conn.State == ConnectionState.Closed)
                    conn.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "select count(*) from SanPham where MACHATLIEU = '" + ma + "'";
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
