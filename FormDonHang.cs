using Guna.UI2.WinForms;
using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FormDonHang : Form
    {
        private DataConnect dataConnect;
        public FormDonHang()
        {
            InitializeComponent();
            dataConnect = new DataConnect();
        }
        private void FormDonHang_Load(object sender, EventArgs e)
        {
            string tenCH = FormLogin.TenCuahang;
            string select = "SELECT ct.MaSanPham FROM tSanPham sp JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap JOIN tCuaHang ch ON ch.MaCuaHang = hd.MaCuaHang WHERE ch.TenCuaHang = N'"+tenCH+"'";
            DataTable result = dataConnect.DataReader(select);
            // ẩn các btn
            btnLuu.Enabled = false;
            btnXuatHoaDon.Enabled = false;


            // Đổ dữ liệu vào ComboBox
            if (result.Rows.Count > 0)
            {
                foreach (DataRow row in result.Rows)
                {
                    cbMaSanPham.Items.Add(row["MaSanPham"].ToString());
                }
            }
        }

        private void cbMaSanPham_SelectedIndexChanged(object sender, EventArgs e)
        {
            string msp = "";
            try
            {
                msp = cbMaSanPham.SelectedItem.ToString();
            }
            catch { }
            // Tìm kiếm
            string select = "SELECT * FROM tSanPham sp JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE MaSanPham = '" + msp + "'";
            DataTable result = dataConnect.DataReader(select);
            if(result.Rows.Count > 0)
            {
                // Gán các thuộc tính của sản phẩm
                txtTenSanPham.Text = result.Rows[0]["TenSanPham"].ToString();
                double donGiaBan;
                if (double.TryParse(result.Rows[0]["DonGiaBan"].ToString(), out donGiaBan))
                {
                    txtDonGiaBan.Text = string.Format("{0:#,###}", donGiaBan) + "vnđ";
                }
                else
                {
                    txtDonGiaBan.Text = "Không hợp lệ"; // Xử lý khi giá không hợp lệ
                }
                txtLoaiSanPham.Text = result.Rows[0]["TenLoai"].ToString();
                txtThuongHieu.Text = result.Rows[0]["TenThuongHieu"].ToString();
                

                // Cập nhật số lượng tồn
                select = "SELECT ISNULL(SUM(ct.SoLuongNhap), 0) FROM tChiTietHDN ct JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap WHERE ct.MaSanPham  = '"+msp+"' ";

                int SLNhap = dataConnect.ExecuteScalar(select);

                select = "SELECT ISNULL(SUM(ct.SoLuongBan), 0) FROM tChiTietHDB ct JOIN tHoaDonBan hd ON hd.MaHoaDon = ct.MaHoaDon WHERE ct.MaSanPham = '" + msp + "' ";
                int SLBan = dataConnect.ExecuteScalar(select);

                txtSoLuongTon.Text = (SLNhap - SLBan).ToString();


            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string msp = txtTimKiemMaSP.Text;
            lbThongBaoSearchMSP.Text = string.Empty;
            // Tìm kiếm sản phẩm
            string select = "SELECT * FROM tSanPham sp JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE MaSanPham = '" + msp + "'";
            DataTable result = dataConnect.DataReader(select);
            if (result.Rows.Count > 0)
            {
                // Thông báo
                lbThongBaoSearchMSP.Text = "Đã tìm thấy";

                // Cập nhật cbMaSanPham
                string valueToDisplay = txtTimKiemMaSP.Text;
                int index = cbMaSanPham.FindStringExact(valueToDisplay);
                cbMaSanPham.SelectedIndex = index;

                // Gán các giá trị đã tìm thấy cho textbox
                txtTenSanPham.Text = result.Rows[0]["TenSanPham"].ToString();
                double donGiaBan;
                if (double.TryParse(result.Rows[0]["DonGiaBan"].ToString(), out donGiaBan))
                {
                    txtDonGiaBan.Text = string.Format("{0:#,###}", donGiaBan) + "vnđ";
                }
                else
                {
                    txtDonGiaBan.Text = "Không hợp lệ"; // Xử lý khi giá không hợp lệ
                }
                txtLoaiSanPham.Text = result.Rows[0]["TenLoai"].ToString();
                txtThuongHieu.Text = result.Rows[0]["TenThuongHieu"].ToString();
                txtSoLuongTon.Text = "0";

                // Cập nhật số lượng tồn
                select = "SELECT ISNULL(SUM(ct.SoLuongNhap), 0) FROM tChiTietHDN ct JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap WHERE ct.MaSanPham  = '" + msp + "' ";

                int SLNhap = dataConnect.ExecuteScalar(select);

                select = "SELECT ISNULL(SUM(ct.SoLuongBan), 0) FROM tChiTietHDB ct JOIN tHoaDonBan hd ON hd.MaHoaDon = ct.MaHoaDon WHERE ct.MaSanPham = '" + msp + "' ";
                int SLBan = dataConnect.ExecuteScalar(select);

                txtSoLuongTon.Text = (SLNhap - SLBan).ToString();
            }
            else
            {
                // Gán về rỗng những thuộc tính
                lbThongBaoSearchMSP.Text = "Không tồn tại sản phẩm này";
                txtDonGiaBan.Text = string.Empty;
                txtLoaiSanPham.Text= string.Empty;
                txtTenSanPham.Text = string.Empty;
                txtThuongHieu.Text = string.Empty;
                txtSoLuongTon.Text = string.Empty;
                cbMaSanPham.SelectedItem = string.Empty;
            }    
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            string sdt = txtTimKiemSDT.Text;
            lblThongBaoSDT.Text = string.Empty;
            string select = "SELECT * FROM tKhachHang WHERE SoDienThoai = '" + sdt + "'";
            DataTable result = dataConnect.DataReader(select);
            if (result.Rows.Count > 0)
            {
                string mkh = result.Rows[0]["MaKhachHang"].ToString();
                lblThongBaoSDT.Text = "Đã chọn khách hàng!";
                txtTenKhachHang.Text = result.Rows[0]["TenKhachHang"].ToString();
                txtDiaChi.Text = result.Rows[0]["DiaChi"].ToString();

                dtpNgaySinh.Value = Convert.ToDateTime(result.Rows[0]["NamSinh"]);
                txtSoDienThoai.Text = result.Rows[0]["SoDienThoai"].ToString();

                select = "SELECT COUNT(*) FROM tHoaDonBan hd JOIN tKhachHang kh ON kh.MaKhachHang = hd.MaKhachHang WHERE kh.SoDienThoai = '" + sdt + "'";
                int count = dataConnect.ExecuteScalar(select);
                select = "UPDATE tKhachHang SET LuotMua = '" + count + "' WHERE MaKhachHang = '" + mkh + "'";
                dataConnect.DataChange(select);
                txtSoLanMua.Text = count.ToString();
                txtUuDai.Text = (count * 0.1).ToString() + "%";
            }
            else
            {
                lblThongBaoSDT.Text = "Không tồn tại khách hàng có số điện thoại này";
                txtTenKhachHang.Text = string.Empty;
                txtSoLanMua.Text = string.Empty;
                dtpNgaySinh.Value = DateTime.Now; // Đặt ngày hiện tại nếu không tìm thấy khách hàng
                txtSoDienThoai.Text = string.Empty;
                txtUuDai.Text = string.Empty;
            }
        }
        private void ResetReadOnly(bool ok)
        {
            txtTenKhachHang.ReadOnly = ok;
            txtSoDienThoai.ReadOnly = ok;
            txtDiaChi.ReadOnly = ok;
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            btnThem.Enabled = false;
            btnLuu.Enabled = true;
            ResetReadOnly(false);
            lblThongBaoSDT.Text = "Vui lòng nhập thông tin khách hàng";
            txtTenKhachHang.Text = string.Empty;
            txtSoDienThoai.Text = string.Empty;
            txtTimKiemSDT.Text = string.Empty;
            txtUuDai.Text = string.Empty;
            txtSoLanMua.Text = string.Empty;
            txtDiaChi.Text = string.Empty;

        }

        private void txtSoDienThoai_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }
        private string GenerateRandomMaKhachHang()
        {
            // Tạo mã khách hàng ngẫu nhiên (ví dụ: sử dụng ngày giờ và GUID)
            string dateTimePart = DateTime.Now.ToString("yyyyMMddHHmmss");
            string guidPart = Guid.NewGuid().ToString("N");
            return dateTimePart + guidPart;
        }
        private void btnLuu_Click(object sender, EventArgs e)
        {
            btnLuu.Enabled = false;
            btnThem.Enabled = true;
            string ten = txtTenKhachHang.Text;
            string namsinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd"); // Định dạng ngày tháng thành yyyy-MM-dd
            string sdt = txtSoDienThoai.Text;
            string diaChi = txtDiaChi.Text;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(namsinh) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng");
                txtTenKhachHang.ReadOnly = true;
                txtDiaChi.ReadOnly = true;
                dtpNgaySinh.Value = DateTime.Now;
                txtSoDienThoai.ReadOnly = true;
            }
            else
            {
               
                string select = "SELECT * FROM tKhachHang WHERE SoDienThoai = '" + sdt + "'";
                DataTable data = dataConnect.DataReader(select);
                if(data.Rows.Count > 0)
                {
                    MessageBox.Show("Số điện thoại đã tồn tại!");
                    txtDiaChi.Text = txtTenKhachHang.Text = txtSoDienThoai.Text = "";
                    return;
                }
                string mkh = GenerateRandomMaKhachHang();
                string insert = "INSERT INTO tKhachHang VALUES('"+mkh+"', '"+sdt+"','"+0+"', '"+namsinh+"', N'"+ten+"', N'"+diaChi+"')";
                dataConnect.DataChange(insert);
                lblThongBaoSDT.Text = "Đã thêm khách hàng";
                ResetReadOnly(true);
            }    
        }

        private void nmSLSanPham_ValueChanged(object sender, EventArgs e)
        {
            try
            {
                if(nmSLSanPham.Value > Convert.ToInt32(txtSoLuongTon.Text))
                {
                    MessageBox.Show("Vượt quá tối đa số lượng sản phẩm tồn kho");
                    nmSLSanPham.Value = int.Parse(txtSoLuongTon.Text);
                    return;
                }
                string donGiaBanText = txtDonGiaBan.Text;
                // Loại bỏ 3 ký tự "vnđ" cuối cùng
                donGiaBanText = donGiaBanText.Substring(0, donGiaBanText.Length - 3);

                decimal donGiaBan = Convert.ToDecimal(donGiaBanText);
                decimal thanhTien = donGiaBan * nmSLSanPham.Value;

                // Định dạng "thanhTien" thành "vnđ" và hiển thị nó trong txtThanhTien
                txtThanhTien.Text = string.Format("{0:#,###}", thanhTien) + "vnđ"; ;

            }
            catch
            {
                MessageBox.Show("Vui lòng chọn sản phẩm trước");
            }
        }
        private bool check()
        {
            if (string.IsNullOrEmpty(txtDiaChi.Text) || string.IsNullOrEmpty(txtTenKhachHang.Text) || string.IsNullOrEmpty(txtSoDienThoai.Text)) return false;
            else return true;
        }
        private string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
        public string GenerateMaHoaDon()
        {
            // Tạo 5 ký tự ngẫu nhiên cho mã đầu tiên
            string maHoaDon = RandomString(5);

            // Lấy thời gian hiện tại
            DateTime now = DateTime.Now;

            // Thêm ngày và giờ vào mã hóa đơn
            maHoaDon += now.ToString("yyyyMMddHHmmss");

            return maHoaDon;
        }
        string mhd = "";
        int slchitiet = 0;
        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            
            if (check() == false)
            {
                MessageBox.Show("Vui lòng chọn khách hàng để thêm hóa đơn!");
                return;
            }
            if(cbMaSanPham.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn sản phẩm cần thêm!");
                return;
            }
            if(nmSLSanPham.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng sản phẩm mua!");
                return;
            }
            string sdt = txtSoDienThoai.Text;
            string select = "SELECT * FROM tKhachHang WHERE SoDienThoai = '" + sdt + "'";
            DataTable result = dataConnect.DataReader(select);
            if (result.Rows.Count > 0)
            {
                if(mhd == "") // Kiểm tra xem tạo hóa đơn hay chưa
                {
                    mhd = GenerateMaHoaDon();
                    string mkh = result.Rows[0]["MaKhachHang"].ToString();
                    string ngaylap = DateTime.Now.ToString("yyyy-MM-dd");
                    string mnv = FormLogin.MaNhanVien;
                    select = "INSERT INTO tHoaDonBan VALUES('" + mhd + "', '" + ngaylap + "','" + 0 + "', '" + mnv + "', '" + mkh + "')";
                    dataConnect.DataChange(select);

                    // Hiện nút xuất hóa đơn
                    btnXuatHoaDon.Enabled = true;
                    txtTimKiemSDT.Enabled = false;
                }
                // Tăng số chi tiết trong một hóa đơn
                ++slchitiet; 
                string cthd = "CT" + slchitiet.ToString() + mhd;
                string donGiaBanText =  txtDonGiaBan.Text.Substring(0, txtDonGiaBan.Text.Length - 3);

                decimal donGiaBan = Convert.ToDecimal(donGiaBanText);
                // Insert Chi tiết hóa đơn vào hóa đơn
                int sl = (int)nmSLSanPham.Value;
                select = "INSERT INTO tChiTietHDB VALUES('" + sl + "', '" + cthd + "', '" + mhd + "', '" + cbMaSanPham.SelectedItem.ToString() + "', '"+sl * donGiaBan+"')";
                dataConnect.DataChange(select);

                // Gán data vào đơn hàng
                select = "SELECT sp.TenSanPham , l.TenLoai , th.TenThuongHieu, ct.SoLuongBan, sp.DonGiaban, ct.TongTien, ct.MaChiTietHDB FROM tSanPham sp JOIN tChiTietHDB ct ON ct.MaSanPham = sp.MaSanPham JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE ct.MaHoaDon = '"+mhd+"'";
                DataTable donhang = dataConnect.DataReader(select);
                dgvDonHang.DataSource = donhang;

                //dgvDonHang.Columns[0].HeaderText = "Tên mới";
                dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dgvDonHang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dgvDonHang.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dgvDonHang.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                foreach (DataGridViewColumn column in dgvDonHang.Columns)
                {
                    column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
                }

                // Cập nhật tổng tiền 

                decimal tongTien = 0;

                foreach (DataGridViewRow row in dgvDonHang.Rows)
                {
                    // Kiểm tra xem hàng đó có dữ liệu và giá trị trong cột "TongTien" có hợp lệ không
                    if (!row.IsNewRow && row.Cells["TongTien"].Value != null)
                    {
                        decimal giaTriTongTien = 0;
                        if (decimal.TryParse(row.Cells["TongTien"].Value.ToString(), out giaTriTongTien))
                        {
                            tongTien += giaTriTongTien;
                        }
                    }
                }
                txtTongTien.Text = string.Format("{0:#,###}", tongTien) + "vnđ";
               

                // Xử lí cập nhật lại số lượng tồn
                btnThem.Enabled = false;
                string msp = cbMaSanPham.SelectedItem.ToString();
                select = "SELECT ISNULL(SUM(ct.SoLuongNhap), 0) FROM tChiTietHDN ct JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap WHERE ct.MaSanPham  = '" + msp + "' ";

                int SLNhap = dataConnect.ExecuteScalar(select);

                select = "SELECT ISNULL(SUM(ct.SoLuongBan), 0) FROM tChiTietHDB ct JOIN tHoaDonBan hd ON hd.MaHoaDon = ct.MaHoaDon WHERE ct.MaSanPham = '" + msp + "' ";
                int SLBan = dataConnect.ExecuteScalar(select);

                txtSoLuongTon.Text = (SLNhap - SLBan).ToString();

                // Xử lí reset numeric số lượng mua
                nmSLSanPham.Value = 0;

                // Xử lí reset các danh mục sản phẩm khi thêm xong chi tiết
                cbMaSanPham.SelectedIndex = -1;
                txtTenSanPham.Text = txtDonGiaBan.Text = txtSoLuongTon.Text = txtLoaiSanPham.Text = txtThuongHieu.Text = txtTimKiemMaSP.Text= "";

                // Xử lí reset textbox thành tiền
                txtThanhTien.Text = "0 vnđ";



            }
            else
            {
                MessageBox.Show("Số điện thoại không tồn tại, vui lòng thêm mới khách hàng!");
            }
        }

        private void txtSoLuongTon_TextChanged(object sender, EventArgs e)
        {

        }

        private void nmSLSanPham_Validating(object sender, CancelEventArgs e)
        {
            if(nmSLSanPham.Value < 1)
            {
                MessageBox.Show("Số lượng sản phẩm phải lớn hơn 0");
            }
        }
        
        private void btnXuatHoaDon_Click(object sender, EventArgs e)
        {
            string select = "UPDATE tHoaDonBan " +
                         "SET TongTien = (SELECT SUM(ct.TongTien) " +
                         "FROM tChiTietHDB ct " +
                         "WHERE ct.MaHoaDon = tHoaDonBan.MaHoaDon) " +
                         "WHERE tHoaDonBan.MaHoaDon = '" + mhd + "'";
            dataConnect.DataChange(select);

            mhd = "";
            btnThem.Enabled = true;
            slchitiet = 0;
            txtTimKiemSDT.Text = "";
            txtTimKiemSDT.Enabled = true;
            btnXuatHoaDon.Enabled = false;
            lblThongBaoSDT.Text = "";
            txtTenKhachHang.Text = txtSoDienThoai.Text = txtDiaChi.Text = txtUuDai.Text = txtSoLanMua.Text = "";
            dtpNgaySinh.Value = DateTime.Now;

            // Loại bỏ đơn hàng
            dgvDonHang.DataSource = null; // Loại bỏ dữ liệu hiện tại
            dgvDonHang.Rows.Clear(); // Xóa tất cả các hàng
            dgvDonHang.Columns.Clear(); // Xóa tất cả các cột

            // Cap Nhat tong tien

            

        }

        private void txtTimKiemSDT_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != (char)Keys.Back)
            {
                // Ngăn người dùng nhập ký tự không hợp lệ
                e.Handled = true;
            }
        }

        private void dgvDonHang_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

        }

        private void dgvDonHang_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string maChiTietHDB = dgvDonHang.Rows[e.RowIndex].Cells["MaChiTietHDB"].Value.ToString();

                DialogResult result = MessageBox.Show("Bạn có muốn xóa chi tiết hóa đơn có mã " + maChiTietHDB + " không?", "Xác nhận xóa", MessageBoxButtons.YesNo);

                if (result == DialogResult.Yes)
                {
                    // Thực hiện xóa chi tiết hóa đơn dựa trên maChiTietHDB
                    // Ví dụ, sử dụng một đối tượng DataConnect đã được tạo để thực hiện xóa
                    string sqlDelete = "DELETE FROM tChiTietHDB WHERE MaChiTietHDB = '" + maChiTietHDB + "'";
                    dataConnect.DataChange(sqlDelete);

                    // Cập nhật bảng dgv_DonHang sau khi xóa
                    // Ví dụ, load lại dữ liệu từ cơ sở dữ liệu và gán lại cho dgv_DonHang
                    // dgv_DonHang.DataSource = LoadDataFromDatabase();
                    // Gán data vào đơn hàng
                    string select = "SELECT sp.TenSanPham , l.TenLoai , th.TenThuongHieu, ct.SoLuongBan, sp.DonGiaban, ct.TongTien, ct.MaChiTietHDB FROM tSanPham sp JOIN tChiTietHDB ct ON ct.MaSanPham = sp.MaSanPham JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE ct.MaHoaDon = '" + mhd + "'";
                    DataTable donhang = dataConnect.DataReader(select);
                    dgvDonHang.DataSource = donhang;

                    //dgvDonHang.Columns[0].HeaderText = "Tên mới";
                    dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                    dgvDonHang.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                    dgvDonHang.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                    dgvDonHang.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                    dgvDonHang.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                    foreach (DataGridViewColumn column in dgvDonHang.Columns)
                    {
                        column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                        column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
                    }
                }
                // Cập nhật tổng tiền 

                decimal tongTien = 0;

                foreach (DataGridViewRow row in dgvDonHang.Rows)
                {
                    // Kiểm tra xem hàng đó có dữ liệu và giá trị trong cột "TongTien" có hợp lệ không
                    if (!row.IsNewRow && row.Cells["TongTien"].Value != null)
                    {
                        decimal giaTriTongTien = 0;
                        if (decimal.TryParse(row.Cells["TongTien"].Value.ToString(), out giaTriTongTien))
                        {
                            tongTien += giaTriTongTien;
                        }
                    }
                }
                txtTongTien.Text = string.Format("{0:#,###}", tongTien) + "vnđ";
            }
        }

    }
}
