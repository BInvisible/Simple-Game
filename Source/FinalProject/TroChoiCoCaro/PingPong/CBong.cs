using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace TroChoiCoCaro
{
    class CBong
    {
        #region "Method"
        private int _Tren;
        private int _Duoi;
        private int _Trai;
        private int _Phai;
        public int vectorX = 3;
        public int vectorY = 4;
        public bool LaVaCham = false;
           
        public int Tren
        {
            get { return _Tren; }
            set { _Tren = value; }
        }
        public int Duoi
        {
            get { return _Duoi; }
            set { _Duoi = value; }
        }
        public int Trai
        {
            get { return _Trai; }
            set { _Trai = value; }
        }
        public int Phai
        {
            get { return _Phai; }
            set { _Phai = value; }
        }
       
        #endregion

        #region "Contructor"
        public CBong()
        {
            this.Tren = 0;
            this.Duoi = 0;
            this.Trai = 0;
            this.Phai = 0;
        }
        public CBong(int tren, int duoi, int trai, int phai)
        {
            this.Tren = tren;
            this.Duoi = duoi;
            this.Trai = trai;
            this.Phai = phai;
        }
        public CBong(CBong Bong)
        {
            this.Tren = Bong.Tren;
            this.Duoi = Bong.Duoi;
            this.Trai = Bong.Trai;
            this.Phai = Bong.Phai;
        }
        #endregion
        #region "Function"

        public void GanToaDo(int l, int r, int t, int b)
        {

            this.Trai = l;
            this.Phai = r;
            this.Tren = t;
            this.Duoi = b;
        }
        public void VeNguoiChoi(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, Trai,Tren, Phai-Trai,Duoi-Tren );
        }
        
        public int DiChuyen(CSanDau SanDau, CNguoiChoi1 P1, CNguoiChoi2 P2)
        {
         
            if (this.Tren <= SanDau.Tren)
                this.vectorY = -this.vectorY;
            if(this.Duoi>=SanDau.Duoi)
                this.vectorY = -this.vectorY;

            if (this.Trai <= SanDau.Trai)
            {
                if (this.Duoi >= P1.Tren && this.Tren <= P1.Duoi)
                {
                    LaVaCham = true;
                    this.vectorX = -this.vectorX;
                }
                else
                    return 1;
            }
            if (this.Phai >= SanDau.Phai)
            {
                if (this.Duoi >= P2.Tren && this.Tren <= P2.Duoi)
                {
                    LaVaCham = true;
                    this.vectorX = -this.vectorX;
                }
                else
                    return 2;
            }
            this.Tren += this.vectorY;
            this.Duoi += this.vectorY;
            this.Trai += this.vectorX;
            this.Phai += this.vectorX;
            return 0;
        }

        public int VaChamNguoiChoi(CSanDau SanDau, CNguoiChoi1 P1, CNguoiChoi2 P2)
        {
            if (this.Trai <= SanDau.Trai)
            {
                if (this.Duoi >= P1.Tren && this.Trai <= P1.Duoi)
                    this.vectorX = -this.vectorX;
                else
                    return 1;
            }
            if (this.Phai >= SanDau.Phai)
            {
                if (this.Duoi >= P2.Tren && this.Tren <= P2.Duoi)
                    this.vectorX = -this.vectorX;
                else
                    return 2;
            }
            return 0;
        }
        #endregion
    }
}
