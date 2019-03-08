using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QLDT
{
    public partial class Addform : Form
    {
        DataHelper dh;

        public delegate void SendData(GV gv1);
        public SendData dataSender;
        public Addform()
        {
            InitializeComponent();
            string s = "Data Source=DESKTOP-CQOEJHG\\SQLEXPRESS;Initial Catalog=quanlydaotao;Integrated Security=True";
            dh = new DataHelper(s);
        }
        void LoadcbKhoa1()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT Khoa FROM GiangVien";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbKhoa1.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }
        }
        void LoadcbHH1()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT HocHam FROM GiangVien";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocHam1.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }
        void LoadcbHV1()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT HocVi FROM GiangVien";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocVi1.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }
        void LoadcbHP1()
        {
            DataTable da = new DataTable();
            string query = "SELECT DISTINCT TenHP FROM HocPhan";
            da = this.dh.GetList(query);
            for (int i = 0; i < da.Rows.Count; i++)
            {
                cbHocPhan1.Items.Add(da.Rows[i].ItemArray[0].ToString());
            }

        }
        void Add()
        {
            GV gv1 = new GV()
            {
                MaGV = txtMGV.Text,
                TenGV = txtTen1.Text,
                Khoa = cbKhoa1.SelectedItem.ToString(),
                HocHam = cbHocHam1.SelectedItem.ToString(),
                HocVi = cbHocVi1.SelectedItem.ToString(),
                HocPhan = cbHocPhan1.SelectedItem.ToString(),
                GioiTinh = rNam1.Checked,
                NgaySinh = dateTimePicker2.Value
            };
            this.dataSender(gv1);            
        }
        private void bOk_Click(object sender, EventArgs e)
        {
            Add();
            this.Hide();
        }

        private void Addform_Load(object sender, EventArgs e)
        {
            LoadcbHH1();
            LoadcbHP1();
            LoadcbHV1();
            LoadcbKhoa1();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
