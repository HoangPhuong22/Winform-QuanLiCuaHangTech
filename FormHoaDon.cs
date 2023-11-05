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
    public partial class FormHoaDon : Form
    {
        DataConnect data = new DataConnect();
        public FormHoaDon()
        {
            InitializeComponent();
        }

        private void FormHoaDon_Load(object sender, EventArgs e)
        {
            string select = "SELECT hd.MaHoaDon, hd.NgayLap, kh.TenKhachHang, nv.TenNhanVien, hd.TongTien " +
                "FROM tCuaHang ch JOIN tNhanVien nv ON nv.MaCuaHang = ch.MaCuaHang JOIN tHoaDonBan hd ON hd.MaNhanVien = nv.MaNhanVien " +
                "JOIN tKhachHang kh ON kh.MaKhachHang = hd.MaKhachHang WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "'";
            DataTable dt = data.DataReader(select);
            dtgvHoaDon.DataSource = dt;
            dtgvHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHoaDon.Columns[1].HeaderText = "Ngày lập";
            dtgvHoaDon.Columns[2].HeaderText = "Tên khách hàng";
            dtgvHoaDon.Columns[3].HeaderText = "Tên nhân viên";
            dtgvHoaDon.Columns[4].HeaderText = "Tổng tiền";

            decimal totalRevenue = 0;

            foreach (DataGridViewRow row in dtgvHoaDon.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    decimal rowTotal = Convert.ToDecimal(row.Cells["TongTien"].Value);
                    totalRevenue += rowTotal;
                }
            }
            txtDoanhThu.Text = string.Format("{0:#,##0} vnđ", totalRevenue);

        }

        private void guna2TextBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string ngay = txtNgay.Text;
            string thang = txtThang.Text;
            string nam = txtNam.Text;

            string select = "SELECT hd.MaHoaDon, hd.NgayLap, kh.TenKhachHang, nv.TenNhanVien, hd.TongTien " +
               "FROM tCuaHang ch JOIN tNhanVien nv ON nv.MaCuaHang = ch.MaCuaHang JOIN tHoaDonBan hd ON hd.MaNhanVien = nv.MaNhanVien " +
               "JOIN tKhachHang kh ON kh.MaKhachHang = hd.MaKhachHang WHERE ch.MaCuaHang = '" + FormLogin.MaCH + "' AND";

            string tmp = "";
            if(ngay != "")
            {
                tmp += " DAY(hd.Ngaylap) = '" + ngay + "' AND";
            }
            if(thang != "")
            {
                tmp+= " MONTH(hd.Ngaylap) = '" + thang + "' AND";
            }
            if (nam != "")
            {
                tmp += " YEAR(hd.Ngaylap) = '" + nam + "' AND";
            }
            select += tmp;
            select = select.Substring(0, select.Length - 3);
            DataTable dt = data.DataReader(select);
            dtgvHoaDon.DataSource = dt;
            dtgvHoaDon.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHoaDon.Columns[1].HeaderText = "Ngày lập";
            dtgvHoaDon.Columns[2].HeaderText = "Tên khách hàng";
            dtgvHoaDon.Columns[3].HeaderText = "Tên nhân viên";
            dtgvHoaDon.Columns[4].HeaderText = "Tổng tiền";

            decimal totalRevenue = 0;

            foreach (DataGridViewRow row in dtgvHoaDon.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    decimal rowTotal = Convert.ToDecimal(row.Cells["TongTien"].Value);
                    totalRevenue += rowTotal;
                }
            }
            txtDoanhThu.Text = string.Format("{0:#,##0} vnđ", totalRevenue);
        }
    }
}
