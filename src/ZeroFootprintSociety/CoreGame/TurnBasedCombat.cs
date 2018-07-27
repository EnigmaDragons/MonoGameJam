using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TurnBasedCombat : IVisual
    {
        private int _activeCharacterIndex;

        public GameMap Map { get; }
        public List<Character> Characters { get; }
        public List<Point> AvailableMoves { get; private set; }
        public Character CurrentCharacter => Characters[_activeCharacterIndex];

        public TurnBasedCombat(GameMap map, List<Character> characters)
        {
            Map = map;
            Characters = characters;
        }

        public void Init()
        {
            Event.Subscribe(EventSubscription.Create<OverwatchBegunEvent>((_event) => { }, this));
            Event.Subscribe(EventSubscription.Create<OverwatchTriggeredEvent>((_event) => { }, this));

            Characters.ForEach(x => x.Init());
            Characters.ForEach(x => x.CurrentTile = Map.Tiles.Random());
            Event.Publish(new CharacterTurnBegun { Character = CurrentCharacter });
            SetAvailableMoves();
        }

        public void MoveTo(int x, int y)
        {
            if (!AvailableMoves.Any(move => move.X == x && move.Y == y))
                return;

            // TODO: Path should be a sequence instead of a teleport to a single tile
            Event.Publish(new MovementConfirmed { Character = CurrentCharacter, Path = new List<Point> { new Point(x, y) } });
            CurrentCharacter.CurrentTile = Map[x, y];
            BeginNextTurn();
        }

        private void BeginNextTurn()
        {
            _activeCharacterIndex++;
            if (_activeCharacterIndex == Characters.Count)
                _activeCharacterIndex = 0;
            SetAvailableMoves();
            Event.Publish(new CharacterTurnBegun { Character = CurrentCharacter });
        }

        private void SetAvailableMoves()
        {
            AvailableMoves = TakeSteps(new Point(CurrentCharacter.CurrentTile.Column,
                CurrentCharacter.CurrentTile.Row), CurrentCharacter.Stats.Movement);
            Event.Publish(new MovementOptionsAvailable { AvailableMoves = AvailableMoves });
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
            Characters.ForEach(x => x.Draw(parentTransform));
        }
    }
}