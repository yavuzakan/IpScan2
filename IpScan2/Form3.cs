using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpScan2
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            int x = Database.x();
            textBox1.Text = x.ToString();

            this.Text = "IP SCAN";
            var stream = File.OpenRead("yca.ico");
            this.Icon = new Icon(stream);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Database.time1Edit(textBox1.Text);
        }
    }
}
