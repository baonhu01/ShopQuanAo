using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QL_ShopQuanAo.Class
{
    public class HoaDon
    {
        //khai báo đối tượng kết nối
        SqlConnection Con = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");

        //B1 Tạo dataset
        DataSet ds_QLHD = new DataSet();
        SqlDataAdapter da_HoaDon;

        public HoaDon()
        {
            loadHoaDon();
        }

        //tương tác với bản hoa don (ánh xạ bản hoa don lên dataset)
        public void loadHoaDon()
        {
            //B1
            string cauLenh = "select * from HoaDon";
            //B2 tạo đối tượng
            da_HoaDon = new SqlDataAdapter(cauLenh, Con);
            //B3 ánh xạ dữ liệu lên dataset
            da_HoaDon.Fill(ds_QLHD, "HoaDon"); // đặt tên giống trong csdl
            ////B4 set khóa chính để phục vụ cho việc xóa dữ liệu
            DataColumn[] key = new DataColumn[1];
            key[0] = ds_QLHD.Tables["HoaDon"].Columns[0];
            //set khóa chính
            ds_QLHD.Tables["HoaDon"].PrimaryKey = key;
        }

        //thêm vào bảng hoa don
        public bool themHoaDon(string mahd, string manv, string ngayban, string makh, string tongtien)
        {
            try
            {
                //B1 tạo ra 1 dòng dữ liệu
                DataRow dr_row = ds_QLHD.Tables["HoaDon"].NewRow();
                //B2 gán dữ liệu cho row
                dr_row["MAHD"] = mahd;
                dr_row["MANV"] = manv;
                dr_row["NGAYBAN"] = ngayban;
                dr_row["MAKH"] = makh;
                dr_row["TONGTIEN"] = tongtien;
                //B3 thêm vào bảng khoa tại dataset
                ds_QLHD.Tables["HoaDon"].Rows.Add(dr_row);
                //B4 chuan bị luu vao co so du lieu
                SqlCommandBuilder builHoaDon = new SqlCommandBuilder(da_HoaDon);
                //B5 updata vào csdl
                da_HoaDon.Update(ds_QLHD, "HoaDon");
                return true;
            }
            catch
            {
                return false;
            }
        }

        //thêm vào bảng hoa don chi tiet
        public bool themHoaDon_CTHD(string mahd, string masp, string soluong, string dongia, string giamgia, string thanhtien)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "insert into CT_HOADON values('" + mahd + "', '" + masp + "', '" + soluong + "', '" + dongia + "', '" + giamgia + "', '" + thanhtien + "')";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }
        
        //xóa hoá đơn
        public bool xoaHoaDon(string mahd)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "delete HoaDon where MAHD='" + mahd + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }

        //xóa hoá đơn chi tiết
        public bool xoaChiTietHD(string mahd)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "delete CT_HOADON where MAHD='" + mahd + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }

        //click vào xóa chi tiết hóa đơn
        public bool xoaChiTietHDClick(string mahd, string masp)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "DELETE CT_HOADON WHERE MAHD='" + mahd + "' AND MASP = '" + masp + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }

        //cap nhat lai so luong cua bang san pham
        public bool capNhatSLSanPham(string maSP, string sl)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "update SanPham set SOLUONG='" + sl + "' where MASP='" + maSP + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }

        //cập nhật lại tổng tiền cho hóa đơn bán
        public bool capNhatTongTienHD(string maHD, string tongtien)
        {
            try
            {
                //B1 kiểm tra kết nối
                if (Con.State == ConnectionState.Closed)
                    Con.Open();
                //B2 chuẩn bị câu lệnh tương tác dữ liệu
                string cauLenh = "UPDATE HoaDon SET TONGTIEN =" + tongtien + " WHERE MAHD = '" + maHD + "'";
                //B3 chuẩn bị đối tượng tương tác dữ liệu
                SqlCommand cmd = new SqlCommand(cauLenh, Con);
                //B4 ra lệnh cho đối tượng cmd thực hiện câu lệnh
                cmd.ExecuteNonQuery();
                //B5 thực hiện xong đóng kết nối
                if (Con.State == ConnectionState.Open)
                    Con.Close();
                //B6 thông báo
                return true;
            }
            catch
            {
                return false;
            }
        }

        //Lấy dữ liệu vào bảng
        public DataTable GetDataToTable(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con); //Định nghĩa đối tượng thuộc lớp SqlDataAdapter
            //Khai báo đối tượng table thuộc lớp DataTable
            DataTable table = new DataTable();
            dap.Fill(table); //Đổ kết quả từ câu lệnh sql vào table
            return table;
        }

        //lấy dữ liệu từ một câu lệnh SQL đổ vào một ComboBox.
        public void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma; //Trường giá trị
            cbo.DisplayMember = ten; //Trường hiển thị
        }

        //lấy dữ liệu từ một câu lệnh SQL.
        public string GetFieldValues(string sql)
        {
            if (Con.State != ConnectionState.Open)
                Con.Open();
            string ma = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader;
            reader = cmd.ExecuteReader();
            while (reader.Read())
                ma = reader.GetValue(0).ToString();
            reader.Close();
            return ma;
        }

        //tao ma hoa don 
        public string CreateKey(string tiento)
        {
            string key = tiento;
            string[] partsDay;
            partsDay = DateTime.Now.ToShortDateString().Split('/');
            //Ví dụ 07/08/2009
            string d = String.Format("{0}{1}{2}", partsDay[0], partsDay[1], partsDay[2]);
            key = key + d;
            string[] partsTime;
            partsTime = DateTime.Now.ToLongTimeString().Split(':');
            //Ví dụ 7:08:03 PM hoặc 7:08:03 AM
            if (partsTime[2].Substring(3, 2) == "PM")
                partsTime[0] = ConvertTimeTo24(partsTime[0]);
            if (partsTime[2].Substring(3, 2) == "AM")
                if (partsTime[0].Length == 1)
                    partsTime[0] = "0" + partsTime[0];
            //Xóa ký tự trắng và PM hoặc AM
            partsTime[2] = partsTime[2].Remove(2, 3);
            string t;
            t = String.Format("_{0}{1}{2}", partsTime[0], partsTime[1], partsTime[2]);
            key = key + t;
            return key;
        }

        //Chuyển đổi từ PM sang dạng 24h
        public string ConvertTimeTo24(string hour)
        {
            string h = "";
            switch (hour)
            {
                case "1":
                    h = "13";
                    break;
                case "2":
                    h = "14";
                    break;
                case "3":
                    h = "15";
                    break;
                case "4":
                    h = "16";
                    break;
                case "5":
                    h = "17";
                    break;
                case "6":
                    h = "18";
                    break;
                case "7":
                    h = "19";
                    break;
                case "8":
                    h = "20";
                    break;
                case "9":
                    h = "21";
                    break;
                case "10":
                    h = "22";
                    break;
                case "11":
                    h = "23";
                    break;
                case "12":
                    h = "0";
                    break;
            }
            return h;
        }

        //Hàm kiểm tra khoá trùng
        public bool CheckKey(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            if (table.Rows.Count > 0)
                return true;
            else return false;
        }

        string[] mNumText = "không;một;hai;ba;bốn;năm;sáu;bảy;tám;chín".Split(';');
        //Viết hàm chuyển số hàng chục, giá trị truyền vào là số cần chuyển và một biến đọc phần lẻ hay không ví dụ 101 => một trăm lẻ một
        private string DocHangChuc(double so, bool daydu)
        {
            string chuoi = "";
            //Hàm để lấy số hàng chục ví dụ 21/10 = 2
            Int64 chuc = Convert.ToInt64(Math.Floor((double)(so / 10)));
            //Lấy số hàng đơn vị bằng phép chia 21 % 10 = 1
            Int64 donvi = (Int64)so % 10;
            //Nếu số hàng chục tồn tại tức >=20
            if (chuc > 1)
            {
                chuoi = " " + mNumText[chuc] + " mươi";
                if (donvi == 1)
                {
                    chuoi += " mốt";
                }
            }
            else if (chuc == 1)
            {//Số hàng chục từ 10-19
                chuoi = " mười";
                if (donvi == 1)
                {
                    chuoi += " một";
                }
            }
            else if (daydu && donvi > 0)
            {//Nếu hàng đơn vị khác 0 và có các số hàng trăm ví dụ 101 => thì biến daydu = true => và sẽ đọc một trăm lẻ một
                chuoi = " lẻ";
            }
            if (donvi == 5 && chuc >= 1)
            {//Nếu đơn vị là số 5 và có hàng chục thì chuỗi sẽ là " lăm" chứ không phải là " năm"
                chuoi += " lăm";
            }
            else if (donvi > 1 || (donvi == 1 && chuc == 0))
            {
                chuoi += " " + mNumText[donvi];
            }
            return chuoi;
        }
        private string DocHangTram(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng trăm ví du 434 / 100 = 4 (hàm Floor sẽ làm tròn số nguyên bé nhất)
            Int64 tram = Convert.ToInt64(Math.Floor((double)so / 100));
            //Lấy phần còn lại của hàng trăm 434 % 100 = 34 (dư 34)
            so = so % 100;
            if (daydu || tram > 0)
            {
                chuoi = " " + mNumText[tram] + " trăm";
                chuoi += DocHangChuc(so, true);
            }
            else
            {
                chuoi = DocHangChuc(so, false);
            }
            return chuoi;
        }
        private string DocHangTrieu(double so, bool daydu)
        {
            string chuoi = "";
            //Lấy số hàng triệu
            Int64 trieu = Convert.ToInt64(Math.Floor((double)so / 1000000));
            //Lấy phần dư sau số hàng triệu ví dụ 2,123,000 => so = 123,000
            so = so % 1000000;
            if (trieu > 0)
            {
                chuoi = DocHangTram(trieu, daydu) + " triệu";
                daydu = true;
            }
            //Lấy số hàng nghìn
            Int64 nghin = Convert.ToInt64(Math.Floor((double)so / 1000));
            //Lấy phần dư sau số hàng nghin 
            so = so % 1000;
            if (nghin > 0)
            {
                chuoi += DocHangTram(nghin, daydu) + " nghìn";
                daydu = true;
            }
            if (so > 0)
            {
                chuoi += DocHangTram(so, daydu);
            }
            return chuoi;
        }
        public string ChuyenSoSangChuoi(double so)
        {
            if (so == 0)
                return mNumText[0];
            string chuoi = "", hauto = "";
            Int64 ty;
            do
            {
                //Lấy số hàng tỷ
                ty = Convert.ToInt64(Math.Floor((double)so / 1000000000));
                //Lấy phần dư sau số hàng tỷ
                so = so % 1000000000;
                if (ty > 0)
                {
                    chuoi = DocHangTrieu(so, true) + hauto + chuoi;
                }
                else
                {
                    chuoi = DocHangTrieu(so, false) + hauto + chuoi;
                }
                hauto = " tỷ";
            } while (ty > 0);
            return chuoi + " đồng";
        }  
    }
}
