namespace QL_ShopQuanAo
{
    partial class frmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.hệThốngToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmQuanLiTK = new System.Windows.Forms.ToolStripMenuItem();
            this.đăngXuấtToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.danhMụcToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmChatLieu = new System.Windows.Forms.ToolStripMenuItem();
            this.frmSanPham = new System.Windows.Forms.ToolStripMenuItem();
            this.frmHoaDon = new System.Windows.Forms.ToolStripMenuItem();
            this.quảnLíToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.frmChucVu = new System.Windows.Forms.ToolStripMenuItem();
            this.frmNhanVien = new System.Windows.Forms.ToolStripMenuItem();
            this.frmKhachHang = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.hệThốngToolStripMenuItem,
            this.danhMụcToolStripMenuItem,
            this.quảnLíToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(1182, 28);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // hệThốngToolStripMenuItem
            // 
            this.hệThốngToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmQuanLiTK,
            this.đăngXuấtToolStripMenuItem});
            this.hệThốngToolStripMenuItem.Name = "hệThốngToolStripMenuItem";
            this.hệThốngToolStripMenuItem.Size = new System.Drawing.Size(83, 24);
            this.hệThốngToolStripMenuItem.Text = "Tài khoản";
            // 
            // frmQuanLiTK
            // 
            this.frmQuanLiTK.Image = ((System.Drawing.Image)(resources.GetObject("frmQuanLiTK.Image")));
            this.frmQuanLiTK.Name = "frmQuanLiTK";
            this.frmQuanLiTK.Size = new System.Drawing.Size(196, 26);
            this.frmQuanLiTK.Text = "Quản lí tài khoản";
            this.frmQuanLiTK.Click += new System.EventHandler(this.frmQuanLiTK_Click);
            // 
            // đăngXuấtToolStripMenuItem
            // 
            this.đăngXuấtToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("đăngXuấtToolStripMenuItem.Image")));
            this.đăngXuấtToolStripMenuItem.Name = "đăngXuấtToolStripMenuItem";
            this.đăngXuấtToolStripMenuItem.Size = new System.Drawing.Size(196, 26);
            this.đăngXuấtToolStripMenuItem.Text = "Đăng xuất";
            this.đăngXuấtToolStripMenuItem.Click += new System.EventHandler(this.đăngXuấtToolStripMenuItem_Click);
            // 
            // danhMụcToolStripMenuItem
            // 
            this.danhMụcToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmChatLieu,
            this.frmSanPham,
            this.frmHoaDon});
            this.danhMụcToolStripMenuItem.Name = "danhMụcToolStripMenuItem";
            this.danhMụcToolStripMenuItem.Size = new System.Drawing.Size(88, 24);
            this.danhMụcToolStripMenuItem.Text = "Danh mục";
            // 
            // frmChatLieu
            // 
            this.frmChatLieu.Image = ((System.Drawing.Image)(resources.GetObject("frmChatLieu.Image")));
            this.frmChatLieu.Name = "frmChatLieu";
            this.frmChatLieu.Size = new System.Drawing.Size(150, 26);
            this.frmChatLieu.Text = "Chất liệu";
            this.frmChatLieu.Click += new System.EventHandler(this.frmChatLieu_Click);
            // 
            // frmSanPham
            // 
            this.frmSanPham.Image = ((System.Drawing.Image)(resources.GetObject("frmSanPham.Image")));
            this.frmSanPham.Name = "frmSanPham";
            this.frmSanPham.Size = new System.Drawing.Size(150, 26);
            this.frmSanPham.Text = "Sản phẩm";
            this.frmSanPham.Click += new System.EventHandler(this.frmSanPham_Click);
            // 
            // frmHoaDon
            // 
            this.frmHoaDon.Image = ((System.Drawing.Image)(resources.GetObject("frmHoaDon.Image")));
            this.frmHoaDon.Name = "frmHoaDon";
            this.frmHoaDon.Size = new System.Drawing.Size(150, 26);
            this.frmHoaDon.Text = "Hóa Đơn";
            this.frmHoaDon.Click += new System.EventHandler(this.frmHoaDon_Click);
            // 
            // quảnLíToolStripMenuItem
            // 
            this.quảnLíToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.frmChucVu,
            this.frmNhanVien,
            this.frmKhachHang});
            this.quảnLíToolStripMenuItem.Name = "quảnLíToolStripMenuItem";
            this.quảnLíToolStripMenuItem.Size = new System.Drawing.Size(68, 24);
            this.quảnLíToolStripMenuItem.Text = "Quản lí";
            // 
            // frmChucVu
            // 
            this.frmChucVu.Image = ((System.Drawing.Image)(resources.GetObject("frmChucVu.Image")));
            this.frmChucVu.Name = "frmChucVu";
            this.frmChucVu.Size = new System.Drawing.Size(161, 26);
            this.frmChucVu.Text = "Chức vụ";
            this.frmChucVu.Click += new System.EventHandler(this.frmChucVu_Click);
            // 
            // frmNhanVien
            // 
            this.frmNhanVien.Image = ((System.Drawing.Image)(resources.GetObject("frmNhanVien.Image")));
            this.frmNhanVien.Name = "frmNhanVien";
            this.frmNhanVien.Size = new System.Drawing.Size(161, 26);
            this.frmNhanVien.Text = "Nhân viên";
            this.frmNhanVien.Click += new System.EventHandler(this.frmNhanVien_Click);
            // 
            // frmKhachHang
            // 
            this.frmKhachHang.Image = ((System.Drawing.Image)(resources.GetObject("frmKhachHang.Image")));
            this.frmKhachHang.Name = "frmKhachHang";
            this.frmKhachHang.Size = new System.Drawing.Size(161, 26);
            this.frmKhachHang.Text = "Khách hàng";
            this.frmKhachHang.Click += new System.EventHandler(this.frmKhachHang_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BackgroundImage = global::QL_ShopQuanAo.Properties.Resources.hìnhNen;
            this.pictureBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pictureBox1.Location = new System.Drawing.Point(0, 28);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1182, 625);
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // frmMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1182, 653);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.menuStrip1);
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "frmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản lí shop bán quần áo";
            this.Load += new System.EventHandler(this.frmMain_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem hệThốngToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem danhMụcToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frmChatLieu;
        private System.Windows.Forms.ToolStripMenuItem frmSanPham;
        private System.Windows.Forms.ToolStripMenuItem frmQuanLiTK;
        private System.Windows.Forms.ToolStripMenuItem quảnLíToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem frmChucVu;
        private System.Windows.Forms.ToolStripMenuItem frmHoaDon;
        private System.Windows.Forms.ToolStripMenuItem frmNhanVien;
        private System.Windows.Forms.ToolStripMenuItem frmKhachHang;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ToolStripMenuItem đăngXuấtToolStripMenuItem;
    }
}

