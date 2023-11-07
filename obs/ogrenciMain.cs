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
    public partial class ogrenciMain : Form
    {
        public int ogrenciId;
        public ogrenciMain()
        {
            InitializeComponent();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            notGoruntule not = new notGoruntule();
            not.ogrenciId = ogrenciId;
            not.Show();
        }
    }
}
