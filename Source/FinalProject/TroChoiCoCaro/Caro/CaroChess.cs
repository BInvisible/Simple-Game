using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TroChoiCoCaro
{
    public enum END
    {
        Draw,
        Player1,
        Player2,
        Player,
        Computer
    }
    class CaroChess
    {
        private ChessBoard _ChessBoard;
        private ChessPieces[,] _ArrayChessPieces;
        public static Pen pen;
        public static SolidBrush sbRed; 
        public static SolidBrush sbBlue;
        public static SolidBrush sbAC;
        private Stack<ChessPieces> stklistmove;
        private Stack<ChessPieces> stklistundo;
        private int _turn;
        private END _end;
        private bool _Ready;
        private int _GameMode;

        public bool Ready
        {
            get
            {
                return _Ready;
            }

            set
            {
                _Ready = value;
            }
        }

        public int GameMode
        {
            get
            {
                return _GameMode;
            }

            set
            {
                _GameMode = value;
            }
        }

        public CaroChess()
        {
            pen = new Pen(Color.Black);
            sbRed = new SolidBrush(Color.Red);
            sbBlue = new SolidBrush(Color.Blue);
            sbAC = new SolidBrush(Color.Gray);
            _ChessBoard = new ChessBoard(20, 20);
            stklistmove = new Stack<ChessPieces>();
            stklistundo = new Stack<ChessPieces>();
            _ArrayChessPieces = new ChessPieces[_ChessBoard.NumberOfRows, _ChessBoard.NumberOfRows];
            _turn = 1;
        }
        public void DrawChessBoard(Graphics g)
        {
            _ChessBoard.DrawChessBoard(g);
        }
        public void InitArrayChess()
        {
            for (int i = 0; i < _ChessBoard.NumberOfRows; i++)
            {
                for (int j = 0; j < _ChessBoard.NumberOfColumns; j++)
                {
                    _ArrayChessPieces[i, j] = new ChessPieces(i, j, new Point(j * ChessPieces._Width, i * ChessPieces._Height), 0);
                }
            }
        }
        public bool PlayChess(int X, int Y, Graphics g)
        {
            if (X % ChessPieces._Width == 0 || Y % ChessPieces._Height == 0)
                return false;
            int columns = X / ChessPieces._Width;
            int rows = Y / ChessPieces._Height;
            if (_ArrayChessPieces[rows, columns].CurrentPlayer != 0)
                return false;
            switch(_turn)
            {
                case 1:
                    _ArrayChessPieces[rows, columns].CurrentPlayer = 1;
                    _ChessBoard.DrawChessPieces(g, _ArrayChessPieces[rows, columns].Pos, sbRed);
                    _turn = 2;
                    break;
                case 2:
                    _ArrayChessPieces[rows, columns].CurrentPlayer = 2;
                    _ChessBoard.DrawChessPieces(g, _ArrayChessPieces[rows, columns].Pos, sbBlue);
                    _turn = 1;
                    break;
            }
            ChessPieces cp = new ChessPieces(_ArrayChessPieces[rows,columns].Row, _ArrayChessPieces[rows, columns].Column, _ArrayChessPieces[rows, columns].Pos, _ArrayChessPieces[rows, columns].CurrentPlayer);                    
            stklistmove.Push(cp);
            return true;

        }
        public void DrawChessPiecesAgain(Graphics g)
        {
            foreach (ChessPieces cp in stklistmove)
            {
                if (cp.CurrentPlayer == 1)
                {
                    _ChessBoard.DrawChessPieces(g, cp.Pos, sbBlue);
                }
                else if (cp.CurrentPlayer == 2)
                {
                    _ChessBoard.DrawChessPieces(g, cp.Pos, sbRed);
                }
            }
        }
        public void StartPvsP(Graphics g)
        {
            _Ready = true;
            stklistmove = new Stack<ChessPieces>();
            stklistundo = new Stack<ChessPieces>();
            InitArrayChess();
            DrawChessBoard(g);
            _turn = 1;
            GameMode = 1;
        }
        public void StartPvsCom(Graphics g)
        {
            _Ready = true;
            stklistmove = new Stack<ChessPieces>();
            stklistundo = new Stack<ChessPieces>();
            InitArrayChess();
            DrawChessBoard(g);
            _turn = 1;
            GameMode = 2;
            StartComputer(g);
        }
        #region Undo,Redo
        public void Undo(Graphics g)
        {
            if (stklistmove.Count != 0)
            {
                ChessPieces cp = stklistmove.Pop();
                stklistundo.Push(new ChessPieces(cp.Row, cp.Column, cp.Pos, cp.CurrentPlayer));
                stklistundo.Push(cp);
                _ArrayChessPieces[cp.Row, cp.Column].CurrentPlayer = 0;
                _ChessBoard.DeleteChessPieces(g, cp.Pos, sbAC);
                if (_turn == 1)
                    _turn = 2;
                else
                    _turn = 1;
            }
        }
        public void Redo(Graphics g)
        {
            if (stklistundo.Count != 0)
            {
                ChessPieces cp = stklistundo.Pop();
                stklistmove.Push(new ChessPieces(cp.Row, cp.Column, cp.Pos, cp.CurrentPlayer));
                _ArrayChessPieces[cp.Row, cp.Column].CurrentPlayer = cp.CurrentPlayer;
                _ChessBoard.DrawChessPieces(g, cp.Pos, cp.CurrentPlayer==1?sbRed:sbBlue);
                if (_turn == 1)
                    _turn = 2;
                else
                    _turn = 1;
            } 
        }
        #endregion
        #region Check Winner
        public void endgame()
        {
            switch(_end)
            {
                case END.Draw:
                    MessageBox.Show("Draw!");
                    break;
                case END.Player1:
                    MessageBox.Show("Player 1 win!");
                    break;
                case END.Player2:
                    MessageBox.Show("Player 2 win!");
                    break;
                case END.Computer:
                    MessageBox.Show("Computer win!");
                    break;
                case END.Player:
                    MessageBox.Show("Player win!");
                    break;
            }
            _Ready = false;
        }
        public bool checkwinner()
        {
            if(stklistmove.Count==_ChessBoard.NumberOfColumns*_ChessBoard.NumberOfRows)
            {
                _end = END.Draw;
                return true;
            }
            foreach(ChessPieces cp in stklistmove)
            {
                if(CheckVertical(cp.Row,cp.Column,cp.CurrentPlayer)|| CheckHorizontal(cp.Row, cp.Column, cp.CurrentPlayer)|| CheckOverCross1(cp.Row, cp.Column, cp.CurrentPlayer) || CheckOverCross2(cp.Row, cp.Column, cp.CurrentPlayer))
                {
                    _end = cp.CurrentPlayer == 1 ? END.Player1 : END.Player2;
                    if(GameMode==2)
                    {
                        _end = cp.CurrentPlayer == 1 ? END.Computer : END.Player;
                    }
                    return true;
                }
            }
            return false;
        }
        private bool CheckVertical(int currRow,int currColumn,int currPlayer)
        {
            int count;
            if (currRow > _ChessBoard.NumberOfRows - 5)
                return false;
            for(count = 1; count < 5; count++)
            {
                if (_ArrayChessPieces[currRow + count, currColumn].CurrentPlayer != currPlayer)
                    return false;
            }
            if (currRow == 0||currRow+count==_ChessBoard.NumberOfRows)
                return true;
            if (_ArrayChessPieces[currRow - 1, currColumn].CurrentPlayer == 0||_ArrayChessPieces[currRow+count,currColumn].CurrentPlayer ==0)
                return true;
            return false;
        }
        private bool CheckHorizontal(int currRow, int currColumn, int currPlayer)
        {
            int count;
            if (currColumn > _ChessBoard.NumberOfRows - 5)
                return false;
            for (count = 1; count < 5; count++)
            {
                if (_ArrayChessPieces[currRow, currColumn+count].CurrentPlayer != currPlayer)
                    return false;
            }
            if (currColumn == 0 || currColumn + count == _ChessBoard.NumberOfColumns)
                return true;
            if (_ArrayChessPieces[currRow, currColumn-1].CurrentPlayer == 0 || _ArrayChessPieces[currRow, currColumn+count].CurrentPlayer == 0)
                return true;
            return false;
        }
        private bool CheckOverCross1(int currRow, int currColumn, int currPlayer)
        {
            int count;
            if (currRow > _ChessBoard.NumberOfRows - 5||currColumn>_ChessBoard.NumberOfColumns-5)
                return false;
            for (count = 1; count < 5; count++)
            {
                if (_ArrayChessPieces[currRow+count, currColumn + count].CurrentPlayer != currPlayer)
                    return false;
            }
            if (currRow==0||currRow+count==_ChessBoard.NumberOfRows|| currColumn == 0 || currColumn + count == _ChessBoard.NumberOfColumns)
                return true;
            if (_ArrayChessPieces[currRow-1, currColumn - 1].CurrentPlayer == 0 || _ArrayChessPieces[currRow+count, currColumn + count].CurrentPlayer == 0)
                return true;
            return false;
        }
        private bool CheckOverCross2(int currRow, int currColumn, int currPlayer)
        {
            int count;
            if (currRow<4||currColumn > _ChessBoard.NumberOfRows - 5)
                return false;
            for (count = 1; count < 5; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn + count].CurrentPlayer != currPlayer)
                    return false;
            }
            if (currRow==4||currRow==_ChessBoard.NumberOfRows-1||currColumn==0||currColumn+count==_ChessBoard.NumberOfColumns)
                return true;
            if (_ArrayChessPieces[currRow+1, currColumn - 1].CurrentPlayer == 0 || _ArrayChessPieces[currRow-count, currColumn + count].CurrentPlayer == 0)
                return true;
            return false;
        }

        #endregion
        #region AI

        private long[] ArrayAttack = new long[7] { 0, 3, 24, 192, 1536, 12288, 98304 };
        private long[] ArrayDefence = new long[7] { 0, 1, 9, 81, 729, 6561, 59049 };
        public void StartComputer(Graphics g)
        {
            if(stklistmove.Count==0)
            {
                PlayChess(_ChessBoard.NumberOfColumns / 2 * ChessPieces._Width + 1, _ChessBoard.NumberOfRows / 2 * ChessPieces._Height + 1, g);
            }
            else
            {
                ChessPieces cp = findmove();
                PlayChess(cp.Pos.X + 1, cp.Pos.Y + 1, g);
            }
        }

        private ChessPieces findmove()
        {
            ChessPieces cp = new ChessPieces();
            long max = 0;
            for(int i = 0;i<_ChessBoard.NumberOfRows;i++)
            {
                for(int j=0;j<_ChessBoard.NumberOfColumns;j++)
                {
                    if(_ArrayChessPieces[i,j].CurrentPlayer==0)
                    {
                        long attackPoint=DiemTC_CheckVertical(i,j)+DiemTC_CheckHorizontal(i, j) +DiemTC_CheckOverCross1(i, j) +DiemTC_CheckOverCross2(i, j);
                        long attackDefence= DiemPN_CheckVertical(i, j) + DiemPN_CheckHorizontal(i, j) + DiemPN_CheckOverCross1(i, j) + DiemPN_CheckOverCross2(i, j);
                        long temp = attackPoint > attackDefence ? attackPoint : attackDefence;
                        if (max < temp)
                        {
                            max = temp;
                            cp = new ChessPieces(_ArrayChessPieces[i, j].Row, _ArrayChessPieces[i, j].Column, _ArrayChessPieces[i, j].Pos, _ArrayChessPieces[i, j].CurrentPlayer);

                        }
                    }
                }
            }
            return cp;
        }
        #region Defence
        private long DiemPN_CheckOverCross2(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns && currRow + count < _ChessBoard.NumberOfRows; count++)
            {
                if (_ArrayChessPieces[currRow + count, currColumn + count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow + count, currColumn + count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0 && currRow - count >= 0; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn - count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += ArrayDefence[SoQuanDich];
            return DiemTong;
        }

        private long DiemPN_CheckOverCross1(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns && currRow - count >= 0; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn + count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn + count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0 && currRow + count < _ChessBoard.NumberOfRows; count++)
            {
                if (_ArrayChessPieces[currRow + count, currColumn - count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow + count, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += ArrayDefence[SoQuanDich];
            return DiemTong;
        }

        private long DiemPN_CheckHorizontal(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns; count++)
            {
                if (_ArrayChessPieces[currRow, currColumn + count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow, currColumn + count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (_ArrayChessPieces[currRow, currColumn - count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += ArrayDefence[SoQuanDich];
            return DiemTong;
        }

        private long DiemPN_CheckVertical(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currRow + count < _ChessBoard.NumberOfRows; count++)
            {
                if (_ArrayChessPieces[currRow + count, currColumn].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow + count, currColumn].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >= 0; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                    break;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanTa == 2)
                return 0;
            DiemTong += ArrayDefence[SoQuanDich];
            return DiemTong;
        }
        #endregion
        #region Attack
        private long DiemTC_CheckOverCross2(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns && currRow + count <_ChessBoard.NumberOfRows; count++)
            {
                if (_ArrayChessPieces[currRow + count, currColumn + count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow + count, currColumn + count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0 && currRow - count >=0; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn - count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= ArrayDefence[SoQuanDich + 1];
            DiemTong += ArrayAttack[SoQuanTa];
            return DiemTong;
        }

        private long DiemTC_CheckOverCross1(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns&&currRow-count>=0; count++)
            {
                if (_ArrayChessPieces[currRow-count, currColumn + count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn + count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0&&currRow+count<_ChessBoard.NumberOfRows; count++)
            {
                if (_ArrayChessPieces[currRow+count, currColumn - count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow+count, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= ArrayDefence[SoQuanDich + 1];
            DiemTong += ArrayAttack[SoQuanTa];
            return DiemTong;
        }

        private long DiemTC_CheckHorizontal(int currRow, int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for (int count = 1; count < 6 && currColumn + count < _ChessBoard.NumberOfColumns; count++)
            {
                if (_ArrayChessPieces[currRow , currColumn+count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow, currColumn+count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currColumn - count >= 0; count++)
            {
                if (_ArrayChessPieces[currRow, currColumn-count].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow, currColumn - count].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= ArrayDefence[SoQuanDich + 1];
            DiemTong += ArrayAttack[SoQuanTa];
            return DiemTong;
        }

        private long DiemTC_CheckVertical(int currRow,int currColumn)
        {
            long DiemTong = 0;
            int SoQuanTa = 0;
            int SoQuanDich = 0;
            for(int count=1;count<6&&currRow+count<_ChessBoard.NumberOfRows;count++)
            {
                if(_ArrayChessPieces[currRow+count,currColumn].CurrentPlayer==1)
                {
                    SoQuanTa++;
                }
                else if(_ArrayChessPieces[currRow+count,currColumn].CurrentPlayer==2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            for (int count = 1; count < 6 && currRow - count >=0 ; count++)
            {
                if (_ArrayChessPieces[currRow - count, currColumn].CurrentPlayer == 1)
                {
                    SoQuanTa++;
                }
                else if (_ArrayChessPieces[currRow - count, currColumn].CurrentPlayer == 2)
                {
                    SoQuanDich++;
                    break;
                }
                else
                {
                    break;
                }
            }
            if (SoQuanDich == 2)
                return 0;
            DiemTong -= ArrayDefence[SoQuanDich+1];
            DiemTong += ArrayAttack[SoQuanTa];
            return DiemTong;
        }
        #endregion
        #endregion
    }
}
