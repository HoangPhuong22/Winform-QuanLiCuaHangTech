using Guna.UI2.WinForms;
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
    public partial class FormSanPham : Form
    {
        private DataConnect data = new DataConnect();

        public FormSanPham()
        {
            InitializeComponent();
        }


        private void guna2CircleButton2_Click(object sender, EventArgs e)
        {
            string ten = txtTKTEN.Text;
            ten = ten.Trim();
            decimal gia;
            try
            {
                gia = Convert.ToDecimal(txtTKGia.Text);
            }
            catch
            {
                gia = decimal.MaxValue;
            }
            string thuonghieu = txtTKTH.Text;
            string loaisp = txtTKLoai.Text;

            // Sử dụng tham số truy vấn SQL để tránh lỗ hổng SQL Injection
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap " +
                           "FROM tSanPham sp " +
                           "JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai " +
                           "JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu " +
                           "JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham " +
                           "JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap " +
                           "JOIN tCuaHang ch ON ch.MaCuaHang = hd.MaCuaHang " +
                           "WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "' " +
                           "AND sp.DonGiaBan <= " + gia + " " +
                           "AND sp.TenSanPham LIKE N'%" + ten + "%' " +
                           "AND l.TenLoai LIKE N'%" + loaisp + "%' " +
                           "AND th.TenThuongHieu LIKE N'%" + thuonghieu + "%'";

            string sx = "";
            if (cbSX.SelectedItem != null)
            {
                string check = cbSX.SelectedItem.ToString();
                if (check == "Đơn giá bán") sx = "ORDER BY sp.DonGiaBan";
                else if (check == "Đơn giá nhập") sx = "ORDER BY sp.DonGiaNhap";
                else if (check == "Tên sản phẩm") sx = "ORDER BY sp.TenSanPham";
                else if (check == "Tên thương hiệu") sx = "ORDER BY th.TenThuongHieu";
                else if (check == "Tên loại sản phẩm") sx = "ORDER BY l.TenLoai";
            }
            select += " " + sx;

            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            dgvSanPham.Columns[0].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Loại sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Thương hiệu";
            dgvSanPham.Columns[3].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[4].HeaderText = "Đơn giá nhập";
        }



        private void guna2DataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void FormSanPham_Load(object sender, EventArgs e)
        {
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap FROM tSanPham sp JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap JOIN tCuaHang ch ON ch.MaCuaHang = hd.MaCuaHang WHERE ch.MaCuaHang = '"+FormLogin.MaCH+"'";
            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            dgvSanPham.Columns[0].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Loại sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Thương hiệu";
            dgvSanPham.Columns[3].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[4].HeaderText = "Đơn giá nhập";

            cbSX.Items.Add("Đơn giá bán");
            cbSX.Items.Add("Đơn giá nhập");
            cbSX.Items.Add("Tên sản phẩm");
            cbSX.Items.Add("Tên thương hiệu");
            cbSX.Items.Add("Tên loại sản phẩm");
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string ten = txtTKTEN.Text;
            ten = ten.Trim();
            decimal gia;
            try
            {
                gia = Convert.ToDecimal(txtTKGia.Text);
            }
            catch
            {
                gia = decimal.MaxValue;
            }
            string thuonghieu = txtTKTH.Text;
            string loaisp = txtTKLoai.Text;

            // Sử dụng tham số truy vấn SQL để tránh lỗ hổng SQL Injection
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap " +
                           "FROM tSanPham sp " +
                           "JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai " +
                           "JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu " +
                           "JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham " +
                           "JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap " +
                           "JOIN tCuaHang ch ON ch.MaCuaHang = hd.MaCuaHang " +
                           "WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "' " +
                           "AND sp.DonGiaBan <= " + gia + " " +
                           "AND sp.TenSanPham LIKE N'%" + ten + "%' " +
                           "AND l.TenLoai LIKE N'%" + loaisp + "%' " +
                           "AND th.TenThuongHieu LIKE N'%" + thuonghieu + "%'";

            string sx = "";
            if (cbSX.SelectedItem != null)
            {
                string check = cbSX.SelectedItem.ToString();
                if (check == "Đơn giá bán") sx = "ORDER BY sp.DonGiaBan";
                else if (check == "Đơn giá nhập") sx = "ORDER BY sp.DonGiaNhap";
                else if (check == "Tên sản phẩm") sx = "ORDER BY sp.TenSanPham";
                else if (check == "Tên thương hiệu") sx = "ORDER BY th.TenThuongHieu";
                else if (check == "Tên loại sản phẩm") sx = "ORDER BY l.TenLoai";
            }
            select += " " + sx;

            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            dgvSanPham.Columns[0].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Loại sản phẩm";
            dgvSanPham.Columns[2].HeaderText = "Thương hiệu";
            dgvSanPham.Columns[3].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[4].HeaderText = "Đơn giá nhập";
        }

        private void txtTKLoai_TextChanged(object sender, EventArgs e)
        {

        }

        private void dgvSanPham_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow selectedRow = dgvSanPham.Rows[e.RowIndex];

                string tenSanPham = selectedRow.Cells["TenSanPham"].Value.ToString();
               
                string thuongHieu = selectedRow.Cells["TenThuongHieu"].Value.ToString();
                decimal donGiaBan = Convert.ToDecimal(selectedRow.Cells["DonGiaBan"].Value);
                decimal donGiaNhap = Convert.ToDecimal(selectedRow.Cells["DonGiaNhap"].Value);

                // Gán giá trị cho các TextBox tương ứng
                txtTenSanPham.Text = tenSanPham;

                txtThuongHieu.Text = thuongHieu;
                txtDonGiaBan.Text = donGiaBan.ToString();
                txtDonGiaNhap.Text = donGiaNhap.ToString();
            }
        }


        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtTenSanPham_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void txtTenSP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
