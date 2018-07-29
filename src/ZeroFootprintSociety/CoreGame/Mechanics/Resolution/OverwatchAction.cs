using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.Calculators;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Mechanics.Resolution
{
    class OverwatchAction
    {
        public OverwatchAction()
        {
            Event.Subscribe(EventSubscription.Create<OverwatchSelected>(OnOverwatchSelected, this));
        }

        private void OnOverwatchSelected(OverwatchSelected e)
        {
            if (!GameWorld.CurrentCharacter.State.OverwatchedTiles.Any())
            {
                var overwatchedTiles = new Dictionary<Point, ShotCoverInfo>();
                var tiles = ExistingTiles(TilesInRange(GameWorld.CurrentCharacter.CurrentTile.Position, GameWorld.CurrentCharacter.Gear.EquippedWeapon.AsRanged().Range));
                tiles.ForEach(x =>
                {
                    var shot = new ShotCalculation(GameWorld.CurrentCharacter.CurrentTile, GameWorld.Map[x]).BestShot();
                    if (GameWorld.CurrentCharacter.Accuracy * shot.BlockChance / 100 > 0)
                        overwatchedTiles[x] = shot;
                });
                Event.Publish(new OverwatchTilesAvailable { OverwatchedTiles = overwatchedTiles });
            }
            Event.Publish(new ActionSelected(() =>
            {
                GameWorld.CurrentCharacter.State.IsOverwatching = true;
                Event.Publish(new ActionResolved());
            }));
        }

        private List<Point> ExistingTiles(List<Point> points)
        {
            return points.Where(x => GameWorld.Map.Exists(x)).ToList();
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
