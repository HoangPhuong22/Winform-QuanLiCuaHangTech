using QLCuaHangBanDoCongNGhe.Data;
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
    public partial class FormKhachHang : Form
    {
        private DataConnect data = new DataConnect();
        public FormKhachHang()
        {
            InitializeComponent();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void FormKhachHang_Load(object sender, EventArgs e)
        {
            string select = "SELECT DISTINCT kh.MaKhachHang, kh.TenKhachHang, kh.DiaChi, kh.NamSinh, kh.LuotMua " +
                "FROM tKhachHang kh JOIN tHoaDonBan hd ON hd.MaKhachHang = kh.MaKhachHang JOIN tNhanVien nv ON nv.MaNhanVien = hd.MaNhanVien " +
                "JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "'";
            dtgvKhachHang.DataSource = data.DataReader(select);
            dtgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dtgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dtgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dtgvKhachHang.Columns[3].HeaderText = "Năm sinh";
            dtgvKhachHang.Columns[4].HeaderText = "Số lần đã mua";
        }

        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            string ten = txtTenKHTimKiem.Text;
            string select = "SELECT DISTINCT kh.MaKhachHang, kh.TenKhachHang, kh.DiaChi, kh.NamSinh, kh.LuotMua " +
                "FROM tKhachHang kh JOIN tHoaDonBan hd ON hd.MaKhachHang = kh.MaKhachHang JOIN tNhanVien nv ON nv.MaNhanVien = hd.MaNhanVien " +
                "JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "' AND kh.TenKhachHang LIKE N'%"+ten+"%'";
            dtgvKhachHang.DataSource = data.DataReader(select);
            dtgvKhachHang.Columns[0].HeaderText = "Mã khách hàng";
            dtgvKhachHang.Columns[1].HeaderText = "Tên khách hàng";
            dtgvKhachHang.Columns[2].HeaderText = "Địa chỉ";
            dtgvKhachHang.Columns[3].HeaderText = "Năm sinh";
            dtgvKhachHang.Columns[4].HeaderText = "Số lần đã mua";
        }

        private void dtgvKhachHang_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Lấy mã khách hàng từ cột "MaKhachHang"
                string maKhachHang = dtgvKhachHang.Rows[e.RowIndex].Cells["MaKhachHang"].Value.ToString();

                // Truy vấn cơ sở dữ liệu để lấy tất cả các hóa đơn của khách hàng có mã khách hàng tương ứng
                string selectHoaDon = "SELECT MaHoaDon, NgayLap, TongTien FROM tHoaDonBan WHERE MaKhachHang = '" + maKhachHang + "'";
                DataTable dtHoaDon = data.DataReader(selectHoaDon);

                // Đổ dữ liệu hóa đơn vào DataGridView dtpHoaDonKH
                dtgHoaDonKH.DataSource = dtHoaDon;

                // Đặt lại tên cột nếu cần thiết
                dtgHoaDonKH.Columns[0].HeaderText = "Mã hóa đơn";
                dtgHoaDonKH.Columns[1].HeaderText = "Ngày lập";
                dtgHoaDonKH.Columns[2].HeaderText = "Tổng tiền";
                // Các cột khác

                // Tùy chỉnh hiển thị và định dạng dữ liệu trong DataGridView dtpHoaDonKH
                // Nếu cần thiết


                dtgHoaDonKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
                dtgHoaDonKH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
                dtgHoaDonKH.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
                dtgHoaDonKH.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
                dtgHoaDonKH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

                foreach (DataGridViewColumn column in dtgHoaDonKH.Columns)
                {
                    column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                    column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
                }
            }
        }

        private void dtgHoaDonKH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                string mhd = dtgHoaDonKH.Rows[e.RowIndex].Cells["MaHoaDon"].Value.ToString();
                string select = "SELECT sp.TenSanPham, ct.SoLuongBan FROM tChiTietHDB ct JOIN tSanPham sp ON sp.MaSanPham = ct.MaSanPham WHERE ct.MaHoaDon = '" + mhd + "'";

                // Xóa tất cả các mục trong ListBox trước khi thêm các sản phẩm và số lượng mới
                ltbHoaDon.Items.Clear();

                // Thực hiện truy vấn và đổ dữ liệu vào ListBox
                DataTable dt = data.DataReader(select);
                foreach (DataRow row in dt.Rows)
                {
                    string tenSanPham = row["TenSanPham"].ToString();
                    int soLuong = Convert.ToInt32(row["SoLuongBan"]);
                    string item = tenSanPham + " - Số lượng: " + soLuong;
                    ltbHoaDon.Items.Add(item);
                }
            }
        }

        private void dtgHoaDonKH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
