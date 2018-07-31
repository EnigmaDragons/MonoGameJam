using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;

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
            IEnumerable<Point> list = new List<Point> { point };
            if (rangeRemaining == 0)
                return list.ToList();
            var directions = new List<Point>
            {
                new Point(point.X - 1, point.Y),
                new Point(point.X + 1, point.Y),
                new Point(point.X, point.Y - 1),
                new Point(point.X, point.Y + 1)
            };
            directions.Select(x => TilesInRange(x, rangeRemaining - 1)).ForEach(x => list = list.Concat(x));
            return list.Distinct().ToList();
        }
    }
}
