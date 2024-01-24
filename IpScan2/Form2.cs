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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            dataGridView1.ColumnHeadersVisible = false;
            Database.Create_db();

            //textBox2.Visible = false;
            ara();
            button2.Enabled = false;
            button3.Enabled = false;
            this.Text = "IP SCAN";
            textBox2.Visible = false;

            var stream = File.OpenRead("yca.ico");
            this.Icon = new Icon(stream);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            if (textBox1.Text == null)
            { }
            else
            {
                if (textBox1.Text.Length > 5)
                { 
                button1.Enabled = false;
                Database.IpEkle(textBox1.Text);
                textBox1.Text = "";
                ara();
                }
            }
            button1.Enabled = true;
        }

        public void ara()
        {
            string stm = "select * FROM ips";
            dataGridView1.Rows.Clear();
            var con = new SQLiteConnection(Database.cs);
            SQLiteDataReader dr;
            con.Open();
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                dataGridView1.Rows.Insert(0, dr.GetValue(0).ToString(), dr.GetValue(1).ToString());


            }
            con.Close();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridViewRow dataGridViewRow = dataGridView1.Rows[e.RowIndex];
                textBox1.Text = dataGridViewRow.Cells["IP"].Value.ToString();
                textBox2.Text = dataGridViewRow.Cells["id"].Value.ToString();
                button1.Enabled = false;
                button2.Enabled = true;
                button3.Enabled = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length > 5)
            {
                Database.IpEdit(textBox1.Text, textBox2.Text);
                button1.Enabled = true;
                button2.Enabled = false;
                button3.Enabled = false;
                textBox2.Text = "";
                textBox1.Text = "";
                ara();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            Database.IpDelete(textBox2.Text);
            button1.Enabled = true;
            button2.Enabled = false;
            button3.Enabled = false;
            textBox2.Text = "";
            textBox1.Text = "";
            ara();
        }
    }
}
