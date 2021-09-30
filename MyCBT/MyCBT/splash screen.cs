using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyCBT
{
    public partial class splash_screen : Form
    {
        public splash_screen()
        {
            InitializeComponent();
            this.FormClosed += new FormClosedEventHandler(close_App);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new App().Show();
            this.Hide();
        }
        private void close_App(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
