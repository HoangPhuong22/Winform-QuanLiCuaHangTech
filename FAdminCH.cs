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
    public partial class FAdminCH : Form
    {
        DataConnect data = new DataConnect();
        public FAdminCH()
        {
            InitializeComponent();
        }
        public void bocuc()
        {
            dtgvCH.Columns[0].HeaderText = "Tên cửa hàng";
            dtgvCH.Columns[1].HeaderText = "Địa chỉ";
            dtgvCH.Columns[2].HeaderText = "Doanh thu";
            dtgvCH.Columns[3].HeaderText = "Trạng thái";

            dtgvCH.Columns["MaCuaHang"].Visible = false;
            // Chỉnh bố cục view
            dtgvCH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvCH.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvCH.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvCH.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvCH.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvCH.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }
        void loadHDN()
        {
            dtgvHDN.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHDN.Columns[1].HeaderText = "Ngày lập";
            dtgvHDN.Columns[2].HeaderText = "Tổng tiền";

            // Chỉnh bố cục view
            dtgvHDN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvHDN.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvHDN.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvHDN.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvHDN.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvHDN.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
            decimal totalRevenue = 0;

            foreach (DataGridViewRow row in dtgvHDN.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    decimal rowTotal = Convert.ToDecimal(row.Cells["TongTien"].Value);
                    totalRevenue += rowTotal;
                }
            }
            txtTongTienNhap.Text = string.Format("{0:#,##0} vnđ", totalRevenue);
        }
        void loadHDB()
        {
            dtgvHDB.Columns[0].HeaderText = "Mã hóa đơn";
            dtgvHDB.Columns[1].HeaderText = "Ngày lập";
            dtgvHDB.Columns[2].HeaderText = "Tổng tiền";

            // Chỉnh bố cục view
            dtgvHDB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dtgvHDB.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dtgvHDB.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dtgvHDB.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dtgvHDB.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dtgvHDB.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
            decimal totalRevenue = 0;

            foreach (DataGridViewRow row in dtgvHDB.Rows)
            {
                if (row.Cells["TongTien"].Value != null)
                {
                    decimal rowTotal = Convert.ToDecimal(row.Cells["TongTien"].Value);
                    totalRevenue += rowTotal;
                }
            }
            txtTongTienBan.Text = string.Format("{0:#,##0} vnđ", totalRevenue);
        }
        private void FAdminCH_Load(object sender, EventArgs e)
        {
            string select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang";
            DataTable dt = data.DataReader(select);
            dtgvCH.DataSource = dt;
            bocuc();

            // reset nut
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            // ADd item
            cbSapXep.Items.Add("Tên");
            cbSapXep.Items.Add("Doanh thu");

            btnTKHDN.Enabled = false;
            btnTKHDB.Enabled = false;
        }

        private void dtgvCH_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        string mch = "";
        private void dtgvCH_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                string tenCH = dtgvCH.Rows[e.RowIndex].Cells["TenCuaHang"].Value.ToString();
                string DiaChi = dtgvCH.Rows[e.RowIndex].Cells["DiaChi"].Value.ToString();
                mch = dtgvCH.Rows[e.RowIndex].Cells["MaCuaHang"].Value.ToString();
                txtTenCuaHang.Text = tenCH;
                txtDiaChi.Text = DiaChi;
                btnSua.Enabled = true;
                btnXoa.Enabled=true;
                btnThem.Enabled = false;

                // HĐN
                string select = "SELECT MaHoaDonNhap, NgayLap, TongTien FROM tHoaDonNhap WHERE MaCuaHang = '" + mch + "'";
                DataTable dtHDN = data.DataReader(select);
                dtgvHDN.DataSource = dtHDN;
                loadHDN();
                btnTKHDN.Enabled = true;
                // HDB
                select = "SELECT hd.MaHoaDon, hd.NgayLap, hd.TongTien FROM tHoaDonBan hd JOIN tNhanVien nv ON nv.MaNhanVien = hd.MaNhanVien JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE ch.MaCuaHang = '" + mch + "'";
                DataTable dtHDB = data.DataReader(select);
                dtgvHDB.DataSource = dtHDB;
                loadHDB();
                btnTKHDB.Enabled = true;
                // add hdn
                if(cbSXHDN.Items.Count == 0)
                {
                    cbSXHDN.Items.Add("Ngày lập");
                    cbSXHDN.Items.Add("Tổng tiền");
                }

                // add hdb
                if (cbSXHDB.Items.Count == 0)
                {
                    cbSXHDB.Items.Add("Ngày lập");
                    cbSXHDB.Items.Add("Tổng tiền");
                }
            }
            else
            {
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = true;
                cbSXHDB.Items.Clear();
                cbSXHDN.Items.Clear();

                dtgvHDB.DataSource = null;
                dtgvHDN.DataSource = null;

            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(mch != "")
            {
                if(txtDiaChi.Text == "" ||  txtTenCuaHang.Text == "")
                {
                    MessageBox.Show("Vui lòng nhập đầy đủ thông tin");
                }
                else
                {
                    string select = "UPDATE tCuaHang SET TenCuaHang = N'" + txtTenCuaHang.Text + "', DiaChi = N'" + txtDiaChi.Text + "' WHERE MaCuaHang = '" + mch + "'";
                    data.DataChange(select);
                    // Load
                    select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang";
                    DataTable dt = data.DataReader(select);
                    dtgvCH.DataSource = dt;
                    bocuc();

                    mch = "";
                    btnSua.Enabled = false;
                    btnXoa.Enabled = false;
                    btnThem.Enabled = true;
                }
            }
        }
        public string GenerateRandomStoreCode()
        {
            Random random = new Random();
            const string characters = "0123456789";
            char[] storeCodeArray = new char[7];

            // Bắt đầu mã cửa hàng với "CH"
            storeCodeArray[0] = 'C';
            storeCodeArray[1] = 'H';

            // Tạo các số ngẫu nhiên
            for (int i = 2; i < 7; i++)
            {
                storeCodeArray[i] = characters[random.Next(characters.Length)];
            }

            return new string(storeCodeArray);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            string maCH = GenerateRandomStoreCode();
            string ten = txtTenCuaHang.Text;
            string dc = txtDiaChi.Text;
            if (string.IsNullOrEmpty(dc) || string.IsNullOrEmpty(ten))
            {
                MessageBox.Show("Vui lòng nhập đầy đủ thông tin!");
            }
            else
            {
                ten = ten.Trim();
                string select = "SELECT * FROM tCuaHang WHERE TenCuaHang = N'" + ten + "'";
                DataTable tmp = data.DataReader(select);
                if(tmp.Rows.Count > 0)
                {
                    MessageBox.Show("Tên cửa hàng đã tồn tại");
                    return;
                }
                string tt = "Đang hoạt động";
                select = "INSERT INTO tCuaHang VALUES(N'"+ten+"', N'"+dc+"','"+maCH+"', '"+0+"', N'"+tt+"')";
                data.DataChange(select);

                // Load data
                select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang";
                DataTable dt = data.DataReader(select);
                dtgvCH.DataSource = dt;
                bocuc();

                mch = "";
            }

        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string tk = txtTKCuaHang.Text.Trim();
            string select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang WHERE TenCuaHang LIKE N'%"+tk+"%'";
            DataTable dt = data.DataReader(select);
            dtgvCH.DataSource = dt;
            
            // Chỉnh bố cục view
            bocuc();

            foreach (DataGridViewColumn column in dtgvCH.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tk = cbSapXep.SelectedItem.ToString();
            string select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang WHERE TenCuaHang LIKE N'%" + txtTKCuaHang.Text.Trim() + "%'";
            string tmp = "";
            if (tk == "Tên") tmp += " ORDER BY TenCuaHang";
            else tmp += " ORDER BY DoanhThu";
            select += tmp;
            DataTable dt = data.DataReader(select);
            dtgvCH.DataSource = dt;

            // Chỉnh bố cục view
            bocuc();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang WHERE MaCuaHang = '"+mch+"'";
            DataTable dt = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn dừng kinh doanh cửa hàng "+dt.Rows[0]["TenCuaHang"].ToString()+ " không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string trangthai = "Dừng hoạt động";
                select = "UPDATE tCuaHang SET TrangThai = N'" + trangthai + "' WHERE MaCuaHang = '" + mch + "'";
                data.DataChange(select);
                select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang";
                DataTable r = data.DataReader(select);
                dtgvCH.DataSource = r;
                bocuc();
                mch = "";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = true;
            }
        }

        private void btnHien_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang WHERE MaCuaHang = '"+mch+"'";
            DataTable dt = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn mở lại kinh doanh " + dt.Rows[0]["TenCuaHang"].ToString() + " không?", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string trangthai = "Đang hoạt động";
                select = "UPDATE tCuaHang SET TrangThai = N'" + trangthai + "' WHERE MaCuaHang = '" + mch + "'";
                data.DataChange(select);
                select = "SELECT TenCuaHang, DiaChi, DoanhThu,TrangThai, MaCuaHang FROM tCuaHang";
                DataTable r = data.DataReader(select);
                dtgvCH.DataSource = r;
                bocuc();
                mch = "";
                btnSua.Enabled = false;
                btnXoa.Enabled = false;
                btnThem.Enabled = true;
            }
        }

        private void cbSXHDN_SelectedIndexChanged(object sender, EventArgs e)
        {
            string check = cbSXHDN.SelectedItem.ToString();
            if (check == "Ngày lập") dtgvHDN.Sort(dtgvHDN.Columns["NgayLap"], ListSortDirection.Ascending);
            else dtgvHDN.Sort(dtgvHDN.Columns["TongTien"], ListSortDirection.Ascending);
        }

        private void cbSXHDB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string check = cbSXHDB.SelectedItem.ToString();
            if (check == "Ngày lập") dtgvHDB.Sort(dtgvHDB.Columns["NgayLap"], ListSortDirection.Ascending);
            else dtgvHDB.Sort(dtgvHDB.Columns["TongTien"], ListSortDirection.Ascending);
        }

        private void btnTKHDN_Click(object sender, EventArgs e)
        {
            string select = "SELECT MaHoaDonNhap, NgayLap, TongTien FROM tHoaDonNhap WHERE MaCuaHang = '" + mch + "'";
            DataTable dtHDN = data.DataReader(select);

            string ngay = txtNgayHDN.Text;
            string thang = txtThangHDN.Text;
            string nam = txtNamHDN.Text;

            if (!string.IsNullOrEmpty(ngay))
            {
                select += " AND DAY(NgayLap) = " + ngay;
            }
            if (!string.IsNullOrEmpty(thang))
            {
                select += " AND MONTH(NgayLap) = " + thang;
            }
            if (!string.IsNullOrEmpty(nam))
            {
                select += " AND YEAR(NgayLap) = " + nam;
            }

            DataTable dt = data.DataReader(select);
            dtgvHDN.DataSource = dt;
            loadHDN();
            if (cbSXHDB.SelectedItem != null)
            {
                string check = cbSXHDN.SelectedItem.ToString();
                if (check == "Ngày lập") dtgvHDN.Sort(dtgvHDN.Columns["NgayLap"], ListSortDirection.Ascending);
                else dtgvHDN.Sort(dtgvHDN.Columns["TongTien"], ListSortDirection.Ascending);
            }
        }

        private void btnTKHDB_Click(object sender, EventArgs e)
        {
            string select = "SELECT hd.MaHoaDon, hd.NgayLap, hd.TongTien FROM tHoaDonBan hd JOIN tNhanVien nv ON nv.MaNhanVien = hd.MaNhanVien JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE ch.MaCuaHang = '" + mch + "'";
            string ngay = txtNgayHDB.Text;
            string thang = txtThangHDB.Text;
            string nam = txtNamHDB.Text;

            if (!string.IsNullOrEmpty(ngay))
            {
                select += " AND DAY(NgayLap) = " + ngay;
            }
            if (!string.IsNullOrEmpty(thang))
            {
                select += " AND MONTH(NgayLap) = " + thang;
            }
            if (!string.IsNullOrEmpty(nam))
            {
                select += " AND YEAR(NgayLap) = " + nam;
            }

            DataTable dtHDB = data.DataReader(select);
            dtgvHDB.DataSource = dtHDB;
            loadHDB();
            if (cbSXHDB.SelectedItem != null)
            {
                string check = cbSXHDB.SelectedItem.ToString();
                if (check == "Ngày lập") dtgvHDB.Sort(dtgvHDB.Columns["NgayLap"], ListSortDirection.Ascending);
                else dtgvHDB.Sort(dtgvHDB.Columns["TongTien"], ListSortDirection.Ascending);
            }
        }
    }
}
