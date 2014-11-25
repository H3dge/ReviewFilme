using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{
    public partial class Form4 : Form
    {
        public Form4(string nume, string film)
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string nu = "nu";
            Form3 form3=new Form3(nu);
            form3.decizie = "nu";
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
          /*  string da = "da";
            Form3 form3 = new Form3(da);
            form3.Show();
            form3.decizie = "da";*/

            //Form5 form5=new Form5();
           // form5.Show();
            this.Hide();
            
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
