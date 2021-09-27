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
    public partial class GameArea : Form
    {
        private Graphics field;
        private int[] xpos, ypos, disp;
        private Alert mssg;
        private int length = 3, point = 0, pnt;
        private SolidBrush brush, brush2, brush1, brs;
        private Pen pencil, ouli;
        private bool missed = false;
        private SnakeAction snake;
        private SnakeMovement snMove;
        private System.Windows.Forms.Timer speed;
        private Label score;
        public List<string> vals;
        public List<int> DisposeX, DisposeY, el;
        private bool readyCl = false;
        string key;
        private Setting data;
        public bool resumed = false, hasEat = false;
        private Panel topbar, main;
        private IDictionary<string, int> speeds;
        private Ball food;
        public GameArea()
        {
            InitializeComponent();
            buildApp();
            InitComp();
        }
        public GameArea(bool arg)
        {
            this.resumed = arg;
            InitializeComponent();
            buildApp();
            InitComp();
            try
            {
                FileStream file = new FileStream(@"C:/Users/Absam/Documents/setting.abs", FileMode.Open);
                StreamReader strd = new StreamReader(file);
                string sets = strd.ReadLine();
                xpos = sets.Split(';').Select(int.Parse).ToArray<int>();
                sets = strd.ReadLine();
                ypos = sets.Split(';').Select(int.Parse).ToArray<int>();
                sets = strd.ReadLine();
                vals = sets.Split(';').ToList();
                this.point = int.Parse(vals[0]);
                food.X1 = int.Parse(vals[1]);
                food.Y1 = int.Parse(vals[2]);
                length = int.Parse(vals[3]);
                snake.setPath(vals[4]);
                sets = strd.ReadLine();
                //disp = sets.Split(';').Select(int.Parse).ToArray();
                DisposeX = sets.Split(';').Select(int.Parse).ToList();
                sets = strd.ReadLine();
                //disp = sets.Split(';').Select(int.Parse);
                DisposeY = sets.Split(';').Select(int.Parse).ToList();
                pnt = DisposeX.Count - 1;
                file.Close();
                //Console.WriteLine(snake.getPath());
                score.Text = "Score: " + this.point;
                speed.Enabled = true;
                speed.Start();
            }
            catch(Exception e)
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
            food = new Ball();
            pnt = -2;
            snake = new SnakeAction();
            xpos = new int[2000];
            DisposeX = new List<int>();
            DisposeY = new List<int>();
            ypos = new int[2000];
            el = new List<int>();
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
            //field.FillRectangle(brs1, 0, 0, 500, 50);
            field.FillRectangle(brush1, food.X1, food.Y1, 10, 10);
            for (int i = 0; i < length; i++)
            {
                field.FillRectangle(brush1, xpos[i], ypos[i], 10, 10);
            }
            if (resumed)
            {
                field.FillRectangle(brush2, food.X1, food.Y1, 10, 10);
                for(int i = 0; i< DisposeX.Count;  i++)
                {
                    Console.WriteLine("=>" + DisposeX[i]);

                    if(DisposeX[i] != -1 && DisposeY[i] != -1)
                        field.FillRectangle(brush, DisposeX[i], DisposeY[i], 10, 10);
                }
            }
            else
                this.showBall();
        }
        public void drawSnake()
        {
                int i = length;
                for (i = length -1; i >= 0; i--)
                {
                    if (i - 1 >= 0)
                    {
                        this.xpos[i] = this.xpos[i - 1];
                        this.ypos[i] = this.ypos[i - 1];
                        field.FillRectangle(brush, xpos[i], ypos[i], 10, 10);
                    }
                }
                this.xpos[0] = snMove.PosX1;
                this.ypos[0] = snMove.PosY1;
                field.FillRectangle(brush, xpos[0], ypos[0], 10, 10);
                snake.set = true;
        }
        public void clearSnake()
        {
            try
            {
                snMove.PosX1 = xpos[0];
                snMove.PosY1 = ypos[0];
                int x = length - 1, y = length - 1;
                field.FillRectangle(brush1, xpos[x], ypos[y], 10, 10);
            }
            catch (Exception e) {
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
            //Check if it move out of box
            if (snMove.getPosition(snMove.PosX1, snMove.PosY1))
            {
                addDisposable(fetchArrayElem(xpos), fetchArrayElem(ypos));
            }
            //Check if it hit itself on entrance 
            for (int i = 0, j = 0; i < DisposeX.Count || j < DisposeY.Count; i++, j++)
            {
                if (DisposeX[i] == snMove.PosX1 && DisposeY[j] == snMove.PosY1)
                {
                    resp = 1;
                }
            }
            //Check if it hit itself
            for (int i = 0, j = 0; i < xpos.Length || j < ypos.Length; i++, j++)
            {
                if (xpos[i] == snMove.PosX1 && ypos[j] == snMove.PosY1)
                {
                    resp = 1;
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
            score.Text = "Score: "+ point;
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
            else if (e.Alt || pth =="escape")
            {
                this.Close();
            }
        }
        public void snake_Speed(object sender, EventArgs e)
        {
            this.clearSnake();
            if (this.Controller())
            {
                checkDisposable();
                this.drawSnake();
            }
            if (this.SnakePoint())
            {
                point++;
                Graphics g = score.CreateGraphics();
                g.Clear(Color.FromArgb(230, 0, 128, 0));
                g.DrawString("Score: " + point, new Font("Arial", 14.25F, FontStyle.Bold), Brushes.White, 0, 0);
                //field.DrawString("Score: " + point, new Font("Arial", 12.25F, FontStyle.Bold), Brushes.White, 20, 20);
                length++;
                this.showBall();
                hasEat = false;
            }
        }
        public void checkDisposable()
        {
            if(pnt >= 0)
            {
                for(int j = 0; j < pnt; j++)
                {
                    try {
                        //Console.WriteLine("Pointtt=> " + pnt + " " + j+"=>"+DisposeX.Count);
                        if (DisposeY[j + 1] == -1 && DisposeX[j + 1] == -1)
                        {
                            field.FillRectangle(brush1, DisposeX[j], DisposeY[j], 10, 10);
                            //Console.WriteLine("Cleaned" + pnt + " " + DisposeX[pnt] + " " + DisposeY[pnt]);
                            DisposeX.RemoveAt(j);
                            DisposeY.RemoveAt(j);
                        }
                    }catch(Exception e)
                    {
                        Console.WriteLine(e.ToString() + j);
                    }
                    pnt = DisposeX.Count -1;
                }
                //pnt--;
            }
            else if(pnt == -1)
            {
                pnt--;
                DisposeX.Clear();
                DisposeY.Clear();
            }
        }
        public void addDisposable(int[] arg1, int [] arg2)
        {
            DisposeX.AddRange(arg1);
            DisposeY.AddRange(arg2);
            DisposeX.Add(-1);
            DisposeY.Add(-1);
            //Console.WriteLine(string.Join(",", DisposeY));
            //Console.WriteLine(string.Join(",", DisposeX));
            pnt = DisposeX.Count - 1;

            for(int i = 0; i < length; i++)
            {
                if (snake.getPath() == "right")
                {
                    xpos[i] = -10;
                    snMove.PosX1 = 0;
                }
                else if (snake.getPath() == "left")
                {         xpos[i] = snake.borderWidthX2;
                    snMove.PosX1 = snake.borderWidthX2 - 30;
                }
                else if (snake.getPath() == "up")
                {
                    ypos[i] = snake.borderWidthY2;
                    snMove.PosY1 = snake.borderWidthY2 - 30;
                }
                else if (snake.getPath() == "down")
               {
                    ypos[i] = 40;
                    snMove.PosY1 = 50;
                }
                //Console.WriteLine(xpos[i+1] +" "+snMove.PosX1);
            }
        }
        public bool SnakePoint()
        {

            if (snMove.PosX1  == food.X1 && snMove.PosY1 == food.Y1 && snMove.PosY1 + 10 == food.Y1 + 10)
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
                for (int i = 0, j = 0; i < xpos.Length || j < ypos.Length; i++, j++)
                {
                    if (!food.checkPosition(xpos[i], ypos[j]))
                        isTrue = false;
                }
            }
            field.FillRectangle(brush2, food.X1, food.Y1, 10, 10);
        }
        private void GameArea_Load(object sender, EventArgs e)
        {
            if (!resumed)
            {
                for (int i = 0; i < length; i++)
                {
                    this.xpos[i] = snMove.PosX1;
                    this.ypos[i] = snMove.PosY1;
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
            if(int.Parse(vals[1]) < scr)
            {
                data.write_Data(@"C:/Users/Absam/Documents/setting2.abs", key + "\n" + scr);
            }
        }
        public void close_App(object sender, FormClosingEventArgs e)
        {
            if (!missed)
            {
                data.resetFile(@"C:/Users/Absam/Documents/setting.abs");
                var ptx = string.Join(";", xpos);
                var pty = string.Join(";", ypos);
                var ptx1 = string.Join(";", DisposeX);
                var pty1 = string.Join(";", DisposeY);
                data.Append_Data(ptx, @"C:/Users/Absam/Documents/setting.abs");
                data.Append_Data(pty, @"C:/Users/Absam/Documents/setting.abs");
                //MessageBox.Show(snake.getPath());
                data.Append_Data(point + ";" + food.X1 + ";" + food.Y1 + ";" + length + ";" + snake.getPath() + ";", @"C:/Users/Absam/Documents/setting.abs");
                data.Append_Data(ptx1, @"C:/Users/Absam/Documents/setting.abs");
                data.Append_Data(pty1, @"C:/Users/Absam/Documents/setting.abs");
            } 
            speed.Enabled = false;
            speed.Stop();
            startPage start = new startPage();
            start.Show();
        }
        public void getHitEntrance()
        {
            
        }
        public int[] fetchArrayElem(int[] arg)
        {
            el.Clear();
            for (int i =0; i< length; i++)
            {
                el.Add(arg[i]);
            }
            //Console.WriteLine(string.Join("=>", el));
            return el.ToArray<int>();
        }
    }
}