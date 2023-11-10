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
    public partial class FAdminTK : Form
    {
        DataConnect data = new DataConnect();
        public FAdminTK()
        {
            InitializeComponent();
        }
        void LoadNV()
        {
            string select = "SELECT nv.TenNhanVien, nv.SoDienThoai,nv.NamSinh, nv.DiaChi, ch.TenCuaHang,nv.TrangThai, nv.MaNhanVien, nv.AnhNhanVien FROM tNhanVien nv JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuahang";
            DataTable dt = data.DataReader(select);
            dtgvNhanVien.DataSource = dt;
            dtgvNhanVien.Columns[0].HeaderText = "Tên nhân viên";
            dtgvNhanVien.Columns[1].HeaderText = "Số điện thoại";
            dtgvNhanVien.Columns[2].HeaderText = "Năm sinh";
            dtgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dtgvNhanVien.Columns[4].HeaderText = "Cửa hàng";
            dtgvNhanVien.Columns[5].HeaderText = "Trạng thái";

            dtgvNhanVien.Columns["MaNhanVien"].Visible = false;
            dtgvNhanVien.Columns["AnhNhanVien"].Visible = false;
            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvNhanVien.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvNhanVien.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvNhanVien.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }
        void loadTK()
        {
            string select = "SELECT nv.TenNhanVien, tk.Role, tk.UserName, tk.PassWord,ch.TenCuaHang " +
                "FROM tTaiKhoan tk JOIN tNhanVien nv ON nv.UserName = tk.UserName " +
                "JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang";
            DataTable dt = data.DataReader(select);
            dtgvTaiKhoan.DataSource = dt;
            dtgvTaiKhoan.Columns[0].HeaderText = "Tên nhân viên";
            dtgvTaiKhoan.Columns[1].HeaderText = "Quyền";
            dtgvTaiKhoan.Columns[2].HeaderText = "User Name";
            dtgvTaiKhoan.Columns[3].HeaderText = "PassWord";
            dtgvTaiKhoan.Columns[4].HeaderText = "Cửa hàng";


            dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvTaiKhoan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvTaiKhoan.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvTaiKhoan.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvTaiKhoan.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }
        void LoadCH()
        {
            string select = "SELECT TenCuaHang FROM tCuaHang";
            DataTable dt = data.DataReader(select);
            foreach(DataRow row in dt.Rows)
            {
                cbCuaHang.Items.Add(row["TenCuaHang"]);
            }
        }
        private void FAdminTK_Load(object sender, EventArgs e)
        {
            LoadNV(); loadTK(); LoadCH();

            btnSua.Enabled = false;
            btnKhoiPhuc.Enabled = false;
            btnXoa.Enabled = false;
            btnSuaTK.Enabled = false;
            
            cbRole.Items.Add("Admin");
            cbRole.Items.Add("Nhân viên");
        }
        string mnv = "";
        private void dtgvNhanVien_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dtgvNhanVien.RowCount)
            {
                try
                {
                    txtTenNV.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["TenNhanVien"].Value.ToString();
                    txtDC.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                    dtpNgaySinh.Value = Convert.ToDateTime(dtgvNhanVien.Rows[e.RowIndex].Cells["NamSinh"].Value.ToString());
                    txtSDT.Text = dtgvNhanVien.Rows[e.RowIndex].Cells["SoDienThoai"].Value.ToString();
                    btnSua.Enabled = true;
                    btnXoa.Enabled = true;
                    btnKhoiPhuc.Enabled = true;
                    btnThem.Enabled = false;
                    mnv = dtgvNhanVien.Rows[e.RowIndex].Cells["MaNhanVien"].Value.ToString();
                    string anh = dtgvNhanVien.Rows[e.RowIndex].Cells["AnhNhanVien"].Value.ToString();
                    // Hiện ảnh 
                    btnDoiAnh.Enabled = true;
                    string imagePath = "D:\\Winform-QuanLiCuaHangTech\\bin\\Debug\\Images\\" + anh;

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
                    

                    btnSuaTK.Enabled = true;
                    txtUSN.ReadOnly = true;


                    string select = "SELECT nv.TenNhanVien, tk.Role, tk.UserName, tk.PassWord,ch.TenCuaHang " +
                           "FROM tTaiKhoan tk JOIN tNhanVien nv ON nv.UserName = tk.UserName " +
                           "JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE nv.MaNhanVien = '"+mnv+"'";
                    DataTable dt = data.DataReader(select);
                    dtgvTaiKhoan.DataSource = dt;
                    dtgvTaiKhoan.Columns[0].HeaderText = "Tên nhân viên";
                    dtgvTaiKhoan.Columns[1].HeaderText = "Quyền";
                    dtgvTaiKhoan.Columns[2].HeaderText = "User Name";
                    dtgvTaiKhoan.Columns[3].HeaderText = "PassWord";
                    dtgvTaiKhoan.Columns[4].HeaderText = "Cửa hàng";


                    dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dtgvTaiKhoan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                    dtgvTaiKhoan.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dtgvTaiKhoan.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                    dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    foreach (DataGridViewColumn column in dtgvTaiKhoan.Columns)
                    {
                        column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
                    }
                }
                catch 
                {
                    btnDoiAnh.Enabled = true;
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnKhoiPhuc.Enabled = false;
                    btnThem.Enabled = true;
                    txtTenNV.Text = "";
                    txtDC.Text = "";
                    dtpNgaySinh.Value = DateTime.Now;
                    txtSDT.Text = "";
                    mnv = "";
                    btnSuaTK.Enabled = false;
                    txtUSN.ReadOnly = false;
                    dtgvTaiKhoan.DataSource = null;
                }
            }
            else
            {
                btnDoiAnh.Enabled = true;
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnThem.Enabled = true;
                txtTenNV.Text = "";
                txtDC.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                txtSDT.Text = "";
                mnv = "";
                btnSuaTK.Enabled = false;
                dtgvTaiKhoan.DataSource = null;
                txtUSN.ReadOnly = false;
            }    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn cho nghỉ việc nhân viên này", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Đã nghỉ việc";
                string select = "UPDATE tNhanVien SET TrangThai = N'" + tt + "' WHERE MaNhanVien = '" + mnv + "'";
                data.DataChange(select);
                mnv = "";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnThem.Enabled = true;
                txtTenNV.Text = "";
                txtDC.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                txtSDT.Text = "";
                mnv = "";

                LoadNV();
            }
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn khôi phục nhân viên này", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Đang làm";
                string select = "UPDATE tNhanVien SET TrangThai = N'" + tt + "' WHERE MaNhanVien = '" + mnv + "'";
                data.DataChange(select);
                mnv = "";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnThem.Enabled = true;
                txtTenNV.Text = "";
                txtDC.Text = "";
                dtpNgaySinh.Value = DateTime.Now;
                txtSDT.Text = "";
                mnv = "";

                LoadNV();
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên hợp lệ!"); return;
            }    
            if(string.IsNullOrEmpty(txtDC.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ!"); return;
            }
            string select = "";
            if (tenAnh == "") select = "UPDATE tNhanVien SET TenNhanVien = N'" + txtTenNV.Text + "',DiaChi = N'" + txtDC.Text + "',SoDienThoai = N'" + txtSDT.Text + "' WHERE MaNhanVien = '" + mnv + "'";
            else select = "UPDATE tNhanVien SET TenNhanVien = N'" + txtTenNV.Text + "',DiaChi = N'" + txtDC.Text + "',SoDienThoai = N'" + txtSDT.Text + "',AnhNhanVien ='"+tenAnh+"' WHERE MaNhanVien = '" + mnv + "'";
            data.DataChange(select);
            LoadNV();
        }
        public string GenerateRandomEmployeeCode()
        {
            Random random = new Random();
            return $"NV{random.Next(1000):D3}";
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            if(cbCuaHang.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn cửa hàng!");
                return;
            }
            if (string.IsNullOrEmpty(txtTenNV.Text))
            {
                MessageBox.Show("Vui lòng nhập tên nhân viên hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtDC.Text))
            {
                MessageBox.Show("Vui lòng nhập địa chỉ hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtSDT.Text))
            {
                MessageBox.Show("Vui lòng nhập số điện thoại hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtUSN.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtPW.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hợp lệ!"); return;
            }
            if (cbRole.SelectedItem == null) 
            {
                MessageBox.Show("Vui lòng chọn quyền cho tài khoản"); return;
            }
            if (tenAnh == "")
            {
                MessageBox.Show("Vui lòng chọn ảnh nhân viên!"); return;
            }    
            string check = "SELECT * FROM tTaiKhoan WHERE UserName = N'" + txtUSN.Text + "'";
            DataTable i = data.DataReader(check);
            if(i.Rows.Count > 0)
            {
                MessageBox.Show("Tài khoản đã tồn tại!"); return;
            }
            string select = "INSERT INTO tTaiKhoan VALUES(N'" + cbRole.SelectedItem.ToString() + "', N'" + txtPW.Text + "', N'" + txtUSN.Text + "')";
            data.DataChange(select);
            string mnv = GenerateRandomEmployeeCode();
            select = "SELECT MaCuaHang FROM tCuaHang WHERE TenCuaHang = N'" + cbCuaHang.SelectedItem.ToString() + "'";
            DataTable tmp = data.DataReader(select);
            string tt = "Đang làm";
            select = "INSERT INTO tNhanVien VALUES(N'" + txtTenNV.Text + "', '" + mnv + "', '" + txtSDT.Text + "', '" + dtpNgaySinh.Value.ToString("yyyy-MM-dd") + "',N'" + txtDC.Text + "', '" + tmp.Rows[0]["MaCuaHang"].ToString() + "', N'" + txtUSN.Text + "', N'"+tt+"', '"+tenAnh+"')";
            data.DataChange(select);

            LoadNV();
        }

        private void dtgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0 && e.RowIndex < dtgvTaiKhoan.RowCount)
            {
                txtUSN.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["UserName"].Value.ToString();
                txtPW.Text = dtgvTaiKhoan.Rows[e.RowIndex].Cells["PassWord"].Value.ToString();
                cbRole.SelectedItem = dtgvTaiKhoan.Rows[e.RowIndex].Cells["Role"].Value.ToString();
            }
        }
        string tenAnh = "";
        private void btnSuaTK_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtUSN.Text))
            {
                MessageBox.Show("Vui lòng nhập tài khoản hợp lệ!"); return;
            }
            if (string.IsNullOrEmpty(txtPW.Text))
            {
                MessageBox.Show("Vui lòng nhập mật khẩu hợp lệ!"); return;
            }
            if (cbRole.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn quyền cho tài khoản"); return;
            }
            string select = "UPDATE tTaiKhoan SET PassWord = N'" + txtPW.Text + "', Role = N'" + cbRole.SelectedItem.ToString() +"' FROM tTaiKhoan JOIN tNhanVien ON tNhanVien.UserName = tTaiKhoan.UserName WHERE tNhanVien.MaNhanVien = '"+mnv+"'";
            data.DataChange(select);


            select = "SELECT nv.TenNhanVien, tk.Role, tk.UserName, tk.PassWord,ch.TenCuaHang " +
                           "FROM tTaiKhoan tk JOIN tNhanVien nv ON nv.UserName = tk.UserName " +
                           "JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE nv.MaNhanVien = '" + mnv + "'";
            DataTable dt = data.DataReader(select);
            dtgvTaiKhoan.DataSource = dt;
            dtgvTaiKhoan.Columns[0].HeaderText = "Tên nhân viên";
            dtgvTaiKhoan.Columns[1].HeaderText = "Quyền";
            dtgvTaiKhoan.Columns[2].HeaderText = "User Name";
            dtgvTaiKhoan.Columns[3].HeaderText = "PassWord";
            dtgvTaiKhoan.Columns[4].HeaderText = "Cửa hàng";


            dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvTaiKhoan.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvTaiKhoan.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvTaiKhoan.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvTaiKhoan.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvTaiKhoan.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string ten = txtTKNhanVien.Text.Trim();
            string select = "SELECT nv.TenNhanVien, nv.SoDienThoai,nv.NamSinh, nv.DiaChi, ch.TenCuaHang,nv.TrangThai, nv.MaNhanVien FROM tNhanVien nv JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuahang WHERE nv.TenNhanVien LIKE N'%"+ten+"%'";
            DataTable dt = data.DataReader(select);
            dtgvNhanVien.DataSource = dt;
            dtgvNhanVien.Columns[0].HeaderText = "Tên nhân viên";
            dtgvNhanVien.Columns[1].HeaderText = "Số điện thoại";
            dtgvNhanVien.Columns[2].HeaderText = "Năm sinh";
            dtgvNhanVien.Columns[3].HeaderText = "Địa chỉ";
            dtgvNhanVien.Columns[4].HeaderText = "Cửa hàng";
            dtgvNhanVien.Columns[5].HeaderText = "Trạng thái";

            dtgvNhanVien.Columns["MaNhanVien"].Visible = false;

            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvNhanVien.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvNhanVien.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvNhanVien.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvNhanVien.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvNhanVien.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }

        private void btnDoiAnh_Click(object sender, EventArgs e)
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
                    tenAnh = newFileName;
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
