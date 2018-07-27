using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameMap : IVisualAutomaton
    {
        private Dictionary<int, Dictionary<int, GameTile>> _tileMap = new Dictionary<int, Dictionary<int, GameTile>>();
        public GameTile this[int x, int y] => _tileMap[x][y];
        public GameTile this[Point point] => _tileMap[point.X][point.Y];
        public List<GameTile> Tiles { get; }

        public GameMap(List<GameTile> tiles)
        {
            tiles.ForEach(x =>
            {
                if (!_tileMap.ContainsKey(x.Position.X))
                    _tileMap[x.Position.X] = new Dictionary<int, GameTile>();
                _tileMap[x.Position.X][x.Position.Y] = x;
            });
            Tiles = _tileMap.Values.SelectMany(x => x.Values).ToList();
        }

        public bool Exists(Point point) => Exists(point.X, point.Y);
        public bool Exists(int x, int y) => _tileMap.ContainsKey(x) && _tileMap[x].ContainsKey(y);

        public Point MapPositionToTile(Vector2 position)
        {
            var tilePositionOnMap = new Vector2(position.X - (position.X % 48), position.Y - (position.Y % 48));
            return new Point((int)tilePositionOnMap.X / 48, (int)tilePositionOnMap.Y / 48);
        }

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            Tiles.ForEach(x => x.Draw(parentTransform));
        }
    }
}
