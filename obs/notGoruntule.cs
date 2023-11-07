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
    public partial class notGoruntule : Form
    {
        SqlConnection baglanti = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
        public int ogrenciId;
        public notGoruntule()
        {
            InitializeComponent();
        }
        private void NotCek()
        {
            baglanti.Open();
            SqlCommand sqlCommand = new SqlCommand("select ders_katalog.ders_adi as 'Ders', Vize, Final,(Vize*0.4)+(Final*0.6) As 'ORTALAMA' from Tbl_not inner join Tbl_ogrenci on Tbl_not.ogrenci_no = Tbl_ogrenci.id inner join ders_katalog on ders_katalog.Id = Tbl_not.ders_kodu where Tbl_ogrenci.id = " + ogrenciId.ToString() + " and ders_katalog.ders_adi like '%" + textBox1.Text + "%' ", baglanti);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(sqlCommand);
            DataTable dataTable = new DataTable();
            dataAdapter.Fill(dataTable);
            dataGridView1.DataSource = dataTable;
            baglanti.Close();
        }
        private void notGoruntule_Load(object sender, EventArgs e)
        {
            NotCek();
        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            NotCek();
        }
    }
}
