using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class MovementOptionsCalculator
    {
        public MovementOptionsCalculator()
        {
            Event.Subscribe(EventSubscription.Create<TurnBegun>(CalculateMovement, this));
        }

        void CalculateMovement(TurnBegun e)
        {
            Event.Publish(new MovementOptionsAvailable { AvailableMoves = TakeSteps(e.Character.CurrentTile.Position, e.Character.Stats.Movement) });
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
            var immidiateMoves = directions.Where(x => GameState.Map.Exists(x.X, x.Y) && GameState.Map[x.X, x.Y].IsWalkable).ToList();
            var extraMoves = immidiateMoves.SelectMany(x => TakeSteps(x, remainingMoves - 1));
            return immidiateMoves.Concat(extraMoves).Distinct().ToList();
        }
    }
}
