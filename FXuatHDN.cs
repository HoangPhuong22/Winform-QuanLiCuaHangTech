using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FXuatHDN : Form
    {
        DataConnect dataConnect = new DataConnect();
        public FXuatHDN()
        {
            InitializeComponent();
        }

        private void printDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Vẽ hình ảnh
            e.Graphics.DrawImage(memoryimg, 0, 0);
        }
        private void Print(Panel pn1)
        {
            PrinterSettings ps = new PrinterSettings();
            panelIn = pn1;

            // Lấy kích thước giấy in
            PaperSize paperSize = new PaperSize("Custom", pn1.Width, ps.DefaultPageSettings.PaperSize.Height);

            // Đặt lại kích thước giấy in
            printDocument.DefaultPageSettings.PaperSize = paperSize;

            getprintarea(pn1);
            printPreviewDialog.Document = printDocument;
            printDocument.PrintPage += new PrintPageEventHandler(printDocument_PrintPage);
            printPreviewDialog.ShowDialog();
        }

        private Bitmap memoryimg;

        private void getprintarea(Panel pn1)
        {
            memoryimg = new Bitmap(pn1.Width, pn1.Height);
            pn1.DrawToBitmap(memoryimg, new Rectangle(0, 0, pn1.Width, pn1.Height));
        }

        private void FXuatHDN_Load(object sender, EventArgs e)
        {
            txtTenCuaHang.Text = FormLogin.TenCuahang;
            txtSDT.Text = FormLogin.SDT;
            txtTenNhanVien.Text = FormLogin.TenNhanVien;
            lbKiTen.Text = FormLogin.TenNhanVien;
            // Gán data vào đơn hàng
            txtNCC.Text = FAdminSP.NCC;
            string select = "SELECT sp.TenSanPham , l.TenLoai , th.TenThuongHieu, ct.SoLuongNhap, sp.DonGiaNhap, ct.TongTien FROM tSanPham sp JOIN tChiTietHDN ct ON ct.MaSanPham = sp.MaSanPham JOIN tLoaiSanPham l ON l.MaLoai = sp.MaLoai JOIN tThuongHieu th ON th.MaThuongHieu = sp.MaThuongHieu WHERE ct.MaHoaDonNhap = '" + FAdminSP.MHD + "'";
            DataTable donhang = dataConnect.DataReader(select);
            dtgvHoaDon.DataSource = donhang;

            dtgvHoaDon.Columns[0].HeaderText = "Sản phẩm";
            dtgvHoaDon.Columns[1].HeaderText = "Loại";
            dtgvHoaDon.Columns[2].HeaderText = "Hãng";
            dtgvHoaDon.Columns[3].HeaderText = "Số lượng";
            dtgvHoaDon.Columns[4].HeaderText = "Đơn giá";
            dtgvHoaDon.Columns[5].HeaderText = "Tổng tiền";
            // Cập nhật tổng tiền 

            decimal tongTien = 0;

            foreach (DataGridViewRow row in dtgvHoaDon.Rows)
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
            txtTongTIen.Text = string.Format("{0:#,###}", tongTien) + "vnđ";
            txtNgayLap.Text = DateTime.Now.ToString();
        }

        private void btnIn_Click(object sender, EventArgs e)
        {
            Print(this.panelIn);
        }
    }
}
