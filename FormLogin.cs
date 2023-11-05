using QLCuaHangBanDoCongNGhe.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLCuaHangBanDoCongNGhe
{
    public partial class FormLogin : Form
    {
        private DataConnect data = new DataConnect();
        public FormLogin()
        {
            InitializeComponent();
        }
        public static string TaiKhoan { get; set; }
        public static string TenCuahang { get; set; }
        public static string MaNhanVien { get; set; }
        public static string DiaChiCH { get; set; }
        public static string MaCH { get; set; }
        private void guna2HtmlLabel2_Click(object sender, EventArgs e)
        {

        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            try
            {
                TenCuahang = cbCuaHang.SelectedItem.ToString();
                TaiKhoan = txtTaiKhoan.Text;
                string MatKhau = txtMatKhau.Text;
                string select = "SELECT ch.MaCuaHang as MaCuaHang ,ch.DiaChi as DiaChi, nv.MaNhanVien as MaNhanVien FROM tTaiKhoan tk JOIN tNhanVien nv ON nv.UserName = tk.UserName JOIN tCuaHang ch ON ch.MaCuaHang = nv.MaCuaHang WHERE ch.TenCuahang = N'" + TenCuahang + "' AND tk.UserName = N'" + TaiKhoan + "' AND tk.PassWord = '" + MatKhau + "'";

                DataTable result = data.DataReader(select);
                if (result.Rows.Count > 0)
                {
                    MaCH = result.Rows[0]["MaCuaHang"].ToString();
                    DiaChiCH = result.Rows[0]["DiaChi"].ToString();
                    MaNhanVien = result.Rows[0]["MaNhanVien"].ToString();
                    FormMain form = new FormMain();
                    form.ShowDialog();
                }
                else
                {
                    lbValidate.Text = "Vui lòng nhập tài khoản và mật khẩu chính xác!";
                }
            }
            catch
            {
                lbValidate.Text = "Vui lòng chọn cửa hàng!";
            }
        }

        private void btnThoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void FormLogin_Load(object sender, EventArgs e)
        {
            string select = "SELECT * FROM tCuaHang";
            DataTable result = data.DataReader(select);
            foreach(DataRow row in result.Rows)
            {
                cbCuaHang.Items.Add(row["TenCuaHang"]);
            }
        }
    }
}
