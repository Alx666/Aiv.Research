using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aiv.Research.Visualizer2D
{
    internal struct Vector2
    {
        public float X;
        public float Y;

        public Vector2(float x, float y)
        {
            this.X = x;
            this.Y = y;
        }

        public Vector2(Point p0)
        {
            this.X = p0.X;
            this.Y = p0.Y;
        }

        public Vector2(PointF p0)
        {
            this.X = p0.X;
            this.Y = p0.Y;
        }

        public static Vector2 operator +(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X + v2.X, v1.Y + v2.Y);
        }

        public static Vector2 operator -(Vector2 v1, Vector2 v2)
        {
            return new Vector2(v1.X - v2.X, v1.Y - v2.Y);
        }

        public static Vector2 operator *(Vector2 v1, float m)
        {
            return new Vector2(v1.X * m, v1.Y * m);
        }

        public static float operator *(Vector2 v1, Vector2 v2)
        {
            return v1.X * v2.X + v1.Y * v2.Y;
        }

        public static Vector2 operator /(Vector2 v1, float m)
        {
            return new Vector2(v1.X / m, v1.Y / m);
        }

        public static float Distance(Vector2 v1, Vector2 v2)
        {
            return (float)Math.Sqrt(Math.Pow(v1.X - v2.X, 2) + Math.Pow(v1.Y - v2.Y, 2));
        }

        public static implicit operator Vector2(Point p)
        {
            return new Vector2(p);
        }

        public static implicit operator Point(Vector2 v)
        {
            return new Point((int)v.X, (int)v.Y);
        }

        public static implicit operator PointF(Vector2 v)
        {
            return new PointF(v.X, v.Y);
        }


        public override string ToString() => $"{this.X};{this.Y}";

        public float Length()
        {
            return (float)Math.Sqrt(X * X + Y * Y);
        }
    }
}
