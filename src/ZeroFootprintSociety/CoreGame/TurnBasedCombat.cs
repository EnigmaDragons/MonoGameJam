using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Events;
using ZeroFootPrintSociety.CoreGame.Mechanics.Resolution;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TurnBasedCombat : IAutomaton
    {
        private readonly List<object> _actionResolvers = ActionResolvers.CreateAll();

        private CharacterTurns _turns;
        private Character CurrentCharacter => _turns.CurrentCharacter;

        public GameMap Map { get; }
        public List<List<Point>> AvailableMoves { get; private set; }
        public List<Target> Targets { get; private set; }
        public List<Character> Characters { get; }

        public TurnBasedCombat(GameMap map, List<Character> characters)
        {
            Map = map;
            if (characters.Any(x => !x.IsInitialized))
                throw new InvalidOperationException("All Characters must be initialized before Level begins.");

            Event.Subscribe(EventSubscription.Create<OverwatchBegun>(OnOverwatchBegun, this));
            Event.Subscribe(EventSubscription.Create<OverwatchTriggered>(OnOverwatchTriggered, this));
            Event.Subscribe(EventSubscription.Create<MovementFinished>(OnMovementFinished, this));
            Event.Subscribe(EventSubscription.Create<ActionResolved>(OnActionResolved, this));
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(x => AvailableMoves = x.AvailableMoves, this));
            Event.Subscribe(EventSubscription.Create<RangedTargetsAvailable>(x => Targets = x.Targets, this));
            Characters = characters.OrderByDescending(x => x.Stats.Agility).ToList();
            _turns = new CharacterTurns();
            GameWorld.Turns = _turns;
        }

        public void Init()
        {
            _turns.Init();
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
            if (AvailableMoves.Any(move => move.Last().X == x && move.Last().Y == y))
                Event.Publish(new MovementConfirmed { Path = AvailableMoves.First(move => move.Last().X == x && move.Last().Y == y) });
        }

        public void Shoot(int x, int y)
        {
            if (!Targets.Any(target => target.Character.CurrentTile.Position.X == x && target.Character.CurrentTile.Position.Y == y))
                return;

            var attackTarget = Targets.First(target => target.Character.CurrentTile.Position.X == x && target.Character.CurrentTile.Position.Y == y);
            Event.Publish(new RangedTargetInspected
            {
                Attacker = GameWorld.CurrentCharacter,
                Defender = attackTarget.Character,
                AttackerBlockChance = attackTarget.TargetterBlockChance,
                DefenderBlockChance = attackTarget.TargetBlockChance
            });

        } 

        public void Update(TimeSpan delta)
        {
            Characters.ForEach(x => x.Update(delta));
        }
    }
}