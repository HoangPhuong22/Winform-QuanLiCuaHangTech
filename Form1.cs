using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FormMain : Form
    {
        private Form formTong = null; // Đặt giá trị mặc định là null
        private DataConnect dataConnect;
        public FormMain()
        {
            InitializeComponent();
            dataConnect = new DataConnect();
        }

        void OpenForm(Form form)
        {
            if (formTong != null)
            {
                formTong.Close();
                panelBody.Controls.Remove(formTong);
            }

            formTong = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelBody.Controls.Add(form);
            panelBody.Tag = form;
            form.BringToFront();
            form.ShowDialog();
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            btnKhachHang.FillColor = Color.FromArgb(53, 45, 125);
            btnSanPham.FillColor = Color.FromArgb(53, 45, 125);
            btnHoaDon.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnDonHang.FillColor = Color.FromArgb(255, 150, 0);
            OpenForm(new FormDonHang());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
            btnKhachHang.FillColor = Color.FromArgb(53, 45, 125);
            btnSanPham.FillColor = Color.FromArgb(53, 45, 125);
            btnHoaDon.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(255, 150, 0);
            btnDonHang.FillColor = Color.FromArgb(53, 45, 125);
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenForm(new FormSanPham());
            btnKhachHang.FillColor = Color.FromArgb(53, 45, 125);
            btnSanPham.FillColor = Color.FromArgb(255, 150, 0);
            btnHoaDon.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnDonHang.FillColor = Color.FromArgb(53, 45, 125);
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenForm(new FormKhachHang());
            btnKhachHang.FillColor = Color.FromArgb(255, 150, 0);
            btnSanPham.FillColor = Color.FromArgb(53, 45, 125);
            btnHoaDon.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnDonHang.FillColor = Color.FromArgb(53, 45, 125);
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenForm(new FormHoaDon());
            btnKhachHang.FillColor = Color.FromArgb(53, 45, 125);
            btnSanPham.FillColor = Color.FromArgb(53, 45, 125);
            btnHoaDon.FillColor = Color.FromArgb(255, 150, 0);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnDonHang.FillColor = Color.FromArgb(53, 45, 125);
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btnChiNhanh.Text = "Chi nhánh : " + FormLogin.DiaChiCH;
            OpenForm(new FormDonHang());
        }
    }
}
