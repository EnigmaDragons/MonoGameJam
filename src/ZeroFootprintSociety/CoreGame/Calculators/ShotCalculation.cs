using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using ZeroFootPrintSociety.CoreGame.Mechanics.Covors;
using ZeroFootPrintSociety.PhysicsMath;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class ShotCalculation
    {
        private const float EPSILON = 0.01f;
        
        private readonly GameTile _aggressor;
        private readonly GameTile _victim;

        public ShotCalculation(GameTile aggressor, GameTile victim)
        {
            _aggressor = aggressor;
            _victim = victim;
        }

        public ShotCoverInfo GetBestShot()
        {
            var viableCovers = new List<List<CoverProvided>>();
            foreach(var covers in CoversFromEachCorner())
            {
                if(covers.Sum(c => (int)c.Cover) == 0)
                    return new ShotCoverInfo(covers);
                viableCovers.Add(covers);
            }
            return new ShotCoverInfo(viableCovers.OrderBy(covers => covers.Sum(x => (int)x.Cover)).First());
        }

        public bool CanShoot()
        {
            return CoversFromEachCorner().Any(covers => covers.Sum(c => (int)c.Cover) < 100);
        }

        private IEnumerable<List<CoverProvided>> CoversFromEachCorner()
        {
            yield return CoversFromThisCorner(new Vector2(_aggressor.Transform.Location.X, _aggressor.Transform.Location.Y), TileSide.TopLeft);
            yield return CoversFromThisCorner(new Vector2(_aggressor.Transform.Location.X + _aggressor.Transform.Size.Width, _aggressor.Transform.Location.Y), TileSide.TopRight);
            yield return CoversFromThisCorner(new Vector2(_aggressor.Transform.Location.X, _aggressor.Transform.Location.Y + _aggressor.Transform.Size.Height), TileSide.BottomLeft);
            yield return CoversFromThisCorner(new Vector2(_aggressor.Transform.Location.X + _aggressor.Transform.Size.Width, _aggressor.Transform.Location.Y + _aggressor.Transform.Size.Height), TileSide.BottomRight);
        }

        private List<CoverProvided> CoversFromThisCorner(Vector2 aggressorCorner, TileSide corner)
        {
            return new List<CoverProvided>
            {
                CoverProvidedBetween(aggressorCorner, corner, new Vector2(_victim.Transform.Location.X, _victim.Transform.Location.Y), TileSide.TopLeft),
                CoverProvidedBetween(aggressorCorner, corner, new Vector2(_victim.Transform.Location.X + _victim.Transform.Size.Width, _victim.Transform.Location.Y), TileSide.TopRight),
                CoverProvidedBetween(aggressorCorner, corner, new Vector2(_victim.Transform.Location.X, _victim.Transform.Location.Y + _victim.Transform.Size.Height), TileSide.BottomLeft),
                CoverProvidedBetween(aggressorCorner, corner, new Vector2(_victim.Transform.Location.X + _victim.Transform.Size.Width, _victim.Transform.Location.Y + _victim.Transform.Size.Height), TileSide.BottomRight)
            };
        }

        private CoverProvided CoverProvidedBetween(Vector2 aggressorVector, TileSide aggressorCorner, Vector2 victimVector, TileSide victimCorner)
        {
            var currentCover = new CoverProvided();
            var currentX = _aggressor.Position.X * 2 + (int)aggressorCorner % 3 - 1;
            var currentY = _aggressor.Position.Y * 2 + ((int)aggressorCorner - 3 - (int)aggressorCorner % 3) / 3;
            var victimX = _victim.Position.X * 2 + (int)victimCorner % 3 - 1;
            var victimY = _victim.Position.Y * 2 + ((int)victimCorner - 3 - (int)victimCorner % 3) / 3;
            var victimCenterX = _victim.Position.X * 2;
            var victimCenterY = _victim.Position.Y * 2;

            var cover = CalculateDiagonalCover(currentX, currentY);
            if (cover.Cover == Cover.Heavy)
                return cover;
            if ((int)cover.Cover > (int)currentCover.Cover)
                currentCover = cover;

            if (currentX == victimX && currentY == victimY)
                return currentCover;
            if (currentX == victimX)
            {
                var direction = Math.Sign(victimY - currentY);
                if (_aggressor.Position.X == _victim.Position.X)
                    for (var y = _aggressor.Position.Y; y != _victim.Position.Y; y += direction)
                    {
                        cover = new CoverProvided(GameWorld.Map[_aggressor.Position.X, y]);
                        if (cover.Cover == Cover.Heavy)
                            return cover;
                        if ((int)cover.Cover > (int)currentCover.Cover)
                            currentCover = cover;
                    }
                currentY += direction;
                if (currentY == victimCenterY)
                    return currentCover;
                while (currentY + direction != victimCenterY)
                {
                    cover = CalculateCover(currentX, currentY);
                    if (cover.Cover == Cover.Heavy)
                        return cover;
                    if ((int)cover.Cover > (int)currentCover.Cover)
                        currentCover = cover;
                    currentY += direction;
                }
                cover = CalculateDiagonalCoverIgnoring(currentX, currentY, _victim);
                if ((int)cover.Cover > (int)currentCover.Cover)
                    currentCover = cover;
                return currentCover;
            }
            if (currentY == victimY)
            {
                var direction = Math.Sign(victimX - currentX);
                if (_aggressor.Position.Y == _victim.Position.Y)
                    for (var x = _aggressor.Position.X; x != _victim.Position.X; x += direction)
                    {
                        cover = new CoverProvided(GameWorld.Map[x, _aggressor.Position.Y]);
                        if (cover.Cover == Cover.Heavy)
                            return cover;
                        if ((int)cover.Cover > (int)currentCover.Cover)
                            currentCover = cover;
                    }
                currentX += direction;
                if (currentX == victimCenterX)
                    return currentCover;
                while (currentX != victimCenterX)
                {
                    cover = CalculateCover(currentX, currentY);
                    if (cover.Cover == Cover.Heavy)
                        return cover;
                    if ((int)cover.Cover > (int)currentCover.Cover)
                        currentCover = cover;
                    currentX += direction;
                }
                cover = CalculateDiagonalCoverIgnoring(currentX, currentY, _victim);
                if ((int)cover.Cover > (int)currentCover.Cover)
                    currentCover = cover;
                return currentCover;
            }

            var currentPreciseX = aggressorVector.X;
            var currentPreciseY = aggressorVector.Y;
            var distanceX = (victimX - currentX) / 2;
            var distanceY = (victimY - currentY) / 2;
            var horizontalDirection = Math.Sign(distanceX);
            var verticalDirection = Math.Sign(distanceY);

            currentX += 1 * Math.Sign(distanceX);
            currentY += 1 * Math.Sign(distanceY);
            while (Math.Abs(currentX - victimCenterX) > 1 || Math.Abs(currentY - victimCenterY) > 1)
            {
                cover = new CoverProvided(GameWorld.Map[currentX / 2, currentY / 2]);
                if (cover.Cover == Cover.Heavy)
                    return cover;
                if ((int)cover.Cover > (int)currentCover.Cover)
                    currentCover = cover;
                
                var timeUntilCrossX = Math.Abs(((distanceX > 0
                        ? Math.Ceiling((currentPreciseX + 0.01) / TileData.RenderWidth)
                        : Math.Floor((currentPreciseX - 0.01) / TileData.RenderWidth))
                    * TileData.RenderWidth - currentPreciseX) / distanceX);
                var timeUntilCrossY = Math.Abs(((distanceY > 0
                        ? Math.Ceiling((currentPreciseY + 0.01) / TileData.RenderHeight)
                        : Math.Floor((currentPreciseY - 0.01) / TileData.RenderHeight))
                    * TileData.RenderHeight - currentPreciseY) / distanceY);

                if(Math.Abs(timeUntilCrossX - timeUntilCrossY) < EPSILON)
                {
                    currentX += 1 * horizontalDirection;
                    currentY += 1 * verticalDirection;
                    if (Math.Abs(currentX - victimCenterX) <= 1 && Math.Abs(currentY - victimCenterY) <= 1)
                    {
                        cover = CalculateDiagonalCoverTraveling(currentX, currentY, horizontalDirection * verticalDirection > 0);
                        if ((int)cover.Cover > (int)currentCover.Cover)
                            currentCover = cover;
                        return currentCover;
                    }
                    cover = CalculateDiagonalCover(currentX, currentY);
                    if (cover.Cover == Cover.Heavy)
                        return cover;
                    if ((int)cover.Cover > (int)currentCover.Cover)
                        currentCover = cover;
                    currentPreciseX += (float)timeUntilCrossX * distanceX;
                    currentPreciseY += (float)timeUntilCrossX * distanceY;
                    currentX += 1 * horizontalDirection;
                    currentY += 1 * verticalDirection;
                }
                else if (timeUntilCrossX < timeUntilCrossY)
                {
                    currentX += 2 * horizontalDirection;
                    currentPreciseX += (float)timeUntilCrossX * distanceX;
                    currentPreciseY += (float)timeUntilCrossX * distanceY;
                }
                else
                {
                    currentY += 2 * verticalDirection;
                    currentPreciseX += (float)timeUntilCrossY * distanceX;
                    currentPreciseY += (float)timeUntilCrossY * distanceY;
                }
            }
            return currentCover;
        }

        private bool IsInBounds(int x, int y)
        {
            return GameWorld.Map.MinX * 2 <= x && x <= GameWorld.Map.MaxX * 2 || GameWorld.Map.MinY * 2 <= y && y <= GameWorld.Map.MaxY * 2;
        }

        private CoverProvided CalculateCover(int x, int y)
        {
            if(x % 2 == 0 && y % 2 == 0)
                return new CoverProvided(GameWorld.Map[x / 2, y / 2]);
            if ((x % 2 == 0) != (y % 2 == 0))
                return CalculateOrthogonalCover(x, y);
            return CalculateDiagonalCover(x, y);
        }

        private CoverProvided CalculateOrthogonalCover(int x, int y)
        {
            if(x % 2 != 0)
                return new CoverProvided(GameWorld.Map[(x - 1) / 2, y / 2], GameWorld.Map[(x + 1) / 2, y / 2]);
            return new CoverProvided(GameWorld.Map[x / 2, (y - 1) / 2], GameWorld.Map[x / 2, (y + 1) / 2]);
        }

        private CoverProvided CalculateDiagonalCover(int x, int y)
        {
            var cover1 = new CoverProvided(GameWorld.Map[(x - 1) / 2, (y - 1) / 2], GameWorld.Map[(x + 1) / 2, (y + 1) / 2]);
            var cover2 = new CoverProvided(GameWorld.Map[(x - 1) / 2, (y + 1) / 2], GameWorld.Map[(x + 1) / 2, (y - 1) / 2]);
            return cover1.Cover > cover2.Cover ? cover1 : cover2;
        }

        private CoverProvided CalculateDiagonalCoverIgnoring(int x, int y, GameTile tile)
        {
            return tile == GameWorld.Map[(x - 1) / 2, (y - 1) / 2] || tile == GameWorld.Map[(x + 1) / 2, (y + 1) / 2]
                ? new CoverProvided(GameWorld.Map[(x - 1) / 2, (y + 1) / 2], GameWorld.Map[(x + 1) / 2, (y - 1) / 2])
                : new CoverProvided(GameWorld.Map[(x - 1) / 2, (y - 1) / 2], GameWorld.Map[(x + 1) / 2, (y + 1) / 2]);
        }

        private CoverProvided CalculateDiagonalCoverTraveling(int x, int y, bool isPorportional)
        {
            return isPorportional
                ? new CoverProvided(GameWorld.Map[(x - 1) / 2, (y - 1) / 2], GameWorld.Map[(x + 1) / 2, (y + 1) / 2])
                : new CoverProvided(GameWorld.Map[(x - 1) / 2, (y + 1) / 2], GameWorld.Map[(x + 1) / 2, (y - 1) / 2]);
        }
    }
}
