using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QLCuaHangBanDoCongNGhe.Data
{
    internal class DataConnect
    {
        string strConnect = "Data Source=HOANGPHUONGDEPT\\SQLEXPRESS;Initial Catalog=QuanLiShopTech;Integrated Security=True";
        SqlConnection sql = null;

        void OpenConnect()
        {
            sql = new SqlConnection(strConnect);
            if(sql.State != ConnectionState.Open)
            {
                sql.Open();
            }
        }
        void CloseConnect()
        {
            if(sql.State !=  ConnectionState.Closed)
            {
                sql.Close();
                sql.Dispose();
            }
        }
        public DataTable DataReader(string sqlSelect)
        {
            DataTable table = new DataTable();
            OpenConnect();
            SqlDataAdapter sqlData = new SqlDataAdapter(sqlSelect, sql);
            sqlData.Fill(table);
            CloseConnect();
            return table;
        }
        public void DataChange(string select)
        {
            OpenConnect();
            SqlCommand sqlcomma = new SqlCommand();
            sqlcomma.Connection = sql;
            sqlcomma.CommandText = select;
            sqlcomma.ExecuteNonQuery();
            CloseConnect();
        }
        // Select sum, count
        public int ExecuteScalar(string sqlSelect)
        {
            int result = 0;
            OpenConnect();
            SqlCommand sqlCommand = new SqlCommand(sqlSelect, sql);
            result = (int)sqlCommand.ExecuteScalar();
            CloseConnect();
            return result;
        }
    }
}
