using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QLDT
{
    public partial class Form1 : Form
    {
        DataHelper dh;
        public Form1()
        {
            InitializeComponent();
            string s = "Data Source=DESKTOP-CQOEJHG\\SQLEXPRESS;Initial Catalog=quanlydaotao;Integrated Security=True";
            dh = new DataHelper(s);
        }        
        
        private void bShow_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = this.dh.GetList("SELECT TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP)");
            int cellnum = 0;
            int rownum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellnum = cellnum + 1;
                dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                rownum = rownum + 1;
            }
            LoadcbKhoa();
            LoadcbHH();
            LoadcbHV();
            LoadcbHP();
        }

        private void dataGridView1_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            try
            {
                string tgv = dataGridView1.SelectedRows[0].Cells["TenGV"].Value.ToString();
                string query = "SELECT TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP AND Giangvien.TenGV= ' " + tgv + "')";
                DataTable da = new DataTable();
                da = this.dh.GetList(query);
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    txtTen.Text = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
                    dateTimePicker1.Value = (DateTime)dataGridView1.SelectedRows[0].Cells[2].Value;
                    cbKhoa.SelectedItem = dataGridView1.SelectedRows[0].Cells[6].Value.ToString();
                    cbHocHam.SelectedItem = dataGridView1.SelectedRows[0].Cells[4].Value.ToString();
                    cbHocVi.SelectedItem = dataGridView1.SelectedRows[0].Cells[5].Value.ToString();
                    cbHocPhan.SelectedItem = dataGridView1.SelectedRows[0].Cells[7].Value.ToString();
                    if (Convert.ToBoolean(dataGridView1.SelectedRows[0].Cells[3].Value) == true)
                        rNam.Checked = true;
                    else rNu.Checked = true;
                }
            }
            catch { }
        }
        void LoadcbKhoa()
        {
            DataTable da = new DataTable();
            string query="SELECT DISTINCT Khoa FROM GiangVien";
            da= this.dh.GetList(query);
            for(int i=0; i<da.Rows.Count; i++)
            {
                cbKhoa.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }
        }
        void LoadcbHH()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT HocHam FROM GiangVien";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocHam.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }
        void LoadcbHV()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT HocVi FROM GiangVien";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocVi.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }
        void LoadcbHP()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT TenHP FROM HocPhan";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocPhan.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }

        private void bDel_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow item in this.dataGridView1.SelectedRows)
            {
                string thp = dataGridView1.Rows[item.Index].Cells[7].Value.ToString();
                string tgv = dataGridView1.Rows[item.Index].Cells[1].Value.ToString();
                string query = "delete from Quanhe where (Quanhe.MaGV = (select MaGV from GiangVien where TenGV ='" + tgv + "')"+ "and Quanhe.MaHP = (select MaHP from HocPhan where TenHP ='"+ thp+"'))";
                dh.ExecuteNonQuery(query);
                dataGridView1.Rows.RemoveAt(item.Index);                
            }            
            int cellnum = 0;
            int rownum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellnum = cellnum + 1;
                dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                rownum = rownum + 1;
            }
        }

        private void bSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string s = txtSearch.Text;
                string query = "SELECT TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP AND GiangVien.Khoa LIKE '%" + s + "%')";
                dataGridView1.DataSource = this.dh.GetList(query);
                int cellnum = 0;
                int rownum = 0;
                foreach (DataGridViewRow row in dataGridView1.Rows)
                {
                    cellnum = cellnum + 1;
                    dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                    rownum = rownum + 1;
                }
            }
            catch { }
        }
        private void bUpdate_Click(object sender, EventArgs e)
        {
            string tgv = dataGridView1.SelectedRows[0].Cells[1].Value.ToString();
            string thp = dataGridView1.SelectedRows[0].Cells["TenHP"].Value.ToString();
            string query = "update GiangVien set " + "TenGV = '" + txtTen.Text + "'," + "NgaySinh = '" + String.Format("{0:yyyy-MM-dd} ", dateTimePicker1.Value) + "'," + "Khoa = '" + cbKhoa.SelectedItem.ToString() + "'," + "HocVi = '" + cbHocVi.SelectedItem.ToString() + "'," + "HocHam = '" + cbHocHam.SelectedItem.ToString() + "'," + "GioiTinh = '" + rNam.Checked + "'" + "where TenGV = '" + tgv + "'"; 
            dh.ExecuteNonQuery(query);
            dataGridView1.DataSource = this.dh.GetList("SELECT TenGV, NgaySinh, Khoa, HocHam, HocVi, GioiTinh, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP)");
            int cellnum = 0;
            int rownum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellnum = cellnum + 1;
                dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                rownum = rownum + 1;
            }
        }
        public void setData(GV gv1)
        {
            string query1= "insert into GiangVien (MaGV, TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa) values ("+gv1.MaGV+",'"+gv1.TenGV+"','"+gv1.NgaySinh+"','"+gv1.GioiTinh+"','"+gv1.HocHam+"','"+gv1.HocVi+"','"+gv1.Khoa+"')";
            string query2= "insert into Quanhe (MaGV, MaHP) values ('"+gv1.MaGV+"',(select MaHP from HocPhan where TenHP ='"+gv1.HocPhan+"'))";
            dh.ExecuteNonQuery(query1);
            dh.ExecuteNonQuery(query2);
            dataGridView1.DataSource = this.dh.GetList("SELECT TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP)");
            int cellnum = 0;
            int rownum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellnum = cellnum + 1;
                dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                rownum = rownum + 1;
            }
        }
        private void bAdd_Click(object sender, EventArgs e)
        {
            Addform frmadd = new Addform();
            frmadd.dataSender = new Addform.SendData(setData);
            frmadd.Show();
        }
        private void bSort_Click(object sender, EventArgs e)
        {
            string s = cbSort.SelectedItem.ToString();
            dataGridView1.DataSource = this.dh.GetList("SELECT TenGV, NgaySinh, GioiTinh, HocHam, HocVi, Khoa, TenHP FROM GiangVien, HocPhan, Quanhe WHERE(GiangVien.MaGV = Quanhe.MaGV AND HocPhan.MaHP = Quanhe.MaHP) order by " + s+" asc");
            int cellnum = 0;
            int rownum = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                cellnum = cellnum + 1;
                dataGridView1.Rows[rownum].Cells[0].Value = cellnum;
                rownum = rownum + 1;
            }
        }
    }
}
