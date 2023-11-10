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
    public partial class FAdminLSPTH : Form
    {
        DataConnect data = new DataConnect();
        public FAdminLSPTH()
        {
            InitializeComponent();
        }
        void loadLSP()
        {

            dtgvLoaiSP.Columns[0].HeaderText = "Loại sản phẩm";
            dtgvLoaiSP.Columns[1].HeaderText = "Trạng thái";
            dtgvLoaiSP.Columns[2].HeaderText = "Mã loại sản phẩm";
        }
        void loadTH()
        {

            dtgvThuongHieu.Columns[0].HeaderText = "Thương hiệu";
            dtgvThuongHieu.Columns[1].HeaderText = "Trạng thái";
            dtgvThuongHieu.Columns[2].HeaderText = "Mã thương hiệu";
        }
        private void FAdminLSPTH_Load(object sender, EventArgs e)
        {
            string select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham";
            DataTable stlsp = data.DataReader(select);
            dtgvLoaiSP.DataSource = stlsp;

            select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
            DataTable dtth = data.DataReader(select);
            dtgvThuongHieu.DataSource = dtth;

            // An cac nut
            btnSuaLSP.Enabled = false;
            btnXoaLSP.Enabled = false;
            btnMoLSP.Enabled = false;

            // An cac nut thuong hieu
            btnSuaTH.Enabled = false;
            btnXoaTH.Enabled = false;
            btnMoTH.Enabled = false;
            loadLSP();
            loadTH();
        }
        string lsp = "";
        private void dtgvLoaiSP_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSuaLSP.Enabled = true;
                btnXoaLSP.Enabled = true;
                btnMoLSP.Enabled = true;
                btnThemLSP.Enabled = false;
                lsp = dtgvLoaiSP.Rows[e.RowIndex].Cells["MaLoai"].Value.ToString();
                txtLSP.Text = dtgvLoaiSP.Rows[e.RowIndex].Cells["TenLoai"].Value.ToString();
            }
            else
            {
                lsp = "";
                btnSuaLSP.Enabled = false;
                btnXoaLSP.Enabled = false;
                btnMoLSP.Enabled = false;
                btnThemLSP.Enabled = true;
            }
        }

        private void btnXoaLSP_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham WHERE MaLoai = '"+lsp+"'";
            DataTable dtlsp = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn xóa loại sản phẩm "+ dtlsp.Rows[0]["TenLoai"], "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Ẩn";
                select = "UPDATE tLoaiSanPham SET TrangThai = N'" + tt + "' WHERE MaLoai = '" + lsp + "'";
                data.DataChange(select);
                lsp = "";

                select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham";
                DataTable stlsp = data.DataReader(select);
                dtgvLoaiSP.DataSource = stlsp;

                select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
                DataTable dtth = data.DataReader(select);
                dtgvThuongHieu.DataSource = dtth;
                loadLSP();
                loadTH();
            }
        }

        private void btnMoLSP_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham WHERE MaLoai = '" + lsp + "'";
            DataTable dtlsp = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn mở bán lại loại sản phẩm " + dtlsp.Rows[0]["TenLoai"], "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Hiện";
                select = "UPDATE tLoaiSanPham SET TrangThai = N'" + tt + "' WHERE MaLoai = '" + lsp + "'";
                lsp = "";
                data.DataChange(select);

                select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham";
                DataTable stlsp = data.DataReader(select);
                dtgvLoaiSP.DataSource = stlsp;

                loadLSP();
            }
        }
        string th = "";
        private void dtgvThuongHieu_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                btnSuaTH.Enabled = true;
                btnXoaTH.Enabled = true;
                btnMoTH.Enabled = true;
                btnThemTH.Enabled = false;
                th = dtgvThuongHieu.Rows[e.RowIndex].Cells["MaThuongHieu"].Value.ToString();
                txtTenThuongHieu.Text = dtgvThuongHieu.Rows[e.RowIndex].Cells["TenThuongHieu"].Value.ToString();
            }
            else
            {
                th = "";
                btnSuaTH.Enabled = false;
                btnXoaTH.Enabled = false;
                btnMoTH.Enabled = false;
                btnThemTH.Enabled = true;
            }
        }

        private void btnXoaTH_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu WHERE MaThuongHieu = '" + th + "'";
            DataTable dtTH = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn xóa thương hiệu " + dtTH.Rows[0]["TenThuongHieu"], "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Ẩn";
                select = "UPDATE tThuongHieu SET TrangThai = N'" + tt + "' WHERE MaThuongHieu = '" + th + "'";
                data.DataChange(select);
                th = "";

                select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
                DataTable dtth = data.DataReader(select);
                dtgvThuongHieu.DataSource = dtth;
                loadTH();
            }
        }

        private void btnMoTH_Click(object sender, EventArgs e)
        {
            string select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu WHERE MaThuongHieu = '" + th + "'";
            DataTable dtTH = data.DataReader(select);
            DialogResult result = MessageBox.Show("Bạn có muốn mở bán lại thương hiệu " + dtTH.Rows[0]["TenThuongHieu"], "Xác nhận", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                string tt = "Hiện";
                select = "UPDATE tThuongHieu SET TrangThai = N'" + tt + "' WHERE MaThuongHieu = '" + th + "'";
                data.DataChange(select);
                th = "";

                select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
                DataTable dtth = data.DataReader(select);
                dtgvThuongHieu.DataSource = dtth;
                loadTH();
            }
        }

        private void btnSuaLSP_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtLSP.Text))
            {
                MessageBox.Show("Nhập đúng thông tin tên loại sản phẩm!");
                return;
            }
            string select = "UPDATE tLoaiSanPham SET TenLoai = N'" + txtLSP.Text + "' WHERE MaLoai = '" + lsp + "'";
            data.DataChange (select);
            select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham";
            DataTable stlsp = data.DataReader(select);
            dtgvLoaiSP.DataSource = stlsp;

            loadLSP();
        }

        private void btnSuaTH_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtTenThuongHieu.Text))
            {
                MessageBox.Show("Vui lòng nhập tên thương hiệu hợp lệ");
                return;
            }
            string select = "UPDATE tThuongHieu SET TenThuongHieu = N'" + txtTenThuongHieu.Text + "' WHERE MaThuongHieu = '" + th + "'";
            data.DataChange(select);
            select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
            DataTable dtth = data.DataReader(select);
            dtgvThuongHieu.DataSource = dtth;
            loadTH();
        }
        public string RandomMLSP()
        {
            Random random = new Random();
            const string characters = "0123456789";
            char[] storeCodeArray = new char[7];

            // Bắt đầu mã cửa hàng với "CH"
            storeCodeArray[0] = 'M';
            storeCodeArray[1] = 'L';

            // Tạo các số ngẫu nhiên
            for (int i = 2; i < 7; i++)
            {
                storeCodeArray[i] = characters[random.Next(characters.Length)];
            }

            return new string(storeCodeArray);
        }
        public string RandomMTH()
        {
            Random random = new Random();
            const string characters = "0123456789";
            char[] storeCodeArray = new char[7];

            // Bắt đầu mã cửa hàng với "CH"
            storeCodeArray[0] = 'T';
            storeCodeArray[1] = 'H';

            // Tạo các số ngẫu nhiên
            for (int i = 2; i < 7; i++)
            {
                storeCodeArray[i] = characters[random.Next(characters.Length)];
            }

            return new string(storeCodeArray);
        }
        private void btnThemLSP_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtLSP.Text))
            {
                MessageBox.Show("Nhập tên loại sản phẩm hợp lệ!");
                return;
            }
            string select = "SELECT * FROM tLoaiSanPham WHERE TenLoai = N'" + txtLSP.Text + "'";
            DataTable dt = data.DataReader(select);
            if(dt.Rows.Count > 0)
            {
                MessageBox.Show("Tên loại sản phẩm đã tồn tại !");
                return;
            }
            string msp = RandomMLSP();
            string tt = "Hiện";

            select = "INSERT INTO tLoaiSanPham VALUES(N'" +txtLSP.Text+ "', '" + msp + "', N'" + tt + "')";
            data.DataChange(select);
            select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham";
            DataTable stlsp = data.DataReader(select);
            dtgvLoaiSP.DataSource = stlsp;

            loadLSP();
        }

        private void btnThemTH_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtTenThuongHieu.Text))
            {
                MessageBox.Show("Nhập tên thương hiệu hợp lệ!");
                return;
            }
            string select = "SELECt * FROM tThuongHieu WHERE TenThuongHieu = N'" + txtTenThuongHieu.Text + "'";
            DataTable check = data.DataReader(select);
            if(check.Rows.Count > 0)
            {
                MessageBox.Show("Tên thương hiệu đã tồn tại !");
                return;
            }
            string mth = RandomMTH();
            string tt = "Hiện";
            select = "INSERT INTO tThuongHieu VALUES(N'" + txtTenThuongHieu.Text + "', '" +mth + "', N'" + tt + "')";
            data.DataChange(select);
            select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu";
            DataTable dtth = data.DataReader(select);
            dtgvThuongHieu.DataSource = dtth;
            loadTH();
        }

        private void btnTKLSP_Click(object sender, EventArgs e)
        {
            string tenlsp = txtTKLSP.Text.Trim();
            string select = "SELECT TenLoai, TrangThai, MaLoai FROM tLoaiSanPham WHERE TenLoai LIKE N'%" + tenlsp + "%'";
            DataTable stlsp = data.DataReader(select);
            dtgvLoaiSP.DataSource = stlsp;

            loadLSP();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            string tenth = txtTKTH.Text.Trim();
            string select = "SELECT TenThuongHieu, TrangThai, MaThuongHieu FROM tThuongHieu WHERE TenThuongHieu LIKE N'%"+tenth+"%'";
            DataTable dtth = data.DataReader(select);
            dtgvThuongHieu.DataSource = dtth;
            loadTH();
        }
    }
}
