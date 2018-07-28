using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.PhsyicsMath;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ShootOptionsCalculator
    {
        public ShootOptionsCalculator()
        {
            Event.Subscribe(EventSubscription.Create<MovementFinished>(CalculateTargets, this));
        }

        public void CalculateTargets(MovementFinished e)
        {
            Event.Publish(new RangedTargetsAvailable { Targets = GameState.Characters.Where(x => x != GameState.CurrentCharacter && CanShoot(GameState.CurrentCharacter, x)).ToList() });
        }

        private bool CanShoot(Character attacker, Character target)
        {
            return CornersThatHaveLineOfSightOfOtherCorners(attacker.CurrentTile, target.CurrentTile).Any(x => x >= 2);
        }

        private List<int> CornersThatHaveLineOfSightOfOtherCorners(GameTile tile, GameTile tile2)
        {
            var result = new List<int>();
            result.Add(CornersInLineOfSight(new Vector2(tile.Transform.Location.X, tile.Transform.Location.Y), tile2));
            result.Add(CornersInLineOfSight(new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width, tile.Transform.Location.Y), tile2));
            result.Add(CornersInLineOfSight(new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width, tile.Transform.Location.Y), tile2));
            result.Add(CornersInLineOfSight(new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width, tile.Transform.Location.Y + tile.Transform.Size.Height), tile2));
            return result;
        }

        private int CornersInLineOfSight(Vector2 corner, GameTile tile)
        {
            var seenCorners = 0;
            if (InLineOfSight(corner, new Vector2(tile.Transform.Location.X, tile.Transform.Location.Y)))
                seenCorners++;
            if (InLineOfSight(corner, new Vector2(tile.Transform.Location.X, tile.Transform.Location.Y + tile.Transform.Size.Height)))
                seenCorners++;
            if (InLineOfSight(corner, new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width, tile.Transform.Location.Y)))
                seenCorners++;
            if (InLineOfSight(corner, new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width, tile.Transform.Location.Y + tile.Transform.Size.Height)))
                seenCorners++;
            return seenCorners;
        }

        private bool InLineOfSight(Vector2 point1, Vector2 point2)
        {
            var currentSpot = point1;
            while (currentSpot.X != point2.X || currentSpot.Y != point2.Y)
            {
                currentSpot = currentSpot.MoveTowards(point2, 0.1);
                var tile = GameState.Map.MapPositionToTile(currentSpot);
                if (!GameState.Map.Exists(tile) || !GameState.Map[tile].IsWalkable)
                    return false;
            }
            return true;
        }
    }
}
