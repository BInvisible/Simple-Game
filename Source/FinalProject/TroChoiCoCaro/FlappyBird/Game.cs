using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace TroChoiCoCaro
{
    internal class Game
    {
        public static RectangleF gameSize;
        public static float gameGroundHeight;
        public static int pipeSpread=75;
        public static float pipeDistance = 120.0f;
        public static float scrollSpeed = 10.0f;
      

        ScrollBackground ground;
        ScrollBackground backGround;
        private string gameResourcePath;
        private float groundHeight;
        private Bird bird;
        private Bitmap pipeTop;
        private Bitmap pipeBottom;

        private float distance;
        private int score;
        private bool isGameOver;
        private bool isGameStart;

        private float distanceTotheFirstPipe;
        private float lastPipe;

        List<Pipe> pipes;
        private Random rand;
        public Game()
        {
            this.gameResourcePath = System.IO.Path.GetDirectoryName(System.Windows.Forms.Application.ExecutablePath) + "\\Resources\\";
            this.groundHeight = 80;
            ground = new ScrollBackground();
            backGround = new ScrollBackground();
            
            ground.Background = (Bitmap)Image.FromFile(this.gameResourcePath + "ground.jpg");
            ground.Height = this.groundHeight;
            ground.Pos = new PointF(0, Game.gameSize.Height - this.groundHeight);
            ground.ScrollingSpeed = 10.0f;

            Game.gameGroundHeight = ground.Height;

            backGround.Background = (Bitmap)Image.FromFile(this.gameResourcePath + "background.jpg");
            backGround.Height = Game.gameSize.Height - this.groundHeight;
            backGround.Pos = new PointF(0, 0);
            backGround.ScrollingSpeed = 5.0f;

            this.pipeTop = (Bitmap)Image.FromFile(this.gameResourcePath + "pipetop.png");
            this.pipeBottom = (Bitmap)Image.FromFile(this.gameResourcePath + "pipebottom.png");


            this.reset();
        }

        public void reset()
        {

          
            Bitmap BirdImg = (Bitmap)Image.FromFile(this.gameResourcePath + "bird.png");
            bird = new Bird(BirdImg);

            this.pipes = new List<Pipe>();

            this.rand = new Random();

            this.distance = 0.0f;
            this.distanceTotheFirstPipe = gameSize.Width * 2;

            this.score = 0;
            this.isGameOver = false;
            this.isGameStart = true;

            addPipe(distanceTotheFirstPipe);
            this.lastPipe = distanceTotheFirstPipe;
        }

        void drawStringCenteratRect(Graphics g,string drawString,float drawRectHeight)
        {
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            sf.Alignment = StringAlignment.Center;

            Font drawFont = new Font("Arial", 30);
            SolidBrush drawBrush = new SolidBrush(Color.Red);

            RectangleF drawRect = new RectangleF(0.0f, 0.0f, gameSize.Width, drawRectHeight);
            g.DrawString(drawString, drawFont, drawBrush, drawRect, sf);
        }

        private void addPipe(float posX)
        {
            float x, y;

            x = posX;


            float actualGameAreaIgnoringGround = Game.gameSize.Height - this.groundHeight;

            int topPipeHeightMin = (int)(actualGameAreaIgnoringGround*0.3f );
            int bottomPipHeightMin = (int)topPipeHeightMin;
            int topPipeHeightMax = (int)(actualGameAreaIgnoringGround - Game.pipeSpread - bottomPipHeightMin);

            int topPipeHeight = rand.Next(topPipeHeightMax + 1 - topPipeHeightMin) + topPipeHeightMin;

            y = topPipeHeight + Game.pipeSpread / 2.0f;


            Pipe pipe = new Pipe(new PointF(x, y), this.pipeTop, this.pipeBottom);
            pipes.Add(pipe);
        }

        internal void handleMouseClick()
        {
            if(this.isGameOver)
            {
                this.reset();
            }
            else if(this.isGameStart)
            {
                this.isGameStart = false;
            }
            else
            {
                bird.flap();
            }
        }

        public void Update()
        {
            if(this.isGameStart)
            {
                return;
            }
            else if(bird.IsDead1)
            {
                this.isGameOver = true;
                return;
            }

            ground.update();
            backGround.update();
            bird.update();

            foreach (Pipe pipeItem in pipes.ToList())
            {
                pipeItem.Scroll(Game.scrollSpeed);

                if (pipeItem.IsScore(bird))
                {
                    ++score;
                    if(score==10)
                    {
                        ground.ScrollingSpeed = 15.0f;
                        backGround.ScrollingSpeed = 10.0f;
                        scrollSpeed = scrollSpeed + 2.0f;
                        
                   
                    }
                }

                if (pipeItem.collides(bird))
                {
                    bird.die();
                }

                if (pipeItem.offScreen())
                {
                    pipes.Remove(pipeItem);
                }
            }
                distance += Game.scrollSpeed;
                float xMaxAfterScroll = distance + Game.scrollSpeed + gameSize.Width;

                while(xMaxAfterScroll>=lastPipe)
                {
                    this.addPipe((this.lastPipe + Pipe.pipeWith + pipeDistance) - distance);
                    this.lastPipe += (pipeDistance + Pipe.pipeWith);
                }
        }
       
        public void Draw(Graphics g)
        {
            ground.Draw(g);
            backGround.Draw(g);
            bird.Draw(g);
            foreach (Pipe pipeItem in pipes.ToList())
            {
                pipeItem.Draw(g);
            }

            this.drawStringCenteratRect(g, this.score + "", 100);
            if(this.isGameOver)
            {
                this.drawStringCenteratRect(g, "Game over!!!", gameSize.Height);
            }
            if(this.isGameStart)
            {
                this.drawStringCenteratRect(g, "Click to start!!!", gameSize.Height - 80.0f);
            }
        }

       
    }
}