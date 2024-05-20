using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DoAn_2023
{
    internal class DB_Connect
    {
        public DB_Connect()
        { }


        SqlConnection conn = KetNoi("DESKTOP-88SK3C6", "DuAn2023_NK04");


        public static SqlConnection KetNoi(string namesever, string namedata)
        {
            string chuoi = "Data Source=" + namesever + ";Initial Catalog=" + namedata + ";Integrated Security=True;Encrypt=False";
            return new SqlConnection(chuoi);
        }

        public void MoKetNoi()
        {
            if (conn.State.ToString() == "Close")
            {
                conn.Open();
            }
        }


        public void DongKetNoi()
        {
            if (conn.State.ToString() == "Open")
            {
                conn.Close();
            }
        }


        public DataTable ExcuteTable(string sql)
        {
            DataTable dt = new DataTable();

            try
            {
                MoKetNoi();
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = sql;

                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Connection = conn;

                SqlDataAdapter adap = new SqlDataAdapter(cmd);
                adap.Fill(dt);

            }
            catch (Exception)
            {
                dt = null;
            }
            finally
            {
                DongKetNoi();
            }
            return dt;
        }
    }
}
