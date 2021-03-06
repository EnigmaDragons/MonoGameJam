﻿using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core;
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
            var friendlyPerception = new DictionaryWithDefault<Point, bool>(false);
            GameWorld.Friendlies.ForEach(friendly =>
            {
                friendly.State.SeeableTiles.ForEach(tile => friendlyPerception[tile.Key] = true);
                friendly.State.PerceivedTiles.ForEach(tile => friendlyPerception[tile.Key] = true);
            });
            GameWorld.FriendlyPerception = friendlyPerception;
        }
    }
}
