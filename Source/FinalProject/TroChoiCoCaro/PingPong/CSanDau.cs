using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace TroChoiCoCaro
{
    /// <summary>
    /// Biểu diễn sân đấu
    /// </summary>
    class CSanDau
    {
        #region "Method"
        private int _Tren ;
        private int _Duoi ;
        private int _Trai ;
        private int _Phai ;

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
        public CSanDau()
        {
            this.Tren = 0;
            this.Duoi = 0;
            this.Trai = 0;
            this.Phai = 0;

        }
        public CSanDau(int tren, int duoi, int trai, int phai)
        {
            this.Tren = tren;
            this.Duoi = duoi;
            this.Trai = trai;
            this.Phai = phai;
        }
        public CSanDau(CSanDau temp)
        {
            this.Tren = temp.Tren;
            this.Duoi = temp.Duoi;
            this.Trai = temp.Trai;
            this.Phai = temp.Phai;
        }
        #endregion

        #region "Function"
        public void VeSanDau(Graphics g)
        {
            Pen pen = new Pen(Brushes.White, 2);
            g.DrawRectangle(pen, new Rectangle(this.Trai,this.Tren,this.Phai-this.Trai,this.Duoi-this.Tren));
        }
        public void GanToaDo(int l,int r,int t,int b)
        {
            
            this.Trai = l;
            this.Phai = r;
            this.Tren = t;
            this.Duoi = b;
        }
        #endregion
    }
}
