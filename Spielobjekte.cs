using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows;

namespace Asteroids_T22_B
{
    abstract class Spielobjekte
    {
        public double x { get; private set; }
        public double y { get; private set; }

        protected double vx, vy;

        public Spielobjekte(double x0, double y, double vx, double vy)
        {
            x = x0;
            this.y = y;
            this.vy = vy;
            this.vx = vx;
        }

        abstract public void Draw(Canvas Zeichenfläche);

        public void Move(TimeSpan t, Canvas Zeichenfläche)
        {
            x += vx * t.TotalSeconds;
            y += vy * t.TotalSeconds;

            if (x < 0) x = Zeichenfläche.ActualWidth;
            if (x > Zeichenfläche.ActualWidth) x = 0;
            if (y < 0) y = Zeichenfläche.ActualHeight;
            if (y > Zeichenfläche.ActualHeight) y = 0;
        }

    }

    class Asteroid : Spielobjekte
    {
        static Random rnd = new Random();
        Polygon umriss;
        public Asteroid(Canvas Zeichenfläche)
            : base (rnd.NextDouble()*Zeichenfläche.ActualWidth,
                    rnd.NextDouble()*Zeichenfläche.ActualHeight,
                    rnd.NextDouble()*200-100,
                    rnd.NextDouble()*200-100)
        {
            umriss = new Polygon();
            for (int i = 0; i < 17; i++)
            {
                double radius = 7 + 9 * rnd.NextDouble();
                double winkel = 2 * Math.PI / 17 * i;
                umriss.Points.Add(new Point(radius * Math.Cos(winkel),
                                            radius * Math.Sin(winkel)));
            }
            umriss.Fill = Brushes.Gray;
        }

        public override void Draw(Canvas Zeichenfläche)
        {            
            Zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, x);
            Canvas.SetTop(umriss, y);
        }
    }

    class Raumschiff : Spielobjekte
    {
        Polygon umriss;
        public Raumschiff(Canvas Zeichenfläche)
            : base(Zeichenfläche.ActualWidth/2, Zeichenfläche.ActualHeight/2,
                   1,1)
        {
            umriss = new Polygon();
            umriss.Points.Add(new Point(0, -10));
            umriss.Points.Add(new Point(5, 7));
            umriss.Points.Add(new Point(-5, 7));
            umriss.Fill = Brushes.Blue;
        }

 

        public override void Draw(Canvas Zeichenfläche)
        {
            double winkel = Math.Atan2(vy, vx) * 180 / Math.PI+90;
            umriss.RenderTransform = new RotateTransform(winkel);
            Zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, x);
            Canvas.SetTop(umriss, y);
        }

        public void Accelerate(bool schneller)
        {
            double faktor = schneller ? 1.1 : 0.9;           
            vx *= faktor;
            vy *= faktor;
        }

        public void Steer(bool nachLinks)
        {
            double winkel = (nachLinks ? -5 : 5) * Math.PI / 180;
            double cos = Math.Cos(winkel);
            double sin = Math.Sin(winkel);
            double vxn, vyn;
            vxn = vx * cos - vy * sin;
            vyn = vx * sin + vy * cos;
            vx = vxn;
            vy = vyn;
        }
    }

    //class Torpedo : Spielobjekte
    //{

    //}
}
