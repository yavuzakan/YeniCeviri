using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace YeniCeviri
{
    class Database
    {
        SQLiteConnection conn;
        SQLiteCommand cmd;
        SQLiteDataReader dr;
        public static string path = @"data.db";
        public static string cs = @"URI=file:" + path;

        public static void Create_db()
        {


            if (!System.IO.File.Exists(path))
            {
                SQLiteConnection.CreateFile(path);
                using (var sqlite = new SQLiteConnection(@"Data Source=" + path))
                {

                    sqlite.Open();
                    string sql = "CREATE TABLE data (id INTEGER, veri TEXT, veri2 TEXT, PRIMARY KEY(id AUTOINCREMENT))";
                    SQLiteCommand command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();



                    sql = "CREATE TABLE lang (id INTEGER, lang TEXT , lang2 TEXT ,    PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();



                    sql = "CREATE TABLE font1 (id INTEGER, font1 TEXT UNIQUE,  PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();

                    sql = "CREATE TABLE size (id INTEGER, width TEXT , height TEXT ,  PRIMARY KEY(id AUTOINCREMENT))";
                    command = new SQLiteCommand(sql, sqlite);
                    command.ExecuteNonQuery();


                    var con = new SQLiteConnection(cs);
                    con.Open();
                    var cmd = new SQLiteCommand(con);
                    cmd.CommandText = "INSERT INTO lang(lang,lang2) VALUES('tr','eng')";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO data(veri) VALUES('')";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO font1(font1) VALUES('12')";

                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO size(width , height) VALUES('1000','150')";

                    cmd.ExecuteNonQuery();


                    sqlite.Close();



                }

            }

        }
        public static void fontupdate(string veri)
        {
            try
            {


                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "UPDATE font1 set font1='" + veri + "'  where id =1";

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception e)
            {


            }


        }
        public static void sizeupdate(string veri , string veri2)
        {
            try
            {


                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "UPDATE size set width ='" + veri + "' , height ='" + veri2 + "'  where id =1";

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception e)
            {


            }


        }
        public static void langupdate1(string veri)
        {
            try
            {

                veri = veri.Replace("'", "''");
                var con = new SQLiteConnection(cs);
                con.Open();
                var cmd = new SQLiteCommand(con);



                string sql = "UPDATE data set veri =' " + veri + "'  where id = 1 ";

                cmd.CommandText = sql;
                cmd.Prepare();
                cmd.ExecuteNonQuery();

                con.Close();


            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message.ToString());

            }


        }



    }
}
