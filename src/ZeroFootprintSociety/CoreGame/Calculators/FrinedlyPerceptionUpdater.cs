using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class FrinedlyPerceptionUpdater
    {
        public FrinedlyPerceptionUpdater()
        {
            Event.Subscribe<TurnEnded>(_ => UpdatePerception(), this);
        }

        public void UpdatePerception()
        {
            var frienlyPerception = new DictionaryWithDefault<Point, bool>(false);
            GameWorld.Friendlies.ForEach(friendly =>
            {
                friendly.State.SeeableTiles.ForEach(tile => frienlyPerception[tile.Key] = true);
                friendly.State.PercievedTiles.ForEach(tile => frienlyPerception[tile.Key] = true);
            });
            GameWorld.FriendlyPerception = frienlyPerception;
        }
    }
}
