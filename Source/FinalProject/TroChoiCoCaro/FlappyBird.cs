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
    public partial class FlappyBird : Form
    {
        private Game game;
        private Bitmap buffer;
        public FlappyBird()
        {
            InitializeComponent();
        }

        private void FlappyBird_Load(object sender, EventArgs e)
        {
            buffer = new Bitmap(this.ClientRectangle.Width, this.ClientRectangle.Height);// tinh luon ca chieu dai va chieu rong thanh cong cu phia tren
            timer1.Start();
            Game.gameSize = this.ClientRectangle;
            game = new Game();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            game.Update();
            Graphics bufferGraphics = Graphics.FromImage(this.buffer);
            bufferGraphics.Clear(Color.White);
            game.Draw(bufferGraphics);
            CreateGraphics().DrawImage(this.buffer, this.ClientRectangle);
        }

        private void FlappyBird_MouseClick(object sender, MouseEventArgs e)
        {
            game.handleMouseClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void fileToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm m = new MainForm();
            m.Show();
        }
    }
}
