using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Security.Cryptography; //MD5

namespace QL_ShopQuanAo
{
    public partial class frmlogin : Form
    {
        public frmlogin()
        {
            InitializeComponent();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = false;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            txtPassword.UseSystemPasswordChar = true;
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtUser.Text = "";
            txtPassword.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            byte[] temp = ASCIIEncoding.ASCII.GetBytes(txtPassword.Text);
            byte[] hasData = new MD5CryptoServiceProvider().ComputeHash(temp);

            string hasPass = "";
            foreach (byte item in hasData)
            {
                hasPass += item;
            }

            if (txtUser.Text.Length == 0)
            {
                MessageBox.Show("Bạn không được để trống tên tài khoản!");
                return;
            }
            if (txtPassword.Text.Length == 0)
            {
                MessageBox.Show("Bạn không được để trống mật khẩu!");
                return;
            }
            SqlConnection conn = new SqlConnection("Data Source = DESKTOP-Q1HN2VJ\\SQLEXPRESS; Initial Catalog = QL_SHOP; Integrated Security = True");
            SqlCommand cmd = new SqlCommand("select * from TAIKHOAN where MAIL = @mail and MATKHAU = @matkhau", conn);
            cmd.Parameters.Add("@mail", SqlDbType.NVarChar).Value = txtUser.Text;
            cmd.Parameters.Add("@matkhau", SqlDbType.VarChar).Value = hasPass;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable table = new DataTable();
            da.Fill(table);
            if (table.Rows.Count == 1)
            {
                frmMain frmmain = new frmMain();
                frmmain.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Tài khoản hoặc mật khẩu sai vui lòng kiểm tra lại!");
            }
        }
    }
}
