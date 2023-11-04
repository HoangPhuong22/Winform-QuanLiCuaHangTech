using QLCuaHangBanDoCongNGhe.Data;
using System;
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
            form.Show();
        }

        private void btnDonHang_Click(object sender, EventArgs e)
        {
            OpenForm(new FormDonHang());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSanPham_Click(object sender, EventArgs e)
        {
            OpenForm(new FormSanPham());
        }

        private void btnKhachHang_Click(object sender, EventArgs e)
        {
            OpenForm(new FormKhachHang());
        }

        private void btnHoaDon_Click(object sender, EventArgs e)
        {
            OpenForm(new FormHoaDon());
        }

        private void FormMain_Load(object sender, EventArgs e)
        {
            btnChiNhanh.Text = "Chi nhánh : " + FormLogin.DiaChiCH;
            OpenForm(new FormDonHang());
        }
    }
}
