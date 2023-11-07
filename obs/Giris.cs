using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace obs
{
    public partial class Giris : Form
    {
        public Giris()
        {
            InitializeComponent();
        }
        public void button1_Click(object sender, EventArgs e)
        {
            ogretmen_giris form = new ogretmen_giris();
            form.Show();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            ogrenci_giris form = new ogrenci_giris();
            form.Show();
        }
        private void hakkımızdaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Bu programın yazarı Berkay Kaya'dır. İletişim İçin; berkay.kaya2620@gop.edu.tr");
        }
        private void çıkışToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            admin_giris form = new admin_giris();
            form.Show();
        }

        private void Giris_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}