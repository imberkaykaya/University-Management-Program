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
    public partial class ogretmenMain : Form
    {
        public int ogretmenId;
        public ogretmenMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            notGiris notlar = new notGiris();
            notlar.ogretmenId = ogretmenId;
            notlar.Show();
            this.Hide();
        }
    }
}