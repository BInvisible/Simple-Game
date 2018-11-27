using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TroChoiCoCaro
{
    class Pipe:GameObject
    {
        public static float pipeWith=50.0f ;

        private Bitmap top;
        private RectangleF topRect;

        private Bitmap bottom;
        private RectangleF bottomRect;

        private bool Score;

        public RectangleF TopRect
        {
            get
            {
                return topRect;
            }

            set
            {
                topRect = value;
            }
        }

        public RectangleF BottomRect
        {
            get
            {
                return bottomRect;
            }

            set
            {
                bottomRect = value;
            }
        }
        public Pipe(PointF pos, Bitmap topPipe, Bitmap bottomPipe)
        {
            this.Score = false;

            this.top = topPipe;
            this.bottom = bottomPipe;

            float midPointofTwoPipe = pos.Y;
            topRect = new RectangleF(pos.X, 0, pipeWith, midPointofTwoPipe - Game.pipeSpread / 2.0f);

            float bottomRectY = midPointofTwoPipe + Game.pipeSpread / 2.0f;
            bottomRect = new RectangleF(pos.X, bottomRectY, pipeWith, Game.gameSize.Height - (midPointofTwoPipe-Game.pipeSpread/2.0f)-Game.pipeSpread-Game.gameGroundHeight);
        }

        public void Scroll(float x)
        {
            topRect.X -=x;
            bottomRect.X = TopRect.X;
        }

        public bool collides(Bird bird)
        {
            RectangleF birdBoundingBox = bird.getBoundingBox();
            if (this.CheckColillisionTwoRect(birdBoundingBox, this.TopRect) || this.CheckColillisionTwoRect(birdBoundingBox, this.BottomRect))
                return true;
            else
                return false;
        }

        public bool IsScore(Bird bird)
        {
            if (Score) return false;

            bool inside = bird.getBoundingBox().X >= TopRect.X;
            if(inside)
            {
                Score = true;
            }
            return inside;
        }

        public bool offScreen()
        {
            return (this.TopRect.Right <= 0);
        }

        public void Draw(Graphics g)
        {
            g.DrawImage(this.top, this.TopRect);
            g.DrawImage(this.bottom, this.BottomRect);
        }
    }
}
