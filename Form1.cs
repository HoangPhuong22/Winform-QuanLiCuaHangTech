using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Drawing;
using System.IO;
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

            // Sử dụng Show() thay vì ShowDialog()
            form.Show();
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
            btnTen.Text = FormLogin.TenNhanVien;
        }

        private void btnThongTin_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            // Thiết lập các thuộc tính cho hộp thoại chọn tệp
            openFileDialog1.Title = "Chọn ảnh";
            openFileDialog1.Filter = "Ảnh (*.png;*.jpg;*.jpeg;*.webp)|*.png;*.jpg;*.jpeg;*.webp";


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
                    dataConnect.DataChange(select);
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
