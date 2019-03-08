using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace QLDT
{
    class DataHelper
    {
        SqlConnection con;

        public DataHelper(string s)
        { 
            this.con = new SqlConnection();
            con.ConnectionString = s;
        }
        public DataTable GetList(string query)
        {
            SqlDataAdapter da = new SqlDataAdapter();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            da = new SqlDataAdapter(cmd);            
            DataTable dt = new DataTable();
            this.con.Open();
            da.Fill(dt);
            this.con.Close();
            return dt;
        }
        public void ExecuteNonQuery(string query)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandText = query;
            this.con.Open();
            cmd.ExecuteNonQuery();
            this.con.Close();
        }
    }
}
