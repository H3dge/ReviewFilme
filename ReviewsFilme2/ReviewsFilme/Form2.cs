using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace WindowsFormsApplication1
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

            //  button1_Click(this, new EventArgs());
            //  button1.PerformClick();

       
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Manita\Desktop\ReviewsFilme\ReviewsFilme\Database1.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;
        SqlDataReader jlk;
        string mod;
        string pass;
        string numeUserLogat;

        private void Form2_Load(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {
            label4.Visible = false;
            LogInGroup.Visible = false;
            RegisterGroup.Visible = true;
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string strfilm = "SELECT COUNT(*) from conturi where username='"+ textBox3.Text +"'";
            con.Open();
            cmd = new SqlCommand(strfilm, con);
            //cmd.ExecuteNonQuery();
            jlk = cmd.ExecuteReader();
            while (jlk.Read())
            {
                mod = Convert.ToString(jlk[0]);

            }
            int nr = Convert.ToInt32(mod);
            MessageBox.Show(mod);

            con.Close();
           // string idFilmul = Convert.ToString(idFilm);
            if (textBox3.TextLength > 8 && textBox4.TextLength > 8 && textBox5.TextLength > 0 && nr == 0)
            {
                string str = "INSERT INTO conturi (username, password, varsta) VALUES('" + textBox3.Text + "','" + textBox4.Text + "','" + textBox5.Text + "')";
                con.Open();
                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
                LogInGroup.Visible = true;
                RegisterGroup.Visible = false;
                MessageBox.Show("Bravo!");
            }
            else if (nr > 0)
                MessageBox.Show("acest nume a fost folosit");
            else if (textBox3.TextLength < 8)
                MessageBox.Show("Numele e prea scurt");
            else if (textBox4.TextLength < 8)
                MessageBox.Show("Parola e prea scurta");
            else if (textBox5.TextLength == 0)
                MessageBox.Show("Introdu o varsta");

            
        }

        private void textBox5_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] exceptions = new char[] { 'a', 'b', 'c', 'd', 'f', 'g', 'h', 'j','k','l','m','n','o','p','q','r','s','t',
                                             'v','q','x','y','z','w',
                                             '!','@','#','$','%',
                '^','&','*','(',')','-','=','\'','"','?','[',']','+','_'};
            
            foreach (char element in exceptions)
            {
                if (e.KeyChar == element)
                    e.Handled = true;

            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.TextLength > 0 && textBox2.TextLength > 0)
            {
                string struser = "SELECT COUNT(*) from conturi where username='" + textBox1.Text + "'";
                con.Open();
                cmd = new SqlCommand(struser, con);
                //cmd.ExecuteNonQuery();
                jlk = cmd.ExecuteReader();
                while (jlk.Read())
                {
                    mod = Convert.ToString(jlk[0]);

                }
                int nr = Convert.ToInt32(mod);
                //MessageBox.Show(mod);

                con.Close();


                if (nr != 0)
                {
                    string strpass = "SELECT password FROM conturi WHERE username ='" + textBox1.Text + "'";
                    con.Open();
                    cmd = new SqlCommand(strpass, con);
                    //cmd.ExecuteNonQuery();
                    jlk = cmd.ExecuteReader();
                    while (jlk.Read())
                    {
                        pass = Convert.ToString(jlk[0]);

                    }
                    con.Close();
                    //MessageBox.Show(mod);
                }
                else if (nr == 0)
                    MessageBox.Show("userul nu exista");

                if (textBox2.Text == pass)
                {
                    MessageBox.Show("logat");
                    numeUserLogat = textBox1.Text;
                    Form3 form3 = new Form3(numeUserLogat);
                    form3.Show();
                    this.Hide();
                }
                else
                    MessageBox.Show("parola gresita");
            }
            else
                MessageBox.Show("introdu parola si userul");
        }

        private void textBox2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                button1_Click(this, new EventArgs());
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {

                textBox2.Focus();
            }
        }
       
    

    

    }
}
