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
    public partial class derskayit : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=.;Initial Catalog=obs;Integrated Security=True");
        int ogrenciId = 0;
        int dersId = 0;
        int notId = 0;
        int notOgrenciId = 0;
        int notDersId = 0;
        public derskayit()
        {
            InitializeComponent();
        }

        private void derskayit_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void derskayit_Load(object sender, EventArgs e)
        {
            OgrenciCek();
            DersCek();
            KayitCek();
        }

        private void OgrenciCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select id,ogrenci_ad+' '+ogrenci_sad as 'Öğrenci' from Tbl_ogrenci ", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
            dataGridView1.Columns[0].Visible = false;
        }
        private void DersCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select Id,ders_adi from ders_katalog ", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView2.DataSource = dataTable;
            baglanti.Close();
            dataGridView2.Columns[0].Visible = false;
        }

        private void KayitCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select Tbl_not.Id,Tbl_ogrenci.ogrenci_ad+' '+Tbl_ogrenci.ogrenci_sad as 'ogrenci',ders_katalog.ders_adi,Tbl_ogrenci.id,ders_katalog.Id  from tbl_not left join ogretmenDersler on ogretmenDersler.dersId = Tbl_not.ders_kodu left join Tbl_ogretmen on ogretmenDersler.ogretmenId = Tbl_ogretmen.Id left join Tbl_ogrenci on Tbl_not.ogrenci_no = Tbl_ogrenci.id left join ders_katalog on ders_katalog.Id = Tbl_not.ders_kodu ", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView3.DataSource = dataTable;
            baglanti.Close();
            dataGridView3.Columns[0].Visible = false;
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ogrenciId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            notOgrenciId = Convert.ToInt32(dataGridView1.CurrentRow.Cells[0].Value);
            textBox2.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            dersId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            notDersId = Convert.ToInt32(dataGridView2.CurrentRow.Cells[0].Value);
            textBox3.Text = dataGridView2.CurrentRow.Cells[1].Value.ToString();
        }

        private void dataGridView3_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            notId = Convert.ToInt32(dataGridView3.CurrentRow.Cells[0].Value);
            notOgrenciId = Convert.ToInt32(dataGridView3.CurrentRow.Cells[3].Value);
            notDersId = Convert.ToInt32(dataGridView3.CurrentRow.Cells[4].Value);
            textBox2.Text = dataGridView3.CurrentRow.Cells[1].Value.ToString();
            textBox3.Text = dataGridView3.CurrentRow.Cells[2].Value.ToString();
            dersId = 0;
            ogrenciId = 0;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (dersId == 0 || ogrenciId == 0)
            {
                MessageBox.Show("Lütfen alanları doldurunuz!", "UYARI");
            }
            else
            {
                baglanti.Open();
                SqlCommand sqlCommand = new SqlCommand("insert into Tbl_not(ogrenci_no,ders_kodu,vize,final) values(@ogrNo,@dersNo,0,0) ", baglanti);
                sqlCommand.Parameters.AddWithValue("@ogrNo", ogrenciId.ToString());
                sqlCommand.Parameters.AddWithValue("@dersNo", dersId.ToString());
                sqlCommand.ExecuteNonQuery();
                ogrenciId = 0;
                dersId = 0;
                baglanti.Close();
                DersCek();
                OgrenciCek();
                KayitCek();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (notId == 0)
            {
                MessageBox.Show("Lütfen alanları doldurunuz!", "UYARI");
            }
            else
            {
                baglanti.Open();
                SqlCommand sqlCommand = new SqlCommand("update Tbl_not set ogrenci_no=@ogrNo,ders_kodu=@derKod where Id=" + notId.ToString(), baglanti);
                sqlCommand.Parameters.AddWithValue("@ogrNo", notOgrenciId.ToString());
                sqlCommand.Parameters.AddWithValue("@derKod", notDersId.ToString());
                sqlCommand.ExecuteNonQuery();
                ogrenciId = 0;
                dersId = 0;
                baglanti.Close();
                DersCek();
                OgrenciCek();
                KayitCek();
                
            }
        }
    }
}
