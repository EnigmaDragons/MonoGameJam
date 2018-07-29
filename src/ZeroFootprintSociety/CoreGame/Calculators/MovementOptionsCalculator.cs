using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
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
            var basePath = new List<Point> { GameWorld.Turns.CurrentCharacter.CurrentTile.Position };
            Event.Publish(new MovementOptionsAvailable
            {
                AvailableMoves = TakeSteps(basePath, GameWorld.Turns.CurrentCharacter.Stats.Movement)
                    .Concat(new List<List<Point>> { basePath }).ToList()
            });
        }

        private List<List<Point>> TakeSteps(List<Point> pathToHere, int remainingMoves)
        {

            if (remainingMoves == 0)
                return new List<List<Point>> { pathToHere };
            var position = pathToHere.Last();
            var directions = new List<Point>
            {
                new Point(position.X - 1, position.Y),
                new Point(position.X + 1, position.Y),
                new Point(position.X, position.Y - 1),
                new Point(position.X, position.Y + 1)
            };
            var immidiateMoves = directions.Where(x => GameWorld.Map.Exists(x.X, x.Y) && GameWorld.Map[x.X, x.Y].IsWalkable).ToList();
            var immidiatePathes = immidiateMoves.Select(x => pathToHere.Concat(new List<Point> {x}).ToList()).ToList();
            var extraPaths = immidiatePathes.SelectMany(x => TakeSteps(x, remainingMoves - 1));
            var results = new List<List<Point>>();
            immidiatePathes.Concat(extraPaths).OrderBy(x => x.Count).ForEach(path =>
            {
                if (results.All(x => x.Last() != path.Last()))
                    results.Add(path);
            });
            return results;
        }
    }
}
