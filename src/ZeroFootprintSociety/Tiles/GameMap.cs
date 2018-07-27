using System;
using System.Collections.Generic;
using System.Linq;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;

namespace ZeroFootPrintSociety.Tiles
{
    public class GameMap : IVisualAutomaton
    {
        private Dictionary<int, Dictionary<int, GameTile>> _tileMap = new Dictionary<int, Dictionary<int, GameTile>>();
        public GameTile this[int x, int y] => _tileMap[x][y];
        public List<GameTile> Tiles { get; }

        public GameMap(List<GameTile> tiles)
        {
            tiles.ForEach(x =>
            {
                if (!_tileMap.ContainsKey(x.Column))
                    _tileMap[x.Column] = new Dictionary<int, GameTile>();
                _tileMap[x.Column][x.Row] = x;
            });
            Tiles = _tileMap.Values.SelectMany(x => x.Values).ToList();
        }

        public bool Exists(int x, int y) => _tileMap.ContainsKey(x) && _tileMap[x].ContainsKey(y);

        public void Update(TimeSpan delta)
        {
        }

        public void Draw(Transform2 parentTransform)
        {
            Tiles.ForEach(x => x.Draw(parentTransform));
        }
    }
}
