using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpScan2
{
    public partial class Form1 : Form
    {
        public static string w1 = "1";
        public static string w2 = "";
        public static string w3 = "";
        public static string w4 = "";

       
        
        


        public Form1()
        {
            InitializeComponent();
            this.dataGridView1.AllowUserToAddRows = false;
            dataGridView1.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dataGridView1.ReadOnly = true;
            Database.Create_db();
            menuStrip1.BackColor = Color.Transparent;
            button1.Text = "SCAN";
            this.Text = "IP SCAN";
            var stream = File.OpenRead("yca.ico");
            this.Icon = new Icon(stream);

            backgroundWorker1.WorkerReportsProgress = true;
            backgroundWorker1.WorkerSupportsCancellation = true;
            Form2 form2 = new Form2();
            timer1.Enabled = true;
            timer1.Tick += timer1_Tick;
            int x = Database.x();

            timer1.Interval = x * 60 * 1000;
            timer1.Start();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form2 = new Form2();
            form2.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            run1();
        }

        public void run1()
        {

            if (backgroundWorker1.IsBusy != true)
            {
                dataGridView1.Rows.Clear();
                backgroundWorker1.RunWorkerAsync();
                button1.Text = "STOP";
            }
            else
            {
                backgroundWorker1.CancelAsync();
                button1.Text = "SCAN";
            }

        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
          
            BackgroundWorker worker = sender as BackgroundWorker;
            string stm = "select * FROM ips";
            var con = new SQLiteConnection(Database.cs);
            SQLiteDataReader dr;
            con.Open();
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();
            int i = 1;
            while (dr.Read())
            {
                Ping ping = new Ping();
                PingReply cevap = ping.Send(dr.GetValue(1).ToString());

                if (cevap.Status == IPStatus.Success)
                {
                        w2 = dr.GetValue(1).ToString();
                        w3 = "Succes";
                        w4 = "Succes.jpg";
                }
                else
                {
                        w2 = dr.GetValue(1).ToString();
                        w3 = "Faild";
                        w4 = "Faild.jpg";
                 

                }
                i++;

                System.Threading.Thread.Sleep(1);
                worker.ReportProgress(1);
            }
            con.Close();

         
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            try
            {
                dataGridView1.Rows.Insert(0, w2, w3, DateTime.Now.ToString("dd/MM/yyyy HH:mm"), Image.FromFile(w4));

                if (w3 == "Faild")
                {
                    Database.LogsAdd(w2, w3, DateTime.Now.ToString("dd/MM/yyyy HH:mm"));
                }
            }
             catch
            {
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            button1.Text = "SCAN";
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            run1();
        }

        private void timeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
        }

        private void reportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form4 form4 = new Form4();
            form4.Show();
        }
    }
}
