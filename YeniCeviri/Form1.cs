using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace YeniCeviri
{
    public partial class Form1 : Form
    {
        int i = 0;
        public Form1()
        {
            InitializeComponent();
            this.TopMost = true;
            this.Load += Form1_Load;
            Database.Create_db();
            font();
            size();
            this.Icon = new Icon("yca.ico");
            notifyIcon1.Icon = new Icon("yca.ico");
            textBox2.Visible = false;

           


        }

        private void button1_Click(object sender, EventArgs e)
        {
      

        }

        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern IntPtr SetClipboardViewer(IntPtr hWndNewViewer);

        public const int WM_DRAWCLIPBOARD = 0x0308;

        private void Form1_Load(object sender, EventArgs e)
        {
            SetClipboardViewer(this.Handle);
        }

        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (m.Msg != WM_DRAWCLIPBOARD)
                return;

            string veri = Clipboard.GetText();
            //Code To handle Clipboard change event
            //database.add(veri);
            Database.langupdate1(veri);
            textBox2.Text = veri;
            cevir();
            ara();
            // mp3yap();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size - 0.5f, textBox1.Font.Style);
            Database.fontupdate(textBox1.Font.Size.ToString());
        }

        private void button3_Click(object sender, EventArgs e)
        {
            textBox1.Font = new Font(textBox1.Font.FontFamily, textBox1.Font.Size + 0.5f, textBox1.Font.Style);
            Database.fontupdate(textBox1.Font.Size.ToString());
        }
        public void font()
        {




            var con = new SQLiteConnection(@"Data Source=" + Database.path);
            SQLiteDataReader dr;
            con.Open();
            string ara = textBox2.Text;
            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM font1 ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                string font1 = dr.GetValue(1).ToString();
                float font2 = float.Parse(font1);
                textBox1.Font = new Font(textBox1.Font.FontFamily, font2, textBox1.Font.Style);

            }

            con.Close();


            // dataGridView1.Columns[0].Visible = false;



        }
        public void size()
        {




            var con = new SQLiteConnection(@"Data Source=" + Database.path);
            SQLiteDataReader dr;
            con.Open();
            string ara = textBox2.Text;
            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM size ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                string width = dr.GetValue(1).ToString();
                string height = dr.GetValue(2).ToString();
                int width2 = int.Parse(width);
                int height2 = int.Parse(height);
                this.Size = new Size(width2,height2);

            }

            con.Close();


            // dataGridView1.Columns[0].Visible = false;



        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            if (WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.Visible = true;
                // notifyIcon1.ShowBalloonTip(1000);
            }
            else if (FormWindowState.Normal == this.WindowState)
            {
                notifyIcon1.Visible = false;
            }

        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
           
            Database.sizeupdate(this.Width.ToString(), this.Height.ToString());
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        public void cevir()
        {
            string q = "";

            try
            {
                String komut = "transpy.exe";
                System.Diagnostics.Process process = new System.Diagnostics.Process();
                process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                process.StartInfo.FileName = "cmd.exe";
                process.StartInfo.Arguments = "/C " + komut;

                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardInput = true;
                process.Start();




                while (!process.HasExited)
                {
                    q += process.StandardOutput.ReadToEnd();
                }



            }
            catch (Exception ex)
            {



            }
           


        }
        public void ara()
        {


            textBox1.Text = "";
            var con = new SQLiteConnection(@"Data Source=" + Database.path);
            SQLiteDataReader dr;
            con.Open();
            string ara = textBox2.Text;
            //string stm = "select * FROM data ORDER BY id ASC  ";
            //SELECT * FROM (SELECT * FROM graphs WHERE sid=2 ORDER BY id DESC LIMIT 10) g ORDER BY g.id
            string stm = "select * FROM data ";
            var cmd = new SQLiteCommand(stm, con);
            dr = cmd.ExecuteReader();



            while (dr.Read())
            {
                textBox1.Text = dr.GetValue(2).ToString();

            }

            con.Close();


            // dataGridView1.Columns[0].Visible = false;



        }

        private void Form1_Activated(object sender, EventArgs e)
        {

                  }

        private void textBox1_Click(object sender, EventArgs e)
        {
           
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
            notifyIcon1.Visible = false;
            WindowState = FormWindowState.Normal;

        }
    }
}
