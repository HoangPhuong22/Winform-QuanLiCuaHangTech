using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Collections;
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
            string select = "SELECT MaSanPham FROM tSanPham";
            DataTable result = dataConnect.DataReader(select);

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
            string msp = cbMaSanPham.SelectedItem.ToString();
            string select = "SELECT * FROM tSanPham sp JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE MaSanPham = '" + msp + "'";
            DataTable result = dataConnect.DataReader(select);
            if(result.Rows.Count > 0)
            {
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
            }
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string msp = txtTimKiemMaSP.Text;
            lbThongBaoSearchMSP.Text = string.Empty;
            string select = "SELECT * FROM tSanPham sp JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE MaSanPham = '" + msp + "'";
            DataTable result = dataConnect.DataReader(select);
            if (result.Rows.Count > 0)
            {
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
                cbMaSanPham.SelectedItem = txtTimKiemMaSP.Text;
            }
            else
            {
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
                txtTenKhachHang.Text = result.Rows[0]["TenKhachHang"].ToString();
                txtDiaChi.Text = result.Rows[0]["DiaChi"].ToString();

                dtpNgaySinh.Value = Convert.ToDateTime(result.Rows[0]["NamSinh"]);
                txtSoDienThoai.Text = result.Rows[0]["SoDienThoai"].ToString();

                select = "SELECT COUNT(*) FROM tHoaDonBan hd JOIN tKhachHang kh ON kh.MaKhachHang = hd.MaKhachHang WHERE kh.SoDienThoai = '" + sdt + "'";
                int count = dataConnect.ExecuteScalar(select);
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
            string ten = txtTenKhachHang.Text;
            string namsinh = dtpNgaySinh.Value.ToString("yyyy-MM-dd"); // Định dạng ngày tháng thành yyyy-MM-dd
            string sdt = txtSoDienThoai.Text;
            string diaChi = txtDiaChi.Text;
            if (string.IsNullOrEmpty(ten) || string.IsNullOrEmpty(namsinh) || string.IsNullOrEmpty(sdt) || string.IsNullOrEmpty(diaChi))
            {
                MessageBox.Show("Vui lòng nhập đủ thông tin khách hàng");
            }
            else
            {
                string mkh = GenerateRandomMaKhachHang();
                string insert = "INSERT INTO tKhachHang VALUES('"+mkh+"', '"+sdt+"','"+0+"', '"+namsinh+"', '"+ten+"', '"+diaChi+"')";
                dataConnect.DataChange(insert);
                lblThongBaoSDT.Text = "Đã thêm khách hàng";
                ResetReadOnly(true);
            }    
        }

        private void nmSLSanPham_ValueChanged(object sender, EventArgs e)
        {
            try
            {
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

        private void btnThemHoaDon_Click(object sender, EventArgs e)
        {

        }
    }
}
