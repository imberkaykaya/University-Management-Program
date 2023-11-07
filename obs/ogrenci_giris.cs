using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace obs
{
    public partial class ogrenci_giris : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        public ogrenci_giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=DESKTOP-0MADKNC;Initial Catalog=obs;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select id, ogrenci_ad from Tbl_ogrenci where ogrenci_no='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı");
                ogrenciMain gecis = new ogrenciMain();
                gecis.ogrenciId = Convert.ToInt32(dr["id"]);
                gecis.label2.Text = dr["ogrenci_ad"].ToString();
                gecis.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Hatalı Giriş");
            }
            con.Close();
        }
    }
}