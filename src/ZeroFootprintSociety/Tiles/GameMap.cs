using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameMap
    {
        private Dictionary<int, Dictionary<int, GameTile>> _tileMap = new Dictionary<int, Dictionary<int, GameTile>>();
        private readonly int RenderSize = TileData.RenderHeight;

        public int MinX { get; }
        public int MaxX { get; }
        public int MinY { get; }
        public int MaxY { get; }
        public GameTile this[int x, int y] => _tileMap[x][y];
        public GameTile this[Point point] => _tileMap[point.X][point.Y];
        public List<GameTile> Tiles { get; }

        public GameMap(List<GameTile> tiles)
        {
            var minX = 0;
            var maxX = 0;
            var minY = 1;
            var maxY = 1;

            tiles.ForEach(t =>
            {
                if (!_tileMap.ContainsKey(t.Position.X))
                    _tileMap[t.Position.X] = new Dictionary<int, GameTile>();
                _tileMap[t.Position.X][t.Position.Y] = t;
                minX = Math.Min(t.Position.X, minX);
                maxX = Math.Max(t.Position.X, maxX);
                minY = Math.Min(t.Position.Y, minY);
                maxY = Math.Max(t.Position.Y, maxY);
            });
            Tiles = _tileMap.Values.SelectMany(x => x.Values).OrderBy(x => x.Position.X).ThenBy(x => x.Position.Y).ToList();

            MinX = minX;
            MaxX = maxX;
            MinY = minY;
            MaxY = maxY;
        }

        public bool Exists(Point point) => Exists(point.X, point.Y);
        public bool Exists(int x, int y) => _tileMap.ContainsKey(x) && _tileMap[x].ContainsKey(y);

        public Point MapPositionToTile(Vector2 position)
        {
            var tilePositionOnMap = new Vector2(position.X - (position.X % RenderSize), position.Y - (position.Y % RenderSize));
            return new Point((int)tilePositionOnMap.X / RenderSize, (int)tilePositionOnMap.Y / RenderSize);
        }

        public Point TileToWorldPosition(Point tile)
        {
            return new Point(tile.X * RenderSize, tile.Y * RenderSize);
        }

        public Transform2 TileToWorldTransform(Point tile)
        {
            return new Transform2(TileToWorldPosition(tile).ToVector2());
        }
    }
}
