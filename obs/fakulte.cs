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

namespace obs
{
    public partial class fakulte : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
        sbyte fakulteId = -1;
        public fakulte()
        {
            InitializeComponent();
        }
        private void engel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void FakulteCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select Id, fakulte_ad as 'Fakulte' from Tbl_fakulte where fakulte_ad like '" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
        }
        private void fakulte_Load(object sender, EventArgs e)
        {
            FakulteCek();
            engel();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into Tbl_fakulte (fakulte_ad) values (@fakulte_ad)", baglanti);
            ekle.Parameters.AddWithValue("@fakulte_ad", textBox1.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            FakulteCek();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("update Tbl_fakulte set fakulte_ad = @f_ad where Tbl_fakulte.Id=@Id", baglanti);
            guncelle.Parameters.AddWithValue("@f_ad", textBox1.Text);
            guncelle.Parameters.AddWithValue("@Id", fakulteId.ToString());
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            FakulteCek();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("delete from Tbl_fakulte where Tbl_fakulte.Id=@Id", baglanti);
            sil.Parameters.AddWithValue("@Id", fakulteId.ToString());
            sil.ExecuteNonQuery();
            baglanti.Close();
            FakulteCek();
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            fakulteId = Convert.ToSByte(dataGridView1.CurrentRow.Cells[0].Value);
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            FakulteCek();
        }
    }
}
