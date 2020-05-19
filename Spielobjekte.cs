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

        public Asteroid(Canvas Zeichenfläche)
            : base (rnd.NextDouble()*Zeichenfläche.ActualWidth,
                    rnd.NextDouble()*Zeichenfläche.ActualHeight,
                    rnd.NextDouble()*200-100,
                    rnd.NextDouble()*200-100)
        {

        }

        public override void Draw(Canvas Zeichenfläche)
        {
            Polygon umriss = new Polygon();
            for (int i = 0; i < 5; i++)
            {
                double radius = 10;
                double winkel = 2 * Math.PI / 5 * i;
                umriss.Points.Add(new Point(radius*Math.Cos(winkel),
                                            radius*Math.Sin(winkel)));
            }


            umriss.Fill = Brushes.Gray;
            Zeichenfläche.Children.Add(umriss);
            Canvas.SetLeft(umriss, x);
            Canvas.SetTop(umriss, y);
        }
    }

    //class Raumschiff : Spielobjekte
    //{

    //}

    //class Torpedo : Spielobjekte
    //{

    //}
}
