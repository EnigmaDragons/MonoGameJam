using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class PointRadiusCalculation
    {
        private readonly Point _point;
        private readonly int _range;

        public PointRadiusCalculation(Point point, int range)
        {
            _point = point;
            _range = range;
        }

        public List<Point> Calculate()
        {
            return TilesInRange(_point, _range);
        }

        private List<Point> TilesInRange(Point point, int rangeRemaining)
        {
            var points = new List<Point>();
            var maxVerticalDistance = 0;
            for (var column = -rangeRemaining; column < 0; column++, maxVerticalDistance++)
                for (var row = -maxVerticalDistance; row <= maxVerticalDistance; row++)
                    points.Add(new Point(point.X + column, point.Y + row));
            for (var column = 0; column <= rangeRemaining; column++, maxVerticalDistance--)
                for (var row = -maxVerticalDistance; row <= maxVerticalDistance; row++)
                    points.Add(new Point(point.X + column, point.Y + row));
            return points;
        }
    }
}
