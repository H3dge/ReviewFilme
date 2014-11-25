using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Xml;
using System.Net;
using System.IO;

namespace WindowsFormsApplication1
{
    public partial class Form3 : Form
    {
        public Form3(string numeUser)
        {
            InitializeComponent();
            label1.Text="Bine ai venit, ";
            label1.Text += numeUser;
            label1.Text += " !";
            string username=numeUser;
            invtextBox1.Text = numeUser;
           
        }
        string nume;
        SqlConnection con = new SqlConnection(@"Data Source=.\SQLEXPRESS;AttachDbFilename=C:\Users\Manita\Desktop\ReviewsFilme\ReviewsFilme\Database1.mdf;Integrated Security=True;User Instance=True");
        SqlCommand cmd;
        SqlDataReader jlk;
        string mod;
        int semafor = 1;
        int nr;
        int nrrich;

        ///////  ultimele filme

        public class Result
        {
            public bool adult { get; set; }
            public string backdrop_path { get; set; }
            public int id { get; set; }
            public string original_title { get; set; }
            public string release_date { get; set; }
            public string poster_path { get; set; }
            public double popularity { get; set; }
            public string title { get; set; }
            public double vote_average { get; set; }
            public int vote_count { get; set; }
        }

       /* public class RootObject
        {
            public int page { get; set; }
            public List<Result> results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }*/
        string data_azi;
        string data_ieri;
        List<string> filme = new List<string>();
        List<string> empty = new List<string>();

        ///////  date film
        public class RootObject
        {
            public string Title { get; set; }
            public string Year { get; set; }
            public string Rated { get; set; }
            public string Released { get; set; }
            public string Runtime { get; set; }
            public string Genre { get; set; }
            public string Director { get; set; }
            public string Writer { get; set; }
            public string Actors { get; set; }
            public string Plot { get; set; }
            public string Language { get; set; }
            public string Country { get; set; }
            public string Awards { get; set; }
            public string Poster { get; set; }
            public string Metascore { get; set; }
            public string imdbRating { get; set; }
            public string imdbVotes { get; set; }
            public string imdbID { get; set; }
            public string Type { get; set; }
            public string Response { get; set; }
            // ultimele filme
            public int page { get; set; }
            public List<Result> results { get; set; }
            public int total_pages { get; set; }
            public int total_results { get; set; }
        }

        public class MovieData
        {
            public string Title { get; set; }
            public string Rated { get; set; }
            public string Released { get; set; }
            public string Genre { get; set; }
            public string Actors { get; set; }
            public string Plot { get; set; }
            public string Language { get; set; }
            public string Country { get; set; }
            public string Awards { get; set; }
            public string Poster { get; set; }
            public string Metascore { get; set; }
            public string imdbRating { get; set; }
            public string imdbVotes { get; set; }

        }
        public string MovieName;
        public string decizie="";

////////////////////////// BUTTON 1

        private void button1_Click(object sender, EventArgs e)
        {
            nume = invtextBox1.Text;
            richTextBox2.Clear();
            richTextBox1.Clear();
/////////////////// chestii json

            if (textBox1.TextLength > 0)
            {
                MovieName = textBox1.Text;
               // textBox1.Text = MovieName;

                try
                {
                    WebClient webclient = new WebClient();
                    webclient.Encoding = Encoding.UTF8;
                    string json = webclient.DownloadString("http://www.omdbapi.com/?i=&t=" + MovieName);
                    MovieData data = JsonConvert.DeserializeObject<MovieData>(json);

                    string date = data.Actors.ToString();
                    char[] delimiterChars = { ',' };
                    string text = date;
                    string[] words = text.Split(delimiterChars);

                    foreach (string s in words)
                    {
                        richTextBox1.Text += s + Environment.NewLine;
                    }
                    semafor = 0;


                }
                catch { MessageBox.Show("film inexistent");
                semafor = 1;
                }
            }
            else
            {
                MessageBox.Show("You forgot to write the name of the movie");
            }
///////////////////// verifica daca filmul a primit reviewuri
            string strfilm = "SELECT COUNT(*) from reviews where film='"+ textBox1.Text +"'";
            con.Open();
            cmd = new SqlCommand(strfilm, con);
            //cmd.ExecuteNonQuery();
            jlk = cmd.ExecuteReader();
            while (jlk.Read())
            {
                mod = Convert.ToString(jlk[0]);

            }
             nr = Convert.ToInt32(mod);
           // MessageBox.Show(mod);
            con.Close();
/////////////////// reviewuri din baza.
            if (nr!=0)
            {
                string str = "SELECT * FROM reviews WHERE film='" + textBox1.Text + "'";
                con.Open();
                cmd = new SqlCommand(str, con);
                // cmd.ExecuteNonQuery();
                jlk = cmd.ExecuteReader();
                while (jlk.Read())
                {
                    richTextBox2.Text += "user: " + Convert.ToString(jlk[1]) + Environment.NewLine;
                    richTextBox2.Text += "review: " + Convert.ToString(jlk[2]) + Environment.NewLine;
                    richTextBox2.Text += Environment.NewLine;
                    //richTextBox2.Text += Environment.NewLine;
                    //MessageBox.Show(mod);
                }    
            }
            else if (nr == 0 && semafor==0)
            {
                decizie = "nu stiu";
                richTextBox2.Text = "Acest film nu are review.Vrei sa fi primul care adauga un review?";
                nrrich = 0;
                Form4 form4 = new Form4(invtextBox1.Text, textBox1.Text);
                form4.Show();
            }
            if (decizie == "nu")
                MessageBox.Show("nu");
            else if (decizie == "da")
                MessageBox.Show("da");

        con.Close();

        }

