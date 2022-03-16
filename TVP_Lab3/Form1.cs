using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TVP_Lab3
{
    public partial class Form1 : Form
    {
        private List<Stripe> stripes;
        private Random rand;
        private const int sWidth = 200;
        private const int sHeight = 50;
        private Graphics g;
        public Form1()
        {
            InitializeComponent();
            this.stripes = new List<Stripe> { };
            this.g = this.CreateGraphics();
            rand = new Random();
            for (int i = 0; i < 20; i++)
            {
                int x = rand.Next(0, this.ClientSize.Width - sWidth);
                int y = rand.Next(0, this.ClientSize.Height - sHeight);
                Color randomColor = Color.FromArgb(this.rand.Next(0, 256), this.rand.Next(0, 256), this.rand.Next(0, 256));
                Stripe stripe = new Stripe(new Rectangle(x, y, sWidth, sHeight));
                stripe.SetColor(randomColor);
                if (x % 2 == 0) stripe.Rotate();
                this.stripes.Add(stripe);
            }
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            foreach (var stripe in this.stripes)
            {
                stripe.Draw(this.g);
            }
        }

        private void Form1_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                for (int i = this.stripes.Count - 1; i >= 0; i--)
                {
                    if (this.stripes[i].IsInside(e.Location))
                    {
                        bool ok = true;
                        for (int j = i + 1; j < this.stripes.Count; j++)
                        {
                            if (this.stripes[j].Intersect(this.stripes[i]))
                            {
                                ok = false;
                            }
                        }
                        if (ok)
                        {
                            this.stripes.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            else if (e.Button == MouseButtons.Right)
            {
                for (int i = this.stripes.Count - 1; i >= 0; i--)
                {
                    if (this.stripes[i].IsInside(e.Location))
                    {
                        this.stripes[i].Rotate();
                        break;
                    }
                }
            }
            this.Invalidate();
        }
    }
}   