using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.Render;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.Mechanics.Resolution;
using ZeroFootPrintSociety.CoreGame.StateEvents;
using ZeroFootPrintSociety.Tiles;

namespace ZeroFootPrintSociety.CoreGame
{
    public class TurnBasedCombat : IAutomaton
    {
        private readonly List<object> _actionResolvers = ActionResolvers.CreateAll();
        
        public GameMap Map { get; }
        public IReadOnlyList<IReadOnlyList<Point>> AvailableMoves { get; private set; }
        public List<Target> Targets { get; private set; }

        public TurnBasedCombat(GameMap map, IReadOnlyList<Character> characters)
        {
            Map = map;
            if (characters.Any(x => !x.IsInitialized))
                throw new InvalidOperationException("All Characters must be initialized before Level begins.");

            Event.Subscribe(EventSubscription.Create<ActionResolved>(OnActionResolved, this));
            Event.Subscribe(EventSubscription.Create<MovementOptionsAvailable>(x => AvailableMoves = x.AvailableMoves, this));
            Event.Subscribe(EventSubscription.Create<RangedTargetsAvailable>(x => Targets = x.Targets, this));
            GameWorld.Turns = new CharacterTurns(characters);
        }

        public void Init()
        {
            GameWorld.Turns.Init();
        }

        private void OnActionResolved(ActionResolved obj)
        {
            EventQueue.Instance.Add(new TurnEnded());
        }

        public void MoveTo(int x, int y)
        {
            //TODO: remove hack for avoid switch weapons button
            var mousePosition = Mouse.GetState().Position;
            var scaledMousePosition = new Point((int)Math.Round(mousePosition.X / CurrentDisplay.Scale), (int)Math.Round(mousePosition.Y / CurrentDisplay.Scale));
            if (scaledMousePosition.X > 700 && scaledMousePosition.Y < 900 && scaledMousePosition.Y > 850)
                return;
            if (AvailableMoves.Any(move => move.Last().X == x && move.Last().Y == y))
                Event.Publish(new MovementConfirmed(AvailableMoves.First(move => move.Last().X == x && move.Last().Y == y)));
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
                AttackerBlockInfo = attackTarget.CoverFromThem,
                DefenderBlockInfo = attackTarget.CoverToThem
            });
        } 

        public void Update(TimeSpan delta)
        {
            GameWorld.Characters.ToList().ForEach(x => x.Update(delta));
        }
    }
}