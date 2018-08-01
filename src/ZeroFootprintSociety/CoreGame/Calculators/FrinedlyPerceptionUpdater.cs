using System.Collections.Generic;
using Microsoft.Xna.Framework;
using MonoDragons.Core.Common;
using MonoDragons.Core.EventSystem;
using ZeroFootPrintSociety.Characters;
using ZeroFootPrintSociety.CoreGame.StateEvents;

namespace ZeroFootPrintSociety.CoreGame.Calculators
{
    public class FrinedlyPerceptionUpdater
    {
        public FrinedlyPerceptionUpdater()
        {
            Event.Subscribe<TurnEnded>(OnTurnEnd, this);
        }

        private void OnTurnEnd(TurnEnded e)
        {
            if (GameWorld.CurrentCharacter.Team != Team.Friendly)
                return;
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
