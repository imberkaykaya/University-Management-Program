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
    public partial class ogretmen : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial sCatalog=obs;Integrated Security=True");
        sbyte ogretmenId;
        public ogretmen()
        {
            InitializeComponent();
        }
        private void Combocek()
        {
            SqlCommand il = new SqlCommand();
            il.CommandText = "select *from Tbl_iller";
            il.Connection = baglanti;
            il.CommandType = CommandType.Text;

            SqlDataReader dr;
            baglanti.Open();
            dr = il.ExecuteReader();
            while (dr.Read())
            {
                comboBox1.Items.Add(dr["Il"]);
            }
            baglanti.Close();

            SqlCommand bolum = new SqlCommand();
            bolum.CommandText = "select *from Tbl_bolum";
            bolum.Connection = baglanti;
            bolum.CommandType = CommandType.Text;

            baglanti.Open();
            dr = bolum.ExecuteReader();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr["bolum_ad"]);
            }
            baglanti.Close();
        }
        private void DGveriEngel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void ogretmen_Load(object sender, EventArgs e)
        {
            Ogretmencek();
            Combocek();
            DGveriEngel();
        }
        private void temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void Ogretmencek()
        {
            try
            {
                baglanti.Open();
                SqlCommand sqlCommand = new SqlCommand("select ogretmen_ad + ' ' + ogretmen_sad as 'Öğretim üyesi', tbl_iller.Il as 'İl', Tbl_bolum.bolum_ad as 'Bölüm', ogretmen_kad as 'Kullanıcı adı', sifre as 'Şifre', ogretmen_dogum as 'Doğum tarihi', ogretmen_ad as 'Ad', ogretmen_sad as 'Soyad',Tbl_ogretmen.Id from Tbl_ogretmen inner join tbl_iller on tbl_iller.Id = Tbl_ogretmen.ogretmen_il inner join Tbl_bolum on Tbl_ogretmen.bolum_no = Tbl_bolum.bolum_no where ogretmen_ad+' '+ogretmen_sad like '" + txbArama.Text + "%'", baglanti);
                SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
                DataTable dataTable = new DataTable();
                dataAdapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
                baglanti.Close();
                comboBox1.SelectedIndex = 0;
                comboBox2.SelectedIndex = 0;
                dataGridView1.Columns[8].Visible = false;
            }
            catch (Exception e)
            {
                MessageBox.Show("Lütfen özel karakter girmeyiniz\n" + e.Message.ToString(), "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Hand);
            }
        }
        private void btnEkle_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "Lütfen il seçin" || comboBox2.Text == "Lütfen bölüm seçin")
            {
                MessageBox.Show("Lütfen alanları düzgün bir şekilde  seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                baglanti.Open();
                SqlCommand ekle = new SqlCommand("insert into Tbl_ogretmen (ogretmen_kad, sifre, ogretmen_ad, ogretmen_sad, ogretmen_dogum, ogretmen_il, bolum_no) values (@ogretmen_kad, @sifre, @ogretmen_ad, @ogretmen_sad, @ogretmen_dogum, (select Id from Tbl_iller  where Il='" + comboBox1.SelectedItem.ToString() + "'), (select bolum_no from Tbl_bolum where bolum_ad='" + comboBox2.SelectedItem.ToString() + "'))", baglanti);
                ekle.Parameters.AddWithValue("@ogretmen_kad", textBox1.Text);
                ekle.Parameters.AddWithValue("@sifre", textBox2.Text);
                ekle.Parameters.AddWithValue("@ogretmen_ad", textBox3.Text);
                ekle.Parameters.AddWithValue("@ogretmen_sad", textBox4.Text);
                ekle.Parameters.AddWithValue("@ogretmen_dogum", dateTimePicker1.Value);
                ekle.ExecuteNonQuery();
                baglanti.Close();
                Ogretmencek();
                temizle();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ogretmenId == -1 || comboBox1.Text == "Lütfen il seçin" || comboBox2.Text == "Lütfen bölüm seçin")
            {
                MessageBox.Show("Lütfen alanları düzgün bir şekilde  seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                baglanti.Open();
                SqlCommand guncelle = new SqlCommand("update Tbl_ogretmen set ogretmen_kad = @ogretmen_kad, sifre = @sifre, ogretmen_ad = @ogretmen_ad, ogretmen_sad = @ogretmen_sad, ogretmen_dogum = @ogretmen_dogum,ogretmen_il = (select Id from tbl_iller where Il = @il),bolum_no=(select bolum_no from Tbl_bolum where bolum_ad = @bolum) where Tbl_ogretmen.Id = @ogretmenId", baglanti);
                guncelle.Parameters.AddWithValue("@sifre", textBox2.Text);
                guncelle.Parameters.AddWithValue("@ogretmen_ad", textBox3.Text);
                guncelle.Parameters.AddWithValue("@ogretmen_sad", textBox4.Text);
                guncelle.Parameters.AddWithValue("@ogretmen_dogum", dateTimePicker1.Value);
                guncelle.Parameters.AddWithValue("@ogretmen_kad", textBox1.Text);
                guncelle.Parameters.AddWithValue("@il", comboBox1.Text);
                guncelle.Parameters.AddWithValue("@bolum", comboBox2.Text);
                guncelle.Parameters.AddWithValue("@ogretmenId", ogretmenId.ToString());
                guncelle.ExecuteNonQuery();
                baglanti.Close();
                Ogretmencek();
                ogretmenId = -1;
                temizle();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ogretmenId == -1)
            {
                MessageBox.Show("Lütfen bir öğretmen seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                baglanti.Open();
                SqlCommand sil = new SqlCommand("delete from Tbl_ogretmen where Tbl_ogretmen.Id = @ogretmenId", baglanti);
                sil.Parameters.AddWithValue("@ogretmenId", ogretmenId.ToString());
                sil.ExecuteNonQuery();
                baglanti.Close();
                Ogretmencek();
                ogretmenId = -1;
                temizle();
            }
        }
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            textBox1.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox2.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
            textBox3.Text = dataGridView1.CurrentRow.Cells[6].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[7].Value.ToString();
            dateTimePicker1.Text = dataGridView1.CurrentRow.Cells[5].Value.ToString();
            comboBox1.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            comboBox2.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            ogretmenId = Convert.ToSByte(dataGridView1.CurrentRow.Cells[8].Value);
        }
        private void txbArama_KeyUp(object sender, KeyEventArgs e)
        {
            Ogretmencek();
        }
    }
}
