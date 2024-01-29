using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Windows.Forms;

namespace IpScan2
{

    class Database
    {
        public static SQLiteConnection conn;
        public static SQLiteCommand cmd;
        public static SQLiteDataReader dr;
        public static string path = "data.db";
        public static string cs = @"URI=file:data.db";

        public static void Create_db()
        {
            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {
                    sqlite.Open();
                    string sql = "CREATE TABLE ips (id INTEGER, ips TEXT  UNIQUE , PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();

                    sql = "CREATE TABLE Logs (id INTEGER, log1 TEXT ,  log2 TEXT, log3 TEXT , PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();

                    sql = "CREATE TABLE time1 (id INTEGER, time1 TEXT , PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();


                    var con = new SQLiteConnection(cs);
                    con.Open();
                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = "INSERT INTO time1(time1) VALUES(@time1)";
                    cmd.Parameters.AddWithValue("@time1", "10");
                    cmd.ExecuteNonQuery();
                    con.Close();

                    sqlite.Close();
                }
            }
        }

        public static void IpEkle(string ips)
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO ips(ips) VALUES(@ips)";
                cmd.Parameters.AddWithValue("@ips", ips);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Kayıt Tamam.");
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Sorunu.");
            }
        }

        public static void IpEdit(string ips, string id)
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                string sql = "UPDATE ips set ips='" + ips + "'  where id =" + id;
                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Tamam.");
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Sorunu.");
            }
        }

        public static void IpDelete(string id)
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                string sql = "DELETE FROM ips  where id =" + id;
                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Tamam.");
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Sorunu.");
            }
        }

        public static int x()
        {


            int cevap = 1;

            var con = new SQLiteConnection(cs);
            con.Open();


            string stm = "select * FROM time1 where id = 1";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();

            while (dr.Read())
            {

            cevap = Int32.Parse(dr.GetValue(1).ToString());
            }

            con.Close();

            return cevap;
        }

        public static void time1Edit(string x)
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                string sql = "UPDATE time1 set time1='" + x + "'  where id =1";
                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Kayıt Tamam.");
                con.Close();
            }
            catch (Exception e)
            {
                MessageBox.Show("Database Sorunu.");
            }
        }

        public static void LogsAdd(string log1, string log2, string log3)
        {
            try
            {
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);
                cmd.CommandText = "INSERT INTO Logs(log1,log2,log3) VALUES(@log1,@log2,@log3)";
                cmd.Parameters.AddWithValue("@log1", log1);
                cmd.Parameters.AddWithValue("@log2", log2);
                cmd.Parameters.AddWithValue("@log3", log3);
                cmd.ExecuteNonQuery();
                con.Close();
             
            }
            catch (Exception e)
            {
               
            }
        }

    }
}
