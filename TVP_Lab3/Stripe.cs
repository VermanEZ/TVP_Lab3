using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace TVP_Lab3
{
    class Stripe
    {
        private Rectangle rect;
        private Color color;
        public Stripe(Rectangle rectangle)
        {
            this.rect = rectangle;
            this.color = Color.Gold;
        }

        public void Draw(Graphics graphics)
        {
            graphics.FillRectangle(new SolidBrush(this.color), this.rect);
            graphics.DrawRectangle(Pens.Black, this.rect);
        }

        public void Move(Point pos)
        {
            this.rect.Location = pos;
        }

        public bool IsInside(Point cursorPos)
        {
            return this.rect.Contains(cursorPos);
        }

        public bool Intersect(Stripe stripe)
        {
            return this.rect.IntersectsWith(stripe.GetRectangle());
        }

        public void Rotate()
        {
            (this.rect.Width, this.rect.Height) = (this.rect.Height, this.rect.Width);
        }

        public Rectangle GetRectangle()
        {
            return this.rect;
        }

        public Color GetColor()
        {
            return this.color;
        }

        public void SetColor(Color color)
        {
            this.color = color;
        }
    }
}