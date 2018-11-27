using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TroChoiCoCaro
{
    class CNguoiChoi1
    {
         #region "Method"
        private int _Tren;
        private int _Duoi;
        private int _Trai;
        private int _Phai;
        
        

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
        public CNguoiChoi1()
        {
            this.Tren = 1;
            this.Duoi = 1;
            this.Trai = 1;
            this.Phai = 1;

        }
        public CNguoiChoi1(int tren, int duoi, int trai, int phai)
        {
            this.Tren = tren;
            this.Duoi = duoi;
            this.Trai = trai;
            this.Phai = phai;
        }
        public CNguoiChoi1(CNguoiChoi1 P)
        {
            this.Tren = P.Tren;
            this.Duoi = P.Duoi;
            this.Trai = P.Trai;
            this.Phai = P.Phai;
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
            g.FillRectangle(Brushes.Blue,new Rectangle(this.Trai, this.Tren, this.Phai - this.Trai, this.Duoi - this.Tren));
            //Pen pen = new Pen(Brushes.White, 2);
            //g.DrawRectangle(pen,new Rectangle(this.Trai, this.Tren, this.Phai - this.Trai, this.Duoi - this.Tren));
        }

        public void DiChuyenLen(CSanDau SanDau)
        {

            if (this.Tren > SanDau.Tren)
            {
                this.Tren = this.Tren - 15;
                this.Duoi = this.Duoi - 15;
            }
        }
        public void DiChuyenXuong(CSanDau SanDau)
        {
            if (this.Duoi < SanDau.Duoi)
            {
                this.Tren = this.Tren + 15;
                this.Duoi = this.Duoi + 15;
            }
        }
        #endregion
    }
}