        private void Form3_Load(object sender, EventArgs e)
        {
            invtextBox1.Visible = false;
            richTextBox2.ReadOnly = true;
            richTextBox1.ReadOnly = true;
            decizie = "dnja";
                
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox3.Visible = true;
            richTextBox2.Visible = false;
            button2.Visible = true;
            button3.Visible = false;
            if (richTextBox2.Text == "Acest film nu are review.Vrei sa fi primul care adauga un review?")
            {
                richTextBox2.Clear();
                
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {

            if (textBox1.TextLength != 0)
            {
                string str = "INSERT INTO reviews (film, username, review) VALUES('" + textBox1.Text + "','" + invtextBox1.Text + "','" + richTextBox3.Text + "')";
                con.Open();
                cmd = new SqlCommand(str, con);
                cmd.ExecuteNonQuery();
                con.Close();
                MessageBox.Show("Bravo!");
            }


            richTextBox3.Visible = false;
            richTextBox2.Visible = true;
            richTextBox2.Text += "user: " +invtextBox1.Text+ Environment.NewLine;
            richTextBox2.Text += "review: " + richTextBox3.Text + Environment.NewLine;
            richTextBox2.Text += Environment.NewLine;
            richTextBox3.Clear();
            button3.Visible = true;
            button2.Visible = false; 
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (decizie == "da")
                MessageBox.Show("da");
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            int count;
            if (textBox2.TextLength == 0)
            {
                count = 5;
            }
            else
            count = Convert.ToInt32(textBox2.Text);

            listBox1.DataSource = empty;
            filme.Clear();
            data_azi = DateTime.Today.ToString("yyyy-M-d"); //!!!
            data_ieri = DateTime.Today.AddDays(-count).ToString("yyyy-M-d");

            try
            {
                WebClient webclient = new WebClient();
                webclient.Encoding = Encoding.UTF8;
                string json = webclient.DownloadString("http://api.themoviedb.org/3/discover/movie?primary_release_date.gte=" + data_ieri + "&primary_release_date.lte=" + data_azi + "&api_key=fdbc3f7a4347c8eecdd244d614b50a7e");
                RootObject data = JsonConvert.DeserializeObject<RootObject>(json);
                for (int j = 0; j <= 15 ; j++)
                {
                    filme.Add(data.results[j].title);
                    //richTextBox2.Text += data.results[j].release_date + Environment.NewLine;
                    // MessageBox.Show(data.results[j].title);
                    
                }
               // MessageBox.Show("m2");
                
                listBox1.DataSource = filme;
            }
            catch { }
        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            string numeFilm="2";
            int index = this.listBox1.IndexFromPoint(e.Location);
            if (index != System.Windows.Forms.ListBox.NoMatches)
            {
                numeFilm=listBox1.SelectedItem.ToString();
                
            }
            textBox1.Text = numeFilm;
            button1.PerformClick();

        }

       
        
    }
}
