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
    public partial class admin_giris : Form
    {
        public admin_giris()
        {
            InitializeComponent();
        }
        SqlConnection con;
        SqlDataReader dr;
        SqlCommand com;

        private void button1_Click(object sender, EventArgs e)
        {
            string user = textBox1.Text;
            string password = textBox2.Text;
            con = new SqlConnection("Data Source=.;Initial Catalog=obs;Integrated Security=True");
            com = new SqlCommand();
            con.Open();
            com.Connection = con;
            com.CommandText = "Select *from admin_giris where name='" + textBox1.Text + "' and password='" + textBox2.Text + "'";

            dr = com.ExecuteReader();
            if (dr.Read())
            {
                MessageBox.Show("Giriş Başarılı");
                Form gecis = new adminMain();
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
