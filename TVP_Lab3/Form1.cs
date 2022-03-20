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
        private List<Stripe> stripes = new List<Stripe> { };
        private Random rand = new Random();
        private const int sWidth = 200;
        private const int sHeight = 50;
        private Graphics g;
        private int lossCount;
        private int level = 0;
        private int timerInterval = 1000;
        private bool gameOver = false;

        const int STRIPES_COUNT = 10;
        const int LOSS_COUNT = 20;
        public Form1()
        {
            InitializeComponent();
            this.g = this.CreateGraphics();
            this.StartGame(STRIPES_COUNT, LOSS_COUNT);
        }

        private void StartGame(int numberOfStripes, int lossCount)
        {
            this.Invalidate();
            this.lossCount = lossCount;
            this.gameOver = false;
            this.level = 0;
            this.timerInterval = 1000;
            this.CreateStripes(numberOfStripes);
            this.NextLevel();
        }

        private void CreateStripes(int numberOfStripes)
        {
            for (int i = 0; i < numberOfStripes; i++)
            {
                int r = rand.Next(2);
                int x = rand.Next(0, this.ClientSize.Width - (r == 0 ? sWidth : sHeight));
                int y = rand.Next(0, this.ClientSize.Height - (r == 0 ? sHeight : sWidth));
                Color randomColor = Color.FromArgb(this.rand.Next(0, 256), this.rand.Next(0, 256), this.rand.Next(0, 256));
                Stripe stripe = new Stripe(new Rectangle(x, y, r == 0 ? sWidth : sHeight, r == 0 ? sHeight : sWidth));
                stripe.SetColor(randomColor);
                this.stripes.Add(stripe);
            }
        }

        private void NextLevel()
        {
            this.level += 1;
            this.timerInterval = (int)(this.timerInterval - this.timerInterval / 10.0);
            this.timer1.Interval = this.timerInterval;
            this.timer1.Start();
        }

        private void GameOver()
        {
            this.stripes.Clear();
            this.timer1.Stop();
            this.gameOver = true;
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Center;
            if (this.gameOver)
            {
                stringFormat.LineAlignment = StringAlignment.Center;
                this.g.DrawString($"Game Over", new Font("arial", 30), Brushes.Black, new Rectangle(new Point(0, 0), this.Size), stringFormat);
                this.g.DrawString($"You reached {this.level} level", new Font("arial", 20), Brushes.Black, new Rectangle(new Point(0, 0), this.Size + new Size(0, 60)), stringFormat);
                
                // StarOverButton Creation
                Button startOverButton = new Button();
                startOverButton.Size = new Size(100, 50);
                startOverButton.Location = new Point((this.Width - startOverButton.Width) / 2, 0);
                startOverButton.BackColor = Color.White;
                startOverButton.ForeColor = Color.Black;
                startOverButton.Text = "Start over";
                startOverButton.Click += new EventHandler(this.StartOverButtonClick);
                this.Controls.Add(startOverButton);
                //
                return;
            }
            foreach (var stripe in this.stripes)
            {
                stripe.Draw(this.g);
            }
            this.g.DrawString($"{this.level} level", new Font("arial", 20), Brushes.Black, new Point(20, 20));
            this.g.DrawString($"{this.stripes.Count} / {this.lossCount}", new Font("arial", 20), Brushes.Black, new Rectangle(new Point(0, 0), this.Size), stringFormat);
        }

        private void StartOverButtonClick(object sender, EventArgs e)
        {
            this.Controls.Clear();
            this.StartGame(STRIPES_COUNT, LOSS_COUNT);
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
            if (this.stripes.Count == 0)
            {
                this.CreateStripes(10);
                this.NextLevel();
                return;
            }
            if (this.stripes.Count >= this.lossCount)
            {
                this.GameOver();
            }
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