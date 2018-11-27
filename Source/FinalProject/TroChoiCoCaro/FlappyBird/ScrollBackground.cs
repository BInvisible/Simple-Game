using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiCoCaro
{
    class ScrollBackground
    {
        private Bitmap background;
        private float height;
        private float scrollingSpeed;

        private PointF pos;

        public Bitmap Background
        {
            get
            {
                return background;
            }

            set
            {
                background = value;
            }
        }

        public float ScrollingSpeed
        {
            get
            {
                return scrollingSpeed;
            }

            set
            {
                scrollingSpeed = value;
            }
        }

        public float Height
        {
            get
            {
                return height;
            }

            set
            {
                height = value;
            }
        }

        public PointF Pos
        {
            get
            {
                return pos;
            }

            set
            {
                pos = value;
            }
        }

        public ScrollBackground()
        {
            Pos = new PointF(0, 0);
            ScrollingSpeed = 10.0f;
        }

        public ScrollBackground(float height)
        {
            this.Height = height;
        }

        public ScrollBackground(PointF pos)
        {
            this.Pos = pos;
        }

        public void update()
        {
            this.pos.X += this.ScrollingSpeed;
            if((Pos.X+Game.gameSize.Width)>this.Background.Width)
            {
                this.pos.X = 0;
            }
        }
        public void Draw(Graphics g)
        {
            RectangleF destRect = new RectangleF(0, this.Pos.Y, Game.gameSize.Width, this.Height);

            RectangleF srcRect = new RectangleF(this.Pos.X,0, Game.gameSize.Width, Background.Height);
            
            GraphicsUnit units = GraphicsUnit.Pixel;

            g.DrawImage(this.Background, destRect, srcRect, units);
        }
    }
}
