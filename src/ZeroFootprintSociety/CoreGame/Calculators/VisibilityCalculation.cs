using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculation
    {
        private readonly Character _character;
        const int calculation = 15;

        public VisibilityCalculation(Character character)
        {
            _character = character;
        }

        public DictionaryWithDefault<Point, bool> Calculate()
        {
            DictionaryWithDefault<Point, bool> canSee = new DictionaryWithDefault<Point, bool>(false);
            var possibleTiles = new PointRadiusCalculation(_character.CurrentTile.Position, calculation).Calculate()
                .Where(x => GameWorld.Map.Exists(x)).ToList();
            possibleTiles
                .Where(x => new ShotCalculation(_character.CurrentTile, GameWorld.Map[x]).BestShot().BlockChance != 100 )
                .ForEach(x => canSee[x] = true);
            canSee.Where(x => GameWorld.Map[x.Key].Cover == Cover.Heavy)
                .ForEach(x =>
                {
                    RecursiveAddHeavyUp(canSee, x.Key, possibleTiles);
                    RecursiveAddHeavyDown(canSee, x.Key, possibleTiles);
                });
            return canSee;
        }

        private void RecursiveAddHeavyUp(DictionaryWithDefault<Point, bool> canSee, Point seenHeavyTile, List<Point> possiblePoints)
        {
            var potentialPoint = new Point(seenHeavyTile.X, seenHeavyTile.Y - 1);
            if (GameWorld.Map.Exists(potentialPoint) && GameWorld.Map[potentialPoint].Cover == Cover.Heavy && possiblePoints.Contains(potentialPoint))
            {
                canSee[potentialPoint] = true;
                RecursiveAddHeavyUp(canSee, potentialPoint, possiblePoints);
            }
        }

        private void RecursiveAddHeavyDown(DictionaryWithDefault<Point, bool> canSee, Point seenHeavyTile, List<Point> possiblePoints)
        {
            var potentialPoint = new Point(seenHeavyTile.X, seenHeavyTile.Y + 1);
            if (GameWorld.Map.Exists(potentialPoint) && GameWorld.Map[potentialPoint].Cover == Cover.Heavy && possiblePoints.Contains(potentialPoint))
            {
                canSee[potentialPoint] = true;
                RecursiveAddHeavyUp(canSee, potentialPoint, possiblePoints);
            }
        }
    }
}
