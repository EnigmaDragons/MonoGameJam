using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class VisibilityCalculator
    {
        public DictionaryWithDefault<Point, bool> lastFriendliesVisibilityMap;

        public VisibilityCalculator()
        {
            Event.Subscribe(EventSubscription.Create<Moved>(OnMoved, this));
        }

        private void OnMoved(Moved obj)
        {
            DictionaryWithDefault<Point, bool> dictio = new DictionaryWithDefault<Point, bool>(false);
            foreach (Character character in GameWorld.Friendlies)
            {
                if (character.GetHashCode() == obj.Character.GetHashCode())
                {
                    obj.Character.State.SeeableTiles.Clear();
                    List<Point> hisTilesInRange = TilesInRange(obj.Position, obj.Character.Stats.Perception);

                    foreach (Point point in hisTilesInRange)
                    {
                        dictio[point] = true;
                        obj.Character.State.SeeableTiles[point] = true;
                    }
                }
                else
                {
                    foreach (KeyValuePair<Point, bool> pointKeyPair in character.State.SeeableTiles)
                    {
                        dictio[pointKeyPair.Key] = true;
                    }
                }
            }

            
            
        }

        private List<Point> TilesInRange(Point point, int rangeRemaining)
        {
            IEnumerable<Point> list = new List<Point> { point };
            if (rangeRemaining == 0)
                return list.ToList();
            var directions = new List<Point>
            {
                new Point(point.X - 1, point.Y),
                new Point(point.X + 1, point.Y),
                new Point(point.X, point.Y - 1),
                new Point(point.X, point.Y + 1)
            };
            directions.Select(x => TilesInRange(x, rangeRemaining - 1)).ForEach(x => list = list.Concat(x));
            return list.Distinct().ToList();
        }
    }
}
