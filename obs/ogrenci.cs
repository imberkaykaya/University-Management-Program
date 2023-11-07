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
    public partial class ogrenci : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
        sbyte ogrenciId = -1;
        public ogrenci()
        {
            InitializeComponent();
        }
        private void Temizle()
        {
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            comboBox1.SelectedIndex = 0;
            comboBox2.SelectedIndex = 0;
        }
        private void OgrenciCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select ogrenci_ad + ' ' + ogrenci_sad as 'Öğrenci Adı', tbl_iller.Il as 'İl', Tbl_bolum.bolum_ad as 'Bölüm', ogrenci_no as 'Öğrenci No', sifre as 'Şifre', ogrenci_dgm as 'Doğum tarihi', ogrenci_ad as 'Ad', ogrenci_sad as 'Soyad',Tbl_ogrenci.id from Tbl_ogrenci inner join tbl_iller on tbl_iller.Id = Tbl_ogrenci.ogrenci_il inner join Tbl_bolum on Tbl_ogrenci.bolum_id = Tbl_bolum.bolum_no where ogrenci_ad+' '+ogrenci_sad like '%" + txbArama.Text + "%'", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
            dataGridView1.Columns[8].Visible = false;
        }
        private void ComboCek()
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
        public void DGveriEngel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void ogrenci_Load(object sender, EventArgs e)
        {
            OgrenciCek();
            ComboCek();
            DGveriEngel();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand ekle = new SqlCommand("insert into Tbl_ogrenci (ogrenci_no, sifre, ogrenci_ad, ogrenci_sad, ogrenci_dgm, ogrenci_il, bolum_id) values (@ogrenci_no, @sifre, @ogrenci_ad, @ogrenci_sad, @ogrenci_dgm,(select Id from Tbl_iller  where Il='" + comboBox1.SelectedItem.ToString() + "'), (select bolum_no from Tbl_bolum where bolum_ad='" + comboBox2.SelectedItem.ToString() + "'))", baglanti);
            ekle.Parameters.AddWithValue("@ogrenci_no", textBox1.Text);
            ekle.Parameters.AddWithValue("@sifre", textBox2.Text);
            ekle.Parameters.AddWithValue("@ogrenci_ad", textBox3.Text);
            ekle.Parameters.AddWithValue("@ogrenci_sad", textBox4.Text);
            ekle.Parameters.AddWithValue("@ogrenci_dgm", dateTimePicker1.Value);
            ekle.ExecuteNonQuery();
            baglanti.Close();
            OgrenciCek();
            Temizle();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (ogrenciId == -1)
            {
                MessageBox.Show("Lütfen bir öğrenci seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                try
                {
                    baglanti.Open();
                    SqlCommand sil = new SqlCommand("delete from Tbl_ogrenci where Tbl_ogrenci.id = @Id", baglanti);
                    sil.Parameters.AddWithValue("@Id", ogrenciId.ToString());
                    sil.ExecuteNonQuery();
                    baglanti.Close();
                    OgrenciCek();
                    ogrenciId = -1;
                    Temizle();
                }
                catch (Exception)
                {
                    MessageBox.Show("Silme işleminde beklenmedik hata", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (ogrenciId == -1 || comboBox1.Text == "Lütfen il seçin" || comboBox2.Text == "Lütfen bölüm seçin")
            {
                MessageBox.Show("Lütfen alanları düzgün bir şekilde  seçiniz!", "UYARI", MessageBoxButtons.OK, MessageBoxIcon.Question);
            }
            else
            {
                try
                {
                    baglanti.Open();
                    SqlCommand guncelle = new SqlCommand("update Tbl_ogrenci set ogrenci_no = @ogrenci_no, sifre = @sifre, ogrenci_ad = @ogrenci_ad, ogrenci_sad = @ogrenci_sad, ogrenci_dgm = @ogrenci_dgm, ogrenci_il = (select Id from tbl_iller where Il = @ogrenci_il), bolum_id=(select bolum_no from Tbl_bolum where bolum_ad = @bolum_id) where Tbl_ogrenci.id = @id", baglanti);
                    guncelle.Parameters.AddWithValue("@sifre", textBox2.Text);
                    guncelle.Parameters.AddWithValue("@ogrenci_ad", textBox3.Text);
                    guncelle.Parameters.AddWithValue("@ogrenci_sad", textBox4.Text);
                    guncelle.Parameters.AddWithValue("@ogrenci_dgm", dateTimePicker1.Value);
                    guncelle.Parameters.AddWithValue("@ogrenci_no", textBox1.Text);
                    guncelle.Parameters.AddWithValue("@ogrenci_il", comboBox1.Text);
                    guncelle.Parameters.AddWithValue("@bolum_id", comboBox2.Text);
                    guncelle.Parameters.AddWithValue("@id", ogrenciId.ToString());
                    guncelle.ExecuteNonQuery();
                    baglanti.Close();
                    OgrenciCek();
                    ogrenciId = -1;
                    Temizle();
                }
                catch (Exception)
                {
                    MessageBox.Show("Güncelleme işleminde beklenmedik hata", "HATA", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
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
            ogrenciId = Convert.ToSByte(dataGridView1.CurrentRow.Cells[8].Value);
        }

        private void txbArama_KeyDown(object sender, KeyEventArgs e)
        {
            OgrenciCek();
        }
    }
}
