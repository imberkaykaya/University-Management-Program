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
    public partial class ogretmen_giris : Form
    {
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;
        public ogretmen_giris()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            con = new SqlConnection("Data Source=.;Initial Catalog=obs;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select Id, ogretmen_ad from Tbl_ogretmen where ogretmen_kad='" + textBox1.Text + "' and sifre='" + textBox2.Text + "'";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı");
                ogretmenMain gecis = new ogretmenMain();
                gecis.ogretmenId = Convert.ToInt32(dr["Id"]);
                gecis.label2.Text = dr["ogretmen_ad"].ToString();
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