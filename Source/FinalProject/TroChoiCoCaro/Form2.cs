using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroChoiCoCaro
{
    public partial class Form2 : Form
    {
        Graphics g;
        Snake snake = new Snake();

        Boolean left = false;
        Boolean right = false;
        Boolean up = false;
        Boolean down = false;

        Random ranFood = new Random();

        Food food;

        int score = 0;
        public Form2()
        {
            InitializeComponent();
            food = new Food(ranFood);
        }

        private void Form2_Load(object sender, EventArgs e)
        {

        }

        private void Form2_Paint(object sender, PaintEventArgs e)
        {
            g = e.Graphics;
            snake.drawSnake(g);

            food.drawFood(g);
        }

        private void Form2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Space)
            {
                timer1.Enabled = true;
                left = false;
                right = false;
                up = false;
                down = false;
                label1.Text = "";
            }
            if (e.KeyData == Keys.Up && down == false)
            {
                up = true;
                down = false;
                left = false;
                right = false;
            }

            if (e.KeyData == Keys.Down && up == false)
            {
                up = false;
                down = true;
                left = false;
                right = false;
            }

            if (e.KeyData == Keys.Left && right == false)
            {
                up = false;
                down = false;
                left = true;
                right = false;
            }

            if (e.KeyData == Keys.Right && left == false)
            {
                up = false;
                down = false;
                left = false;
                right = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            toolStripStatusLabelScore.Text = score.ToString();
            if (down == true)
            {
                snake.moveDown();
            }

            if (up == true)
            {
                snake.moveUp();
            }

            if (left == true)
            {
                snake.moveLeft();
            }

            if (right == true)
            {
                snake.moveRight();
            }

            for (int i = 0; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[i].IntersectsWith(food.foodRec))
                {
                    score += 10;
                    snake.growSnake();
                    food.foodLocation(ranFood);
                    timer1.Interval -= 5;
                }
            }
            Collision();
            this.Invalidate();
        }
        public void Collision()
        {
            for (int i = 1; i < snake.SnakeRec.Length; i++)
            {
                if (snake.SnakeRec[0].IntersectsWith(snake.SnakeRec[i]))
                {
                    Restart();
                }
            }
            if (snake.SnakeRec[0].Y < 0 || snake.SnakeRec[0].Y > 290)
            {
                Restart();
            }

            if (snake.SnakeRec[0].X < 0 || snake.SnakeRec[0].X > 290)
            {
                Restart();
            }
        }

        void Restart()
        {
            timer1.Enabled = false;
            MessageBox.Show("Snake died!!! Your score is :" + score.ToString() + "\n" + "THANKS FOR PLAYING");
            label1.Text = "Space to play";
            toolStripStatusLabelScore.Text = "0";
            score = 0;
            snake = new Snake();
            timer1.Interval = 200;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
        }

        private void toolStripStatusLabel2_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm m = new MainForm();
            m.Show();
        }
    }
}
