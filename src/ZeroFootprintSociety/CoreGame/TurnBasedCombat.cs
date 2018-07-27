using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TurnBasedCombat : IVisual
    {
        private int _index;

        public GameMap Map { get; }
        public List<Character> Characters { get; }
        public List<Point> AvailableMoves { get; private set; }
        public Character CurrentCharacter => Characters[_index];

        public TurnBasedCombat(GameMap map, List<Character> characters)
        {
            Map = map;
            Characters = characters;
        }

        public void Init()
        {
            Characters.ForEach(x => x.Body.Init());
            Characters.ForEach(x => x.Body.CurrentTile = Map.Tiles.Random());
            SetAvailableMoves();
        }

        public void MoveTo(int x, int y)
        {
            if (!AvailableMoves.Any(move => move.X == x && move.Y == y))
                return;
            CurrentCharacter.Body.CurrentTile = Map[x, y];
            _index++;
            if (_index == Characters.Count)
                _index = 0;
            SetAvailableMoves();
            CurrentCharacter.Body.OnTurnStart();
        }

        private void SetAvailableMoves()
        {
            AvailableMoves = TakeSteps(new Point(CurrentCharacter.Body.CurrentTile.Column,
                CurrentCharacter.Body.CurrentTile.Row), CurrentCharacter.Stats.Movement);
        }

        private List<Point> TakeSteps(Point position, int remainingMoves)
        {
            if (remainingMoves == 0)
                return new List<Point>();
            var directions = new List<Point>
            {
                new Point(position.X - 1, position.Y),
                new Point(position.X + 1, position.Y),
                new Point(position.X, position.Y - 1),
                new Point(position.X, position.Y + 1)
            };
            var immidiateMoves = directions.Where(x => Map.Exists(x.X, x.Y) && Map[x.X, x.Y].IsWalkable).ToList();
            var extraMoves = immidiateMoves.SelectMany(x => TakeSteps(x, remainingMoves - 1));
            return immidiateMoves.Concat(extraMoves).Distinct().ToList();
        }

        public void Draw(Transform2 parentTransform)
        {
            Map.Draw(parentTransform);
            Characters.ForEach(x => x.Body.Draw(parentTransform));
        }
    }
}
