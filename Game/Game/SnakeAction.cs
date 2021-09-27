using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading.Tasks;

namespace Game
{
    class SnakeAction
    {
        public bool set = true;
        private string path = "";
        public int borderWidthX1 = 0, borderWidthY1 = 50, borderWidthX2 = 500, borderWidthY2 = 500;

        public void setPath(string path)
        {
            if (set)
            {
                if (this.path == "")
                    this.path = path;
                else if ((this.path == "right" && path != "left") || (this.path == "up" && path != "down") || (this.path == "left" && path != "right") || (this.path == "down" && path != "up"))
                    this.path = path;

                set = false;
            }
        }
        public string getPath()
        {
            if (this.path == "")
                this.path = "right";

            return this.path;
        }
    }
    class SnakeMovement : SnakeAction
    {
        public int PosX1 = 100, PosY1 = 50, PosX2 = 10, PosY2 = 10;

        public bool getPosition(int x1, int y1)
        {
            if (this.borderWidthX1 > x1 || this.borderWidthX2 == x1 + 20 || this.borderWidthY1 > y1 || this.borderWidthY2 == y1 + 20)
                return true;
            else
                return false;
        }
    }
    class Ball
    {
        private Random ballX, ballY;
        private int x1, y1;
        public int X1
        {
            get { return x1; }
            set { x1 = value; }
        }
        public int Y1
        {
            get { return y1; }
            set { y1 = value; }
        }
        public Ball()
        {
            ballX = ballY = new Random();
        }
        public void setBallPosition()
        {
            x1 = this.ballX.Next(0, 480);
            y1 = this.ballY.Next(50, 480);
            x1 = x1 - (x1 % 10);
            y1 = y1 - (y1 % 10);
        }
        public bool checkPosition(int x, int y)
        {
            if (this.x1 == x && this.y1 == y)
                return false;
            else
                return true;
        }
    }
}
