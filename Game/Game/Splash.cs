using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class Splash : Form
    {
        private startPage start;
        private PictureBox pics;
        private Label lbl, lbl1, lbl2, lbl3;
        private Timer delay;
        int i = 0;
        public Splash()
        {
            InitializeComponent();
            pics = new PictureBox();
            lbl = new Label();
            lbl1 = new Label();
            lbl2 = new Label();
            lbl3 = new Label();
            delay = new Timer();
            delay.Tick += new EventHandler(cnt_Delay);
            start = new startPage();
            BuildApp();
        }
        private void BuildApp()
        {
            //Timer
            delay.Enabled = true;
            delay.Interval = 5000;
            delay.Start();

            //Label
            lbl.Text = "S";
            lbl.Font = new Font("Courier New", 60F, FontStyle.Bold);
            lbl.Location = new Point(170, 20);
            lbl.ForeColor = Color.Orange;
            lbl.AutoSize = true;
            lbl.BackColor = Color.Transparent;
            lbl.SendToBack();

            //Label2
            lbl1.Text = "na";
            lbl1.Font = new Font("Courier New", 30F, FontStyle.Bold);
            lbl1.Location = new Point(230, 50);
            lbl1.ForeColor = Color.Blue;
            lbl1.AutoSize = true;
            lbl1.BackColor = Color.Transparent;
            lbl1.BringToFront();

            //Label3
            lbl2.Text = "ke";
            lbl2.Font = new Font("Courier New", 30F, FontStyle.Bold);
            lbl2.Location = new Point(285, 50);
            lbl2.ForeColor = Color.Red;
            lbl2.AutoSize = true;
            lbl2.BackColor = Color.Transparent;
            lbl2.BringToFront();

            //Label3
            lbl3.Text = "Game";
            lbl3.Font = new Font("Comic Sans MS", 22F, FontStyle.Bold);
            lbl3.Location = new Point(245, 90);
            lbl3.ForeColor = Color.Yellow;
            lbl3.AutoSize = true;
            lbl3.BackColor = Color.Transparent;
            //lbl2.BringToFront();

            //Picture Box;
            pics.Image =(Image) Properties.Resources.sn2;
            pics.SizeMode = PictureBoxSizeMode.AutoSize;
            pics.Size = new Size(120, 180);
            pics.BackColor = Color.Transparent;
            pics.Location = new Point(20, 5);


            this.Size = new Size(400, 250);
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.ControlBox = false;
            this.Text = "";
            this.BackgroundImage = (Image)Properties.Resources.sn1;
            this.BackgroundImageLayout = ImageLayout.Tile;
            this.BackColor = Color.LawnGreen;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.Controls.Add(lbl2);
            this.Controls.Add(lbl3);
            this.Controls.Add(lbl1);
            this.Controls.Add(lbl);
            this.Controls.Add(pics);
        }
        private void cnt_Delay(object Sender, EventArgs e)
        {
                delay.Enabled = false;
                delay.Stop();
                this.Hide();
                start.Show();
        }
    }
}
