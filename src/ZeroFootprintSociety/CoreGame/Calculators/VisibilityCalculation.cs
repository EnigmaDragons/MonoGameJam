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
                .Where(x => new ShotCalculation(_character.CurrentTile, GameWorld.Map[x]).BestShot().BlockChance != 100)
                .ForEach(x => canSee[x] = true);
            return canSee;
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
