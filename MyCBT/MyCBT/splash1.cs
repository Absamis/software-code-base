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
    public partial class splash1 : Form
    {
        int tick;
        public splash1()
        {
            InitializeComponent();
            timer1.Start();
        }

        private void splash1_Load(object sender, EventArgs e)
        {

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            tick++;
            this.Text = "Please wait.....";
            //this.Font = new Font("Time new roman", 12.25F, FontStyle.Regular);
            if (tick == 6)
            {
                this.Text = "stop";
                timer1.Stop();
                new splash_screen().Show();
                this.Hide();
            }
        }

        private void progressBar1_Click(object sender, EventArgs e)
        {
            
        }
    }
}
