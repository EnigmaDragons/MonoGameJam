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
            var targetsAvailable = new RangedTargetsAvailable
            {
                Targets = GameWorld.Characters
                    .Where(x => x != GameWorld.Turns.CurrentCharacter && CanShoot(GameWorld.Turns.CurrentCharacter, x))
                    .Select(x => new Target
                    {
                        Character = x,
                        CoverToThem = BestShot(GameWorld.Turns.CurrentCharacter, x),
                        CoverFromThem = CanShoot(x, GameWorld.Turns.CurrentCharacter)
                            ? BestShot(x, GameWorld.Turns.CurrentCharacter)
                            : new List<CoverProvided>(),
                    }).ToList()
            };
            targetsAvailable.Targets.ForEach(x =>
            {
                x.TargetBlockChance = x.CoverToThem.Sum(y => (int) y.Cover) * (x.Character.State.IsHiding ? 2 : 1);
                x.TargetterBlockChance = x.CoverFromThem.Sum(y => (int)y.Cover) * (GameWorld.CurrentCharacter.State.IsHiding ? 2 : 1);
            });
            Event.Publish(targetsAvailable);
        }

        private bool CanShoot(Character attacker, Character target)
        {
            return !AreSameTeam(attacker, target) 
                && attacker.Gear.EquippedWeapon.IsRanged
                && attacker.Gear.EquippedWeapon.AsRanged().EffectiveRanges.ContainsKey(attacker.CurrentTile.Position.TileDistance(target.CurrentTile.Position));
        }

        private bool AreSameTeam(Character attacker, Character target) => attacker.Team == target.Team;

        private List<CoverProvided> BestShot(Character attacker, Character target)
        {
            return OptimalShot(CoversFromEachCorner(attacker.CurrentTile, target.CurrentTile));
        }

        private List<CoverProvided> OptimalShot(List<List<CoverProvided>> options)
        {
            return options.OrderBy(covers => covers.Sum(x => (int) x.Cover)).First();
        }

        private List<List<CoverProvided>> CoversFromEachCorner(GameTile tile, GameTile tile2)
        {
            return new List<List<CoverProvided>>
            {
                CoversFromThisCorner(new Vector2(tile.Transform.Location.X + 0.1f, tile.Transform.Location.Y + 0.1f), tile2),
                CoversFromThisCorner(new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width - 0.1f, tile.Transform.Location.Y + 0.1f), tile2),
                CoversFromThisCorner(new Vector2(tile.Transform.Location.X + 0.1f, tile.Transform.Location.Y + tile.Transform.Size.Height - 0.1f), tile2),
                CoversFromThisCorner(new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width - 0.1f, tile.Transform.Location.Y + tile.Transform.Size.Height - 0.1f), tile2)
            };
        }

        private List<CoverProvided> CoversFromThisCorner(Vector2 corner, GameTile tile)
        {
            return new List<CoverProvided>
            {
                CoverProvidedBetween(corner, new Vector2(tile.Transform.Location.X + 0.1f, tile.Transform.Location.Y + 0.1f)),
                CoverProvidedBetween(corner, new Vector2(tile.Transform.Location.X + 0.1f, tile.Transform.Location.Y + tile.Transform.Size.Height -0.1f)),
                CoverProvidedBetween(corner, new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width - 0.1f, tile.Transform.Location.Y + 0.1f)),
                CoverProvidedBetween(corner, new Vector2(tile.Transform.Location.X + tile.Transform.Size.Width - 0.1f, tile.Transform.Location.Y + tile.Transform.Size.Height - 0.1f))
            };
        }

        private CoverProvided CoverProvidedBetween(Vector2 point1, Vector2 point2)
        {
            var currentCover = new CoverProvided { Cover = Cover.None };
            var currentSpot = point1.MoveTowards(point2, 0.1);
            while (currentSpot.X != point2.X || currentSpot.Y != point2.Y)
            {
                var tile = GameWorld.Map.MapPositionToTile(currentSpot);
                if (GameWorld.Map.Exists(tile) && GameWorld.Map[tile].Cover > currentCover.Cover)
                    currentCover = new CoverProvided { Cover = GameWorld.Map[tile].Cover, Provider = GameWorld.Map[tile] };
                currentSpot = currentSpot.MoveTowards(point2, 0.1);
            }
            return currentCover;
        }
    }
}
