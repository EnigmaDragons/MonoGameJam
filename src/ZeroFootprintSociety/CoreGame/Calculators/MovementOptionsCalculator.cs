using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.Engine;
using MonoDragons.Core.EventSystem;
using MonoDragons.Core.UserInterface;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class MovementOptionsCalculator
    {
        private readonly ConcurrentDictionaryWithDefault<Point, bool> _overwatchedTiles = new ConcurrentDictionaryWithDefault<Point, bool>(false);

        public MovementOptionsCalculator()
        {
            Event.Subscribe<TurnBegun>(x =>
            {
                UpdateOverwatchers();
                CalculateMovement();
            }, this);
        }

        public void UpdateOverwatchers()
        {
            _overwatchedTiles.Clear();
            GameWorld.LivingCharacters
                .Where(x => x.State.IsOverwatching && x.Team != GameWorld.CurrentCharacter.Team)
                .ForEach(x => x.State.OverwatchedTiles.ForEach(tile => _overwatchedTiles[tile.Key] = true));
        }

        void CalculateMovement()
        {
            var basePath = new List<Point> { GameWorld.Turns.CurrentCharacter.CurrentTile.Position };
            Event.Publish(new MovementOptionsAvailable
            {
                AvailableMoves = RandomizeBetweenDuplicates(
                    EliminateDuplicatesThatCrossOverOverwatchedTiles(
                        TakeSteps(basePath, GameWorld.Turns.CurrentCharacter.Stats.Movement)))
                    .Concat(new List<List<Point>> { basePath }).ToList()
            });
        }

        private List<List<Point>> RandomizeBetweenDuplicates(List<List<Point>> paths)
        {
            return paths.GroupBy(path => path.Last()).Select(group =>
            {
                var leastMoves = group.OrderBy(x => x.Count).First().Count;
                return group.Where(x => x.Count == leastMoves).ToList().Random();
            }).ToList();
        }

        private List<List<Point>> EliminateDuplicatesThatCrossOverOverwatchedTiles(List<List<Point>> paths)
        {
            return paths.GroupBy(path => path.Last()).SelectMany(group =>
            {
                var nonOverwatchedPaths = new List<List<Point>>();
                var overwatchedPaths = new List<List<Point>>();
                group.ForEach(path =>
                {
                    if (path.Any(point => _overwatchedTiles[point]))
                        overwatchedPaths.Add(path);
                    else
                        nonOverwatchedPaths.Add(path);
                });
                return nonOverwatchedPaths.Any() ? nonOverwatchedPaths : overwatchedPaths;
            }).ToList();
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
            return immidiatePathes.Concat(extraPaths).ToList();
        }
    }
}
