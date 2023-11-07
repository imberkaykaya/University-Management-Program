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
    public partial class adminMain : Form
    {
        public adminMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ogretmen ogretmen = new ogretmen();
            ogretmen.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            ogrenci ogrenci = new ogrenci();
            ogrenci.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            bolum bolum = new bolum();
            bolum.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            fakulte fakulte = new fakulte();
            fakulte.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            ders ders = new ders();
            ders.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            derskayit derskayit = new derskayit();
            derskayit.Show();
        }
    }
}
