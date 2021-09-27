using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public partial class startPage : Form
    {
        private Button resume, start, level, quit, Hscore;
        private Setting set;
        public Popup ppp;
        public string[] st;
        private bool resumeGame;
        //private GameArea strt;
        public startPage()
        {
            InitializeComponent();
            resume = new Button();
            start =  new Button();
            set = new Setting();
            ppp = new Popup();
            level = new Button();
            Hscore = new Button();
            this.Paint += new PaintEventHandler(OnPaint);
            this.FormClosed += new FormClosedEventHandler(this.close_App);
            quit = new Button();
            BuildApp();
        }

        private void BuildApp()
        {
            //Resume
            resume.AutoSize = false;
            resume.Text = "Resume Game";
            resume.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            resume.Size = new Size(400, 60);
            resume.TextAlign = ContentAlignment.MiddleLeft;
            resume.BackColor = Color.Green;
            resume.FlatStyle = FlatStyle.Flat;
            resume.Location = new Point(10, 10);
            resume.TabIndex = 1;
            resume.Click += new EventHandler(resume_Game);

            //start
            start.AutoSize = false;
            start.Text = "Start Game";
            start.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            start.Size = new Size(400, 60);
            start.TextAlign = ContentAlignment.MiddleLeft;
            start.BackColor = Color.Green;
            start.FlatStyle = FlatStyle.Flat;
            start.Location = new Point(10, 80);
            start.TabIndex = 2;
            start.Click += new EventHandler(App_Startup);

            //level
            level.AutoSize = false;
            level.Text = "Select Difficulty";
            level.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            level.Size = new Size(400, 60);
            level.TextAlign = ContentAlignment.MiddleLeft;
            level.BackColor = Color.Green;
            level.FlatStyle = FlatStyle.Flat;
            level.Location = new Point(10, 150);
            level.TabIndex = 3;
            level.Click += new EventHandler(select_Level);

            //Highscore
            Hscore.AutoSize = false;
            Hscore.Text = "High Score";
            Hscore.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            Hscore.Size = new Size(400, 60);
            Hscore.TextAlign = ContentAlignment.MiddleLeft;
            Hscore.BackColor = Color.Green;
            Hscore.FlatStyle = FlatStyle.Flat;
            Hscore.Location = new Point(10, 220);
            Hscore.Click += new EventHandler(show_Score);

            //quit
            quit.AutoSize = false;
            quit.Text = "Quit Game";
            quit.Font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Bold);
            quit.Size = new Size(400, 60);
            quit.TextAlign = ContentAlignment.MiddleLeft;
            quit.BackColor = Color.Green;
            quit.FlatStyle = FlatStyle.Flat;
            quit.Location = new Point(10, 290);
            quit.Click += new EventHandler(this.close_App);

            this.Text = "Snakeey";
            //this.Icon = new Icon() ;
            this.Size = new Size(505, 500);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.LimeGreen;
            this.MinimizeBox = false;
            this.FormClosed += new FormClosedEventHandler(this.close_App);
            //if(resumeGame)
                

            this.Controls.Add(start);
            this.Controls.Add(level);
            this.Controls.Add(Hscore);
            this.Controls.Add(quit);
            this.ResumeLayout(false);
        }
        private void  OnPaint(object sender, PaintEventArgs e)
        {
            if (!set.FileEmpty(@"C:/Users/Absam/Documents/setting.abs"))
                this.Controls.Add(resume);
        }
        private void resume_Game(object sender, EventArgs e)
        {
            this.Hide();
            new GameArea(true).Show();
        }
        private void show_Score(object sender, EventArgs e)
        {
            st = set.fetch_Data(@"C:/Users/Absam/Documents/setting2.abs", true);
            MessageBox.Show("Highscore: " + st[1]);
        }
        private void App_Startup(object sender, EventArgs e)
        {  
            this.Hide();
            // GameArea strt = new GameArea();
            GameArea strt = new GameArea();
            strt.Show();
        }
        private void select_Level(object sender, EventArgs e)
        {
            ppp.ShowDialog();
        }
        private void close_App(object sender, EventArgs e)
        {
            if(sender == quit)
                set.resetFile(@"C:/Users/Absam/Documents/setting.abs");

            Application.Exit();
        }
    }
    

    //Select level popup alert
    public class Popup : Form
    {
        private startPage strt;
        private RadioButton[] levels;
        private RadioButton lvl1, lvl2, lvl3;
        private Setting set;
        string set2;
        public Popup()
        {
            //strt = new startPage();
            lvl1 = new RadioButton();
            lvl2 = new RadioButton();
            lvl3 = new RadioButton();
            set = new Setting();
            levels = new RadioButton[] { lvl1, lvl2, lvl3 };
            InitializeComponent();
        }

        public void InitializeComponent()
        {
            //Levels
            int i = 0;
            int x = 20, y = 10;
            string[] lvl = new string[] { "Easy", "Normal", "Hard" };
            foreach(RadioButton level in levels)
            {
                
                level.Text = lvl[i];
                level.AutoSize = true;
                level.Font = new Font("Arial", 14.25F, FontStyle.Regular);
                level.FlatStyle = FlatStyle.Flat;
                level.TextAlign = ContentAlignment.MiddleCenter;
                level.Size = new Size(150, 50);
                level.Location = new Point(x, y);
                level.Click += new EventHandler(selected_Option);
                this.Controls.Add(level);
                i++;
                y += 30;
            }
            this.Text = "Level";
            this.Size = new Size(200, 200);
            this.MinimizeBox = false;
            this.MaximizeBox = false;
            this.ShowInTaskbar = false;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Load += new EventHandler(pop_Load);
            this.FormClosed += new FormClosedEventHandler(reload_Page);
            this.ResumeLayout(false);
        }
        private void pop_Load(object sender, EventArgs e)
        {
            set2 = set.fetch_Data(@"C:/Users/Absam/Documents/setting2.abs");
            switch (set2)
            {
                case "Easy":
                    lvl1.Checked = true;
                    break;
                case "Normal":
                    lvl2.Checked = true;
                    break;
                case "Hard":
                    lvl3.Checked = true;
                    break;
            }
        }
        private void reload_Page(object sender, FormClosedEventArgs e)
        {
            
            //Console.WriteLine("Refreshed");
        }
        private void selected_Option(object sender, EventArgs e)
        {
            RadioButton rads = (sender as RadioButton);
            string[] st = set.fetch_Data(@"C:/Users/Absam/Documents/setting2.abs", true);
            string s = st[1];
            set.write_Data(@"C:/Users/Absam/Documents/setting2.abs", rads.Text +"\n"+ s);
        }
    }
}
