using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Characters;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculation
    {
        private readonly Character _character;

        public VisibilityCalculation(Character character)
        {
            _character = character;
        }

        public DictionaryWithDefault<Point, bool> Calculate()
        {
            DictionaryWithDefault<Point, bool> canSee = new DictionaryWithDefault<Point, bool>(false);
            TilesInRange(_character.CurrentTile.Position, 10)
                .Where(x => GameWorld.Map.Exists(x))
                .Where(x => new ShotCalculation(_character.CurrentTile, GameWorld.Map[x]).BestShot().BlockChance < 100)
                .ForEach(x => canSee[x] = true);
            return canSee;
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
