using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace www
{
    public partial class welcome : Form
    {
        public welcome()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string userName = textBox1.Text.Trim();
            if (string.IsNullOrEmpty(userName))
            {
                MessageBox.Show("Введите свое имя");
                return;
            }

            DateTime currentTime = DateTime.Now;

            string greeting;
            if (currentTime.Hour >= 6 && currentTime.Hour < 12)
            {
                greeting = "Доброе утро";
            }
            else if (currentTime.Hour >= 12 && currentTime.Hour < 18)
            {
                greeting = "Добрый день";
            }
            else
            {
                greeting = "Добрый вечер";
            }
            MessageBox.Show($"{greeting}, {userName}!", "Приветствие", MessageBoxButtons.OK, MessageBoxIcon.Information);

            dab dab = new dab();
            dab.Show();
            this.Hide();
        }
    }
}
