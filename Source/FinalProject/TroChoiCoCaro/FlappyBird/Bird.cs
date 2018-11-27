using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiCoCaro
{
    class Bird
    {
        private Bitmap bird;
        private PointF pos;
        private float birdspeed;
        private int totalframe;
        private int currentframe;
        private int birdWidth;
        private int birdHeight;

        private float gravity;

        private bool IsDead;

        private float flapSpeedY;

        public Bitmap Bird1
        {
            get
            {
                return bird;
            }

            set
            {
                bird = value;
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

        public float Birdspeed
        {
            get
            {
                return birdspeed;
            }

            set
            {
                birdspeed = value;
            }
        }

        public int Totalframe
        {
            get
            {
                return totalframe;
            }

            set
            {
                totalframe = value;
            }
        }

        public int Currentframe
        {
            get
            {
                return currentframe;
            }

            set
            {
                currentframe = value;
            }
        }

        public float Gravity
        {
            get
            {
                return gravity;
            }

            set
            {
                gravity = value;
            }
        }

        public bool IsDead1
        {
            get
            {
                return IsDead;
            }

            set
            {
                IsDead = value;
            }
        }

        public Bird(Bitmap BirdImg)
        {
            this.Bird1 = BirdImg;
            this.Pos = new PointF(Game.gameSize.Width / 2.0f, Game.gameSize.Height / 2.0f);
            this.Birdspeed = 0;
            this.Totalframe = 3;
            this.Currentframe = 0;

            this.birdWidth = 40;
            this.birdHeight = 20;

            float ratio = this.Bird1.Height / (this.Bird1.Width / 3.0f);
            this.birdHeight = (int)(this.birdWidth * ratio);

            this.Gravity = 2.5f;

            this.flapSpeedY = -10.0f;

            this.IsDead = false;
        }

        public bool IsBirdHitGround()
        {
            if((this.Pos.Y+this.birdHeight)>=(Game.gameSize.Height-Game.gameGroundHeight))
            {
                return true;
            }
            return false;
        }

        public void flap()
        {
            if (this.IsDead) return;
            this.Birdspeed = flapSpeedY;
            if(Pos.Y-bird.Height/2<=0)
            {
                pos.Y = bird.Height / 2;
                this.Birdspeed = gravity;
            }
        }

        public void die()
        {
            this.IsDead1 = true;
        }
        public void update()
        {
            if (this.IsDead) return;
            updateAnimation();
            this.pos.Y += this.Birdspeed;
            this.Birdspeed += this.Gravity;

            if (this.IsBirdHitGround())
            {
                this.pos.Y = Game.gameSize.Height - Game.gameGroundHeight - this.birdHeight / 2;
                this.IsDead = true;
            }
        }

        public void updateAnimation()
        {
            Currentframe += 1;
            if (Currentframe >= Totalframe)
                Currentframe = 0;
        }

        public RectangleF getBoundingBox()
        {
            return new RectangleF(this.pos.X - this.birdWidth / 2, this.pos.Y - this.birdHeight / 2, this.birdWidth, this.birdHeight);
        }
        public void Draw(Graphics g)
        {
            RectangleF destRect = new RectangleF(this.pos.X-this.birdWidth/2, this.pos.Y-this.birdHeight/2,this.birdWidth,this.birdHeight);

            RectangleF srcRect = new RectangleF(this.currentframe*(this.Bird1.Width/3.0f), 0, this.Bird1.Width/3, this.Bird1.Height);

            GraphicsUnit units = GraphicsUnit.Pixel;

            g.DrawImage(this.Bird1, destRect, srcRect, units);

        }
    }
}
