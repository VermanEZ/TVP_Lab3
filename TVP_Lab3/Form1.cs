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
            this.rand = new Random();
            this.g = this.CreateGraphics();
            for (int i = 0; i < 20; i++)
            {
                int r = rand.Next(2);
                int x = rand.Next(0, this.ClientSize.Width - (r == 0 ? sWidth : sHeight));
                int y = rand.Next(0, this.ClientSize.Height - (r == 0 ? sHeight : sWidth));
                Color randomColor = Color.FromArgb(this.rand.Next(0, 256), this.rand.Next(0, 256), this.rand.Next(0, 256));
                Stripe stripe = new Stripe(new Rectangle(x, y, r == 0 ? sWidth : sHeight, r == 0 ? sHeight: sWidth));
                stripe.SetColor(randomColor);
                this.stripes.Add(stripe);
            }
            this.timer1.Interval = 1000;
            this.timer1.Start();
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
            this.Invalidate();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            int r = rand.Next(2);
            int x = rand.Next(0, this.ClientSize.Width - (r == 0 ? sWidth : sHeight));
            int y = rand.Next(0, this.ClientSize.Height - (r == 0 ? sHeight : sWidth));
            Color randomColor = Color.FromArgb(this.rand.Next(0, 256), this.rand.Next(0, 256), this.rand.Next(0, 256));
            Stripe stripe = new Stripe(new Rectangle(x, y, r == 0 ? sWidth : sHeight, r == 0 ? sHeight : sWidth));
            stripe.SetColor(randomColor);
            this.stripes.Add(stripe);
            this.Invalidate();
        }
    }
}   