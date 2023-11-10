using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FormAdmin : Form
    {
        private Form formTong = null;
        public FormAdmin()
        {
            InitializeComponent();
        }
        void OpenForm(Form form)
        {
            if (formTong != null)
            {
                panelBody.Controls.Remove(formTong);
                using (formTong)
                {
                    formTong.Dispose();
                }
            }

            formTong = form;
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;
            panelBody.Controls.Add(form);
            panelBody.Tag = form;

            form.Opacity = 1;
            form.BringToFront();
            form.Show();
        }



        private void btnQuanLiCH_Click(object sender, EventArgs e)
        {

            btnQLHL.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiSP.FillColor = Color.FromArgb(53, 45, 125);
            btnTaiKhoan.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiCH.FillColor = Color.FromArgb(255, 150, 0);

            OpenForm(new FAdminCH());
        }

        private void btnQuanLiSP_Click(object sender, EventArgs e)
        {
            btnQLHL.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiSP.FillColor = Color.FromArgb(255, 150, 0);
            btnTaiKhoan.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiCH.FillColor = Color.FromArgb(53, 45, 125);

            OpenForm(new FAdminSP());
        }

        private void btnQLHL_Click(object sender, EventArgs e)
        {
            btnQLHL.FillColor = Color.FromArgb(255, 150, 0);
            btnQuanLiSP.FillColor = Color.FromArgb(53, 45, 125);
            btnTaiKhoan.FillColor = Color.FromArgb(53, 45, 125);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiCH.FillColor = Color.FromArgb(53, 45, 125);
            OpenForm(new FAdminLSPTH());
        }

        private void btnTaiKhoan_Click(object sender, EventArgs e)
        {
            btnQLHL.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiSP.FillColor = Color.FromArgb(53, 45, 125);
            btnTaiKhoan.FillColor = Color.FromArgb(255, 150, 0);
            btnThoat.FillColor = Color.FromArgb(53, 45, 125);
            btnQuanLiCH.FillColor = Color.FromArgb(53, 45, 125);
            OpenForm(new FAdminTK());
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormAdmin_Load(object sender, EventArgs e)
        {
            this.MinimumSize = new Size(1361, 600); // Đặt kích thước tối thiểu cho Form.
            this.StartPosition = FormStartPosition.CenterScreen;
            OpenForm(new FAdminCH());
        }
    }
}
