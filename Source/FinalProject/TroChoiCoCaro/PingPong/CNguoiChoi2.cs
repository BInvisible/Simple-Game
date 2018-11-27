using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TroChoiCoCaro
{
    class CNguoiChoi2
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
        public CNguoiChoi2()
        {
            this.Tren = 0;
            this.Duoi = 0;
            this.Trai = 0;
            this.Phai = 0;

        }
        public CNguoiChoi2(int tren, int duoi, int trai, int phai)
        {
            this.Tren = tren;
            this.Duoi = duoi;
            this.Trai = trai;
            this.Phai = phai;
        }
        public CNguoiChoi2(CNguoiChoi2 P)
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
            g.FillRectangle(Brushes.Red, new Rectangle(this.Trai, this.Tren, this.Phai - this.Trai, this.Duoi - this.Tren));
            //Pen pen = new Pen(Brushes.White, 2);
            //g.DrawRectangle(pen, new Rectangle(this.Trai, this.Tren, this.Phai - this.Trai, this.Duoi - this.Tren));
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
