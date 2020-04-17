using System;
using System.Windows.Forms;

namespace Риэлторское_агенство
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //переход на другую форму  
        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.Show();
        }
        //переход на другую форму
        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form3 f3 = new Form3();
            f3.Show();
        }
        //переход на другую форму
        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form4 f4 = new Form4();
            f4.Show();
        }
        //переход на другую форму
        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form5 f5 = new Form5();
            f5.Show();
        }
        //переход на другую форму
        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form6 f6 = new Form6();
            f6.Show();
        }
        //переход на другую форму
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
        //переход на другую форму
        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form7 form7 = new Form7();
            form7.Show();
        }
    }
}
