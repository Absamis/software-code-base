using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Game
{
    public class Sampler : Form
    {
        private Graphics field;
        private int[,] xpos, ypos;
        private Alert mssg;
        private int[] length;
        private int point = 0, pnt = 0;
        private SolidBrush brush, brush2, brush1, brs;
        private Pen pencil, ouli;
        private bool missed = false;
        private SnakeAction snake;
        private SnakeMovement snMove;
        private System.Windows.Forms.Timer speed;
        private Label score;
        public List<string> vals;
        private bool readyCl = false;
        string key;
        private Setting data;
        public bool resumed = false, hasEat = false;
        private Panel topbar, main;
        private IDictionary<string, int> speeds;
        private Ball food;
        public Sampler()
        {
            //InitializeComponent();
            buildApp();
            InitComp();
        }
        public Sampler(bool arg)
        {
            this.resumed = arg;
            //InitializeComponent();
            buildApp();
            InitComp();
            try
            {
                FileStream file = new FileStream(@"C:/Users/Absam/Documents/setting.abs", FileMode.Open);
                StreamReader strd = new StreamReader(file);
                string sets = strd.ReadLine();
                int[] val = sets.Split(';').Select(int.Parse).ToArray<int>();
                storeArray(ref xpos, val);
                //Console.WriteLine(xpos.GetValue(0));
                sets = strd.ReadLine();
                //ypos.SetValue(sets.Split(';').Select(int.Parse).ToArray<int>(), 0);
                val = sets.Split(';').Select(int.Parse).ToArray<int>();
                storeArray(ref ypos, val);
                sets = strd.ReadLine();
                vals = sets.Split(';').ToList();
                this.point = int.Parse(vals[0]);
                food.X1 = int.Parse(vals[1]);
                food.Y1 = int.Parse(vals[2]);
                length[0] = int.Parse(vals[3]);
                snake.setPath(vals[4]);
                file.Close();
                Console.WriteLine(snake.getPath());
                score.Text = "Score: " + this.point;
                speed.Enabled = true;
                speed.Start();
            }
            catch (Exception e)
            {
                Console.WriteLine("Resume error: " + e);
                //this.Close();
            }
        }
        public int Point
        {
            get { return point; }
            set { point = value; }
        }
        private void InitComp()
        {
            pnt = 0;
            length = new int[2000];
            length[0] = 3;
            food = new Ball();
            snake = new SnakeAction();
            xpos = new int[200, 2000];
            //set = new Setting();
            ypos = new int[200, 2000];
            vals = new List<string>();
            field = CreateGraphics();
            main = new Panel();
            brush = new SolidBrush(Color.White);
            brush1 = new SolidBrush(Color.DarkGreen);
            brush2 = new SolidBrush(Color.Black);
            brs = new SolidBrush(Color.FromArgb(150, 0, 128, 0));
            pencil = new Pen(Color.Blue, 10);
            ouli = new Pen(Color.Black, 1);
            data = new Setting();
            snMove = new SnakeMovement();
            speed = new System.Windows.Forms.Timer();
            speeds = new Dictionary<string, int>() { { "Easy", 300 }, { "Normal", 100 }, { "Hard", 50 } };
            //thr = new Thread(new ThreadStart(snake_Speed));
            mssg = new Alert();
            speed.Tick += new EventHandler(snake_Speed);
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(preview_Press);
            this.Load += new EventHandler(GameArea_Load);
            this.Paint += new PaintEventHandler(show_Game);
            this.FormClosing += new FormClosingEventHandler(close_App);
        }
        private void show_Game(object sender, PaintEventArgs e)
        {
            field.FillRectangle(brush1,food.X1, food.Y1, 10, 10);
            for (int i = 0; i < length[0]; i++)
            {
                field.FillRectangle(brush1, xpos[0, i], ypos[0, i], 10, 10);
            }
            if (resumed)
                field.FillRectangle(brush2, food.X1, food.Y1, 10, 10);
            else
                this.showBall();
        }
        public void drawSnake()
        {
            int i = length[0];
            for (i = length[0] - 1; i >= 0; i--)
            {
                if (i - 1 >= 0)
                {
                    this.xpos[0, i] = this.xpos[0, i - 1];
                    this.ypos[0, i] = this.ypos[0, i - 1];
                    field.FillRectangle(brush, xpos[0, i], ypos[0, i], 10, 10);
                }
            }
            this.xpos[0, 0] = snMove.PosX1;
            this.ypos[0, 0] = snMove.PosY1;
            field.FillRectangle(brush, xpos[0, 0], ypos[0, 0], 10, 10);
            snake.set = true;
        }
        public void clearSnake()
        {
            try
            {
                snMove.PosX1 = xpos[0, 0];
                snMove.PosY1 = ypos[0, 0];
                int x = length[0] - 1, y = length[0] - 1;
                field.FillRectangle(brush1, xpos[0, x], ypos[0, y], 10, 10);
            }
            catch (Exception e)
            {
                Application.Exit();
            };
        }
        public bool Controller()
        {
            if (snake.getPath() == "right")
                snMove.PosX1 = snMove.PosX1 + 10;
            else if (snake.getPath() == "down")
                snMove.PosY1 = snMove.PosY1 + 10;
            else if (snake.getPath() == "up")
                snMove.PosY1 = snMove.PosY1 - 10;
            else if (snake.getPath() == "left")
                snMove.PosX1 = snMove.PosX1 - 10;
            int resp = 0;
            if (snMove.getPosition(snMove.PosX1, snMove.PosY1))
            {
                speed.Enabled = false;
                speed.Stop();
                mssg.setLbl(this.point);
                mssg.ShowDialog();
                mssg.setLbl(this.point);
                data.resetFile(@"C:/Users/Absam/Documents/setting.abs");
                missed = true;
                check_Score(this.point);
                this.Close();
                return false;

            }
            else
            {
                for (int i = 0, j = 0; i < length[0] || j < length[0]; i++, j++)
                {
                    if (xpos[0, i] == snMove.PosX1 && ypos[0, j] == snMove.PosY1)
                    {
                        Console.WriteLine(xpos[0, i] + " " + snMove.PosX1 + " " + ypos[0, j] + " " + snMove.PosY1 + " " + i + " " + j);
                        resp = 1;
                    }
                }
            }
            if (resp == 1)
            {
                speed.Enabled = false;
                speed.Stop();
                mssg.triger = true;
                mssg.setLbl(this.point);
                mssg.ShowDialog();
                data.resetFile(@"C:/Users/Absam/Documents/setting.abs");
                missed = true;
                check_Score(this.point);
                this.Close();
                return false;
            }
            else
                return true;
        }
        private void buildApp()
        {
            //score Box
            score = new Label();
            score.Text = "Score: " + point;
            score.Font = new Font("Microsoft san serif", 14.25F);
            this.score.BackColor = Color.Transparent;
            score.ForeColor = Color.White;
            score.Location = new Point(15, 11);

            //Topbar
            topbar = new Panel();
            topbar.AutoSize = true;
            topbar.Location = new Point(0, 0);
            topbar.Size = new Size(495, 50);
            topbar.BackColor = Color.FromArgb(150, 0, 128, 0);
            topbar.Controls.Add(score);
            topbar.ResumeLayout(false);

            ///Main
            main = new Panel();
            main.Location = new Point(0, 50);
            main.Size = this.Size;
            main.BackColor = Color.Red;
            main.SendToBack();
            main.ResumeLayout(false);

            this.Text = "Snakeey";
            this.Size = new Size(495, 520);
            this.AutoScaleMode = AutoScaleMode.Font;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.BackColor = Color.DarkGreen;
            this.MaximizeBox = false;
            this.Controls.Add(topbar);
            //this.Controls.Add(main);
        }
        public void preview_Press(object sender, PreviewKeyDownEventArgs e)
        {
            string pth = e.KeyCode.ToString().ToLower();
            if (pth == "up" || pth == "down" || pth == "right" || pth == "left")
                snake.setPath(pth);
            else if (e.Alt || pth == "escape")
            {
                missed = false;
                this.Close();
            }
        }
        public void snake_Speed(object sender, EventArgs e)
        {
            this.clearSnake();
            if (this.Controller())
                this.drawSnake();
            if (this.SnakePoint())
            {
                point++;
                score.Text = "Score: " + point;
                length[0]++;
                //field.FillRectangle(brush1, food.X1, food.Y1, 10, 10);
                this.xpos[0, length[0] - 1] = snMove.PosX1;
                this.ypos[0, length[0] - 1] = snMove.PosY1;
                this.showBall();
                hasEat = false;
            }
        }

        public bool SnakePoint()
        {

            if (snMove.PosX1 == food.X1 && snMove.PosY1 == food.Y1 && snMove.PosY1 + 10 == food.Y1 + 10)
                return true;
            else
                return false;
        }
        public void showBall()
        {
            bool isTrue = false;
            while (!isTrue)
            {
                isTrue = true;
                food.setBallPosition();
                for (int i = 0, j = 0; i < length[0] || j < length[0]; i++, j++)
                {
                    if (!food.checkPosition(xpos[0, i], ypos[0, j]))
                        isTrue = false;
                }
            }
            field.FillRectangle(brush2, food.X1, food.Y1, 10, 10);
        }
        private void GameArea_Load(object sender, EventArgs e)
        {
            ///MessageBox.Show(resumed.ToString());
            if (!resumed)
            {
                for (int i = 0; i < length[0]; i++)
                {
                    this.xpos[0, i] = snMove.PosX1;
                    this.ypos[0, i] = snMove.PosY1;
                    snMove.PosX1 -= 10;
                }
                snMove.PosX1 += 10;
                speed.Enabled = true;
                speed.Start();
            }

            string spd = data.fetch_Data(@"C:/Users/Absam/Documents/setting2.abs");
            if (speeds.ContainsKey(spd))
            {
                key = spd;
                speed.Interval = speeds[spd];
            }
            //speed.Interval = 500;
        }
        public void check_Score(int scr)
        {
            string[] vals = data.fetch_Data(@"C:/Users/Absam/Documents/setting2.abs", true);
            if (int.Parse(vals[1]) < scr)
            {
                data.write_Data(@"C:/Users/Absam/Documents/setting2.abs", key + "\n" + scr);
            }
        }
        public void close_App(object sender, FormClosingEventArgs e)
        {
            if (!missed)
            {
                data.resetFile(@"C:/Users/Absam/Documents/setting.abs");
                int[] val = fetchArray(xpos, 0);
                string ptx = string.Join(";", val);
                val = fetchArray(ypos, 0);
                string pty = string.Join(";", val);
                data.Append_Data(ptx, @"C:/Users/Absam/Documents/setting.abs");
                data.Append_Data(pty, @"C:/Users/Absam/Documents/setting.abs");
                data.Append_Data(point + ";" + food.X1 + ";" + food.Y1 + ";" + length[0] + ";" + snake.getPath() + ";", @"C:/Users/Absam/Documents/setting.abs");
            }
            speed.Enabled = false;
            speed.Stop();
            startPage start = new startPage();
            start.Show();
        }
        private int[] fetchArray(int[,] arg1, int arg2)
        {
            List<int> el = new List<int>();
            for(int i =0; i< arg1.GetLength(0); i++)
            {
                for(int j =0; j< arg1.GetLength(1); j++)
                {
                    if (i == arg2 && arg1[i, j] != 0)
                        el.Add(arg1[i, j]);
                }
                if (i == arg2)
                    break;
            }
            return el.ToArray<int>();
        }
        private void storeArray(ref int[,] arg1, int[] arg2)
        {
            for(int i =0; i< arg2.Length; i++)
            {
                arg1[0, i] = arg2[i];
            }
        }
    }
}