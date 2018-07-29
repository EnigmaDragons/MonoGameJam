using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.PhsyicsMath;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ShotCalculation
    {
        private readonly GameTile _aggressor;
        private readonly GameTile _victim;

        public ShotCalculation(GameTile aggressor, GameTile victim)
        {
            _aggressor = aggressor;
            _victim = aggressor;
        }

        public ShotCoverInfo BestShot()
        {
            return new ShotCoverInfo(OptimalShot(CoversFromEachCorner(_aggressor, _victim)));
        }

        private List<CoverProvided> OptimalShot(List<List<CoverProvided>> options)
        {
            return options.OrderBy(covers => covers.Sum(x => (int)x.Cover)).First();
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
                if (GameWorld.Map.Exists(tile) && GameWorld.Map[tile].Cover >= currentCover.Cover)
                    currentCover = new CoverProvided { Cover = GameWorld.Map[tile].Cover, Provider = GameWorld.Map[tile] };
                currentSpot = currentSpot.MoveTowards(point2, 0.1);
            }
            return currentCover;
        }
    }
}
