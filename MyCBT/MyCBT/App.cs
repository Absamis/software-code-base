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
    public partial class App : Form
    {
        private int qnum;
        private AppOperation app;
        private Timer time;
        int min = 10, sec = 0;
        public App()
        {
            InitializeComponent();
            app = new AppOperation();
            time = new Timer();
            qnum = 0;
            time.Tick += new EventHandler(Timer_Click);
        }
        private void App_Load(object sender, EventArgs e)
        {
            //app.strt = true;
            app.QuestionNavigation();
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            time.Enabled = true;
            time.Start();
            time.Interval = 1000;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            app.qnum--;
            app.QuestionNavigation();
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }
        private void Form_Load(object sender, EventArgs e)
        {

        }
        private void loadOpt()
        {
            int height = 20;
            if (panel2.Controls.Count > 0)
                panel2.Controls.Clear();
            for (int i = 0; i <= 3; i++)
            {
                RadioButton rty = new RadioButton();
                rty.Text = app.option[i];
                rty.Font = new Font("Arial", 12.25F, FontStyle.Regular);
                rty.AutoSize = true;
                rty.Name = "rad" + i;
                if (rty.Text == app.fetchAnswer())
                    rty.Checked = true;
           
                rty.Location = new Point(30, height);
                rty.Click += new EventHandler(chooseAnswer);
                panel2.Controls.Add(rty);
                height += 30;
            }
        }
        private void chooseAnswer(object sender, EventArgs e)
        {
            RadioButton rads = (sender as RadioButton);
            app.storeAns(rads.Text);
        }
        private void button2_Click(object sender, EventArgs e)
        {
            app.qnum++;
            app.QuestionNavigation();
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
            //Console.WriteLine(app.LoadOption());
        }

        private void Timer_Click(object sender, EventArgs e)
        {
            if (min == 0 && sec == 0)
            {
                time.Enabled = false;
                time.Stop();
                this.Close();
                new splash_screen().Show();
                new Alert("Score = " + (app.ComputeAnswer().ToString())).ShowDialog();
            }
            else
            {
                if(sec == 0)
                {
                    min--;
                    sec = 59;
                }
                label4.Text = min+"mins:" + sec+"sec";
                sec-=1;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {
            MessageBoxButtons btn = MessageBoxButtons.YesNo;
            DialogResult rep = MessageBox.Show("Are you sure you want to submit?", "CBT", btn);
            if (rep == DialogResult.Yes)
            {
                timer1.Enabled = false;
                timer1.Stop();
                this.Close();
                new splash_screen().Show();
                new Alert("Score = " + (app.ComputeAnswer())).ShowDialog();

            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            app.fetchSubject(0);
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            app.fetchSubject(1);
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            app.fetchSubject(2);
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }

        private void navbutton_Click(object sender, EventArgs e)
        {
            Button nav = (sender as Button);
            app.fetchQuestion(int.Parse(nav.Text));
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            app.fetchSubject(3);
            label1.Text = app.showQuestion();
            loadOpt();
            label5.Text = app.qnum + " of " + app.ans;
            label3.Text = app.ShowSbj();
        }
    }
    public class Alert : Form
    {

        private Label lbl;
        string txt;
        public Alert(string arg)
        {
            txt = arg;
            lbl = new Label();
            //MessageBox.Show(arg);
            build();
        }
        private void build()
        {
            lbl.Text = txt;
            lbl.Font = new Font("Arial", 14.25F, FontStyle.Regular);
            lbl.Location = new Point(50, 50);
            lbl.AutoSize = true;

            this.AutoScaleMode = AutoScaleMode.Font;
            this.Size = new Size(200, 200);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.ShowInTaskbar = false;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Controls.Add(lbl);
            this.ResumeLayout(false);
        }
    }
}
