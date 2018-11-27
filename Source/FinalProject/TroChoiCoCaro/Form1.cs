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
    public partial class Form1 : Form
    {
        private CaroChess caroChess;
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            caroChess = new CaroChess();
            caroChess.InitArrayChess();
            g = pnlChessBoard.CreateGraphics();
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {        
        }

        private void playerVsPlayerToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pnlChessBoard_Paint(object sender, PaintEventArgs e)
        {
            caroChess.DrawChessBoard(g);
            caroChess.DrawChessPiecesAgain(g);
        }

        private void pnlChessBoard_MouseClick(object sender, MouseEventArgs e)
        {

            if (!caroChess.Ready)
                return;
            caroChess.PlayChess(e.X, e.Y,g);
            if (caroChess.checkwinner())
                caroChess.endgame();
            else
            {
                if (caroChess.GameMode == 2)
                {
                    caroChess.StartComputer(g);
                    if (caroChess.checkwinner())
                        caroChess.endgame();
                }
            }
        }

        private void playerVsPlayerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            g.Clear(pnlChessBoard.BackColor);
            caroChess.StartPvsP(g);
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caroChess.Undo(g);
        }

        private void redoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            caroChess.Redo(g);
        }

        private void playerVsComputerToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            g.Clear(pnlChessBoard.BackColor);
            caroChess.StartPvsCom(g);
        }

        private void playerVsComputerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            MainForm m = new MainForm();
            m.Show();
        }
    }
}
