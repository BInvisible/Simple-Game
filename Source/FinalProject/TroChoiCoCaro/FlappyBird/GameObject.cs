﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace TroChoiCoCaro
{
    public abstract class GameObject
    {
        private PointF pos;

        protected PointF Pos
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
        public bool CheckColillisionTwoRect(RectangleF rect1, RectangleF rect2)
        {
            if (rect1.X < rect2.X + rect2.Width &&
                rect1.X + rect1.Width > rect2.X &&
                rect1.Y < rect2.Y + rect2.Height &&
                rect1.Height + rect1.Y > rect2.Y)
                return true;
            else
                return false;
        }
    }
}
