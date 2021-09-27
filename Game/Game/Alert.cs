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
    public partial class Alert : Form
    {
        private Label alertBox, lbl;
        private Button restart;
        public bool triger;
        public Alert()
        {
            alertBox = new Label();
            lbl = new Label();
            restart = new Button();
            InitializeComponent();
            BuildApp();
        }
        private void BuildApp()
        {
            //Button
            restart.Text = "Ok";
            restart.Location = new Point(50, 100);
            restart.Font = new Font("Arial", 13.25F, FontStyle.Regular);
            restart.AutoSize = true;
            restart.TabIndex = 3;
            restart.ForeColor = Color.Black;
            restart.BackColor = Color.White;
            restart.FlatStyle = FlatStyle.Standard;
            restart.Click += new EventHandler(restart_App);

            //Label
            alertBox.Text = "Game Over";
            alertBox.Font = new Font("Arial", 15.25F, FontStyle.Bold);
            alertBox.Location = new Point(25, 25);
            alertBox.AutoSize = true;

            //Lbl score
            lbl.Text = "";
            lbl.AutoSize = true;
            lbl.Font = new Font("Arial", 14.25F, FontStyle.Bold);
            lbl.Location = new Point(35, 65);

            this.Text = "Alert";
            this.Size = new Size(200, 200);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.MinimizeBox = false;
            this.ControlBox = false;
            this.MaximizeBox = false;
            this.Controls.Add(alertBox);
            this.ShowInTaskbar = false;
            this.Controls.Add(restart);
            this.Controls.Add(lbl);
            this.ResumeLayout(false);
            //this. += new EventHandler(exit_App);
        }
        public void setLbl(int num)
        {
            lbl.Text = "Score: " + num;
        }
        private void exit_App(object sender, EventArgs e)
        {
            Application.Exit();
        }
        private void restart_App(object sender, EventArgs e)
        {
            this.Close();
        }
    }   
}
