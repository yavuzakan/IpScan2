using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpScan2
{
    public partial class Form4 : Form
    {
        public Form4()
        {
            InitializeComponent();
            this.Text = "IP SCAN";
            var stream = File.OpenRead("yca.ico");
            this.Icon = new Icon(stream);
            ara();
        }

        public void ara()
        {
            string stm = "select * FROM Logs";
            dataGridView1.Rows.Clear();
            var con = new SQLiteConnection(Database.cs);
            SQLiteDataReader dr;
            con.Open();
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                dataGridView1.Rows.Insert(0, dr.GetValue(1).ToString(), dr.GetValue(2).ToString(), dr.GetValue(3).ToString());


            }
            con.Close();
        }
    }
}
