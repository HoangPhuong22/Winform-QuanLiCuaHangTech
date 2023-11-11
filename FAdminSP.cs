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
    public partial class FAdminSP : Form
    {
        DataConnect data = new DataConnect();
        public FAdminSP()
        {
            InitializeComponent();
        }
        void LoadSP()
        {
            dgvSanPham.Columns[0].HeaderText = "Tên sản phẩm";
            dgvSanPham.Columns[1].HeaderText = "Tên loại";
            dgvSanPham.Columns[2].HeaderText = "Thương hiệu";
            dgvSanPham.Columns[3].HeaderText = "Đơn giá bán";
            dgvSanPham.Columns[4].HeaderText = "Đơn giá nhập";
            dgvSanPham.Columns[5].HeaderText = "Trạng thái";
            dgvSanPham.Columns["MaSanPham"].Visible = false;

            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.DisplayedCells;
            dgvSanPham.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.DisplayedCells;
            dgvSanPham.ColumnHeadersDefaultCellStyle.WrapMode = DataGridViewTriState.True;
            dgvSanPham.AutoResizeRowHeadersWidth(DataGridViewRowHeadersWidthSizeMode.AutoSizeToAllHeaders);
            dgvSanPham.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;

            foreach (DataGridViewColumn column in dgvSanPham.Columns)
            {
                column.HeaderText = column.HeaderText.Replace("\r\n", " "); // Loại bỏ các ký tự xuống dòng trong tiêu đề cột
                column.AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells; // Đặt kích thước của header cột dựa trên nội dung
            }
        }
        void LoadLSP()
        {
            string select = "SELECT TenLoai FROM tLoaiSanPham";
            DataTable dt = data.DataReader(select);
            foreach(DataRow row in dt.Rows)
            {
                cbLSP.Items.Add(row["TenLoai"].ToString());
            }
        }
        void LoadTH()
        {
            string select = "SELECT TenThuongHieu FROM tThuongHieu";
            DataTable dt = data.DataReader(select);
            foreach (DataRow row in dt.Rows)
            {
                cbTH.Items.Add(row["TenThuongHieu"].ToString());
            }
        }
        void LoadNCC()
        {
            string select = "SELECT TenNhaCungCap FROM tNhaCungCap";
            DataTable dt = data.DataReader(select);
            foreach (DataRow row in dt.Rows)
            {
                cbNCC.Items.Add(row["TenNhaCungCap"].ToString());
            }
        }
        private void FAdminSP_Load(object sender, EventArgs e)
        {
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu";
            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            LoadSP();
            btnXoa.Enabled = false;
            btnKhoiPhuc.Enabled = false;
            cbSX.Items.Add("Đơn giá bán");
            cbSX.Items.Add("Đơn giá nhập");
            cbSX.Items.Add("Tên sản phẩm");


            // Load Cua Hang
            // Xóa các mục cũ trong ComboBox trước khi thêm mục mới
            cbCuaHang.Items.Clear();

            // Thực hiện truy vấn SQL để lấy danh sách cửa hàng từ cơ sở dữ liệu
            string query = "SELECT TenCuaHang FROM tCuaHang";

            // Thực hiện truy vấn và đổ kết quả vào DataTable
            DataTable ch = data.DataReader(query);

            // Kiểm tra xem có dữ liệu không trước khi thêm vào ComboBox
            if (ch != null && ch.Rows.Count > 0)
            {
                foreach (DataRow row in ch.Rows)
                {
                    // Lấy giá trị từ cột "TenCuaHang" và thêm vào ComboBox
                    string tenCuaHang = row["TenCuaHang"].ToString();
                    cbCuaHang.Items.Add(tenCuaHang);
                }
            }



            // Load Cửa Hang 2

            cbCH.Items.Clear();
            // Kiểm tra xem có dữ liệu không trước khi thêm vào ComboBox
            if (ch != null && ch.Rows.Count > 0)
            {
                foreach (DataRow row in ch.Rows)
                {
                    // Lấy giá trị từ cột "TenCuaHang" và thêm vào ComboBox
                    string tenCuaHang = row["TenCuaHang"].ToString();
                    cbCH.Items.Add(tenCuaHang);
                }
            }
            LoadLSP();
            LoadTH();
            LoadNCC();
        }
        string msp = "";
        private void dgvSanPham_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                btnXoa.Enabled = true;
                btnKhoiPhuc.Enabled = true;

                btnSua.Enabled = true;
                msp = dgvSanPham.Rows[e.RowIndex].Cells["MaSanPham"].Value.ToString();
                txtTenSanPham.Text = dgvSanPham.Rows[e.RowIndex].Cells["TenSanPham"].Value.ToString();
            }
            else
            {
                msp = "";
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnSua.Enabled = false;
                txtTenSanPham.Text = "";
            }    
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn ẩn sản phẩm ", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Ẩn";
                string select = "UPDATE tSanPham SET TrangThai = N'" + tt + "' WHERE MaSanPham = '" + msp + "'";
                data.DataChange(select);

                msp = "";

                 select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu";
                DataTable dt = data.DataReader(select);
                dgvSanPham.DataSource = dt;
                LoadSP();
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
            }    
        }

        private void btnKhoiPhuc_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn mở kinh donah lại sản phẩm này", "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Hiện";
                string select = "UPDATE tSanPham SET TrangThai = N'" + tt + "' WHERE MaSanPham = '" + msp + "'";
                data.DataChange(select);
                msp = "";

                 select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu";
                DataTable dt = data.DataReader(select);
                dgvSanPham.DataSource = dt;
                LoadSP();
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnSua.Enabled = false;
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtTenSanPham.Text))
            {
                MessageBox.Show("Nhập sản phẩm hợp lệ!");
            }
            else
            {
                string select = "UPDATE tSanPham SET TenSanPham = N'" + txtTenSanPham.Text.Trim() + "' WHERE MaSanPham = '" + msp + "'";
                data.DataChange(select);
                select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu";
                DataTable dt = data.DataReader(select);
                dgvSanPham.DataSource = dt;
                LoadSP();
                btnXoa.Enabled = false;
                btnKhoiPhuc.Enabled = false;
                btnSua.Enabled = false;
            }
        }

        private void btnTK_Click(object sender, EventArgs e)
        {
            string ten = txtTKSanPham.Text.Trim();
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE sp.TenSanPham LIKE N'%"+ten+"%'";
            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            LoadSP();
        }

        private void guna2ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string selectedSortColumn = cbSX.SelectedItem.ToString();

            // Kiểm tra xem giá trị được chọn là gì và sắp xếp tương ứng
            if (dgvSanPham.DataSource != null && dgvSanPham.DataSource is DataTable)
            {
                DataTable dt = (DataTable)dgvSanPham.DataSource;

                switch (selectedSortColumn)
                {
                    case "Đơn giá bán":
                        dt.DefaultView.Sort = "DonGiaBan ASC";
                        break;

                    case "Đơn giá nhập":
                        dt.DefaultView.Sort = "DonGiaNhap ASC";
                        break;

                    case "Tên sản phẩm":
                        dt.DefaultView.Sort = "TenSanPham ASC";
                        break;

                    // Thêm các trường sắp xếp khác nếu cần
                    // ...

                    default:
                        // Đặt lại sắp xếp nếu không có sự chọn hợp lệ
                        dt.DefaultView.Sort = string.Empty;
                        break;
                }
            }
        }

        private void cbSapXep_SelectedIndexChanged(object sender, EventArgs e)
        {
            string mch = cbCuaHang.SelectedItem.ToString();
            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu, sp.DonGiaBan, sp.DonGiaNhap, sp.TrangThai, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham JOIN tHoaDonNhap hd ON hd.MaHoaDonNhap = ct.MaHoaDonNhap JOIN tCuaHang ch ON ch.MaCuaHang = hd.MaCuaHang WHERE ch.TenCuaHang = N'"+mch+"'";
            DataTable dt = data.DataReader(select);
            dgvSanPham.DataSource = dt;
            LoadSP();
        }
        static string SinhMaHoaDonNhap()
        {
            // Bắt đầu với chuỗi cố định "HDN"
            string maHoaDonNhap = "HDN";

            // Thêm ngẫu nhiên 5 chữ cái
            maHoaDonNhap += SinhChuoiNgauNhien(5);

            // Thêm ngày và giờ hiện tại
            maHoaDonNhap += DateTime.Now.ToString("yyMMddHHmmss");

            return maHoaDonNhap;
        }

        static string SinhChuoiNgauNhien(int doDai)
        {
            // Chuỗi ký tự cho phép
            const string chuoiKyTu = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            // Ngẫu nhiên chọn ký tự từ chuỗi ký tự
            Random random = new Random();
            char[] chuoiNgauNhien = new char[doDai];
            for (int i = 0; i < doDai; i++)
            {
                chuoiNgauNhien[i] = chuoiKyTu[random.Next(chuoiKyTu.Length)];
            }

            return new string(chuoiNgauNhien);
        }
        static string SinhMaSanPham()
        {
            return $"SP{Guid.NewGuid().ToString().Substring(0, 5)}";
        }
        string mhdn = "";
        int cthdn = 0;
        string SinhMaChiTietHoaDonNhap()
        {
            return "CTN" + cthdn.ToString() + mhdn;
        }
        private void btnMua_Click(object sender, EventArgs e)
        {
           
            if(cbCH.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn cửa hàng để nhập hàng!");
                return;
            }
            if (cbLSP.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn loại sản phẩm để nhập hàng!");
                return;
            }
            if (cbNCC.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn nhà cung cấp để nhập hàng!");
                return;
            }
            if (cbTH.SelectedItem == null)
            {
                MessageBox.Show("Vui lòng chọn thương hiệu để nhập hàng!");
                return;
            }
            if(string.IsNullOrEmpty(txtTSP.Text))
            {
                MessageBox.Show("Tên sản phẩm không hợp lệ!");
                return;
            }
            if(string.IsNullOrEmpty(txtDonGiaBan.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá bán"); return;
            }
            if (string.IsNullOrEmpty(txtDonGiaNhap.Text))
            {
                MessageBox.Show("Vui lòng nhập đơn giá nhập"); return;
            }
            if(nmSLN.Value == 0)
            {
                MessageBox.Show("Vui lòng chọn số lượng nhập > 0"); return;
            }    
            decimal dongiaban = Convert.ToDecimal(txtDonGiaBan.Text);
            decimal dongianhap = Convert.ToDecimal(txtDonGiaNhap.Text);
            if(dongiaban <= dongianhap)
            {
                MessageBox.Show("Đơn giá bán phải lớn hơn đơn giá nhập!");
                return;
            }
            if(mhdn == "")
            {
                mhdn = SinhMaHoaDonNhap();
                string ch = cbCH.SelectedItem.ToString();
                string check = "SELECT MaCuaHang FROM tCuaHang WHERE TenCuaHang = N'" + ch + "'";
                DataTable h = data.DataReader(check);
                string ncc = cbNCC.SelectedItem.ToString();
                string tmpncc = "SELECT MaNhaCungCap FROM tNhaCungCap WHERE TenNhaCungCap = N'" + ncc + "'";
                DataTable n = data.DataReader(tmpncc);
                string st = "INSERT INTO tHoaDonNhap VALUES('" + mhdn + "', '" + 0 + "', '" + DateTime.Now.ToString("yyyy/MM/dd") + "', '" + n.Rows[0]["MaNhaCungCap"].ToString() + "', '" + h.Rows[0]["MaCuaHang"].ToString() + "')";
                data.DataChange(st);
            }
            cthdn++;
            string msp = SinhMaSanPham();
            string tmp = "SELECT MaLoai FROM tLoaiSanPham WHERE TenLoai = N'" + cbLSP.SelectedItem.ToString() + "'";
            DataTable lsp = data.DataReader(tmp);
            tmp = "SELECT MaThuongHieu FROM tThuongHieu WHERE TenThuongHieu = N'" + cbTH.SelectedItem.ToString() + "'"; string tt = "Hiện";
            DataTable lth = data.DataReader(tmp);
            string tsp = "INSERT INTO tSanPham VALUES('"+dongiaban+"', '"+dongianhap+"', N'"+txtTSP.Text+"', '"+msp+"', '" + lsp.Rows[0]["MaLoai"].ToString() +"', '"+lth.Rows[0]["MaThuongHieu"].ToString()+"',N'" + tt + "')";
            data.DataChange(tsp);

            string mct = SinhMaChiTietHoaDonNhap();
            int sl = (int)nmSLN.Value;
            string ct = "INSERT INTO tChiTietHDN VALUES('" + mct + "', '" + sl + "', '" + msp + "', '" + mhdn + "','" + sl * dongianhap + "' )";
            data.DataChange(ct);

            string select = "SELECT sp.TenSanPham, l.TenLoai, th.TenThuongHieu,ct.TongTien, sp.MaSanPham FROM tSanPham sp JOIn tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham WHERE ct.MaHoaDonNhap = '"+mhdn+"'";
            DataTable dt = data.DataReader(select);
            dtgvHoaDon.DataSource = dt;
            LoadHoaDon();
            cbNCC.Enabled = false;
            cbCuaHang.Enabled = false;
        }
        void LoadHoaDon()
        {
            dtgvHoaDon.Columns[0].HeaderText = "Tên sản phẩm";
            dtgvHoaDon.Columns[1].HeaderText = "Tên loại";
            dtgvHoaDon.Columns[2].HeaderText = "Thương hiệu";
            dtgvHoaDon.Columns[3].HeaderText = "Tổng tiền";
            dtgvHoaDon.Columns["MaSanPham"].Visible = false;
        }

        private void nmSLN_ValueChanged(object sender, EventArgs e)
        {
            txtTongTienNhap.Text = (nmSLN.Value * Convert.ToDecimal(txtDonGiaNhap.Text)).ToString();
        }
        public static string MHD = "";
        public static string NCC = "";

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            MHD = mhdn;
            mhdn = "";

            NCC = cbNCC.SelectedItem.ToString();
            cbNCC.Enabled = true;
            cbCuaHang.Enabled = true;

            FXuatHDN f = new FXuatHDN();
            f.ShowDialog();
        }
    }
}
