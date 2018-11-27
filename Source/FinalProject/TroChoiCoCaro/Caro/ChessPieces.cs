using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiCoCaro
{
    class ChessPieces
    {
        public const int _Width= 25;
        public const int _Height = 25;

        private int _Row;
        private int _Column;
        private Point _Pos;
        private int _CurrentPlayer;

        public int Column
        {
            get
            {
                return _Column;
            }

            set
            {
                _Column = value;
            }
        }

        public int Row
        {
            get
            {
                return _Row;
            }

            set
            {
                _Row = value;
            }
        }

        public Point Pos
        {
            get
            {
                return _Pos;
            }

            set
            {
                _Pos = value;
            }
        }
        public ChessPieces(int rows,int columns,Point pos,int currentPlayer)
        {
            _Row = rows;
            _Column = columns;
            _Pos = pos;
            _CurrentPlayer = currentPlayer;

        }
        public ChessPieces()
        {

        }
        public int CurrentPlayer
        {
            get
            {
                return _CurrentPlayer;
            }

            set
            {
                _CurrentPlayer = value;
            }
        }
    }
}
