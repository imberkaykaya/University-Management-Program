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
    public partial class bolum : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
        sbyte bolumId = -1;
        public bolum()
        {
            InitializeComponent();
        }
        private void VeriEngel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void ComboCek()
        {
            SqlCommand bolum = new SqlCommand();
            bolum.CommandText = "select *from Tbl_ogretmen";
            bolum.Connection = baglanti;
            bolum.CommandType = CommandType.Text;
            SqlDataReader dr;
            baglanti.Open();
            dr = bolum.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["ad_sad"]);
            }
            baglanti.Close();

            SqlCommand fakulte = new SqlCommand();
            fakulte.CommandText = "select *from Tbl_fakulte";
            fakulte.Connection = baglanti;
            fakulte.CommandType = CommandType.Text;
            SqlDataReader dk;
            baglanti.Open();
            dk = fakulte.ExecuteReader();
            while (dk.Read())
            {
                comboBox2.Items.Add(dk["fakulte_ad"]);
            }
            baglanti.Close();
        }
        private void BolumCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select bolum_ad as 'Bölüm Adı', Tbl_fakulte.fakulte_ad , Tbl_ogretmen.ogretmen_ad + ' ' + ogretmen_sad AS 'Bölüm Başkanı', Tbl_ogretmen.Id, Tbl_bolum.bolum_no from Tbl_bolum inner join Tbl_ogretmen on Tbl_ogretmen.Id = Tbl_bolum.baskan_ID inner join Tbl_fakulte on Tbl_bolum.fakulte_ID=Tbl_fakulte.Id where bolum_ad like '" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
            dataGridView1.Columns[3].Visible = false;
            dataGridView1.Columns[4].Visible = false;
        }
        private void bolum_Load(object sender, EventArgs e)
        {
            BolumCek();
            ComboCek();
            VeriEngel();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into Tbl_bolum (bolum_ad, fakulte_ID, baskan_ID) values (@bolum_ad, (select Id from Tbl_fakulte where fakulte_ad='"+comboBox2.SelectedItem.ToString()+"'),(select Id from Tbl_ogretmen where ad_sad='" + comboBox1.SelectedItem.ToString() + "'))", baglanti);
            ekle.Parameters.AddWithValue("@bolum_ad", textBox1.Text);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            BolumCek();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand guncelle = new SqlCommand("update Tbl_bolum set bolum_ad = @bolum_ad, baskan_ID = (select Id from Tbl_ogretmen where ogretmen_ad+' '+ogretmen_sad =@ogrt  ) where Tbl_bolum.bolum_no = @bolum_no", baglanti);
            //select Tbl_bolum.bolum_ad as 'Bölüm Adı', Tbl_bolum.bolum_no as 'Bölüm No', Tbl_ogretmen.ad_sad as 'Bölüm Başkanı' from Tbl_ogretmen inner join Tbl_bolum on Tbl_ogretmen.bolum_no=Tbl_bolum.bolum_no
            guncelle.Parameters.AddWithValue("@bolum_ad", textBox1.Text);
            guncelle.Parameters.AddWithValue("@ogrt", comboBox1.Text);
            guncelle.Parameters.AddWithValue("@bolum_no", bolumId.ToString());
            guncelle.ExecuteNonQuery();
            baglanti.Close();
            bolumId = -1;
            BolumCek();
        }
        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand sil = new SqlCommand("delete from Tbl_bolum where Tbl_bolum.bolum_no = @id", baglanti);
            sil.Parameters.AddWithValue("@id", bolumId.ToString());
            sil.ExecuteNonQuery();
            baglanti.Close();
            BolumCek();
            bolumId = -1;
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            bolumId = Convert.ToSByte(dataGridView1.CurrentRow.Cells[4].Value);
        }
        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            BolumCek();
        }
    }
}
