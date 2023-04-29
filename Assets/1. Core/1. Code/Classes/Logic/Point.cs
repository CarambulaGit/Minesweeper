using System.Collections.Generic;
using RelationsInspector.Extensions;

namespace CodeBase.Infrastructure.Logic {
    public struct Point {
        public int Y { get; }
        public int X { get; }

        public Point(int y, int x) {
            Y = y;
            X = x;
        }

        public static Point operator +(Point p1, Point p2) {
            return new Point(p1.Y + p2.Y, p1.X + p2.X);
        }

        public static Point operator -(Point p1, Point p2) {
            return new Point(p1.Y - p2.Y, p1.X - p2.X);
        }

        public override string ToString() { 
            return $"y = {Y}, x = {X}";
        }

        public override bool Equals(object obj) {
            if (!(obj is Point point)) {
                return false;
            }

            return X == point.X && Y == point.Y;
        }
        
        public List<Point> FindNeighbors() {
            var result = new List<Point>();
            for (int xAdd = -1; xAdd <= 1; xAdd++) {
                for (int yAdd = -1; yAdd <= 1; yAdd++) {
                    result.Add(new Point(Y + yAdd, X + xAdd));
                }
            }

            result.Remove(this);
            return result;
        }
    }
}