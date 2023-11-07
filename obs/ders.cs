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
    public partial class ders : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
        sbyte dersId;
        public ders()
        {
            InitializeComponent();
        }
        private void ComboCek()
        {
            SqlCommand bolum = new SqlCommand();
            bolum.CommandText = "select *from Tbl_bolum";
            bolum.Connection = baglanti;
            bolum.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = bolum.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["bolum_ad"]);
            }
            baglanti.Close();
        }
        private void Engel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void DersCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select ders_katalog.ders_adi as 'Ders Adı', ders_katalog.ders_kodu as 'Ders Kodu', Tbl_bolum.bolum_ad as 'Bölüm', ders_katalog.kredi as 'Kredi', ders_katalog.Id from ders_katalog inner join Tbl_bolum on ders_katalog.bolum_no=Tbl_bolum.bolum_no", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
            dataGridView1.Columns[4].Visible = false;
        }
        private void ders_Load(object sender, EventArgs e)
        {
            DersCek();
            ComboCek();
            Engel();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into ders_katalog (ders_kodu, ders_adi, bolum_no, kredi) values (@ders_kodu, @ders_adı,(select bolum_no from Tbl_bolum  where bolum_ad='" + comboBox1.SelectedItem.ToString() + "'), @kredi)", baglanti);
            ekle.Parameters.AddWithValue("@ders_kodu", textBox1.Text);
            ekle.Parameters.AddWithValue("@ders_adı", textBox2.Text);
            ekle.Parameters.AddWithValue("@kredi", textBox3.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            DersCek();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("update ders_katalog set ders_kodu = @ders_kodu, ders_adi = @ders_adi, kredi = @kredi, bolum_no=(select bolum_no from tbl_bolum where bolum_ad = @bolum_ad)where ders_katalog.id = @dersId", baglanti);
            guncelle.Parameters.AddWithValue("@ders_kodu", textBox1.Text);
            guncelle.Parameters.AddWithValue("@ders_adi", textBox2.Text);
            guncelle.Parameters.AddWithValue("@kredi", textBox3.Text);
            guncelle.Parameters.AddWithValue("@bolum_ad", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@dersId", dersId.ToString());
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            DersCek();
            dersId = -1;
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("delete from ders_katalog where ders_katalog.id = @dersId", baglanti);
            guncelle.Parameters.AddWithValue("@dersId", dersId.ToString());
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            DersCek();
            dersId = -1;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            dersId = Convert.ToSByte(dataGridView1.CurrentRow.Cells[4].Value);
        }
    }
}
