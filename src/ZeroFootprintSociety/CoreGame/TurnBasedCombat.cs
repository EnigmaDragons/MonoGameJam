using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.PhysicsEngine;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.Mechanics.Resolution;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TurnBasedCombat : IAutomaton, IVisual
    {
        private readonly List<object> _actionResolvers = ActionResolvers.CreateAll();

        private CharacterTurns _turns;
        private Character CurrentCharacter => _turns.CurrentCharacter;

        public GameMap Map { get; }
        public List<Point> AvailableMoves { get; private set; }
        public List<Character> Characters { get; }

        public TurnBasedCombat(GameMap map, List<Character> characters)
        {
            Map = map;

            Event.Subscribe(EventSubscription.Create<OverwatchBegun>(OnOverwatchBegun, this));
            Event.Subscribe(EventSubscription.Create<OverwatchTriggered>(OnOverwatchTriggered, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(OnMovementFinished, this));
            Event.Subscribe(EventSubscription.Create<ActionResolved>(OnActionResolved, this));
            Event.Subscribe(EventSubscription.Create<TurnBegun>(OnTurnBegun, this));
            Characters = characters.OrderByDescending(x => x.Stats.Agility).ToList();
            _turns = new CharacterTurns(Characters);
        }

        public void Init()
        {
            Characters.ForEach(x => x.Init());
            Characters.ForEach(x => x.CurrentTile = Map.Tiles.Random(t => t.IsWalkable));
            _turns.Init();
        }

        private void OnTurnBegun(TurnBegun e)
        {
            AvailableMoves = TakeSteps(new Point(e.Character.CurrentTile.Column,
                e.Character.CurrentTile.Row), e.Character.Stats.Movement);
            Event.Publish(new MovementOptionsAvailable { AvailableMoves = AvailableMoves });
        }

        private void OnActionResolved(ActionResolved obj)
        {
            Event.Publish(new TurnEnded());
        }

        private void OnOverwatchTriggered(OverwatchTriggered obj)
        {
            // TODO: Handle triggering of overwatch.
        }

        public void OnOverwatchBegun(OverwatchBegun ob)
        {
            // TODO: Handle beginning of overwatch action.
        }

        private void OnMovementFinished(MovementFinished e)
        {
            Event.Publish(new ActionOptionsAvailable());
        }

        public void MoveTo(int x, int y)
        {
            if (!AvailableMoves.Any(move => move.X == x && move.Y == y))
                return;

            // TODO: Path should be a sequence instead of a teleport to a single tile
            Event.Publish(new MovementConfirmed { Character = CurrentCharacter, Path = new List<Point> { new Point(x, y) } });
            CurrentCharacter.CurrentTile = Map[x, y];
            Event.Publish(new MovementFinished());
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

        public void Update(TimeSpan delta)
        {
            Characters.ForEach(x => x.Update(delta));
        }
    }
}