using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FormAdmin : Form
    {
        private Form formTong = null;
        DataConnect data = new DataConnect();
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
            btnTen.Text = FormLogin.TenNhanVien;
            OpenForm(new FAdminCH());
            // Đường dẫn của ảnh bạn muốn hiển thị
            string imagePath = "D:\\Winform-QuanLiCuaHangTech\\bin\\Debug\\Images\\" + FormLogin.AnhNhanVien;

            // Kiểm tra xem tệp ảnh có tồn tại không trước khi hiển thị
            if (File.Exists(imagePath))
            {
                // Hiển thị ảnh trên PictureBox
                ptbAnhDaiDien.Image = Image.FromFile(imagePath);
            }
            else
            {
                // Hiển thị một hình ảnh mặc định hoặc thông báo lỗi nếu tệp không tồn tại
                // ptbAnhDaiDien.Image = yourDefaultImage; // Thay yourDefaultImage bằng hình ảnh mặc định của bạn
                MessageBox.Show("Không thể tìm thấy tệp ảnh!");
            }
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Thiết lập các thuộc tính cho hộp thoại chọn tệp
            openFileDialog1.Title = "Chọn ảnh";
            openFileDialog1.Filter = "Tất cả các tệp|*.*|Ảnh|*.jpg;*.png;*.gif;*.bmp";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    // Lấy đường dẫn của tệp đã chọn
                    string selectedImagePath = openFileDialog1.FileName;

                    // Thiết lập đường dẫn đích để sao chép tệp
                    string destinationPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "D:\\Winform-QuanLiCuaHangTech\\bin\\Debug\\Images\\");

                    // Kiểm tra xem thư mục đích đã tồn tại chưa, nếu chưa thì tạo mới
                    if (!Directory.Exists(destinationPath))
                    {
                        Directory.CreateDirectory(destinationPath);
                    }

                    // Tạo tên mới cho tệp ảnh bằng cách sử dụng ngày và giờ hiện tại
                    string newFileName = "Image_" + DateTime.Now.ToString("yyyyMMdd_HHmmss") + Path.GetExtension(selectedImagePath);
                    string select = "UPDATE tNhanVien SET AnhNhanVien = '" + newFileName + "' WHERE MaNhanVien = '" + FormLogin.MaNhanVien + "'";
                    data.DataChange(select);
                    // Tạo đường dẫn đến tệp mới
                    string destinationFilePath = Path.Combine(destinationPath, newFileName);

                    // Sao chép tệp từ đường dẫn nguồn đến đường dẫn đích
                    File.Copy(selectedImagePath, destinationFilePath, true);

                    // Hiển thị ảnh trên PictureBox
                    ptbAnhDaiDien.Image = Image.FromFile(destinationFilePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Đã xảy ra lỗi: " + ex.Message);
                }
            }
        }
    }
}
