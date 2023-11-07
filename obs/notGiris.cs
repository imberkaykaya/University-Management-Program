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
    public partial class notGiris : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=obs;Integrated Security=True");
        ogretmenMain ogrMain = new ogretmenMain();
        public int ogretmenId;
        int notId = 0;
        public notGiris()
        {
            InitializeComponent();
        }

        private void DersCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select Tbl_not.Id,Tbl_ogrenci.ogrenci_ad+' '+Tbl_ogrenci.ogrenci_sad as 'ogrenci',ders_katalog.ders_adi ,Tbl_not.Vize,Tbl_not.Final,Tbl_ogretmen.ogretmen_ad+' '+Tbl_ogretmen.ogretmen_sad as 'ogretmenAdi' from tbl_not left join ogretmenDersler on ogretmenDersler.dersId = Tbl_not.ders_kodu left join Tbl_ogretmen on ogretmenDersler.ogretmenId = Tbl_ogretmen.Id left join Tbl_ogrenci on Tbl_not.ogrenci_no = Tbl_ogrenci.id left join ders_katalog on ders_katalog.Id = Tbl_not.ders_kodu where  Tbl_ogretmen.Id= " + ogretmenId.ToString() + " and ders_katalog.ders_adi like '%" + textBox1.Text + "%' and Tbl_ogrenci.ogrenci_ad+' '+Tbl_ogrenci.ogrenci_sad like '%" + textBox2.Text + "%'", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
            dataGridView1.Columns[0].Visible = false;
        }
        private void Engel()
        {
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.ReadOnly = true;
        }
        private void notGiris_Load(object sender, EventArgs e)
        {
            DersCek();
            Engel();
        }
        private void textBox2_KeyUp(object sender, KeyEventArgs e)
        {
            DersCek();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox3.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            textBox4.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            textBox5.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();
            textBox6.Text = dataGridView1.CurrentRow.Cells[4].Value.ToString();
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (notId == 0)
            {
                MessageBox.Show("Lütfen öğrenci seçiniz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                baglanti.Open();
                SqlCommand sqlCommand = new SqlCommand("update Tbl_not set Vize=@vize, Final=@final where Id=" + notId.ToString(), baglanti);
                sqlCommand.Parameters.AddWithValue("@vize", textBox5.Text);
                sqlCommand.Parameters.AddWithValue("@final", textBox6.Text);
                sqlCommand.ExecuteNonQuery();
                baglanti.Close();
                DersCek();
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
                textBox6.Text = "";
                notId = 0;
            }
        }
    }
}
