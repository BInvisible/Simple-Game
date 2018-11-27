using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TroChoiCoCaro
{
    class Snake
    {
        private Rectangle[] snakeRec;

    
        private SolidBrush brush;
        private int x, y, width, height;

        public Rectangle[] SnakeRec
        {
            get
            {
                return snakeRec;
            }

            set
            {
                snakeRec = value;
            }
        }

        public Snake()
        {
            SnakeRec = new Rectangle[5];
            brush = new SolidBrush(Color.Blue);

            x = 40;
            y = 0;
            width = 10;
            height = 10;

            for(int i=0;i<SnakeRec.Length;i++)
            {
                SnakeRec[i] = new Rectangle(x, y, width, height);
                x = x - 10;
            }
        }

        public void drawSnake(Graphics g)
        {
            foreach(Rectangle rec in SnakeRec)
            {
                g.FillEllipse(brush, rec);
            }
        }

        public void snakeRun()
        {
            for(int i=SnakeRec.Length-1;i>0;i--)
            {
                SnakeRec[i] = SnakeRec[i-1];
            }
        }

        public void moveDown()
        {
            snakeRun();
            SnakeRec[0].Y += 10;
        }

        public void moveUp()
        {
            snakeRun();
            SnakeRec[0].Y -= 10;
        }

        public void moveLeft()
        {
            snakeRun();
            SnakeRec[0].X -= 10;
        }

        public void moveRight()
        {
            snakeRun();
            SnakeRec[0].X += 10;
        }

        public void growSnake()
        {
            List<Rectangle> rec = snakeRec.ToList();
            rec.Add(new Rectangle(snakeRec[snakeRec.Length - 1].X, snakeRec[snakeRec.Length - 1].Y, width, height));
            snakeRec = rec.ToArray();
        }
    }
}
