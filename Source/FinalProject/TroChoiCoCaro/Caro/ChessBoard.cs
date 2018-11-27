using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiCoCaro
{
    class ChessBoard
    {
        private int _NumberOfRows;
        private int _NumberOfColumns;

        public int NumberOfRows
        {
            get
            {
                return _NumberOfRows;
            }

            set
            {
                _NumberOfRows = value;
            }
        }

        public int NumberOfColumns
        {
            get
            {
                return _NumberOfColumns;
            }

            set
            {
                _NumberOfColumns = value;
            }
        }
        public ChessBoard()
        {
            _NumberOfRows = 0;
            _NumberOfColumns = 0;
        }
        public ChessBoard(int numberOfRows,int numberOfColumns)
        {
            _NumberOfColumns = numberOfColumns;
            _NumberOfRows = numberOfRows;
        }
        public void DrawChessBoard(Graphics g)
        {
            for (int i=0;i<=_NumberOfColumns;i++)
            {
                g.DrawLine(CaroChess.pen, i * ChessPieces._Width,0, i * ChessPieces._Width, _NumberOfRows * ChessPieces._Width);
            }   
            for (int j = 0; j <= _NumberOfRows; j++)
            {
                g.DrawLine(CaroChess.pen, 0,j*ChessPieces._Height,_NumberOfColumns*ChessPieces._Width,j*ChessPieces._Height);
            }
        }
        public void DrawChessPieces(Graphics g,Point pos,SolidBrush sb)
        {
            g.FillEllipse(sb, pos.X+1, pos.Y+1, ChessPieces._Width-2, ChessPieces._Height-2);
        }
        public void DeleteChessPieces(Graphics g, Point pos,SolidBrush sb)
        {
            g.FillRectangle(sb, pos.X+1, pos.Y+1, ChessPieces._Width-2, ChessPieces._Height-2);
        }
    }
}
